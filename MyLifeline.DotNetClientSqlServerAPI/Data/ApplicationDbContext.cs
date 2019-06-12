using MyLifeline.DotNetClientSqlServerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyLifeline.DotNetClientSqlServerAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<DeviceLog> DeviceLogs {get;set;}
    }
}
