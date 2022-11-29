using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WannaMove.Data;
using WannaMove.Models;

namespace WannaMove.Controllers
{
    public class UaScoresDataFramesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UaScoresDataFramesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // create list method for dropdown
        public List<UaScoresDataFrame> GetDataByContinent()
        {
            using(var context = _context)
            {
                var values = _context.UaScoresDataFrame.Include(x => x.Continent).ToList();
                return values;
            }
        }

        [HttpGet]
        public IActionResult FilterByContinent()
        {
            List<SelectListItem> cont = (from x in _context.UaScoresDataFrame.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Continent,
                                                       Value = x.CityId.ToString()
                                                   }).ToList();
            ViewBag.v = cont;
            return View();
        }

        // GET: UaScoresDataFrames
        public async Task<IActionResult> Index()
        {
            return View(await _context.UaScoresDataFrame.ToListAsync());
        }

        // GET: UaScoresDataFrames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uaScoresDataFrame = await _context.UaScoresDataFrame
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (uaScoresDataFrame == null)
            {
                return NotFound();
            }

            return View(uaScoresDataFrame);
        }

        // GET: UaScoresDataFrames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UaScoresDataFrames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityId,CityName,Country,Housing,CostofLiving,Startups,TravelConnectivity,Commute,BusinessFreedom,Safety,Healthcare,Education,EnvironmentalQuality,Economy,Taxation,InternetAccess,LeisureCulture,Tolerance,Outdoors")] UaScoresDataFrame uaScoresDataFrame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uaScoresDataFrame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uaScoresDataFrame);
        }

        // GET: UaScoresDataFrames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uaScoresDataFrame = await _context.UaScoresDataFrame.FindAsync(id);
            if (uaScoresDataFrame == null)
            {
                return NotFound();
            }
            return View(uaScoresDataFrame);
        }

        // POST: UaScoresDataFrames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityId,CityName,Country,Housing,CostofLiving,Startups,TravelConnectivity,Commute,BusinessFreedom,Safety,Healthcare,Education,EnvironmentalQuality,Economy,Taxation,InternetAccess,LeisureCulture,Tolerance,Outdoors")] UaScoresDataFrame uaScoresDataFrame)
        {
            if (id != uaScoresDataFrame.CityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uaScoresDataFrame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UaScoresDataFrameExists(uaScoresDataFrame.CityId))
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
            return View(uaScoresDataFrame);
        }

        // GET: UaScoresDataFrames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uaScoresDataFrame = await _context.UaScoresDataFrame
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (uaScoresDataFrame == null)
            {
                return NotFound();
            }

            return View(uaScoresDataFrame);
        }

        // POST: UaScoresDataFrames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uaScoresDataFrame = await _context.UaScoresDataFrame.FindAsync(id);
            _context.UaScoresDataFrame.Remove(uaScoresDataFrame);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UaScoresDataFrameExists(int id)
        {
            return _context.UaScoresDataFrame.Any(e => e.CityId == id);
        }
    }
}
