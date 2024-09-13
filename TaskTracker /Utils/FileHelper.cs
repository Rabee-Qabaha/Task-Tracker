using TaskTracker.Utils.Interfaces;

namespace TaskTracker.Utils;

public class FileHelper
{
    private static IFileSystem? _fileSystem;

    public FileHelper(IFileSystem? fileSystem)
    {
        _fileSystem = fileSystem;
    }
    
    public async Task EnsureFileExistsAsync(string filePath, string defaultContent = "[]")
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }
        
        try
        {
            if (!await _fileSystem!.FileExistsAsync(filePath))
            {
                await _fileSystem.WriteAllTextAsync(filePath, defaultContent);
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Error: Unable to create or access the file at {filePath}. Permission denied, details: {ex.Message}");
            throw;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"IO Error: Failed to access the file at {filePath}, details: {ex.Message}");
            throw;
        }
    }

    public async Task<string> GetFilePath(string fileName)
    {
        try
        {
            var homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return await _fileSystem!.CombinePathAsync(homeDirectory, fileName);
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Error: Unable to create or access the file {fileName}. Permission denied, more details: {e.Message}");
            throw;
        }
    }

    public async Task<string> ReadFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }

        if (! await _fileSystem!.FileExistsAsync(filePath)) throw new FileNotFoundException("File not found", filePath);
        return await _fileSystem.ReadAllTextAsync(filePath);
    }

    public async Task WriteFileAsync(string filePath, string content)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }
        
        await _fileSystem!.WriteAllTextAsync(filePath, content);
    }
}