using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TaskManager.API.Interfaces;
using TaskManager.API.Models;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BaseTask>> GetAllTasks()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("priority-bugs")]
        public ActionResult<IEnumerable<BugReportTask>> GetPriorityBugs()
        {
            var allTasks = _repository.GetAll();
            var filteredBugs = TaskFilterService.GetHighPriorityPendingBugs(allTasks);
            return Ok(filteredBugs);
        }

        [HttpGet("features/total-hours")]
        public ActionResult<double> GetFeaturesTotalHours()
        {
            var allTasks = _repository.GetAll();
            var totalHours = TaskFilterService.GetTotalEstimatedHoursForPendingFeatures(allTasks);
            return Ok(totalHours);
        }

        [HttpPut("{id}/complete")]
        public IActionResult CompleteTask(Guid id)
        {
            var task = _repository.GetById(id);
            if (task == null) return NotFound("Task not found");

            task.OnTaskCompleted += (t) =>
            {
                Console.WriteLine($"[EVENT LOG]: Task '{t.Title}' was successfully completed!");
            };

            task.CompleteTask();
            return Ok($"Task '{task.Title}' was finished");
        }

        [HttpGet("{id}")]
        public ActionResult<BaseTask> GetTaskById(Guid id)
        {
            var task = _repository.GetById(id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} was not found.");
            }
            return Ok(task);
        }

        [HttpPost("bugs")]
        public ActionResult<BugReportTask> CreateBug([FromBody] BugReportTask bug)
        {
            _repository.Add(bug);
            return CreatedAtAction(nameof(GetTaskById), new { id = bug.Id }, bug);
        }

        [HttpPost("features")]
        public ActionResult<FeatureRequestTask> CreateFeature([FromBody] FeatureRequestTask feature)
        {
            _repository.Add(feature);
            return CreatedAtAction(nameof(GetTaskById), new { id = feature.Id }, feature);
        }
    }
}
