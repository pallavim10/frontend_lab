using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using OfficeOpenXml;
using System.Web.Services.Description;
using Microsoft.SqlServer.Server;
using System.IO;
using System.ComponentModel;

namespace WebApplication2
{
    public class Global : System.Web.HttpApplication
    {
        public LicenseUsageMode UsageMode { get; private set; }

        protected void Application_Start(object sender, EventArgs e)
        {

        }
    }
}