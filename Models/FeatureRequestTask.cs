using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Models
{
    public class FeatureRequestTask : BaseTask
    {
        [Range(0.1, 100.0, ErrorMessage = "Estimated hours must be between 0.1 and 100")]
        public double EstimatedHours { get; set; }
    }
}
