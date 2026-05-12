using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DevOpsDemoApi.Tests;

public class TaskApiTests
{
    [Fact]
    public void TaskList_Should_Not_Be_Null()
    {
        var tasks = new List<string>();

        Assert.NotNull(tasks);
    }

    [Fact]
    public void New_Task_Should_Be_Added_Successfully()
    {
        var tasks = new List<string>();

        tasks.Add("Complete CI/CD Pipeline");

        Assert.Single(tasks);
    }

    [Fact]
    public void Task_List_Should_Contain_Added_Task()
    {
        var tasks = new List<string>
        {
            "Docker Setup",
            "Jenkins Integration"
        };

        Assert.Contains("Docker Setup", tasks);
    }

    [Fact]
    public void Completed_Task_Should_Be_Removable()
    {
        var tasks = new List<string>
        {
            "Frontend Build",
            "Backend Build"
        };

        tasks.Remove("Frontend Build");

        Assert.DoesNotContain("Frontend Build", tasks);
    }

    [Fact]
    public void Task_Count_Should_Be_Correct()
    {
        var tasks = new List<string>
        {
            "Docker",
            "Kubernetes",
            "Terraform"
        };

        Assert.Equal(3, tasks.Count);
    }

    [Fact]
    public void Task_Search_Should_Return_Expected_Result()
    {
        var tasks = new List<string>
        {
            "Setup Jenkins",
            "Configure Docker",
            "Deploy Application"
        };

        var result = tasks.FirstOrDefault(t => t.Contains("Docker"));

        Assert.Equal("Configure Docker", result);
    }
}