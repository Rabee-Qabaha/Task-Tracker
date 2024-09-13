namespace TaskTracker.Utils.Interfaces;

public interface IFileSystem
{
    Task<bool> FileExistsAsync(string path);
    Task WriteAllTextAsync(string path, string content);
    Task<string> ReadAllTextAsync(string path);
    Task<string> CombinePathAsync(string path1, string path2);
    Task<string> GetCurrentDirectoryAsync();
}