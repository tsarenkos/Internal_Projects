namespace TaskTracker.BL.Interfaces
{
    public interface ICheckUserService
    {
        bool AccessGrantedForUser(int taskId);
    }
}
