using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogParade.Models;

namespace DogParade.Controllers
{
    public class DogsUnitTestController : Controller
    {
        public List<Dog> GetDogList()
        {
            return new List<Dog>
            {
                new Dog
                {
                    Name = "Rex",
                    Breed = "German Shepherd",
                    Age = 5,
                    Notes = "None",
                    Group = 1,
                },
                new Dog
                {
                    Name = "Bubbles",
                    Breed = "Cocker Spaniel",
                    Age = 2,
                    Notes = "None",
                    Group = 2,
                },
            };

        }

        public IActionResult Index()
        {
            var dogs = from d in GetDogList() select d;
            return View(dogs);
        }

        public IActionResult Dog()
        {
            var dogs = from e in GetDogList()
                       orderby e.Did
                       select e;
            return View(dogs);
        }

        public ActionResult Details(int Id)
        {
            return View("Details");
        }
    }
}
