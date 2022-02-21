using MyShop.Models;

namespace MyShop.MyFolder
{
    public static class CurrentUser
    {
        public static User currentUser { get; set; }

        public static List<Good> cart { get; set; } = new List<Good>();
    }
}
