using Textroad.DataLayer;

namespace Textroad.Controllers
{
    public class BaseApiController<T> : GenericApiController.GenericApiController<T> where T : class
    {
        public BaseApiController() : base(new TextRoadDBEntities())
        {

        }
    }
}