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
            int? id;
            id = 2;
            var controller = new BuildingsController();
            var result = controller.Details(id) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }
    }
}
