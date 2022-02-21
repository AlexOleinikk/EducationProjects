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

namespace MyShop.Controllers
{
    public class CartController : Controller
    {
        private readonly MyShopContext _context;

        public CartController(MyShopContext context)
        {
            _context = context;
        }

        // GET: OrderGoods
        public IActionResult Index()
        {
            if (CurrentUser.currentUser != null)
            {
                return View(CurrentUser.cart);
            }
            else
            {
                return RedirectToAction("Index", "Authorization");
            }
        }

        public async Task<IActionResult> Add(int id)
        {
            var good = await _context.Good.Include(g => g.Category).FirstOrDefaultAsync(g => g.ID == id);

            if (CurrentUser.cart.Find(g => g.ID == id) == null)
            {
                CurrentUser.cart.Add(good);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            CurrentUser.cart.RemoveAll(g => g.ID == id);

            return RedirectToAction("Index");
        }

        // GET: Goods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var good = await _context.Good
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (good == null)
            {
                return NotFound();
            }

            return View(good);
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder()
        {
            Order order = new Order();

            List<OrderGood> list = new List<OrderGood>();

            order.DateTime = DateTime.Now;

            order.UserEmail = CurrentUser.currentUser.Email;

            foreach (var item in CurrentUser.cart)
            {
                order.TotalSum += item.Price;

                item.Category = null;

                OrderGood orderGood = new OrderGood();

                orderGood.Quantity = 1;

                orderGood.Order = order;

                orderGood.GoodID = item.ID;

                orderGood.Sum = item.Price;

                _context.Add(orderGood);

                list.Add(orderGood);
            }

            order.OrderGoods = list;

            _context.Add(order);

            await _context.SaveChangesAsync();

            CurrentUser.cart = new List<Good>();

            return RedirectToAction("Index", "Goods");
        }
    }
}
