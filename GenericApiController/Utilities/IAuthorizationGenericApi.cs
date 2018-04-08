using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApiController.Utilities
{
    public interface IAuthorizationGenericApi<TEntity> where TEntity : class
    {
        GenericAuthorizRoles<TEntity> AuthorizRole { get; set; }
        //System.Linq.Expressions.Expression<Func<TEntity, bool>> DataConstrains { get; set; }
        string[] UserRoles { get; set; }
        
        //void SetActionRoles(Actions action, string roles);
        void SetGetActionRoles(string roles);
        void SetGetByIdActionRoles(string roles);
        void SetGetByOptionsActionRoles(string roles);
        void SetPutActionRoles(string roles);
        void SetDeleteActionRoles(string roles);
        void AddActionDataConstrains(Actions action, string roles, System.Linq.Expressions.Expression<Func<TEntity, bool>> ConstrainsExpression);
    }
}
