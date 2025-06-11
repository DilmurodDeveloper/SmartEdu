using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartEdu.Api.Models.Foundations.Users;

namespace SmartEdu.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<User> Users { get; set; }

        public async ValueTask<User> InsertUserAsync(User user)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<User> userEntityEntry =
                await broker.Users.AddAsync(user);

            await broker.SaveChangesAsync();

            return userEntityEntry.Entity;
        }
    }
}
