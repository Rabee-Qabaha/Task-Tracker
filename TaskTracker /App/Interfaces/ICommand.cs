namespace TaskTracker.App.Interfaces;

public interface ICommand
{
    Task Execute(string[] args);
}