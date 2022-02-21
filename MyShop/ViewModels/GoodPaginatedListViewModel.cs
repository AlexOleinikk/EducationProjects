using MyShop.MyFolder;
using MyShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyShop.ViewModels
{
    public class GoodPaginatedListViewModel
    {
        public IEnumerable<MyShop.Models.Good> goods { get; set; }

        public PaginatedList<Good> listGood { get; set; }

        public Good good { get; set; }

        public List<string> categories { get; set; }

        public GoodPaginatedListViewModel(IEnumerable<MyShop.Models.Good> goods, PaginatedList<Good> listGood)
        {
            this.goods = goods;

            this.listGood = listGood;

            this.good = new Good();

            this.categories = new List<string>();
        }

        internal dynamic Select(Func<object, SelectListItem> p)
        {
            return p(this.categories);
        }
    }
}
