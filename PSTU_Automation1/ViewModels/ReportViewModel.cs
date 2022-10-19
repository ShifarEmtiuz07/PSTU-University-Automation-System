using System;
using FastReport.Web;

namespace PSTU_Automation1.ViewModels
{
    public class ReportViewModel
    {
        public WebReport WebReport { get; set; }
        public string ReportName { get; set; }
        public Guid RouteId { get; set; }
    }
}