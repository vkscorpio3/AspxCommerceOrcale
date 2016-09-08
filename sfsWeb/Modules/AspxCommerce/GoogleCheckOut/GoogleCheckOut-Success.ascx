﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleCheckOut-Success.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxPaymentSuccess_GoogleCheckOutSuccess" %>

<script type="text/javascript">
    //<![CDATA[   
    var orderID = "<%=orderID%>";
    $(function() {
        $(".sfLocale").localize({
            moduleKey: GoogleCheckOut
        });
        var aspxservicePath = AspxCommerce.utils.GetAspxServicePath();
        var aspxCommonObj = function() {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                CustomerID: AspxCommerce.utils.GetCustomerID()
            };
            return aspxCommonInfo;
        };       
        var AspxOrder = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: 0, ///0 for get categories and bind, 1 for notification,2 for versions bind
                checkCartExist: false,
                sessionValue: "",
                error: 0
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: AspxOrder.config.type,
                    contentType: AspxOrder.config.contentType,
                    cache: AspxOrder.config.cache,
                    async: AspxOrder.config.async,
                    url: AspxOrder.config.url,
                    data: AspxOrder.config.data,
                    dataType: AspxOrder.config.dataType,
                    success: AspxOrder.ajaxSuccess,
                    error: AspxOrder.ajaxFailure
                });
            },
            init: function() {
                AspxOrder.ViewOrders();
                $('#btnPrint').on("click", function() {
                    AspxOrder.PrintPage();
                });
            },
            PrintPage: function() {
                var content = $('#<%=divPage.ClientID%>').html();
                var pwin = window.open('', 'print_content', 'width=100,height=100');
                pwin.document.open();
                pwin.document.write('<html><body onload="window.print()">' + content + '</body></html>');
                pwin.document.close();
                setTimeout(function() { pwin.close(); }, 5000);
            },
            ViewOrders: function() {
                var orderId = orderID;
                $("#divOrderDetailForm").show();
                AspxOrder.BindAllOrderDetailsForm(orderId);
            },
            BindAllOrderDetailsForm: function(orderId) {
                this.config.url = this.config.baseURL + "AspxCommerceWebService.asmx/GetAllOrderDetailsForView";
                this.config.data = JSON2.stringify({ orderId: orderId, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            ajaxSuccess: function(data) {
                switch (AspxOrder.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:                        
                        var elements = '';
                        var tableElements = '';
                        var grandTotal = '';
                        var couponAmount = '';
                        var rewardAmount = '';
                        var showRewardPoint = false;
                        var subTotal = '';
                        var taxTotal = '';
                        var giftCard = '';
                        var shippingCost = '';
                        var discountAmount = '';
                        var additionalNote = "";
                        $.each(data.d, function(index, value) {
                            Array.prototype.clean = function(deleteValue) {
                                for (var i = 0; i < this.length; i++) {
                                    if (this[i] == deleteValue) {
                                        this.splice(i, 1);
                                        i--;
                                    }
                                }
                                return this;
                            };
                            if (index < 1) {
                                var billAdd = '';
                                var arrBill;
                                arrBill = value.BillingAddress.split(',');
                                billAdd += '<li><h4>' + getLocale(GoogleCheckOut, "Billing Address :") + '</h4></li>';
                                billAdd += '<li>' + arrBill[0] + ' ' + arrBill[1] + '</li>';
                                billAdd += '<li>' + arrBill[2] + '</li><li>' + arrBill[3] + '</li><li>' + arrBill[4] + '</li>';
                                billAdd += '<li>' + arrBill[5] + ' ' + arrBill[6] + ' ' + arrBill[7] + '</li><li>' + arrBill[8] + '</li><li>' + arrBill[9] + ', ' + arrBill[10] + '</li><li>' + arrBill[11] + '</li><li>' + arrBill[12] + '</li>';
                                $("#divOrderDetailForm").find('ul').html(billAdd);
                                //$("#OrderDate").html(value.OrderedDate);
                                //$("#invoiceNo").html(value.InVoiceNumber);
                                //   $("#PaymentGatewayType").html(value.PaymentGatewayTypeName);
                                //$("#PaymentMethod").html(value.PaymentMethodName);
                                additionalNote = value.Remarks;
                                // $("#storeName").html(storeName);
                                // $("#storeDescription").html(value.StoreDescription);
                            }
                            tableElements += '<tr>';
                            tableElements += '<td>' + value.SKU + '</td>';
                            if (value.CostVariants != "") {
                                tableElements += '<td>' + value.ItemName + '<br/>' + '(' + value.CostVariants + ')' + '</td>';
                            } else {
                                tableElements += '<td>' + value.ItemName + '<br/></td>';
                            }
                            var shippingAddress = new Array();
                            var shipAdd = '';
                            if (value.ShippingAddress != "N/A") {
                                shippingAddress = value.ShippingAddress.split(",");
                                // shippingAddress.clean(" "); // for removing blank space but now it is done by $.trim()
                                if ($.trim(shippingAddress[0]) != '')
                                    shipAdd = $.trim(shippingAddress[0]) + '</br>';
                                if ($.trim(shippingAddress[1]) != '')
                                    shipAdd += $.trim(shippingAddress[1]) + '</br>';
                                if ($.trim(shippingAddress[2]) != '')
                                    shipAdd += $.trim(shippingAddress[2]) + '</br>';
                                if ($.trim(shippingAddress[3]) != '')
                                    shipAdd += $.trim(shippingAddress[3]) + '</br>';
                                if ($.trim(shippingAddress[4]) != '')
                                    shipAdd += $.trim(shippingAddress[4]) + ' ';
                                if ($.trim(shippingAddress[5]) != '')
                                    shipAdd += $.trim(shippingAddress[5]) + ' ';
                                if ($.trim(shippingAddress[6]) != '')
                                    shipAdd += $.trim(shippingAddress[6]) + '</br>';
                                if ($.trim(shippingAddress[7]) != '')
                                    shipAdd += $.trim(shippingAddress[7]) + '</br>';
                                if ($.trim(shippingAddress[8]) != '')
                                    shipAdd += $.trim(shippingAddress[8]) + '</br>';
                                if ($.trim(shippingAddress[9]) != '')
                                    shipAdd += $.trim(shippingAddress[9]) + ' ';
                                if ($.trim(shippingAddress[10]) != '')
                                    shipAdd += $.trim(shippingAddress[10]) + '</br>';
                                if ($.trim(shippingAddress[11]) != '')
                                    shipAdd += $.trim(shippingAddress[11]) + '</br>';
                                if ($.trim(shippingAddress[12]) != '')
                                    shipAdd += $.trim(shippingAddress[12]);
                            } else {
                                shipAdd = value.ShippingAddress.split(",");
                            }
                            tableElements += '<td>' + value.ShippingMethod + '</td>';
                            tableElements += '<td>' + shipAdd + '</td>';
                            tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (value.ShippingRate * rate).toFixed(2) + '</span></td>';
                            tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (value.Price * rate).toFixed(2) + '</span></td>';
                            tableElements += '<td class="cssOrderItemQuantity">' + value.Quantity + '</td>';
                            tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (value.Price * rate * value.Quantity).toFixed(2) + '</span></td>';
                            tableElements += '</tr>';
                            if (index == 0) {
                                subTotal = '<tr>';
                                subTotal += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>' + getLocale(GoogleCheckOut, "Sub Total :") + '</b></td>';
                                subTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (value.GrandSubTotal * rate).toFixed(2) + '</span></td>';
                                subTotal += '</tr>';

                                var orderID = value.OrderID;
                                $.ajax({
                                    type: "POST",
                                    url: aspxservicePath + "AspxCommerceWebService.asmx/GetTaxDetailsByOrderID",
                                    data: JSON2.stringify({ orderId: orderID, aspxCommonObj: aspxCommonObj() }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: false,
                                    success: function(msg) {
                                        $.each(msg.d, function(index, val) {
                                            if (val.TaxSubTotal != 0) {
                                                taxTotal += '<tr>';
                                                taxTotal += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>' + val.TaxManageRuleName + ':' + '</b></td>';
                                                taxTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (val.TaxSubTotal * rate).toFixed(2) + '</span></td>';
                                                taxTotal += '</tr>';
                                            }
                                        });

                                    }

                                });
                                //taxTotal = '<tr>';
                                //taxTotal += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>'+ htmlTaxRuleName +'</b></td>';
                                //taxTotal += '<td><span class="cssClassFormatCurrency" >' + htmlTaxRateValue + '</span></td>';
                                //taxTotal += '</tr>';
                                shippingCost = '<tr>';
                                shippingCost += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>' + getLocale(GoogleCheckOut, "Shipping Cost :") + '</b></td>';
                                shippingCost += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (value.TotalShippingCost * rate).toFixed(2) + '</span></td>';
                                shippingCost += '</tr>';
                                discountAmount = '<tr>';
                                discountAmount += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>' + getLocale(GoogleCheckOut, "Discount Amount :") + '</b></td>';
                                discountAmount += '<td class="cssClassAlignRight"><span class="cssClassMinus"> - </span><span class="cssClassFormatCurrency" >' + (value.DiscountAmount * rate).toFixed(2) + '</span></td>';
                                discountAmount += '</tr>';
                                couponAmount = '<tr>';
                                couponAmount += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>' + getLocale(GoogleCheckOut, "Coupon Amount :") + '</b></td>';
                                // if (value.IsPercentage == false) {
                                couponAmount += '<td class="cssClassAlignRight"><span class="cssClassMinus"> - </span><span class="cssClassFormatCurrency" >' + (value.CouponAmount * rate).toFixed(2) + '</span></span></td>';
                                //                            }
                                //                            else {
                                //                                couponAmount += '<td><span>' + value.CouponAmount + '%' + '</span></span></td>';
                                //                            }
                                couponAmount += '</tr>';
                                //reward points
                                var RewardDiscountAmount = value.RewardDiscountAmount;                 
                                if (RewardDiscountAmount > 0) {
                                    showRewardPoint = true;
                                }
                                rewardAmount = '<tr>';
                                rewardAmount += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>' + getLocale(GoogleCheckOut, "Discount ( Reward Points ) :") + '</b></td>';
                                rewardAmount += '<td class="cssClassAlignRight"><span class="cssClassMinus"> - </span><span class="cssClassFormatCurrency" >' + (value.RewardDiscountAmount * rate).toFixed(2) + '</span></span></td>';
                                rewardAmount += '</tr>';
                                //giftCard
                                if (value.GiftCard != "" && value.GiftCard != null) {
                                    var giftCardUsed = value.GiftCard.split('#');
                                    for (var g = 0; g < giftCardUsed.length; g++) {
                                        var keyVal = giftCardUsed[g].split('=');
                                        giftCard += '<tr>';
                                        giftCard += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>' + getLocale(GoogleCheckOut, "Gift Card") + '(' + keyVal[0] +
                                        ') :</b></td>';
                                        giftCard += '<td class="cssClassAlignRight" ><span class="cssClassFormatCurrency" >' + (keyVal[1] * rate).toFixed(2) + '</span></td>';
                                        giftCard += '</tr>';
                                    }
                                }
                                grandTotal = '<tr>';
                                grandTotal += '<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="cssClassLabel"><b>' + getLocale(GoogleCheckOut, "Grand Total :") + '</b></td>';
                                grandTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (value.GrandTotal * rate).toFixed(2) + '</span></td>';
                                grandTotal += '</tr>';
                            }
                        });
                        $("#divOrderDetailForm").find('table>tbody').html(tableElements);
                        $("#tblSuccessTotal").append(subTotal);
                        $("#tblSuccessTotal").append(discountAmount);
                        $("#tblSuccessTotal").append(taxTotal);
                        $("#tblSuccessTotal").append(shippingCost);
                        $("#tblSuccessTotal").append(couponAmount);
                        if (showRewardPoint) {
                            $("#tblSuccessTotal").append(rewardAmount);
                        }
                        giftCard != "" ? $("#tblSuccessTotal").append(giftCard) : giftCard = "";
                        $("#tblSuccessTotal").append(grandTotal);
                        $("#divOrderDetailForm").find("table>tbody tr:even").addClass("sfEven");
                        $("#divOrderDetailForm").find("table>tbody tr:odd").addClass("sfOdd");
                        if (additionalNote != '' && additionalNote != undefined) {
                            $(".remarks").html("").html(getLocale(GoogleCheckOut, "*Additional Note :- ") + "'" + additionalNote + "'");
                        } else {
                            $(".remarks").html("");
                        }
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                        $("#divOrderDetailForm").show();
                        break;
                }
            },
            ajaxFailure: function() {
                switch (AspxOrder.config.error) {
                    case 0:
                        break;
                    case 1:
                        break;
                }
            }

        }
        AspxOrder.init();
    });

    //]]>
</script>

<div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="cssClassLoadingBG">
                &nbsp;</div>
            <div class="cssClassloadingDiv">
                <asp:Image ID="imgPrgress" runat="server" AlternateText="Loading..." ToolTip="Loading..."
                    meta:resourcekey="imgPrgressResource1" />
                <br />
                <asp:Label ID="lblPrgress" runat="server" Text="Please wait..." meta:resourcekey="lblPrgressResource1"></asp:Label>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="divPageOuter" class="PageOuter">
        <div id="error" runat="server">
            <asp:Label ID="lblerror" runat="server" meta:resourcekey="lblerrorResource1"></asp:Label>
        </div>
        <div id="divClickAway">
            <div class="sfButtonwrapper">
                <asp:HyperLink ID="hlnkHomePage" runat="server" meta:resourcekey="hlnkHomePageResource1">Back to Home page</asp:HyperLink>
                <label class="cssClassOrangeBtn i-print"><button id="btnPrint" type="button">
                    <span class="sfLocale">Print</span></button></label>
            </div>
        </div>
        <div id="divPage" class="Page" runat="server">
            <div id="divThankYou">
                <span class="sfLocale">Thank you for your order!</span></div>
            <hr class="HrTop" />
            <div id="divReceiptMsg"class="sfLocale">
                You may print this receipt page for your records.
            </div>
            <div class="SectionBar sfLocale">
                Order Information</div>
            <table id="tablePaymentDetails2Rcpt" cellspacing="0" cellpadding="0" width="60%">
                <tr>
                    <td class="LabelColInfo1R">
                        <strong class="sfCloale">Date/Time:</strong> 
                    </td>
                    <td class="DataColInfo1R">
                        <asp:Label ID="lblDateTime" runat="server" meta:resourcekey="lblDateTimeResource1"></asp:Label>
                    </td>
                    <td class="LabelColInfo1R">
                        <strong  class="sfLocale">Invoice Number:</strong>
                    </td>
                    <td class="DataColInfo1R">
                        <asp:Label ID="lblInvoice" runat="server" meta:resourcekey="lblInvoiceResource1"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr id="hrBillingShippingBefore" />
            <div id="divOrderDetailsBottomSpacerR">
            </div>
            <div class="SectionBar">
            </div>
            <table class="PaymentSectionTable" cellspacing="0" cellpadding="0" width="25%">
                <tr>
                    <td class="LabelColInfo2R">
                        <strong class="sfLcoale">Transaction ID:</strong> 
                    </td>
                    <td class="DataColInfo2R">
                        <asp:Label ID="lblTransaction" runat="server" meta:resourcekey="lblTransactionResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="LabelColInfo2R">
                        <strong class="sfLocale">Payment Method:</strong>
                    </td>
                    <td class="DataColInfo2R">
                        <asp:Label ID="lblPaymentMethod" runat="server" meta:resourcekey="lblPaymentMethodResource1"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="PaymentSectionSpacer">
            </div>
            <hr id="hrOrderDetails" />
            <div class="sfFormwrapper">
                <div class="cssClassCommonBox Curve" id="divOrderDetailForm" style="display: none">
                    <div class="cssClassHeader">
                    </div>
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2 class="sfLocale">
                                Order Details:</h2>
                        </div>
                        <div class="sfGridwrapper">
                            <div class="sfGridWrapperContent">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <thead>
                                        <tr class="cssClassHeading">
                                            <td>
                                                 <strong class="sfLocale">SKU</strong>
                                            </td>
                                            <td>
                                                 <strong class="sfLocale">Item Name</strong>
                                            </td>
                                            <td>
                                                 <strong class="sfLocale">Shipping Method</strong>
                                            </td>
                                            <td>
                                                 <strong class="sfLocale">Shipping Address</strong>
                                            </td>
                                            <td>
                                                 <strong class="sfLocale">Shipping Rate</strong>
                                            </td>
                                            <td>
                                                 <strong class="sfLocale">Unit Price</strong>
                                            </td>
                                            <td>
                                                 <strong class="sfLocale">Quantity</strong>
                                            </td>
                                            <td>
                                                 <strong class="sfLocale">Line Total</strong>
                                            </td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                                <table id="tblSuccessTotal" width="100%"></table>
                                <span class="remarks"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>