using System.Collections.Generic;

namespace $ext_safeprojectname$.Web.Base
{
    public class PagedResponse<T>
    {
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }
    }
}
