using Moq;
using SmartEdu.Api.Models.Foundations.Users;
using SmartEdu.Api.Models.Foundations.Users.Exceptions;

namespace SmartEdu.Api.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfUserIsNullAndLogItAsync()
        {
            //given
            User nullUser = null;
            var nullUserException = new NullUserException();

            var expectedUserValidationException =
                new UserValidationException(nullUserException);

            //when
            ValueTask<User> addUserTask =
                this.userService.AddUserAsync(nullUser);

            //then
            await Assert.ThrowsAsync<UserValidationException>(() =>
                addUserTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedUserValidationException))),
                        Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertUserAsync(It.IsAny<User>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
