using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.BaseProvider.ServicePath
{
    public static class PathProvider 
    {
        private static IWebHostEnvironment _hostingEnvironment = MyWebHostEnvironmentProvider.MyWebHostEnvironment;
        public static string GetContentRootPath(string path)
        {
            return Path.Combine(_hostingEnvironment.ContentRootPath, path);
        }

        public static IWebHostEnvironment GetHostingEnvironment()
        {
            return _hostingEnvironment;
        }

        public static string GetWebRootPath(string path)
        {
            if (_hostingEnvironment.WebRootPath==null)
            {
                _hostingEnvironment.WebRootPath = _hostingEnvironment.ContentRootPath + "/wwwroot";
                return Path.Combine(_hostingEnvironment.WebRootPath, path);
            }
            return Path.Combine(_hostingEnvironment.ContentRootPath, path);
        }
    }
}
