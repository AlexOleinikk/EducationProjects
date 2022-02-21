#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Views
{
    public class OrderGoodsController : Controller
    {
        private readonly MyShopContext _context;

        public OrderGoodsController(MyShopContext context)
        {
            _context = context;
        }

        // GET: OrderGoods
        public async Task<IActionResult> Index()
        {
            var myShopContext = _context.OrderGood.Include(o => o.Good).Include(o => o.Order);
            return View(await myShopContext.ToListAsync());
        }

        // GET: OrderGoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IEnumerable orderGood = from o in _context.OrderGood.Include(o => o.Good)
                            where o.OrderID == id
                            select o;

            if (orderGood == null)
            {
                return NotFound();
            }

            return View(orderGood);
        }

        // GET: OrderGoods/Create
        public IActionResult Create()
        {
            ViewData["GoodID"] = new SelectList(_context.Good, "ID", "AuthorMail");
            ViewData["OrderID"] = new SelectList(_context.Order, "ID", "ID");
            return View();
        }

        // POST: OrderGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Quantity,Sum,OrderID,GoodID")] OrderGood orderGood)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(orderGood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GoodID"] = new SelectList(_context.Good, "ID", "AuthorMail", orderGood.GoodID);
            ViewData["OrderID"] = new SelectList(_context.Order, "ID", "ID", orderGood.OrderID);
            return View(orderGood);
        }

        // GET: OrderGoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderGood = await _context.OrderGood.FindAsync(id);
            if (orderGood == null)
            {
                return NotFound();
            }
            ViewData["GoodID"] = new SelectList(_context.Good, "ID", "AuthorMail", orderGood.GoodID);
            ViewData["OrderID"] = new SelectList(_context.Order, "ID", "ID", orderGood.OrderID);
            return View(orderGood);
        }

        // POST: OrderGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Quantity,Sum,OrderID,GoodID")] OrderGood orderGood)
        {
            if (id != orderGood.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderGood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderGoodExists(orderGood.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GoodID"] = new SelectList(_context.Good, "ID", "AuthorMail", orderGood.GoodID);
            ViewData["OrderID"] = new SelectList(_context.Order, "ID", "ID", orderGood.OrderID);
            return View(orderGood);
        }

        // GET: OrderGoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderGood = await _context.OrderGood
                .Include(o => o.Good)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderGood == null)
            {
                return NotFound();
            }

            return View(orderGood);
        }

        // POST: OrderGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderGood = await _context.OrderGood.FindAsync(id);
            _context.OrderGood.Remove(orderGood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderGoodExists(int id)
        {
            return _context.OrderGood.Any(e => e.ID == id);
        }
    }
}
