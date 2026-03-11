using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Models
{
    public delegate void TaskCompletedHandler(BaseTask task);

    public abstract class BaseTask
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
        public string Title { get; set; } = string.Empty;

        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public bool IsCompleted { get; protected set; }

        public event TaskCompletedHandler? OnTaskCompleted;

        public void CompleteTask()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                OnTaskCompleted?.Invoke(this);
            }
        }
    }
}
