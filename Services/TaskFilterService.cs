using System.Collections.Generic;
using System.Linq;
using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public static class TaskFilterService
    {
        public static IEnumerable<BugReportTask> GetHighPriorityPendingBugs(IEnumerable<BaseTask> tasks)
        {
            return tasks
                .OfType<BugReportTask>()
                .Where(t => !t.IsCompleted && t.SeverityLevel == "High")
                .OrderByDescending(t => t.CreatedAt);
        }

        public static double GetTotalEstimatedHoursForPendingFeatures(IEnumerable<BaseTask> tasks)
        {
            return tasks
                .OfType<FeatureRequestTask>()
                .Where(t => !t.IsCompleted)
                .Sum(t => t.EstimatedHours);
        }
    }
}
