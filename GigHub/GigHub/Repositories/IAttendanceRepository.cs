using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
        Attendance GetAttendance(string userId, int gigId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}