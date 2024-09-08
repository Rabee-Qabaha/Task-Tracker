namespace TaskTracker.App.Interfaces;

public interface ICommand
{
    void Execute(string[] args);
}