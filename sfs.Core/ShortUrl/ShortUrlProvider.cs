﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace SageFrame.Core
{
    public class ShortUrlProvider
    {
        public string EncodeUrl(string url, string code)
        {
            try
            {
                List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
                ParameterCollection.Add(new KeyValuePair<string, object>("@Url", url));
                ParameterCollection.Add(new KeyValuePair<string, object>("@Code", code));
                OracleHandler objHandler = new OracleHandler();
                return objHandler.ExecuteAsScalar<string>("USP_SHORTURL_ENCODE", ParameterCollection);
            }
            catch (Exception ex)
            {
                //SageFrame.Web.ProcessException(ex);
                throw;
            }
        }

        public string DecodeUrl(string key)
        {
            try
            {
                List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
                ParameterCollection.Add(new KeyValuePair<string, object>("@Key", key));
                OracleHandler objHandler = new OracleHandler();
                return objHandler.ExecuteAsScalar<string>("USP_SHORTURL_DECODE", ParameterCollection);
            }
            catch (Exception ex)
            {
                //SageFrame.Web.ProcessException(ex);
                throw;
            }
        }

    }
}
