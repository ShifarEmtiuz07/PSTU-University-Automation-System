using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FastReport.Utils;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSTU_Automation1.Data;
using PSTU_Automation1.Models;
using PSTU_Automation1.ViewModels;

namespace PSTU_Automation1.Controllers
{
    public class ReportsController : Controller
    {
        private static readonly string ReportsFolder = FindReportsFolder();
        private readonly ApplicationDbContext _context;
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private static string FindReportsFolder()
        {
            string fReportsFolder = "";
            string thisFolder = Config.ApplicationFolder;

            for (int i = 0; i < 6; i++)
            {
                string dir = Path.Combine(thisFolder, "Reports");
                if (Directory.Exists(dir))
                {
                    string rep_dir = Path.GetFullPath(dir);
                    if (System.IO.File.Exists(Path.Combine(rep_dir, "reports.xml")))
                    {
                        fReportsFolder = rep_dir;
                        break;
                    }
                }
                thisFolder = Path.Combine(thisFolder, @"..");
            }
            return fReportsFolder;
        }
        public async Task<IActionResult> ApplyPreview(int ID)
        {
            var reportableDataSet = new List<Undergraduation_Apply>
            {
                
            };
            
                    reportableDataSet.Add(await _context.Undergraduation_Apply.Where(p => p.ID == ID).FirstOrDefaultAsync());
                
            
            
                ReportViewModel model;
                model = GenerateReportViewModel(reportableDataSet, "Undergraduation_Apply");

               
                return View("ReportViewer", model);
        }

        private ReportViewModel GenerateReportViewModel<T>(IEnumerable<T> reportableDataSet)
        {
            var model = new ReportViewModel
            {
                WebReport = new WebReport(),
                ReportName = typeof(T).Name
            };

            var reportToLoad = model.ReportName;
            model.WebReport.Report.Load(Path.Combine(ReportsFolder, $"{reportToLoad}.frx"));
            model.WebReport.Report.RegisterData(reportableDataSet, $"{model.ReportName}s");
            model.WebReport.Report.GetDataSource($"{model.ReportName}s").Enabled = true;
            model.WebReport.Report.Prepare();
            return model;
        }
        private ReportViewModel GenerateReportViewModel<T>(IEnumerable<T> reportableDataSet, string templateName)
        {
            var model = new ReportViewModel
            {
                WebReport = new WebReport(),
                ReportName = typeof(T).Name
            };

            var reportToLoad = templateName;
            model.WebReport.Report.Load(Path.Combine(ReportsFolder, $"{reportToLoad}.frx"));
            model.WebReport.Report.RegisterData(reportableDataSet, $"{model.ReportName}s");
            model.WebReport.Report.GetDataSource($"{model.ReportName}s").Enabled = true;
            model.WebReport.Report.Prepare();
            return model;
        }

    }
}
