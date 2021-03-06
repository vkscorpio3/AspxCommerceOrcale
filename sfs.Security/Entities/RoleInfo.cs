﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.Security.Entities
{
    public class RoleInfo
    {
        public int ID { get; set; }
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public string ApplicationName { get; set; }
        public int PortalID { get; set; }
        public int IsActive { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }

        public RoleInfo() { }
    }
}