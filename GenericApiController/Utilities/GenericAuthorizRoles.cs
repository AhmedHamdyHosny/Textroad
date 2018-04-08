using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApiController.Utilities
{
    public class GenericAuthorizRoles<TEntity>
    {
        public List<GenericActionRoles> ActionRoles { get; set; }
        public List<GenericDataAccess<TEntity>> DataAccess { get; set; }

    }

    public class GenericActionRoles
    {
        public Actions Action { get; set; }
        public string Roles { get; set; }
        
    }

    public class GenericDataAccess<TEntity>
    {
        public Actions Action { get; set; }
        public string Roles { get; set; }
        public System.Linq.Expressions.Expression<Func<TEntity, bool>> ExpressionFunc { get; set; }
    }

    public enum Actions
    {
        Get, GetById, GetGridView, Post, Put, Delete, Clear, GetByOptions,Import,Export
    }
}
