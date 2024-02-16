using System.Collections.Generic;
using System.Linq;

namespace Core.Framework
{
    public class GridResult<T> : GenericResult, Core.Abstractions.IGridResult<T>
    {
        public GridResult()
        {
            _data = null;
            TotalRecords = 0;
        }

        private IEnumerable<T> _data;

        public IEnumerable<T> Data 
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                TotalRecords = _data.Count();
            }
        }

        public int TotalRecords { get; set; }
    }
}
