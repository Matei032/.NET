namespace Lab2;

public static class Utils
{
    public static void ShowInfo(object obj)
    {
        if (obj is Task t)
        {
            var status = t.IsCompleted ? "Completed" : "In progress";
            Console.WriteLine($"Task: {t.Title}, Status: {status}");
        }
        else if (obj is Project p)
        {
            Console.WriteLine($"Project: {p.Name}, Tasks: {p.Tasks.Count}");
        }
        else
        {
            Console.WriteLine("Unknown type");
        }
    }
}