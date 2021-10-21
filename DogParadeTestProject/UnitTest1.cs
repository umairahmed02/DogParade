using Microsoft.VisualStudio.TestTools.UnitTesting;
using DogParade.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DogParadeTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DogsUnitTest()
        {
            DogsUnitTestController controller = new DogsUnitTestController();

            IActionResult result = controller.Index() as IActionResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WalkersUnitTest()
        {
            WalkersUnitTestController controller = new WalkersUnitTestController();

            IActionResult result = controller.Index() as IActionResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WalkingGroupsUnitTest()
        {
            WalkingGroupsUnitTestController controller = new WalkingGroupsUnitTestController();

            IActionResult result = controller.Index() as IActionResult;

            Assert.IsNotNull(result);
        }
    }
}
