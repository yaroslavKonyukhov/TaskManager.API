using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Models
{
    public class BugReportTask : BaseTask
    {
        [RegularExpression("Low|Medium|High", ErrorMessage = "Invalid Severity Level")]
        public string SeverityLevel { get; set; } = "Low";
    }
}
