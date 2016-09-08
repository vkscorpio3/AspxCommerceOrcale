﻿/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2014 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxStoreLocator_slEdit : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName;

    protected void page_init(object sender, EventArgs e)
    {
        InitializeJS();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("StoreLocatorEdit", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/PopUp/style.css");
                IncludeJs("StoreLocatorEdit",  "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/js/PopUp/custom.js");
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserModuleID = SageUserModuleID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;               
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("GoogleMapAPI", ResolveUrl("http://maps.googleapis.com/maps/api/js?sensor=true"));
    }

    public string UserModuleID { get; set; }
}
