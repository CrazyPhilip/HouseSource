using HouseSource.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseSource.Utils
{
    public static class GlobalVariables
    {
        public static bool IsLogged { get; set; }

        public static UserInfo LoggedUser { get; set; }

        public static bool DarkMode { get; set; }
    }
}
