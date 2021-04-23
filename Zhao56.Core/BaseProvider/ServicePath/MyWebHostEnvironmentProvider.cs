using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.BaseProvider.ServicePath
{
    public static class MyWebHostEnvironmentProvider
    {
        public static IWebHostEnvironment MyWebHostEnvironment
        {
            get; set;
        }
    }
}
