using System;
using System.Collections.Generic;
using TaskManager.API.Models;

namespace TaskManager.API.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<BaseTask> GetAll();

        BaseTask? GetById(Guid id);

        void Add(BaseTask task);
    }
}
