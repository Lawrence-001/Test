using AspNetCore.Reporting;
using BootstrapLayout.Data;
using BootstrapLayout.Models;
using BootstrapLayout.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootstrapLayout.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;

        public ReportsController(IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\TestReport.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var systemUsers = await _context.Users.ToListAsync();
            LocalReport report = new LocalReport(path);
            report.AddDataSource("DataSet1", systemUsers);
            var result = report.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "Application/pdf");

        }

        public async Task<IActionResult> Word()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\StaffMembers.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var staffMembers = (await _context.StaffMembers.ToListAsync()).Select(s => new StaffReportVM
            {
                StaffNumber =s.StaffNumber,
                StaffName = s.StaffName,
                Department = Enum.GetName(typeof(Department),s.Department)
            }).ToList();
            LocalReport report = new LocalReport(path);
            report.AddDataSource("StaffMembers", staffMembers);
            var result = report.Execute(RenderType.Word, extension, parameters, mimtype);
            return File(result.MainStream, "Application/word");

        }
    }
}
