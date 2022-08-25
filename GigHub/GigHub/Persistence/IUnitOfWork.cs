using GigHub.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        INotificationRepository Notifications { get; }
        IUserNotificationRepository UserNotifications { get; }
        IAttendanceRepository Attendances { get; }

        void Complete();
    }
}