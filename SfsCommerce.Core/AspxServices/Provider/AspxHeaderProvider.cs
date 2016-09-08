﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxHeaderProvider
    {
        public AspxHeaderProvider()
       {
       }

       public static decimal GetCartItemsCount(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
               OracleHandler sqlH = new OracleHandler();
               return sqlH.ExecuteAsScalar<decimal>("usp_Aspx_GetCartItemsCount", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static int CountWishItems(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamNoCID(aspxCommonObj);
               OracleHandler sqlH = new OracleHandler();
               return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetWishItemsCount", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static string GetHeaderSetting(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               OracleHandler sqlHandle = new OracleHandler();
               string headerType = sqlHandle.ExecuteAsScalar<string>("usp_Aspx_GetHeaderSettings",parameterCollection);
               return headerType;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static void SetHeaderSetting(string headerType, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
           parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
           parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
           parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
           parameterCollection.Add(new KeyValuePair<string, object>("HeaderType", headerType));
           OracleHandler sqlhandle = new OracleHandler();
           sqlhandle.ExecuteNonQuery("[usp_Aspx_UpdateHeaderSettings]", parameterCollection);
       }
    }
}
