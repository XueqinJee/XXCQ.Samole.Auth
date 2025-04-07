using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Dto.Page
{
    public class PageData<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int Total { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
}
