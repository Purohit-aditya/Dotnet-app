using DevOpsDemoApi.Models;

namespace DevOpsDemoApi.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new()
    {
        new TaskItem
        {
            Id = 1,
            Title = "Setup Docker",
            IsCompleted = true
        },

        new TaskItem
        {
            Id = 2,
            Title = "Configure Jenkins",
            IsCompleted = false
        }
    };

    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem Add(TaskItem task)
    {
        task.Id = _tasks.Count + 1;

        _tasks.Add(task);

        return task;
    }

    public void Delete(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task != null)
        {
            _tasks.Remove(task);
        }
    }

    public void Toggle(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
        }
    }
}