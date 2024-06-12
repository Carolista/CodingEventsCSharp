using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingEvents.Controllers
{
    [Route("EventCategory")]
    public class EventCategoryController : Controller
    {
        private EventDbContext context;

        public EventCategoryController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<EventCategory> categories = context.Categories.ToList();
            return View(categories);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            AddEventCategoryViewModel addEventCategoryViewModel = new();
            return View(addEventCategoryViewModel);
        }

        [HttpPost("create")]
        public IActionResult ProcessCreateEventCategoryForm(
            AddEventCategoryViewModel addEventCategoryViewModel
        )
        {
            if (!ModelState.IsValid)
            {
                // Send back to form
                return View("Create", addEventCategoryViewModel);
            }
            // Otherwise create eventCategory object, add, and save changes
            EventCategory eventCategory = new() { Name = addEventCategoryViewModel.Name };
            context.Categories.Add(eventCategory);
            context.SaveChanges();
            return Redirect("/EventCategory");
        }
    }
}
