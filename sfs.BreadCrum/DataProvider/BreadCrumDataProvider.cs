﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.BreadCrum
{
    public class BreadCrumDataProvider
    {

        public List<BreadCrumInfo> GetBreadCrumb(string SEOName, int PortalID, int MenuId, string CultureCode)
        {
            try
            {

                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("SEOName", SEOName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("CultureCode", CultureCode));
                //if (MenuId != 0)
                //{
                //    ParaMeterCollection.Add(new KeyValuePair<string, object>("@MenuID", MenuId));
                //    SQLHandler SQLH = new SQLHandler();
                //    SQLH.ExecuteNonQuery("[dbo].[usp_BreadCrumbGetFromMenuItem]", ParaMeterCollection);
                //    return SQLH.ExecuteAsList<BreadCrumInfo>("usp_BreadCrumbMenuItemPath");
                //}
                //else
                //{
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                return SQLH.ExecuteAsList<BreadCrumInfo>("usp_BreadCrumbGetFromPageName", ParaMeterCollection);
                //}
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}