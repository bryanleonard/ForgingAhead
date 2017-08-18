using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public IActionResult Create(Character character)
        {
            _context.Characters.Add(character);
            _context.SaveChanges();

            return RedirectToAction('index');
        }
    }
}
