using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.API.Interfaces;
using TaskManager.API.Models;

namespace TaskManager.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<BaseTask> _tasks = new();

        public TaskRepository()
        {
            SeedData();
        }

        public IEnumerable<BaseTask> GetAll() => _tasks;

        public BaseTask? GetById(Guid id) => _tasks.FirstOrDefault(t => t.Id == id);

        public void Add(BaseTask task) => _tasks.Add(task);

        private void SeedData()
        {
            Add(new BugReportTask
            {
                Title = "Critical server error",
                SeverityLevel = "High"
            });

            Add(new FeatureRequestTask
            {
                Title = "Add a dark theme",
                EstimatedHours = 5
            });
        }
    }
}
