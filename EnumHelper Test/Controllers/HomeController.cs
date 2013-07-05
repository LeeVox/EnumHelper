using System;
using System.Web.Mvc;
using EnumHelper_Test.Models;

namespace EnumHelper_Test.Controllers
{
    public class HomeController : Controller
    {
        private StoreModel getSampleModel()
        {
            return new StoreModel()
            {
                theA = new A() { theB = new B() { user = UserType.Reseller, AllItemTypes = ItemType.Desktop | ItemType.Laptop | ItemType.Phone } },
                Address = "www.amazon.com",
                Email = "info@amazon.com"
            };
        }

        public ActionResult Index()
        {
            return View(getSampleModel());
        }
        
        [EnumModelBindingAttribute]
        public string Action(StoreModel model)
        {
            return "";
        }
    }
}
