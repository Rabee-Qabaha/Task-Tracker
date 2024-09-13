using TaskTracker.Utils.Interfaces;

namespace TaskTracker.Utils;

public class SystemFileSystem: IFileSystem
{
    public async Task<bool> FileExistsAsync(string path)
    {
        return await Task.Run(() => File.Exists(path));
    }

    public async Task WriteAllTextAsync(string path, string content)
    {
        await File.WriteAllTextAsync(path, content);
    }

    public async Task<string> ReadAllTextAsync(string path)
    {
        return await Task.Run(() => File.ReadAllText(path));
    }

    public async Task<string> CombinePathAsync(string path1, string path2)
    {
        return await Task.Run(() => Path.Combine(path1, path2));
    }

    public async Task<string> GetCurrentDirectoryAsync()
    {
        return await Task.Run(Directory.GetCurrentDirectory);
    }
}