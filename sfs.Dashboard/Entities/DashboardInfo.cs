﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.Dashboard
{
    public class DashboardInfo
    {

        public int PageID { get; set; }
        public int PageOrder { get; set; }

        public string PageName { get; set; }
        public string Url { get; set; }
        public string IconFile { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        public string TabPath { get; set; }


        public DashboardInfo() { }


    }
}