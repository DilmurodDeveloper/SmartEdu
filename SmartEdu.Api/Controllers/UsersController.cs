using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using SmartEdu.Api.Models.Foundations.Users;
using SmartEdu.Api.Models.Foundations.Users.Exceptions;
using SmartEdu.Api.Services.Foundations.Users;

namespace SmartEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : RESTFulController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<User>> PostUserAsync(User user)
        {
            try
            {
                User postedUser = await this.userService.AddUserAsync(user);

                return Created(postedUser);
            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
                when (userDependencyValidationException.InnerException
                    is AlreadyExistsUserException)
            {
                return Conflict(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
            {
                return BadRequest(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException.InnerException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException.InnerException);
            }
        }

        [HttpGet("All")]
        public ActionResult<IQueryable<User>> GetAllUsers()
        {
            try
            {
                IQueryable<User> allUsers =
                    this.userService.RetrieveAllUsers();

                return Created(allUsers);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(
                    userDependencyException.InnerException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(
                    userServiceException.InnerException);
            }
        }

        [HttpGet("ById")]
        public async ValueTask<ActionResult<User>> GetUserByIdAsync(Guid userId)
        {
            try
            {
                User getUser = await this.userService.RetrieveUserByIdAsync(userId);

                return Created(getUser);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException.InnerException);
            }
            catch (UserValidationException userValidationException)
                when (userValidationException.InnerException is NotFoundUserException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException.InnerException);
            }
        }
    }
}