using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogParade.Models;

namespace DogParade.Controllers
{
    public class WalkingGroupsUnitTestController : Controller
    {
        public List<WalkingGroup> GetWalkingGroupList()
        {
            return new List<WalkingGroup>
            {
                new WalkingGroup
                {
                    Walker = 1,
                    Dogs = 1,
                    Time = DateTime.Now,
                    DurationMins = 30,
                    MeetupLocation = "Park",
                },
                new WalkingGroup
                {
                    Walker = 2,
                    Dogs = 2,
                    Time = DateTime.Now,
                    DurationMins = 20,
                    MeetupLocation = "Town Square",
                },
            };
        }

        public IActionResult Index()
        {
            var groups = from d in GetWalkingGroupList() select d;
            return View(groups);
        }

        public IActionResult WalkingGroup()
        {
            var groups = from e in GetWalkingGroupList()
                       orderby e.Gid
                       select e;
            return View(groups);
        }

        public ActionResult Details(int Id)
        {
            return View("Details");
        }
    }
}
