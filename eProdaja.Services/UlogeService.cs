using AutoMapper;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class UlogeService : BaseCRUDService<Model.Uloge, Database.Uloge, UlogeSearchObject, UlogeUpsertRequest, UlogeUpsertRequest>, IUlogeService
    {
        public UlogeService(eProdajaContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
