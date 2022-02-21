using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using MyShop.MyFolder;
using MyShop.ViewModels;

namespace MyShop.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly MyShopContext _context;

        public RegistrationController(MyShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: Registration/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationViewModel userViewModel)
        {
            User user = new User();

            user.Email = userViewModel.Email;
            user.Name = userViewModel.Name;
            user.Surname = userViewModel.Surname;
            user.Phone = userViewModel.Phone;
            user.Password = userViewModel.Password;
            user.Status = userViewModel.Status;

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                CurrentUser.currentUser = user;
                return RedirectToAction("Index", "Goods");
            }
            
            return View(userViewModel);
        }
    }
}
