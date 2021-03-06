﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.ServiceItem
{
    public class ServiceItemSettingInfo
    {
        public string AppointmentSuccessPage
        {
            get;
            set;
        }

        public string BookAnAppointmentPage
        {
            get;
            set;
        }

        public bool IsEnableService
        {
            get;
            set;
        }

        public bool IsEnableServiceRss
        {
            get;
            set;
        }

        public int ServiceCategoryCount
        {
            get;
            set;
        }

        public int ServiceCategoryInARow
        {
            get;
            set;
        }

        public string ServiceDetailsPage
        {
            get;
            set;
        }

        public string ServiceItemDetailsPage
        {
            get;
            set;
        }

        public int ServiceRssCount
        {
            get;
            set;
        }

        public string ServiceRssPage
        {
            get;
            set;
        }

        public ServiceItemSettingInfo()
        {
        }
    }
}