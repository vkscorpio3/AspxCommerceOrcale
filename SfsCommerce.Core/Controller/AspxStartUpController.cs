﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxStartUpController
    {
        public AspxStartUpController()
        { 
        }

        public void DeleteAbandonedCartItems(int storeID, int portalID, decimal timeToDeleteCartItems, decimal timeToAbandonCart)
        {
            try
            {
                List<SecurePageInfo> portalRoleCollection = new List<SecurePageInfo>();
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("StoreID", storeID));
                ParaMeter.Add(new KeyValuePair<string, object>("PortalID", portalID));
                ParaMeter.Add(new KeyValuePair<string, object>("TimeToDeleteCartItems", timeToDeleteCartItems));
                ParaMeter.Add(new KeyValuePair<string, object>("AbandonedCartTime", timeToAbandonCart));
                sqlH.ExecuteAsList<SecurePageInfo>("usp_Aspx_DeleteAbandonedCartIt", ParaMeter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetFullName(int portalId, string userName)
        {
            var paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("PortalID", portalId));
            paramCol.Add(new KeyValuePair<string, object>("UserName", userName));
            var sageSQL = new OracleHandler();
            //return sageSQL.ExecuteAsScalar<string>("usp_Aspx_GetUserFirstandLastName", paramCol);
            return sageSQL.ExecuteAsScalar<string>("usp_Aspx_GetUserFirstandLastNa", paramCol);

        }
    }
}
