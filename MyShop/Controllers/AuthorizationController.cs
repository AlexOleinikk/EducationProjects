using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using MyShop.ViewModels;
using MyShop.MyFolder;

namespace MyShop.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly MyShopContext _context;

        public AuthorizationController(MyShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            CurrentUser.currentUser = null;

            return View();
        }

        public async Task<IActionResult> Authorize(UserAuthorizationViewModel userAuthorizationViewModel)
        {
            await foreach (var _user in _context.User)
            {
                if (_user.Email == userAuthorizationViewModel.Email)
                {
                    if (_user.Password == userAuthorizationViewModel.Password)
                    {
                        CurrentUser.currentUser = _user;
                        return RedirectToAction("Index", "Goods");
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
