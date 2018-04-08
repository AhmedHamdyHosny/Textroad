using Security.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Security.Controllers
{
    public class BaseSecurityApiController<T> : GenericApiController.GenericApiController<T> where T : class
    {
        public BaseSecurityApiController() : base(new SecurityEntities())
        {

        }
    }
}
