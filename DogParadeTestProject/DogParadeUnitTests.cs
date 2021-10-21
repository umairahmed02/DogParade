using Microsoft.VisualStudio.TestTools.UnitTesting;
using DogParade.Controllers;
using Microsoft.AspNetCore.Mvc;
using DogParade.Models;
using System.Collections.Generic;

namespace DogParadeTestProject
{
    [TestClass]
    public class DogParadeUnitTests
    {
        /// <summary>
        /// To test if a Dog object is created as specified in Dog.cs and returned by the controller.
        /// </summary>
        [TestMethod]
        public void DogsUnitTest()
        {
            DogsUnitTestController controller = new DogsUnitTestController();

            IActionResult result = controller.Index() as IActionResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// To test if a Walker object is created as specified in Walker.cs and returned by the controller.
        /// </summary>
        [TestMethod]
        public void WalkersUnitTest()
        {
            WalkersUnitTestController controller = new WalkersUnitTestController();

            IActionResult result = controller.Index() as IActionResult;

            Assert.IsNotNull(result);
        }
        /// <summary>
        /// To test if a WalkingGroup object is created as specified in WalkingGroup.cs and returned by the controller.
        /// </summary>
        [TestMethod]
        public void WalkingGroupsUnitTest()
        {
            WalkingGroupsUnitTestController controller = new WalkingGroupsUnitTestController();

            IActionResult result = controller.Index() as IActionResult;

            Assert.IsNotNull(result);
        }
        /// <summary>
        /// To test if a dog object is created with the specified values
        /// </summary>
        [TestMethod]
        public void CreateDogTest()
        {
            Dog d = new Dog();
            d.Name = "Sam";
            d.Breed = "Irish Wolfhound";
            d.Age = 10;
            d.Notes = "None";
            d.Group = 1;

            Assert.AreEqual("Sam", d.Name);
            Assert.AreEqual("Irish Wolfhound", d.Breed);
            Assert.AreEqual(10, d.Age);
            Assert.AreEqual("None", d.Notes);
            Assert.AreEqual(1, d.Group);
        }

        /// <summary>
        /// To test if a walker object is created with the specified values
        /// </summary>
        [TestMethod]
        public void CreateWalkerTest()
        {
            Walker w = new Walker();
            w.Name = "Emma Talbot";
            w.Age = 27;
            w.Group = 1;

            Assert.AreEqual("Emma Talbot", w.Name);
            Assert.AreEqual(27, w.Age);
            Assert.AreEqual(1, w.Group);
        }

        /// <summary>
        /// To test if a walking group object is created with the specified values
        /// </summary>
        [TestMethod]
        public void CreateWalkingGroupTest()
        {
            WalkingGroup w = new WalkingGroup();
            w.Walker = 1;
            w.Dogs = 1;
            w.Time = System.DateTime.Today;
            w.DurationMins = 30;
            w.MeetupLocation = "Park";

            Assert.AreEqual(1, w.Walker);
            Assert.AreEqual(1, w.Dogs);
            Assert.AreEqual(System.DateTime.Today, w.Time);
            Assert.AreEqual(30, w.DurationMins);
            Assert.AreEqual("Park", w.MeetupLocation);
        }
    }
}
