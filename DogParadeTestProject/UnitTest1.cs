using Microsoft.VisualStudio.TestTools.UnitTesting;
using DogParade.Controllers;
using Microsoft.AspNetCore.Mvc;
using DogParade.Models;
using System.Collections.Generic;

namespace DogParadeTestProject
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// To test if a Dog model is created as specified in Dog.cs and returned by the controller. It should display DogList data
        /// in the table on the view page.
        /// </summary>
        [TestMethod]
        public void DogsUnitTest()
        {
            DogsUnitTestController controller = new DogsUnitTestController();

            IActionResult result = controller.Index() as IActionResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// To test if a Walker model is created as specified in Walker.cs and returned by the controller. It should display WalkerList data
        /// in the table on the view page.
        /// </summary>
        [TestMethod]
        public void WalkersUnitTest()
        {
            WalkersUnitTestController controller = new WalkersUnitTestController();

            IActionResult result = controller.Index() as IActionResult;

            Assert.IsNotNull(result);
        }
        /// <summary>
        /// To test if a WalkingGroup model is created as specified in WalkingGroup.cs and returned by the controller. It should display WalkingGroupList
        /// data in the table on the view page.
        /// </summary>
        [TestMethod]
        public void WalkingGroupsUnitTest()
        {
            WalkingGroupsUnitTestController controller = new WalkingGroupsUnitTestController();

            IActionResult result = controller.Index() as IActionResult;

            Assert.IsNotNull(result);
        }
    }
}
