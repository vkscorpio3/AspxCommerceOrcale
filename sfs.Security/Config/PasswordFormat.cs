﻿#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace SageFrame.Security.Config
{
    public enum PasswordFormat
    {
        Clear = 0,
        Hashed = 1,
        Encrypted = 2
    }
}
