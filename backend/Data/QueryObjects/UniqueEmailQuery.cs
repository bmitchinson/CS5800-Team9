using backend.Data.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.QueryObjects
{
    public static class UniqueEmailQuery
    {
        public static bool EmailIsTaken(
            this ApplicationDbContext context,
            string email) =>
                context
                .Students
                .Where(_ => _.Email == email)
                .Any() 
                ||
                context
                .Instructors
                .Where(_ => _.Email == email)
                .Any()
                ||
                context
                .Administrators
                .Where(_ => _.Email == email)
                .Any();
    }
}