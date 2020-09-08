using HouseSource.Models;
using HouseSource.ResponseData;

namespace HouseSource.Utils
{
    public static class GlobalVariables
    {
        public static bool IsLogged { get; set; }

        public static LoginRD LoggedUser { get; set; }

        public static bool DarkMode { get; set; }
    }
}
