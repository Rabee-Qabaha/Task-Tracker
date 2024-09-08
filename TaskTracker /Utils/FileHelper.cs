namespace TaskTracker.Utils;

public static class FileHelper
{
    
    public static void EnsureFileExists(string filePath, string defaultContent = "[]")
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }
        
        try
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, defaultContent);
            }
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Error: Unable to create or access the file at {filePath}. Permission denied, details: {e.Message}");
            throw;
        }
        catch (IOException e)
        {
            Console.WriteLine($"IO Error: Failed to access the file at {filePath}, details: {e.Message}");
            throw;
        }
    }

    public static string GetFilePath(string fileName)
    {
        try
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(homeDirectory, fileName);
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Error: Unable to create or access the file {fileName}. Permission denied, more details: {e.Message}");
            throw;
        }
    }

    public static async Task<string> ReadFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }

        if (!File.Exists(filePath)) throw new FileNotFoundException("File not found", filePath);
        return await File.ReadAllTextAsync(filePath);

    }

    public static async Task WriteFileAsync(string filePath, string content)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }
        
        await File.WriteAllTextAsync(filePath, content);
    }
}