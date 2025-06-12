using SmartEdu.Api.Models.Foundations.Users;

namespace SmartEdu.Api.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> AddUserAsync(User user);
        IQueryable<User> RetrieveAllUsers();
    }
}
