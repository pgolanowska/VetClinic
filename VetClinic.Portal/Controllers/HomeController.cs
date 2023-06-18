using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Staff;
using VetClinic.Portal.Models;

namespace VetClinic.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly VetClinicContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(VetClinicContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.ServiceGroupModel = (from serviceGroup in _context.ServiceGroup
                                         where serviceGroup.ServiceGroupIsActive == true
                                         select serviceGroup).ToList();

            ViewBag.News = (from news in _context.News
                                         where news.NewsIsNotArchived == true
                                         orderby news.AddedDate
                            select news).ToList();

            ViewBag.InfoPages = (from infoPage in _context.InfoPage
                            where infoPage.IsActive == true
                            orderby infoPage.InfoPageId
                            select infoPage).ToList();
            return View();
        }
        public IActionResult AboutUs()
        {
            ViewBag.InfoPages = (from infoPage in _context.InfoPage
                                 where infoPage.IsActive == true
                                 orderby infoPage.InfoPageId
                                 select infoPage).ToList();
            // Set the folder where your images are stored
            string folderPath = "../VetClinic.Portal/wwwroot/gallery";

            // Get the list of image file paths
            List<string> imageFiles = Directory.GetFiles(folderPath).ToList();

            // Convert the absolute paths to relative paths
            List<string> relativeImagePaths = imageFiles.Select(file => "/gallery/" + Path.GetFileName(file)).ToList();
            ViewData["ImagePaths"] = relativeImagePaths;

            return View((from info in _context.Info
                         where info.InfoTitle == "About"
                         select info).FirstOrDefault());
        }

        public IActionResult OurStaff()
        {
            ViewBag.InfoPages = (from infoPage in _context.InfoPage
                                 where infoPage.IsActive == true
                                 orderby infoPage.InfoPageId
                                 select infoPage).ToList();

            return View((from employee in _context.Employee
                         join title in _context.Title on employee.TitleId equals title.TitleId
                         join position in _context.Position on employee.PositionId equals position.PositionId
                         where employee.EmployeeIsActive == true
                         select new Employee
                         {
                             EmployeeId = employee.EmployeeId,
                             EmployeeName = employee.EmployeeName,
                             EmployeeSurname = employee.EmployeeSurname,
                             Title = title,
                             Position = position,
                             EmployeeEducation = employee.EmployeeEducation,
                             EmployeeBio = employee.EmployeeBio,
                             EmployeePhoto = employee.EmployeePhoto
                         }).ToList());
        }
        public IActionResult Services()
        {
            ViewBag.InfoPages = (from infoPage in _context.InfoPage
                                 where infoPage.IsActive == true
                                 orderby infoPage.InfoPageId
                                 select infoPage).ToList();

            return View((from serviceGroup in _context.ServiceGroup
                        where serviceGroup.ServiceGroupIsActive == true
                        select serviceGroup).ToList());
        }
        public IActionResult InfoPage(int? id)
        {
            ViewBag.InfoPages = (from infoPage in _context.InfoPage
                                 where infoPage.IsActive == true
                                 orderby infoPage.InfoPageId
                                 select infoPage).ToList();

            if (id == null)
            {
                id = _context.InfoPage.First().InfoPageId;
            }

            var item = _context.InfoPage.Find(id);
            return View(item);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult GetPhoto(int id)
        {
            var employee = _context.Employee.FirstOrDefault(e => e.EmployeeId == id);
            if (employee?.EmployeePhoto != null)
            {
                return File(employee.EmployeePhoto, "image/jpeg");
            }
            return NotFound();
        }
    }
}