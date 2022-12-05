﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        [HttpGet]
        public async Task<IActionResult> FilterByContinent(string[] continentNames)
        {
            var c = _context.UaScoresDataFrame.ToList();
            List<ContinentModel> cont = new List<ContinentModel>();
            //cont.Add(new ContinentModel("All", isActive: true));
           

            foreach (var item in c)
            {
                if (!item.Continent.Equals(cont.ToList()))
                {
                    cont.Add(new ContinentModel(item.Continent, isActive: false));
                }
               
            }
            var withoutDuplicates = cont.GroupBy(x => x.Continent).Select(x => x.First()).ToList();
            ViewData["Continents"] = withoutDuplicates;

            //
            ViewData["CurrentFilter"] = continentNames;

            var con = from co in _context.UaScoresDataFrame
                    select co;

            foreach (var item in continentNames)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    con = con.Where(s => s.Continent.Contains(item));
                }

            }
            return View(await con.ToListAsync());

        }

        public IActionResult GetMessage()
        {
            //string message = "Hello there, you're doing fine";
            var jsonCont = JsonConvert.SerializeObject(_context.UaScoresDataFrame.ToList());
            return Json(jsonCont);
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
