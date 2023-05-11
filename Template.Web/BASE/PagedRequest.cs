using Microsoft.AspNetCore.Mvc;

namespace $ext_safeprojectname$.Web.Base
{
    public class PagedRequest
    {
        [FromQuery]
        public int Page { get; set; }

        [FromQuery]
        public int Size { get; set; }
    }
}
