using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PayForMe1._1.Models;

namespace PayForMe1._0.Controllers
{
    public class OrdersController : Controller
    {
        private readonly PayForMe_DBContext _context;

        public OrdersController(PayForMe_DBContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var payForMe_DBContext = _context.Orders
                .Include(o => o.LastService)
                .Include(o => o.MainService)
                .Include(o => o.MiddleService).Include(o => o.User).OrderByDescending(o=>o.OrderDate).ToListAsync();
            return View(await payForMe_DBContext);
        }

        public async Task<IActionResult> UnpaidOrders()
        {
            var UnpaidOrders = _context.Orders
                .Include(o => o.LastService)
                .Include(o => o.MainService)
                .Include(o => o.MiddleService)
                .Include(o => o.User)
                .Where(o=>o.IsDebt == true)
                .OrderByDescending(o => o.OrderDate).ToListAsync();
            return View(await UnpaidOrders);
        }

        public async Task<IActionResult> PaidOrders()
        {
            var paidOrders = _context.Orders
                .Include(o => o.LastService)
                .Include(o => o.MainService)
                .Include(o => o.MiddleService)
                .Include(o => o.User)
                .Where(o => o.IsDebt == false)
                .OrderByDescending(o => o.OrderDate).ToListAsync();
            return View(await paidOrders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.LastService)
                .Include(o => o.MainService)
                .Include(o => o.MiddleService)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            //ViewData["LastServiceId"] = new SelectList(_context.LastServices, "LastServiceId", "LastServiceName");
            ViewData["MainServiceId"] = new SelectList(_context.MainServices, "MainServiceId", "MainServiceName");
            //ViewData["MiddleServiceId"] = new SelectList(_context.MiddleServices, "MiddleServiceId", "MiddleServiceName");
            ViewData["UserId"] = new SelectList(_context.Users.OrderBy(u=>u.UserName), "UserId", "UserName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,UserId,MainServiceId,MiddleServiceId,LastServiceId,PhoneNumber,LandLineNumber,Cost,Tax,IsDebt,Total,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                var user = await _context.Users.FindAsync(order.UserId);
                if (user != null)
                {
                    if (order.IsDebt == true)
                        user.Debits += order.Total;
                    user.Points += (double) order.Total;
                    user.OrdersCount += 1;
                    _context.Update(user);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["LastServiceId"] = new SelectList(_context.LastServices, "LastServiceId", "LastServiceName", order.LastServiceId);
            ViewData["MainServiceId"] = new SelectList(_context.MainServices, "MainServiceId", "MainServiceName", order.MainServiceId);
            //ViewData["MiddleServiceId"] = new SelectList(_context.MiddleServices, "MiddleServiceId", "MiddleServiceName", order.MiddleServiceId);
            ViewData["UserId"] = new SelectList(_context.Users.OrderBy(u => u.UserName), "UserId", "UserName", order.UserId);
            return View(order);
        }

        public IActionResult GetMiddleServices(int MainServiceId)
        {
            var middleServices = _context.MiddleServices.Where(x => x.MainServiceId == MainServiceId).ToList();
            return Ok(middleServices);
        }
        public IActionResult GetLastServices(int MiddleServiceId)
        {
            var lastServices = _context.LastServices.Where(x => x.MiddleServiceId == MiddleServiceId).ToList();
            return Ok(lastServices);
        }
        public IActionResult GetMainServiceCost(int ServiceId)
        {
            var mainservicecost = _context.MainServices.Find(ServiceId);
            var cost = mainservicecost.MainServiceCost;
            return Ok(cost);
        }
        public IActionResult GetMainServiceTax(int ServiceId)
        {
            var mainservicecost = _context.MainServices.Find(ServiceId);
            if(mainservicecost != null)
                return Ok(mainservicecost.MainServiceTax);
            return BadRequest();
        }
        public IActionResult GetMiddleServiceCost(int ServiceId)
        {
            var mainservicecost = _context.MiddleServices.Find(ServiceId);
            var cost = mainservicecost.MiddleServiceCost;
            return Ok(cost);
        }
        public IActionResult GetMiddleServiceTax(int ServiceId)
        {
            var mainservicecost = _context.MiddleServices.Find(ServiceId);
            var cost = mainservicecost.MiddleServiceTax;
            return Ok(cost);
        }
        public IActionResult GetLastServiceCost(int ServiceId)
        {
            var mainservicecost = _context.LastServices.Find(ServiceId);
            var cost = mainservicecost.LastServiceCost;
            return Ok(cost);
        }
        public IActionResult GetLastServiceTax(int ServiceId)
        {
            var mainservicecost = _context.LastServices.Find(ServiceId);
            var cost = mainservicecost.LastServiceTax;
            return Ok(cost);
        }
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["LastServiceId"] = new SelectList(_context.LastServices, "LastServiceId", "LastServiceName", order.LastServiceId);
            ViewData["MainServiceId"] = new SelectList(_context.MainServices, "MainServiceId", "MainServiceName", order.MainServiceId);
            ViewData["MiddleServiceId"] = new SelectList(_context.MiddleServices, "MiddleServiceId", "MiddleServiceName", order.MiddleServiceId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,UserId,MainServiceId,MiddleServiceId,LastServiceId,PhoneNumber,LandLineNumber,Cost,Tax,IsDebt,Total,OrderDate")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["LastServiceId"] = new SelectList(_context.LastServices, "LastServiceId", "LastServiceName", order.LastServiceId);
            ViewData["MainServiceId"] = new SelectList(_context.MainServices, "MainServiceId", "MainServiceName", order.MainServiceId);
            ViewData["MiddleServiceId"] = new SelectList(_context.MiddleServices, "MiddleServiceId", "MiddleServiceName", order.MiddleServiceId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.LastService)
                .Include(o => o.MainService)
                .Include(o => o.MiddleService)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'PayForMe_DBContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
