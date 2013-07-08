using System;
using System.Web.Mvc;
using EnumHelper_Test.Models;

namespace EnumHelper_Test.Controllers
{
    public class HomeController : Controller
    {
        public const int ALL_COUSES_ID = 4096;

        private Student getSampleModel()
        {
            return new Student()
            {
                PersonalInfo = new PersonalInfo() { Name = "Bob", Gender = Gender.Female },
                AttendedCourses = Course.ComputerSicene | Course.Math | Course.Chemistry | Course.Philosophy
            };
        }

        public ActionResult Index()
        {
            return View(getSampleModel());
        }

        [FlagEnumModel] // Use this attribute in case of using Flag Enum (AttendedCourses)
        public string SubmitAction(Student model)
        {
            string result = string.Format(@"Name: {0}<br /> Gender: {1}<br />",
                    model.PersonalInfo.Name,
                    model.PersonalInfo.Gender.GetDescription() // Should use GetDescription() instead of ToString()
                );

            if (model.AttendedCourses.HasFlag(ALL_COUSES_ID))
            {
                result += "Courses: All";
            }
            else
            {
                result += "Course: " + model.AttendedCourses.GetDescription();
            }

            return result;
        }
    }
}
