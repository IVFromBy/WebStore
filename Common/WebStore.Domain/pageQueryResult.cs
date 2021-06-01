using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain
{
    public class pageQueryResult<T>
    {
        public IEnumerable<T> Items { get; init; }
        public int TotalCount { get; set; }

//        public int Page { get; init; }
        //public int TotalPageCount => (int)Math.Ceiling((double)TotalCount / Items.Count());

    }
}
