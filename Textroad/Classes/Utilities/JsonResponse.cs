using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes.Utilities
{
    public class JsonResponse 
    {
        public int Status { get; set; } //1 for success, 0 for fail
        public string Message { get; set; }
        public dynamic Result { get; set; }
        public string RedirectTo { get; set; }
    }
}