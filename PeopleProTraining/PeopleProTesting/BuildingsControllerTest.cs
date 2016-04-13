using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleProTraining.Controllers;

namespace PeopleProTesting.Controllers
{
    [TestClass]
    public class BuildingsControllerTest
    {
        [TestMethod]
        public void TestDetailsView()
        {
            var controller = new BuildingsController();
            var result = controller.Details(2) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }
    }
}
