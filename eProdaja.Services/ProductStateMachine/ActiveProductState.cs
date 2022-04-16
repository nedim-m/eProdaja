using AutoMapper;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services.ProductStateMachine
{
    public class ActiveProductState : BaseState
    {
        public ActiveProductState(IMapper mapper, IServiceProvider serviceProvider, eProdajaContext context) : base(mapper, serviceProvider, context)
        {
        }
    }
}
