using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace LandSoftBuildingMain
{
    public class NewWebClient : WebClient
    {
        public int TimeOut { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest web = base.GetWebRequest(address);
            if (TimeOut > 0)
            {
                web.Timeout = TimeOut;
            }
            return web;
        }
    }
}
