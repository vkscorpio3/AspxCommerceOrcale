﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using System.Web.Script.Serialization;

namespace AspxCommerce.Core
{
    public class AspxCommonProvider
    {
        public AspxCommonProvider()
        {
        }

        public static bool CheckIfItemIsGrouped(int itemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ItemID", itemID));
                OracleHandler sqlH = new OracleHandler();
                bool isGrouped = sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckIfGroupedItem", parameter, "IsGrouped");
                return isGrouped;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<SearchTermInfo> GetSearchStatistics(int count, string commandName, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("Count", count));
            parameter.Add(new KeyValuePair<string, object>("CommandName", commandName));
            OracleHandler sqlH = new OracleHandler();
            List<SearchTermInfo> lstSearchTerm = sqlH.ExecuteAsList<SearchTermInfo>("usp_Aspx_GetSearchTermStatistics", parameter);
            return lstSearchTerm;
        }

        public static void UpdateItemRating(ItemReviewBasicInfo ratingManageObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("RatingCriteriaValue", ratingManageObj.ItemRatingCriteria));
                parameter.Add(new KeyValuePair<string, object>("StatusID", ratingManageObj.StatusID));
                parameter.Add(new KeyValuePair<string, object>("ReviewSummary", ratingManageObj.ReviewSummary));
                parameter.Add(new KeyValuePair<string, object>("Review", ratingManageObj.Review));
                parameter.Add(new KeyValuePair<string, object>("ItemReviewID", ratingManageObj.ItemReviewID));
                parameter.Add(new KeyValuePair<string, object>("ViewFromIP", ratingManageObj.ViewFromIP));
                parameter.Add(new KeyValuePair<string, object>("ViewFromCountry", ratingManageObj.viewFromCountry));
                parameter.Add(new KeyValuePair<string, object>("ItemID", ratingManageObj.ItemID));
                parameter.Add(new KeyValuePair<string, object>("UserName", ratingManageObj.UserName));
                parameter.Add(new KeyValuePair<string, object>("UserBy", aspxCommonObj.UserName));
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateItemRating", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<RatingCriteriaInfo> GetItemRatingCriteria(AspxCommonInfo aspxCommonObj, bool isFlag)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("IsFlag", Convert.ToString(isFlag)));
                List<RatingCriteriaInfo> lstRating = sqlH.ExecuteAsList<RatingCriteriaInfo>("usp_Aspx_GetItemRatingCriteria", parameter);
                return lstRating;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<ItemReviewDetailsInfo> GetItemRatingByReviewID(int itemReviewID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ItemReviewID", itemReviewID));
                List<ItemReviewDetailsInfo> lstItemRVDetail = sqlH.ExecuteAsList<ItemReviewDetailsInfo>("usp_Aspx_GetItemReviewDetails", parameter);
                return lstItemRVDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteSingleItemRating(string itemReviewID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ItemReviewID", itemReviewID));
                OracleHandler sqlH = new OracleHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteSingleItemRatingInformation", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<RatingLatestInfo> GetRecentItemReviewsAndRatings(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("offset", offset));
                parameter.Add(new KeyValuePair<string, object>("limit", limit));
                List<RatingLatestInfo> lstRatingNew = sqlH.ExecuteAsList<RatingLatestInfo>("usp_Aspx_GetRecentReviewsAndRatings", parameter);
                return lstRatingNew;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<StatusInfo> GetStatus(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                List<StatusInfo> lstStatus = sqlH.ExecuteAsList<StatusInfo>("usp_Aspx_GetStatusList", parameter);
                return lstStatus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessBoolean(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, bool attributeValue)
        {
            try
            {

                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("usp_Aspx_CheckUniqueness_Boolean", parameterCollection);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessDecimal(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, Decimal attributeValue)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("usp_Aspx_CheckUniqueness_Decimal", parameterCollection);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessDate(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, DateTime attributeValue)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("usp_Aspx_CheckUniqueness_Date", parameterCollection);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessFile(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, string attributeValue)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("usp_Aspx_CheckUniqueness_File", parameterCollection);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static bool CheckUniquenessInt(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, Int32 attributeValue)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("usp_Aspx_CheckUniqueness_Int", parameterCollection);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessNvarchar(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, string attributeValue)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("usp_Aspx_CheckUniqueness_Nvarchar", parameterCollection);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessText(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, string attributeValue)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("usp_Aspx_CheckUniqueness_Text", parameterCollection);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //---------------- Added for unique name check ---------------------
        public static bool CheckUniqueName(AttributeBindInfo attrbuteUniqueObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeName", attrbuteUniqueObj.AttributeName));
                parameterCollection.Add(new KeyValuePair<string, object>("AttributeID", attrbuteUniqueObj.AttributeID));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("usp_Aspx_AttributeUniquenessCheck", parameterCollection, "IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<StatusInfo> GetStatusList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                List<StatusInfo> lstStatus = sqlH.ExecuteAsList<StatusInfo>("usp_Aspx_BindOrderStatusList", parameter);
                return lstStatus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<PortalRole> GetPortalRoles(int portalID, bool isAll, string userName)
        {
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("PortalID", portalID));
            paramList.Add(new KeyValuePair<string, object>("IsAll", isAll));
            paramList.Add(new KeyValuePair<string, object>("UserName", userName));
            List<PortalRole> lstPortalRole = sqlHandler.ExecuteAsList<PortalRole>("sp_PortalRoleList", paramList);
            return lstPortalRole;
        }
        //--------------------Store Lists------------------------    

        public static List<StoreInfo> GetAllStores(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
            paramList.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
            paramList.Add(new KeyValuePair<string, object>("Culture", aspxCommonObj.CultureName));
            OracleHandler sqlHandler = new OracleHandler();
            List<StoreInfo> lstStore = sqlHandler.ExecuteAsList<StoreInfo>("usp_Aspx_PortalStoreList", paramList);
            return lstStore;
        }
        //----------------country list------------------------------    

        public static List<CountryInfo> BindCountryList()
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<CountryInfo> lstCountry = sqlH.ExecuteAsList<CountryInfo>("usp_Aspx_BindTaxCountryList");
                return lstCountry;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //----------------state list--------------------------     

        public static List<StateInfo> BindStateList(string countryCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("CountryCode", countryCode));
                OracleHandler sqlH = new OracleHandler();
                List<StateInfo> lstState = sqlH.ExecuteAsList<StateInfo>("usp_Aspx_BindStateList", parameter);
                return lstState;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static ItemCommonInfo GetItemInfoFromSKU(string SKU, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("SKU", SKU));
                OracleHandler sqlH = new OracleHandler();
                ItemCommonInfo lstItem = sqlH.ExecuteAsObject<ItemCommonInfo>("usp_Aspx_GetItemInfoFromSKU", parameter);
                return lstItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string GetUserBillingEmail(int addressID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFParamNoSCode(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("AddressID", addressID));
                OracleHandler sqlH = new OracleHandler();
                string userEmail = sqlH.ExecuteAsScalar<string>("[usp_Aspx_GetUserBillingEmail]", parameter);
                return userEmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #region AddToCart

        public static int AddItemstoCart(int itemID, decimal itemPrice, int itemQuantity, bool isItemPage, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                var cartobj = new CartManageSQLProvider();
                StoreSettingConfig ssc = new StoreSettingConfig();

                if (cartobj.CheckItemCart(itemID, aspxCommonObj.StoreID, aspxCommonObj.PortalID, "0@"))
                {
                    List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamNoCID(aspxCommonObj);
                    parameter.Add(new KeyValuePair<string, object>("ItemID", itemID));
                    parameter.Add(new KeyValuePair<string, object>("Price", itemPrice));
                    parameter.Add(new KeyValuePair<string, object>("Quantity", itemQuantity));
                    OracleHandler sqlH = new OracleHandler();
                    int i;
                    if (isItemPage)
                        i = sqlH.ExecuteNonQueryAsGivenType<int>("usp_Aspx_CheckCostVariantForItemDetail", parameter, "IsExist");
                    else
                       // i = sqlH.ExecuteNonQueryAsGivenType<int>("usp_Aspx_CheckCostVariantForItem", parameter, "IsExist");
                        i = sqlH.ExecuteNonQueryAsGivenType<int>("usp_Aspx_CheckCostVariantForIt", parameter, "IsExist");
                    return i;

                }
                else
                {
                    if (bool.Parse(ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName)))
                    {
                        List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamNoCID(aspxCommonObj);
                        parameter.Add(new KeyValuePair<string, object>("ItemID", itemID));
                        parameter.Add(new KeyValuePair<string, object>("Price", itemPrice));
                        parameter.Add(new KeyValuePair<string, object>("Quantity", itemQuantity));
                        parameter.Add(new KeyValuePair<string, object>("isItemPage", isItemPage));
                        OracleHandler sqlH = new OracleHandler();
                        int i;
                        if (isItemPage)
                            i = sqlH.ExecuteNonQueryAsGivenType<int>("usp_Aspx_CheckCostVariantForItemDetail", parameter, "IsExist");
                        else
                            i = sqlH.ExecuteNonQueryAsGivenType<int>("usp_Aspx_CheckCostVariantForIt", parameter, "IsExist");
                        //i = sqlH.ExecuteNonQueryAsGivenType<int>("usp_Aspx_CheckCostVariantForItem", parameter, "IsExist");
                        return i;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static int AddItemstoCartFromDetail(AddItemToCartInfo AddItemToCartObj, AspxCommonInfo aspxCommonObj, GiftCard giftCardDetail, CartKit kitInfo)
        {
            try
            {

                StoreSettingConfig ssc = new StoreSettingConfig();
                if (AspxCartController.CheckItemCart(AddItemToCartObj.ItemID, aspxCommonObj.StoreID, aspxCommonObj.PortalID, AddItemToCartObj.CostVariantIDs))
                {
                    decimal cartItemId = 0;
                    if (AddItemToCartObj.IsKitItem)
                    {
                        //if kit type 
                        //logic 
                        // serialize
                        //   kitInfo.Data into string and save it db and also description
                        //++price and weight will be from kit info it contains total configured price and weight
                        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                        string kitconfigureddata = json_serializer.Serialize(kitInfo.Data);

                        List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUS(aspxCommonObj);
                        parameter.Add(new KeyValuePair<string, object>("ItemID", AddItemToCartObj.ItemID));
                        parameter.Add(new KeyValuePair<string, object>("Price", kitInfo.Price));
                        parameter.Add(new KeyValuePair<string, object>("Weight", kitInfo.Weight));
                        parameter.Add(new KeyValuePair<string, object>("Quantity", AddItemToCartObj.Quantity));
                        parameter.Add(new KeyValuePair<string, object>("CostVariantsValueIDs", AddItemToCartObj.CostVariantIDs));
                        parameter.Add(new KeyValuePair<string, object>("KitDescription", kitInfo.Description));
                        parameter.Add(new KeyValuePair<string, object>("KitData", kitconfigureddata));
                        OracleHandler sqlH = new OracleHandler();
                        //cartItemId = sqlH.ExecuteAsScalar<int>("usp_Aspx_AddToCart", parameter);
                        cartItemId = sqlH.ExecuteAsScalar<decimal>("usp_Aspx_AddToCart", parameter);
                    }
                    else
                    {
                        List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUS(aspxCommonObj);
                        parameter.Add(new KeyValuePair<string, object>("ItemID", AddItemToCartObj.ItemID));
                        parameter.Add(new KeyValuePair<string, object>("Price", AddItemToCartObj.Price));
                        parameter.Add(new KeyValuePair<string, object>("Weight", AddItemToCartObj.Weight));
                        parameter.Add(new KeyValuePair<string, object>("Quantity", AddItemToCartObj.Quantity));
                        parameter.Add(new KeyValuePair<string, object>("CostVariantsValueIDs", AddItemToCartObj.CostVariantIDs));
                        parameter.Add(new KeyValuePair<string, object>("KitDescription", null));
                        parameter.Add(new KeyValuePair<string, object>("KitData", null));
                        OracleHandler sqlH = new OracleHandler();
                        cartItemId = sqlH.ExecuteAsScalar<decimal>("usp_Aspx_AddToCart", parameter);
                    }

                    if (AddItemToCartObj.IsGiftCard == true && giftCardDetail != null)
                    {
                        List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();
                        param.Add(new KeyValuePair<string, object>("CartItemId", cartItemId));
                        param.Add(new KeyValuePair<string, object>("ItemId", AddItemToCartObj.ItemID));
                        param.Add(new KeyValuePair<string, object>("GiftCardTypeId", giftCardDetail.GiftCardTypeId));
                        param.Add(new KeyValuePair<string, object>("GiftCardGraphicsId", giftCardDetail.GraphicThemeId));
                        param.Add(new KeyValuePair<string, object>("Amount", giftCardDetail.Price));
                        param.Add(new KeyValuePair<string, object>("GiftCardCode", giftCardDetail.GiftCardCode));
                        param.Add(new KeyValuePair<string, object>("RecipientName", giftCardDetail.RecipientName));
                        param.Add(new KeyValuePair<string, object>("RecipientEmail", giftCardDetail.RecipientEmail));
                        param.Add(new KeyValuePair<string, object>("SenderName", giftCardDetail.SenderName));
                        param.Add(new KeyValuePair<string, object>("SenderEmail", giftCardDetail.SenderEmail));
                        param.Add(new KeyValuePair<string, object>("Messege", giftCardDetail.Messege));
                        param.Add(new KeyValuePair<string, object>("StoreId", aspxCommonObj.StoreID));
                        param.Add(new KeyValuePair<string, object>("PortalId", aspxCommonObj.PortalID));
                        param.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                        param.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
                        OracleHandler sqlH = new OracleHandler();
                        sqlH.ExecuteNonQuery("usp_Aspx_AddGiftCard", param);
                    }
                    return 1;
                }
                else
                {
                    if (bool.Parse(ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName)))
                    {
                        int cartItemId = 0;
                        if (AddItemToCartObj.IsKitItem)
                        {
                            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUS(aspxCommonObj);
                            parameter.Add(new KeyValuePair<string, object>("ItemID", AddItemToCartObj.ItemID));
                            parameter.Add(new KeyValuePair<string, object>("Price", AddItemToCartObj.Price));
                            parameter.Add(new KeyValuePair<string, object>("Weight", AddItemToCartObj.Weight));
                            parameter.Add(new KeyValuePair<string, object>("Quantity", AddItemToCartObj.Quantity));
                            parameter.Add(new KeyValuePair<string, object>("CostVariantsValueIDs", AddItemToCartObj.CostVariantIDs));
                            parameter.Add(new KeyValuePair<string, object>("KitDescription", null));
                            parameter.Add(new KeyValuePair<string, object>("KitData", null));
                            OracleHandler sqlH = new OracleHandler();
                            cartItemId = sqlH.ExecuteAsScalar<int>("usp_Aspx_AddToCart", parameter);
                        }
                        else
                        {
                            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUS(aspxCommonObj);
                            parameter.Add(new KeyValuePair<string, object>("ItemID", AddItemToCartObj.ItemID));
                            parameter.Add(new KeyValuePair<string, object>("Price", AddItemToCartObj.Price));
                            parameter.Add(new KeyValuePair<string, object>("Weight", AddItemToCartObj.Weight));
                            parameter.Add(new KeyValuePair<string, object>("Quantity", AddItemToCartObj.Quantity));
                            parameter.Add(new KeyValuePair<string, object>("CostVariantsValueIDs", AddItemToCartObj.CostVariantIDs));
                            parameter.Add(new KeyValuePair<string, object>("KitDescription", null));
                            parameter.Add(new KeyValuePair<string, object>("KitData", null));
                            OracleHandler sqlH = new OracleHandler();
                            cartItemId = sqlH.ExecuteAsScalar<int>("usp_Aspx_AddToCart", parameter);
                        }


                        if (AddItemToCartObj.IsGiftCard == true && giftCardDetail != null)
                        {
                            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();
                            param.Add(new KeyValuePair<string, object>("CartItemId", cartItemId));
                            param.Add(new KeyValuePair<string, object>("ItemId", AddItemToCartObj.ItemID));
                            param.Add(new KeyValuePair<string, object>("GiftCardTypeId", giftCardDetail.GiftCardTypeId));
                            param.Add(new KeyValuePair<string, object>("GiftCardGraphicsId", giftCardDetail.GraphicThemeId));
                            param.Add(new KeyValuePair<string, object>("Amount", giftCardDetail.Price));
                            param.Add(new KeyValuePair<string, object>("GiftCardCode", giftCardDetail.GiftCardCode));
                            param.Add(new KeyValuePair<string, object>("RecipientName", giftCardDetail.RecipientName));
                            param.Add(new KeyValuePair<string, object>("RecipientEmail", giftCardDetail.RecipientEmail));
                            param.Add(new KeyValuePair<string, object>("SenderName", giftCardDetail.SenderName));
                            param.Add(new KeyValuePair<string, object>("SenderEmail", giftCardDetail.SenderEmail));
                            param.Add(new KeyValuePair<string, object>("Messege", giftCardDetail.Messege));
                            param.Add(new KeyValuePair<string, object>("StoreId", aspxCommonObj.StoreID));
                            param.Add(new KeyValuePair<string, object>("PortalId", aspxCommonObj.PortalID));
                            param.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                            param.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
                            OracleHandler sqlH = new OracleHandler();
                            sqlH.ExecuteNonQuery("usp_Aspx_AddGiftCard", param);
                        }
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckItemOutOfStock(int itemID, string costVariantsValueIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("CostVariantsValueIDs", costVariantsValueIDs));
                OracleHandler sqlH = new OracleHandler();
                string i = sqlH.ExecuteNonQueryAsGivenType<string>("usp_Aspx_CheckItemOutOfStock", parameter, "IsOutOfStock");
                return Convert.ToBoolean(i);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MiniCart Display
        //----------------------Count my cart items--------------------     
        public static int GetCartItemsCount(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                decimal cartItemCount = sqlH.ExecuteAsScalar<decimal>("usp_Aspx_GetCartItemsCount", parameter);
                return Convert.ToInt32(cartItemCount);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region StoreSettingImplementation

        public static decimal GetTotalCartItemPrice(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPSCt(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                //decimal cartPrice = sqlH.ExecuteAsScalar<decimal>("usp_Aspx_GetCartItemsTotalAmount", parameter);
                decimal cartPrice = sqlH.ExecuteAsScalar<decimal>("usp_Aspx_GetCartItemsTotalAmou", parameter);
                return cartPrice;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static int GetCompareItemsCount(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUS(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                int compCount = sqlH.ExecuteAsScalar<int>("usp_Aspx_GetCompareItemsCount", parameter);
                return compCount;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static bool CheckAddressAlreadyExist(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPSCt(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                bool isExist = sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckForMultipleAddress", parameter, "IsExist");
                return isExist;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        public static bool GetModuleInstallationInfo(string moduleFriendlyName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("ModuleFriendlyName", moduleFriendlyName));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                bool isInstalled = sqlH.ExecuteNonQueryAsBool("usp_Aspx_IsModuleInstaled", parameterCollection, "IsInstalled");
                return isInstalled;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetStateCode(string cCode, string stateName)
        {
            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("CountryCode", cCode));
            paramCol.Add(new KeyValuePair<string, object>("StateName", stateName));
            OracleHandler sqlHl = new OracleHandler();
            string stateCode = sqlHl.ExecuteAsScalar<string>("usp_Aspx_GetStateCodeByName", paramCol);
            return stateCode;
        }

        public UsersInfo GetUserDetails(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
                UsersInfo lstStatus = sqlH.ExecuteAsObject<UsersInfo>("usp_Aspx_GetUserDetails", parameter);
                return lstStatus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
