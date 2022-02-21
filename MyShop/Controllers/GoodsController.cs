#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.MyFolder;
using MyShop.Data;
using MyShop.Models;
using MyShop.ViewModels;

namespace MyShop.Views
{
    public class GoodsController : Controller
    {
        private readonly MyShopContext _context;

        public GoodsController(MyShopContext context)
        {
            _context = context;
        }

        // GET: Goods
        public async Task<IActionResult> Index(string sortOrder, int? pageNumber, string searchString)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["CurrentFilter"] = searchString;

            IQueryable<Good> goods = from g in _context.Good
                                     where g.AuthorMail != CurrentUser.currentUser.Email
                                     select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                goods = goods.Where(g => g.CategoryName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    goods = goods.OrderByDescending(g => g.Name);
                    break;
                case "Price":
                    goods = goods.OrderBy(g => g.Price);
                    break;
                case "price_desc":
                    goods = goods.OrderByDescending(g => g.Price);
                    break;
                default:
                    goods = goods.OrderBy(g => g.Name);
                    break;
            }

            int pageSize = 1;

            var toReturn = new GoodPaginatedListViewModel(await goods.Include(g => g.Category).AsNoTracking().ToListAsync(), await PaginatedList<Good>.CreateAsync(goods.Include(g => g.Category).AsNoTracking(), pageNumber ?? 1, pageSize));

            toReturn.categories.Add("All");

            foreach (var categ in _context.Category)
            {
                toReturn.categories.Add(categ.Name);
            }

            ViewBag.AvaiableEnums = toReturn.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.ToString()
                                  });

            return View(toReturn);
        }

        public async Task<IActionResult> MyGoods()
        {
            var goods = from g in _context.Good
                        where g.AuthorMail == CurrentUser.currentUser.Email
                        select g;

            return View(await goods.Include(g => g.Category).AsNoTracking().ToListAsync());
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

        // GET: Goods/Create
        public IActionResult Create()
        {
            if (CurrentUser.currentUser != null)
            {
                ViewData["CategoryName"] = new SelectList(_context.Category, "Name", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Authorization");
            }
        }

        // POST: Goods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Price,Quantity,ShortDesc,LongDesc,AuthorMail,CategoryName")] Good good)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(good);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryName"] = new SelectList(_context.Category, "Name", "Name", good.CategoryName);
            return View(good);
        }

        // GET: Goods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var good = await _context.Good.FindAsync(id);
            if (good == null)
            {
                return NotFound();
            }
            ViewData["CategoryName"] = new SelectList(_context.Category, "Name", "Name", good.CategoryName);
            return View(good);
        }

        // POST: Goods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,Quantity,ShortDesc,LongDesc,AuthorMail,CategoryName")] Good good)
        {
            if (id != good.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(good);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodExists(good.ID))
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
            ViewData["CategoryName"] = new SelectList(_context.Category, "Name", "Name", good.CategoryName);
            return View(good);
        }

        // GET: Goods/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var good = await _context.Good.FindAsync(id);
            _context.Good.Remove(good);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodExists(int id)
        {
            return _context.Good.Any(e => e.ID == id);
        }
    }
}
