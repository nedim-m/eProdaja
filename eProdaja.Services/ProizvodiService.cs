using AutoMapper;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using eProdaja.Services.ProductStateMachine;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class ProizvodiService : BaseCRUDService<Model.Proizvodi, Database.Proizvodi, ProizvodiSearchObject, ProizvodiInsertRequest, ProizvodiUpdateRequest>, IProizvodiService
    {
        public BaseState BaseState { get; set; }
        public ProizvodiService(eProdajaContext context, IMapper mapper, BaseState baseState) : base(context, mapper)
        {
            BaseState = baseState;
        }

        public override Model.Proizvodi Insert(ProizvodiInsertRequest insert)
        {
            //return base.Insert(insert);
            var state = BaseState.CreateState("initial");

            return state.Insert(insert);
        }

        public override Model.Proizvodi Update(int id, ProizvodiUpdateRequest update)
        {
            var product = _context.Proizvodis.Find(id);
            //return base.Update(id, update);
            var state = BaseState.CreateState(product.StateMachine);
            state.CurrentEntity = product;

            state.Update(update);

            return GetById(id);
        }

        public Model.Proizvodi Activate(int id)
        {
            var product = _context.Proizvodis.Find(id);

            var state = BaseState.CreateState(product.StateMachine);
            state.CurrentEntity = product;

            state.Activate();

            return GetById(id);
        }





        public override IEnumerable<Model.Proizvodi> Get(ProizvodiSearchObject search = null)
        {
            return base.Get(search);
        }


        public override IQueryable<Database.Proizvodi> AddFilter(IQueryable<Database.Proizvodi> query, ProizvodiSearchObject search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.Sifra))
            {
                filteredQuery = filteredQuery.Where(x => x.Sifra == search.Sifra);
            }

            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                filteredQuery = filteredQuery.Where(x => x.Naziv.Contains(search.Naziv));
            }

            return filteredQuery;
        }

        public List<string> AllowedActions(int id)
        {
            var product = GetById(id);
            var state = BaseState.CreateState(product.StateMachine);
            return state.AllowedActions();
        }

        static object isLocked = new object();
        static MLContext mLContext = null;
        static ITransformer model = null;
        public List<Model.Proizvodi> Recommend(int id)
        {
            lock (isLocked)
            {
                if (mLContext == null) //da ne trenira svaki put kada se okine recommend
                {
                    mLContext = new MLContext();

                    var tmpData = _context.Narudzbes.Include("NarudzbaStavkes").ToList();

                    var data = new List<ProductEntry>();

                    foreach (var item in tmpData)
                    {
                        if (item.NarudzbaStavkes.Count>1)
                        {
                            var distinctItemId = item.NarudzbaStavkes.Select(x => x.ProizvodId).ToList();

                            distinctItemId.ForEach(i =>
                            {
                                var relatedItems = item.NarudzbaStavkes.Where(z => z.ProizvodId!=i);
                                foreach (var y in relatedItems)
                                {
                                    data.Add(new ProductEntry()
                                    {
                                        ProductID=(uint)i,
                                        CoPurchaseProductID=(uint)y.ProizvodId
                                    });
                                }
                            });
                        }
                    }
                    var traindata = mLContext.Data.LoadFromEnumerable(data);

                    MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
                    options.MatrixColumnIndexColumnName =nameof(ProductEntry.ProductID);
                    options.MatrixRowIndexColumnName=nameof(ProductEntry.CoPurchaseProductID);
                    options.LabelColumnName="Label";
                    options.LossFunction=MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
                    options.Alpha=0.01;
                    options.Lambda=0.025;
                    options.NumberOfIterations=100;
                    options.C=0.00001;

                    var est = mLContext.Recommendation().Trainers.MatrixFactorization(options);

                    model = est.Fit(traindata);

                }

                var allItems = _context.Proizvodis.Where(x => x.ProizvodId!=id);

                var predictionResult = new List<Tuple<Database.Proizvodi, float>>();

                foreach (var item in allItems)
                {
                    var predictionEngine = mLContext.Model.CreatePredictionEngine<ProductEntry, Copurchase_prediction>(model);

                    var prediction = predictionEngine.Predict(new ProductEntry()
                    {
                        ProductID=(uint)id,
                        CoPurchaseProductID=(uint)item.ProizvodId
                    });


                    predictionResult.Add(new Tuple<Database.Proizvodi, float>(item, prediction.Score));

                }

                var finalResult = predictionResult.OrderByDescending(x => x.Item2).Select(x => x.Item1).Take(3).ToList();

                return _mapper.Map<List<Model.Proizvodi>>(finalResult);
            }
        }
    }

    public class Copurchase_prediction
    {
        public float Score { get; set; }
    }
    public class ProductEntry
    {
        [KeyType(count: 10)]
        public uint ProductID { get; set; }

        [KeyType(count: 10)]
        public uint CoPurchaseProductID { get; set; }

        public float Label { get; set; }
    }
}
