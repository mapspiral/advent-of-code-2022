using System.Reflection;

public static class RunnerHelpers
{
    public static DirectoryInfo? FindContentDirectory(Assembly assembly)
    {
        var currentDirectory = new FileInfo(assembly.Location).Directory;
        while (currentDirectory  != null && !currentDirectory.GetDirectories(".git").Any())
        {
            currentDirectory = currentDirectory.Parent;
        }
        return currentDirectory?.GetDirectories("content").FirstOrDefault();
    }
}