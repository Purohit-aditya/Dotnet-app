using Microsoft.AspNetCore.Mvc;
using DevOpsDemoApi.Models;
using DevOpsDemoApi.Services;

namespace DevOpsDemoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public IActionResult GetTasks()
    {
        return Ok(_taskService.GetAll());
    }

    [HttpPost]
    public IActionResult AddTask([FromBody] TaskItem task)
    {
        var createdTask = _taskService.Add(task);

        return Ok(createdTask);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        _taskService.Delete(id);

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult ToggleTask(int id)
    {
        _taskService.Toggle(id);

        return NoContent();
    }
}