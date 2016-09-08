﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;
using SageFrame.PagePermission;
using SageFrame.Common;


namespace SageFrame.Pages
{
    public class PageDataProvider
    {
        public List<PageEntity> GetMenuFront(int PortalID, bool isAdmin)
        {
            try
            {
                List<PageEntity> lstPages = new List<PageEntity>();
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("IsAdmin", isAdmin));
                OracleHandler sagesql = new OracleHandler();
                //SQLHandler sagesql = new SQLHandler();
                lstPages = sagesql.ExecuteAsList<PageEntity>("usp_GetPages", ParaMeterCollection);
                return lstPages;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddUpdatePages(PageEntity objPage)
        { 
            //SQLHandler sqlH = new SQLHandler();
            OracleHandler sqlH = new OracleHandler();
            //SqlTransaction tran = (SqlTransaction)sqlH.GetTransaction();
            OracleTransaction tran = (OracleTransaction)sqlH.GetTransaction();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                                new KeyValuePair<string, object>(
                                                                                    "PageID", objPage.PageID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "PageOrder", objPage.PageOrder),
                                                                                new KeyValuePair<string, object>(
                                                                                    "PageName", objPage.PageName.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "IsVisible", objPage.IsVisible),
                                                                                new KeyValuePair<string, object>(
                                                                                    "ParentID", objPage.ParentID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "IconFile", objPage.IconFile.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@DisableLink", objPage.DisableLink),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@Title", objPage.Title.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "Description", objPage.Description.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "KeyWords", objPage.KeyWords.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "Url", objPage.Url.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "StartDate", objPage.StartDate.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "EndDate", objPage.EndDate.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "RefreshInterval",
                                                                                    objPage.RefreshInterval),
                                                                                new KeyValuePair<string, object>(
                                                                                    "PageHeadText",
                                                                                    objPage.PageHeadText.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "IsSecure", objPage.IsSecure),
                                                                                new KeyValuePair<string, object>(
                                                                                    "IsActive", objPage.IsActive),
                                                                                new KeyValuePair<string, object>(
                                                                                    "IsShowInFooter",
                                                                                    objPage.IsShowInFooter),
                                                                                new KeyValuePair<string, object>(
                                                                                    "IsRequiredPage",
                                                                                    objPage.IsRequiredPage),
                                                                                new KeyValuePair<string, object>(
                                                                                    "BeforeID", objPage.BeforeID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "AfterID", objPage.AfterID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "PortalID", objPage.PortalID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "AddedBy", objPage.AddedBy.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "IsAdmin", objPage.IsAdmin), 
                                                                            };
                int pageID = 0;
                pageID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_AddUpdatePage", ParaMeterCollection, "InsertedPageID");
                if (pageID > 0)
                {
                    // objPage.PortalID = objPage.PortalID == -1 ? 1 : objPage.PortalID;
                    AddUpdatePagePermission(objPage.LstPagePermission, tran, pageID, objPage.PortalID, objPage.AddedBy, objPage.IsAdmin);
                    if (!objPage.IsAdmin)
                    {
                        if (objPage.Mode == "A")
                        {
                            MenuPageUpdate(objPage.MenuList, tran, pageID);
                        }
                    }
                    if (objPage.MenuList != "0")
                    {
                        AddUpdateSelectedMenu(objPage.MenuList, tran, pageID, objPage.ParentID, objPage.Mode, objPage.Caption, objPage.PortalID, objPage.UpdateLabel);
                    }

                }
                tran.Commit();
            }
            catch (OracleException sqlEx)
            {
                tran.Rollback();
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PageEntity> GetPages(int PortalID, bool isAdmin)
        {
            List<PageEntity> lstPages = new List<PageEntity>();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("IsAdmin", isAdmin));

            try
            {
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                lstPages = sagesql.ExecuteAsList<PageEntity>("usp_GetPages", ParaMeterCollection);
                return lstPages;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddUpdatePagePermission(List<PagePermissionInfo> lstPPI, OracleTransaction tran, int pageID, int portalID, string addedBy, bool isAdmin)
        {
            try
            {

                List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("PageID",
                                                                                                           pageID),
                                                                          new KeyValuePair<string, object>("PortalID",
                                                                                                           portalID),
                                                                          new KeyValuePair<string, object>("IsAdmin",
                                                                                                           isAdmin)
                                                                      };
                //SQLHandler sql = new SQLHandler();
                OracleHandler sql = new OracleHandler();
                sql.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_PagePermissionDeleteByPageID",
                                    ParaMeterColl);

                foreach (PagePermissionInfo objPagePI in lstPPI)
                {
                    if (objPagePI == null) continue;
                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "PageID", pageID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "RoleID", objPagePI.RoleID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "PermissionID",
                                                                                        objPagePI.PermissionID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "AllowAccess",
                                                                                        objPagePI.AllowAccess),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "UserName", objPagePI.Username),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "IsActive", objPagePI.IsActive),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "PortalID", portalID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "AddedBy", addedBy),
                                                                                    new KeyValuePair<string, object>("IsAdmin",
                                                                                                       isAdmin)
                                                                                };
                    //SQLHandler sqlH = new SQLHandler();
                    OracleHandler sqlH = new OracleHandler();
                    sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_AddPagePermission",
                                         ParaMeterCollection);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AddUpdateSelectedMenu(string SelectedMenu, OracleTransaction tran, int pageID, int ParentID, string Mode, string Caption, int PortalID, string UpdateLabel)
        {
            try
            {
                string[] menuArr = SelectedMenu.Split(',');
                foreach (string menu in menuArr)
                {

                    List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("MenuID",
                                                                                                           menu),
                                                                          new KeyValuePair<string, object>("MenuIDs",
                                                                                                           SelectedMenu),
                                                                                                           new KeyValuePair<string, object>("Mode",
                                                                                                           Mode),
                                                                          new KeyValuePair<string, object>("PageID",
                                                                                                           pageID),
                                                                          new KeyValuePair<string, object>("ParentID",
                                                                                                           ParentID),
                                                                        new KeyValuePair<string, object>("caption",
                                                                                                           Caption),
                                                                        new KeyValuePair<string, object>("UpdateLabel",
                                                                                                           UpdateLabel)
                                                                        
                                                                      };
                    //SQLHandler sqlH = new SQLHandler();
                    OracleHandler sqlH = new OracleHandler();
                    sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_PageManagerAddPageToMenu",
                                        ParaMeterColl);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void MenuPageUpdate(string MenuIDs, OracleTransaction tran, int pageID)
        {
            try
            {
                string[] menuArr = MenuIDs.Split(',');

                List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("MenuIDs",
                                                                                                           MenuIDs),
                                                                          new KeyValuePair<string, object>("PageID",
                                                                                                           pageID)
                                                                      };
                //SQLHandler sqlH = new SQLHandler();
                OracleHandler sqlH = new OracleHandler();
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_PageManagerMenuPageUpdate",
                                    ParaMeterColl);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public PageEntity GetPageDetails(int pageID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                                new KeyValuePair<string, object>(
                                                                                    "PageID", pageID)
                                                                            };
                //SQLHandler sqlH = new SQLHandler();
                OracleHandler sqlH = new OracleHandler();
                PageEntity objPE = sqlH.ExecuteAsObject<PageEntity>("sp_PagesGetByPageID", ParaMeterCollection);
                return objPE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PagePermissionInfo> GetPagePermissionDetails(PageEntity obj)
        {
            //SqlDataReader reader = null;
            OracleDataReader reader = null;
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {
                                                                            new KeyValuePair<string, object>("PageID",
                                                                                                             obj.PageID),
                                                                            new KeyValuePair<string, object>(
                                                                                "portalID", obj.PortalID)
                                                                        };
                //SQLHandler sqlH = new SQLHandler();
                OracleHandler sqlH = new OracleHandler();
                List<PagePermissionInfo> lst = new List<PagePermissionInfo>();
                reader = sqlH.ExecuteAsDataReader("sp_GetPagePermissionByPageID", ParaMeterCollection);

                while (reader.Read())
                {
                    PagePermissionInfo objPP = new PagePermissionInfo
                    {
                        PageID = int.Parse(reader["PageID"].ToString()),
                        PermissionID =
                            int.Parse(reader["PermissionID"].ToString()),
                        RoleID =
                            reader["RoleID"] == DBNull.Value
                                ? ""
                                : reader["RoleID"].ToString(),
                        Username =
                            reader["Username"] == DBNull.Value
                                ? ""
                                : reader["Username"].ToString(),
                        IsActive =
                            bool.Parse(reader["IsActive"].ToString()),
                        AllowAccess =
                            bool.Parse(reader["AllowAccess"].ToString()),
                        AddedBy = reader["AddedBy"].ToString()
                    };
                    lst.Add(objPP);
                }
                return lst;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public List<PageEntity> GetChildPage(int parentID, bool? isActive, bool? isVisiable, bool? isRequiredPage, string userName, int portalID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {
                                                                            new KeyValuePair<string, object>("ParentID",
                                                                            parentID),
                                                                             new KeyValuePair<string, object>("IsActive",
                                                                            isActive),
                                                                             new KeyValuePair<string, object>("IsVisible",
                                                                            isVisiable),
                                                                             new KeyValuePair<string, object>("IsRequiredPage",
                                                                            isRequiredPage),
                                                                             new KeyValuePair<string, object>("UserName",
                                                                            userName),
                                                                             new KeyValuePair<string, object>("PortalID",
                                                                            portalID)
                                                                          
                                                                        };
            //SQLHandler sqlH = new SQLHandler();
            OracleHandler sqlH = new OracleHandler();
            return sqlH.ExecuteAsList<PageEntity>("sp_PageGetByParentID", ParaMeterCollection);
        }

        public List<PageModuleInfo> GetPageModules(int pageID, int portalID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {                                                                          
                                                                             new KeyValuePair<string, object>("PageID",
                                                                            pageID),
                                                                             new KeyValuePair<string, object>("PortalID",
                                                                            portalID)                                                                          
                                                                        };
            //SQLHandler sqlH = new SQLHandler();
            OracleHandler sqlH = new OracleHandler();
            return sqlH.ExecuteAsList<PageModuleInfo>("usp_GetPageModulesByPageID", ParaMeterCollection);




        }

        public void DeletePageModule(int moduleID, string deletedBY, int portalID)
        {
            List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("ModuleID",
                                                                                                           moduleID),
                                                                             new KeyValuePair<string, object>("DeletedBy",
                                                                            deletedBY),
                                                                             new KeyValuePair<string, object>("PortalID",
                                                                            portalID)                                                                        
                                                                      };
            //SQLHandler sql = new SQLHandler();
            OracleHandler sql = new OracleHandler();
            sql.ExecuteNonQuery("sp_ModulesDelete", ParaMeterColl);

        }

        public void DeleteChildPage(int pageID, string deletedBY, int portalID)
        {
            List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("PageID",
                                                                                                           pageID),
                                                                             new KeyValuePair<string, object>("DeletedBy",
                                                                            deletedBY),
                                                                             new KeyValuePair<string, object>("PortalID",
                                                                            portalID)                                                                        
                                                                      };
            //SQLHandler sql = new SQLHandler();
            OracleHandler sql = new OracleHandler();
            sql.ExecuteNonQuery("sp_PagesDelete", ParaMeterColl);

        }

        public void UpdatePageAsContextMenu(int pageID, bool? isVisiable, bool? isPublished, int portalID, string userName, string updateFor)
        {
            //SQLHandler sqlH = new SQLHandler();
            OracleHandler sqlH = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                                new KeyValuePair<string, object>(
                                                                                    "PageID", pageID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "IsVisible", isVisiable),
                                                                                new KeyValuePair<string, object>(
                                                                                    "IsPublish", isPublished),
                                                                                new KeyValuePair<string, object>( 
                                                                                    "PortalID", portalID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "AddedBy", userName),
                                                                                new KeyValuePair<string, object>(
                                                                                    "updateFor", updateFor)                                                                                 
                                                                            };

                sqlH.ExecuteNonQuery("sp_UpdatePageAsContextMenu", ParaMeterCollection);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SortFrontEndMenu(int pageID, int parentID, string pageName, int BeforeID, int AfterID, int portalID, string userName)
        {
            //SQLHandler sqlH = new SQLHandler();
            OracleHandler sqlH = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                                new KeyValuePair<string, object>(
                                                                                    "PageID", pageID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "ParentID", parentID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "PageName", pageName),                                                                                                                                                      
                                                                                new KeyValuePair<string, object>(
                                                                                    "BeforeID", BeforeID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "AfterID", AfterID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "PortalID", portalID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "AddedBy", userName)
                                                                            };

                sqlH.ExecuteNonQuery("usp_SortPages", ParaMeterCollection);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SortAdminPages(List<PageEntity> lstPages)
        {
            //SQLHandler sqlH = new SQLHandler();
            OracleHandler sqlH = new OracleHandler();
            try
            {
                foreach (PageEntity obj in lstPages)
                {
                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                                new KeyValuePair<string, object>(
                                                                                    "PageID", obj.PageID),                                                                                                                                                                                                                                   
                                                                                new KeyValuePair<string, object>(
                                                                                    "PageOrder", obj.PageOrder),
                                                                                new KeyValuePair<string, object>(
                                                                                    "PortalID", obj.PortalID)
                                                                            };
                    sqlH.ExecuteNonQuery("usp_SortAdminPages", ParaMeterCollection);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PageEntity> GetPortalPages(int PortalID, string UserName, string prefix, bool IsActive, bool IsDeleted, object IsVisible, object IsRequiredPage)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {
                                                                            new KeyValuePair<string, object>("prefix",
                                                                            prefix),
                                                                             new KeyValuePair<string, object>("IsActive",
                                                                            IsActive),
                                                                             new KeyValuePair<string, object>("IsDeleted",
                                                                            IsDeleted),
                                                                             new KeyValuePair<string, object>("PortalID",
                                                                            PortalID),
                                                                             new KeyValuePair<string, object>("UserName",
                                                                            UserName),
                                                                             new KeyValuePair<string, object>("IsVisible",
                                                                            IsVisible),
                                                                            new KeyValuePair<string, object>("@IsRequiredPage",
                                                                            IsRequiredPage)
                                                                          
                                                                        };
            //SQLHandler sqlH = new SQLHandler();
            OracleHandler sqlH = new OracleHandler();
            return sqlH.ExecuteAsList<PageEntity>("sp_PagePortalGetByCustomPrefix", ParaMeterCollection);

        }

        public List<PageEntity> GetActivePortalPages(int PortalID, string UserName, string prefix, bool IsActive, bool IsDeleted, object IsVisible, object IsRequiredPage)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {
                                                                            new KeyValuePair<string, object>("prefix",
                                                                            prefix),
                                                                             new KeyValuePair<string, object>("IsActive",
                                                                            IsActive),
                                                                             new KeyValuePair<string, object>("IsDeleted",
                                                                            IsDeleted),
                                                                             new KeyValuePair<string, object>("PortalID",
                                                                            PortalID),
                                                                             new KeyValuePair<string, object>("UserName",
                                                                            UserName),
                                                                             new KeyValuePair<string, object>("IsVisible",
                                                                            IsVisible),
                                                                            new KeyValuePair<string, object>("IsRequiredPage",
                                                                            IsRequiredPage)
                                                                          
                                                                        };
            //SQLHandler sqlH = new SQLHandler();
            OracleHandler sqlH = new OracleHandler();
            return sqlH.ExecuteAsList<PageEntity>("usp_PagePortalGetByCustomPrefix", ParaMeterCollection);

        }

        public void UpdSettingKeyValue(string PageName, int PortalID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("Page", PageName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            //SQLHandler sageSQL = new SQLHandler();
            OracleHandler sageSQL = new OracleHandler();
            sageSQL.ExecuteNonQuery("usp_PageMgr_UpdSettingKeyValue", ParaMeterCollection);
        }

        public List<SecurePageInfo> GetSecurePage(int portalID, string culture)
        {
            try
            {
                //SQLHandler sqLH = new SQLHandler();
                OracleHandler sqLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("PortalID", portalID));
                ParaMeter.Add(new KeyValuePair<string, object>("CultureName", culture));
                List<SecurePageInfo> portalRoleCollection = new List<SecurePageInfo>();
                portalRoleCollection = sqLH.ExecuteAsList<SecurePageInfo>("usp_GetAllSecurePages", ParaMeter);
                return portalRoleCollection;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}