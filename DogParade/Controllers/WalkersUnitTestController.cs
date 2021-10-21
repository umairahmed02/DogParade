using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogParade.Models;

namespace DogParade.Controllers
{
    public class WalkersUnitTestController : Controller
    {
        public List<Walker> GetWalkerList()
        {
            return new List<Walker>
            {
                new Walker
                {
                    Name = "Daniel Short",
                    Age = 31,
                },
                new Walker
                {
                    Name = "Emily-Rose Wolfe",
                    Age = 22,
                },
            };

        }

        public IActionResult Index()
        {
            var walkers = from d in GetWalkerList() select d;
            return View(walkers);
        }

        public IActionResult Walker()
        {
            var walkers = from e in GetWalkerList()
                       orderby e.Wid
                       select e;
            return View(walkers);
        }

        public ActionResult Details(int Id)
        {
            return View("Details");
        }
    }
}
