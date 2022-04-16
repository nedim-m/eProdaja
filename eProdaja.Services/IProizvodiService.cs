
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public interface IProizvodiService:ICRUDService<Model.Proizvodi, ProizvodiSearchObject,ProizvodiInsertRequest,ProizvodiUpdateRequest>
    {
        Model.Proizvodi Activate(int id );

        List<string> AllowedActions(int id);

    }
}
