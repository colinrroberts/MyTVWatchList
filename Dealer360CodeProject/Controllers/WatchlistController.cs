using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dealer360CodeProject.Models;
using Dealer360CodeProject.Data;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Dealer360CodeProject.Controllers
{
    public class WatchlistController : Controller
    {
        private readonly ShowContext _context;
        public WatchlistController(ShowContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            List<Show> shows = await _context.Shows.OrderByDescending(s => s.Interest).ThenByDescending(s => s.Rating).ToListAsync();

            return View(shows);
        }

        public async Task<IActionResult> Add([Bind("Name,Interest")] Show show)
        {

            await show.GetTVMazeData();
            _context.Shows.Add(show);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Show record = await _context.Shows.FindAsync(id);
            _context.Shows.Remove(record);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id, [Bind("Genre,Interest")] Show show)
        {
            Show record = await _context.Shows.FindAsync(id);

            record.Genre = show.Genre;
            record.Interest = show.Interest;

            _context.Update(record);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult UpdatePartial(int id)
        {
            Show record = _context.Shows.Find(id);

            return PartialView(record);
        }
    }
}
