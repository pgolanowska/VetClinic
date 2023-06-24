using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using System.Diagnostics;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clients;
using VetClinic.Data.Data.Staff;
using VetClinic.Portal.Models;
using VetClinic.Portal.ViewModels;

namespace VetClinic.Portal.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager, IWebHostEnvironment hostingEnvironment, VetClinicContext context)
            :base(userManager, signInManager, hostingEnvironment, context)
        {
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ServiceGroupModel = (from serviceGroup in context.ServiceGroup
                                         where serviceGroup.ServiceGroupIsActive == true
                                         select serviceGroup).ToList();

            ViewBag.News = (from news in context.News
                                         where news.NewsIsNotArchived == true
                                         orderby news.AddedDate
                            select news).ToList();

            ViewBag.InfoPages = (from infoPage in context.InfoPage
                            where infoPage.IsActive == true
                            orderby infoPage.InfoPageId
                            select infoPage).ToList();

            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                ProfileViewModel CurrentUser = new ProfileViewModel
                {
                    Email = user.Email,
                    Name = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientName).FirstOrDefault(),
                    Surname = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientSurname).FirstOrDefault(),
                    //PhoneNumber = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientPhoneNumber).SingleOrDefault(),
                };

                HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(CurrentUser));
            }

            return View();
        }
        public IActionResult AboutUs()
        {
            ViewBag.InfoPages = (from infoPage in context.InfoPage
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

            return View((from info in context.Info
                         where info.InfoTitle == "About"
                         select info).FirstOrDefault());
        }

        public IActionResult OurStaff()
        {
            ViewBag.InfoPages = (from infoPage in context.InfoPage
                                 where infoPage.IsActive == true
                                 orderby infoPage.InfoPageId
                                 select infoPage).ToList();

            return View((from employee in context.Employee
                         join title in context.Title on employee.TitleId equals title.TitleId
                         join position in context.Position on employee.PositionId equals position.PositionId
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
            ViewBag.InfoPages = (from infoPage in context.InfoPage
                                 where infoPage.IsActive == true
                                 orderby infoPage.InfoPageId
                                 select infoPage).ToList();

            return View((from serviceGroup in context.ServiceGroup
                        where serviceGroup.ServiceGroupIsActive == true
                        select serviceGroup).ToList());
        }
        public IActionResult InfoPage(int? id)
        {
            ViewBag.InfoPages = (from infoPage in context.InfoPage
                                 where infoPage.IsActive == true
                                 orderby infoPage.InfoPageId
                                 select infoPage).ToList();

            if (id == null)
            {
                id = context.InfoPage.First().InfoPageId;
            }

            var item = context.InfoPage.Find(id);
            return View(item);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult GetPhoto(int id)
        {
            var employee = context.Employee.FirstOrDefault(e => e.EmployeeId == id);
            if (employee?.EmployeePhoto != null)
            {
                return File(employee.EmployeePhoto, "image/jpeg");
            }
            return NotFound();
        }
    }
}