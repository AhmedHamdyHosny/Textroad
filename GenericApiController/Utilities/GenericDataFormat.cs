using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApiController.Utilities
{
    public class GenericDataFormat
    {
        public List<FilterItems> Filters { get; set; }
        public List<SortItems> Sorts { get; set; }
        public IncludeItems Includes { get; set; }
        public PagingItem Paging { get; set; }

        public class FilterItems
        {
            public string Property { get; set; }
            public object Value { get; set; }
            public FilterOperations Operation { get; set; }
            public LogicalOperations LogicalOperation { get; set; }
        }

        public class SortItems
        {
            public string Property { get; set; }
            public SortType SortType { get; set; }
            public int Priority { get; set; }
        }
        public class IncludeItems
        {
            public string Properties { get; set; }
            public string References { get; set; }
        }

        public class PagingItem
        {
            public int? PageNumber { get; set; }
            public int? PageSize { get; set; }
        }

        public enum SortType
        {
            Asc,Desc

        }

        public enum FilterOperations
        {
            Equal,NotEqual,
            GreaterThan,LessThan,GreaterThanOrEqual,LessThanOrEqual,
            //In,NotIn,
            //Between,
            Like
        }

        public enum LogicalOperations
        {
            And,Or
        }
    }

    public class UpdateItemFormat<T>
    {
        public Guid id { get; set; }
        public T newValue { get; set; }
    }

    public class PaginationResult<T>
    {
        public int TotalItemsCount { get; set; }
        public List<T> PageItems { get; set; }
    }
   
}
