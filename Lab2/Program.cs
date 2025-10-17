using Lab2;
using Task = Lab2.Task;

var manager = new Manager("Elena", "Platform Team", "elena.ionescu@example.com");
Console.WriteLine($"\nManager: {manager.Name}, Team: {manager.Team}, Email: {manager.Email}\n");

List<Task> initial = new()
{
    new Task("Prepare project plan",  false, DateTime.Now.AddDays(4)),
    new Task("Create API skeleton",   false, DateTime.Now.AddDays(2)),
    new Task("Set up monitoring",     true,  DateTime.Now.AddDays(6))
};

var project = new Project("Portal v2", initial);

var updatedTasks = new List<Task>(project.Tasks)
{
    new Task("Write docs",        false, DateTime.Now.AddDays(3)),
    new Task("Fix login issue",   false, DateTime.Now.AddDays(-1))
};
project = project with { Tasks = updatedTasks };

Console.WriteLine("Task list:");
for (int i = 0; i < project.Tasks.Count; i++)
    Console.WriteLine($"{i + 1}. {project.Tasks[i].Title} | Completed: {project.Tasks[i].IsCompleted} | Due: {project.Tasks[i].DueDate:yyyy-MM-dd}");

Console.Write("\nEnter task number to mark as completed (Enter to skip): ");
var input = Console.ReadLine();

if (int.TryParse(input, out int idx) && idx >= 1 && idx <= project.Tasks.Count)
{
    var current = project.Tasks[idx - 1];
    project.Tasks[idx - 1] = current with { IsCompleted = true };
    Console.WriteLine($"Task '{current.Title}' marked as completed.");
}
else if (!string.IsNullOrWhiteSpace(input))
{
    Console.WriteLine("Invalid number. No task updated.");
}

Console.WriteLine("\nAll tasks:");
foreach (var t in project.Tasks)
    Console.WriteLine($"- {t.Title} | Completed: {t.IsCompleted} | Due: {t.DueDate:yyyy-MM-dd}");

Console.WriteLine("\nDetails:");
Utils.ShowInfo(project);
if (project.Tasks.Count > 0) Utils.ShowInfo(project.Tasks[0]);

Func<Task, bool> isOverdue = static t => !t.IsCompleted && t.DueDate < DateTime.Now;
var overdueTasks = project.Tasks.Where(isOverdue).ToList();

Console.WriteLine("\nOverdue tasks:");
if (overdueTasks.Count == 0)
    Console.WriteLine("- None");
else
    foreach (var t in overdueTasks)
        Console.WriteLine($"- {t.Title} (Due: {t.DueDate:yyyy-MM-dd})");

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
