using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Models;

namespace MyShop.ViewModels
{
    public class CategoriesViewModel
    {
        public Category Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
