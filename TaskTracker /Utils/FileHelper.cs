namespace TaskTracker.Utils;

public static class FileHelper
{
    
    public static void EnsureFileExists(string filePath, string defaultContent = "[]")
    {
        try
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, defaultContent);
            }
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Error: Unable to create or access the file at {filePath}. Permission denied.");
            throw;
        }
    }

    public static string GetFilePath(string fileName)
    {
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return Path.Combine(homeDirectory, fileName);
    }

    public static async Task<string> ReadFileAsync(string filePath)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException("File not found", filePath);
        return await File.ReadAllTextAsync(filePath);
    }

    public static void WriteFile(string filePath, string content)
    {
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, content);
        }
    }
    
}