using BootstrapLayout.Data;
using BootstrapLayout.Models;
using BootstrapLayout.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Xml.Linq;

namespace BootstrapLayout.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffRepository _staffRepository;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StaffController(IStaffRepository staffRepository, AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _staffRepository = staffRepository;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        //[Authorize(Policy ="WebPolicy")]
        public IActionResult Index()
        {
            var model = _staffRepository.GetAllStaff().ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff(StaffCreateVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.StaffPhoto != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.StaffPhoto.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.StaffPhoto.CopyTo(new FileStream(filePath, FileMode.Create));

                }
                Staff staff = new Staff()
                {
                    StaffNumber = model.StaffNumber,
                    StaffName = model.StaffName,
                    StaffPhoto = uniqueFileName,
                    StaffEmail = model.StaffEmail,
                    Department = model.Department,
                    Salary = model.Salary
                };
                _staffRepository.CreateStaff(staff);
                //EmailNotification mail = new EmailNotification();
                //mail.SendProfileCreatedNotification("Jogn Doe", "sender@example.com");
               await  sendEmail("Profile Notification #Created", $"Greeting {model.StaffName}, we are glad to inform you that your staff profile has been created", "mbingumuindi34@gmail.com");
                return RedirectToAction("index");
            }
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var staff = _staffRepository.GetStaffById(id);
            if (staff != null)
            {
                return View(staff);
            }
            return View("NotFound");
        }

        [HttpGet]
        public  IActionResult Edit(int id)
        {
            var staffToUpdate = _staffRepository.GetStaffById(id);
            if (staffToUpdate != null)
            {
                StaffEditVM staff = new StaffEditVM()
                {
                    StaffNumber = staffToUpdate.StaffNumber,
                    StaffName = staffToUpdate.StaffName,
                    StaffPhoto = staffToUpdate.StaffPhoto,
                    StaffEmail = staffToUpdate.StaffEmail,
                    Department = staffToUpdate.Department,
                    Salary = staffToUpdate.Salary
                };
               
                return View(staff);
            }
            return View("NotFound");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, StaffEditVM model)
        {
            if (ModelState.IsValid)
            {
                var staff = _staffRepository.GetStaffById(id);

                staff.StaffNumber = model.StaffNumber;
                staff.StaffName = model.StaffName;
                staff.StaffPhoto = model.StaffPhoto;
                staff.StaffEmail = model.StaffEmail;
                staff.Department = model.Department;
                staff.Salary = model.Salary;

                _staffRepository.EditStaff(staff);
                await sendEmail("Profile Notification #Edited", $"Greeting {model.StaffName}, we are glad to inform you that your staff profile has been edited.", "mbingumuindi34@gmail.com");
                return RedirectToAction("index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var staff = _staffRepository.GetStaffById(id);
            if (staff != null)
            {
                _staffRepository.DeleteStaff(id);
                await sendEmail("Profile Notification #Deleted", $"Greeting {staff.StaffName}, we are sad to inform you that your staff profile has been deleted.", "mbingumuindi34@gmail.com");
                return RedirectToAction("index");
            }
            return View("NotFound");
        }

        [HttpGet]
        public static async Task sendEmail(string subject, string emailMmessage, string receiverEmail)
        {
            try
            {
                MailMessage message = new MailMessage();
                string emailAccount = "malelulawrence@gmail.com";
                string password = "rsgpmuzzoouscrzo";
                int port = 587;
                string smtpServer = "smtp.gmail.com";
                bool useSsl = true;
                //string receiverEmail = "muindimbingu34@gmail.com";



                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(emailAccount, "STAFF ALERT");
                message.To.Add(receiverEmail);
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = emailMmessage;
                smtp.UseDefaultCredentials = false;
                smtp.Port = port;
                smtp.Host = smtpServer; //for gmail host;
                smtp.EnableSsl = useSsl;
                smtp.Credentials = new NetworkCredential(emailAccount, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                string userState = "test message1";

                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email sending failed");
            }
        }
    }
}
