﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class Component
{
    public string Name { get; set; }
    public string FriendlyName { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public string BusinesscontrollerClass { get; set; }
    public string ZipFile { get; set; }
    public bool IsChecked { get; set; }


}