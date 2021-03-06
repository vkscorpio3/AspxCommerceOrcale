﻿using SageFrame.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AuthenticateService
/// </summary>
namespace SageFrame.Services
{
    public class AuthenticateService : System.Web.Services.WebService
    {
        public AuthenticateService()
        {
        }
        /// <summary>
        /// strict use for http post method for admin part
        /// </summary>
        /// <param name="portalId">portalId</param>
        /// <param name="userModuleId">userModuleId</param>
        /// <param name="userName">userName</param>
        /// <returns>bool </returns>
        public bool IsPostAuthenticated(int portalId, int userModuleId, string userName, string authToken)
        {

            string user = GetUsername(portalId, authToken);
            if (user == "superuser")
            {
                return true;
            }
            else if (user != "anonymoususer" && user == userName)
            {
                List<KeyValuePair<string, object>> para = new List<KeyValuePair<string, object>>();
                para.Add(new KeyValuePair<string, object>("UserModuleID", userModuleId));
                para.Add(new KeyValuePair<string, object>("PortalID", portalId));
                para.Add(new KeyValuePair<string, object>("userName", userName));
                //SQLHandler handler = new SQLHandler();
                OracleHandler handler = new OracleHandler();
                int flag = handler.ExecuteAsScalar<int>("usp_CheckModulePermissionEdit", para);
                if (flag == 1)
                    return true;
                else
                    return false;
            }
            return false;
        }

        /// <summary>
        /// strict use for http get method for admin part
        /// </summary>
        /// <param name="portalId">portalId</param>
        /// <param name="userModuleId">userModuleId</param>
        /// <param name="userName">userName</param>
        /// <returns>bool </returns>
        public bool IsPostAuthenticatedView(int portalId, int userModuleId, string userName, string authToken)
        {
            string user = GetUsername(portalId, authToken);
            if (user == "superuser")
            {
                return true;
            }
            else if (user != "anonymoususer" && user == userName)
            {
                List<KeyValuePair<string, object>> para = new List<KeyValuePair<string, object>>();
                para.Add(new KeyValuePair<string, object>("UserModuleID", userModuleId));
                para.Add(new KeyValuePair<string, object>("PortalID", portalId));
                para.Add(new KeyValuePair<string, object>("userName", userName));
                //SQLHandler handler = new SQLHandler();
                OracleHandler handler = new OracleHandler();
                int flag = handler.ExecuteAsScalar<int>("usp_CheckModulePermissionView", para);
                if (flag == 1)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private string GetUsername(int portalID, string authToken)
        {
            try
            {
                SecurityPolicy objSecurity = new SecurityPolicy();
                string userName = objSecurity.GetUser(portalID, authToken);
                if (userName != ApplicationKeys.anonymousUser)
                {
                    return userName;
                }
                else
                {
                    return ApplicationKeys.anonymousUser;
                }
            }
            catch
            {
                return ApplicationKeys.anonymousUser;
            }
        }
    }
}