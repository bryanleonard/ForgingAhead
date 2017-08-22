using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using ForgingAhead.Models;

namespace ForgingAhead.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _context;

        // constructor injection. Inject ApplicationDbContext into the constructor param 
        // without creating a new one inside the class everytime
        public CharacterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // create method
        [HttpPost]
        public IActionResult Create(Character character)
        {

            // backend validation via ModelState
            if (_context.Characters.Any(e => e.Name == character.name))
            {
                ModelState.AddModelError("Name", "That name is already in use.");
            }
            if (!ModelState.IsValid) 
            {
                return View(character); //this will pop the FE validation
            }
            _context.Characters.Add(character);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // index method (read all)
        [HttpGet] //define specific action
        public IActionResult Index()
        {
            ViewData["Title"] = "Characters";
            var model = _context.Characters.ToList();
            return View(model);
        }

        public IActionResult GetActive()
        {
            var model = _context.Characters.Where(e => e.IsActive).ToList();
            return View(model);

			//lamda expressions are condense forms of querying 
			//var model = new List<character>;
			//foreach (var e in _context.Characters)
			//{
			//    if (e.isActive) {
			//        model.Add(e);
			//    }
			//}
		}

        [Route("Character/{name}/Details")] //custom routing right here. Sitewide would go in Startup.cs
		public IActionResult Details(string name)
		{
			var model = _context.Characters.FirstOrDefault(e => e.Name == name);
			return View(model);
		}

        [HttpPost]
		public IActionResult Edit(string name)
		{
            ViewDetails["Title"] = "Edit " + name;
            var model = _context.Characters.FirstOrDefault(e => e.Name == name);
			return View(model);
		}

        [HttpPost]
        public IActionResult Udpate(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
			_context.SaveChanges();

            return RedirectToAction("Index");
		}

        [HttpPost]
        public IActionResult Delete()
        {
            var original = _context.Characters.FirstOrDefault(e => e.IsActive).ToList();

            if (original != null)
            {
                _context.Characters.Remove(original);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
