using FluentAssertions;
using Moq;
using SmartEdu.Api.Models.Foundations.Users;
using SmartEdu.Api.Models.Foundations.Users.Exceptions;

namespace SmartEdu.Api.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionIfUserIsNullAndLogItAsync()
        {
            //given
            User nullUser = null;
            var nullUserException = new NullUserException();

            var expectedUserValidationException =
                new UserValidationException(nullUserException);

            //when
            ValueTask<User> modifyUserTask =
                this.userService.ModifyUserAsync(nullUser);

            UserValidationException actualUserValidationException =
                await Assert.ThrowsAsync<UserValidationException>(
                    modifyUserTask.AsTask);

            //then
            actualUserValidationException.Should().BeEquivalentTo(
                expectedUserValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedUserValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateUserAsync(It.IsAny<User>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyUserIfUserIsInvalidAndLogItAsync(
            string invalidText)
        {
            //given
            User invalidUser = new User
            {
                FirstName = invalidText
            };

            var invalidUserException = new InvalidUserException();
            
            invalidUserException.AddData(
                key: nameof(User.Id),
                values: "Id is required");

            invalidUserException.AddData(
                key: nameof(User.FirstName),
                values: "Text is required");

            invalidUserException.AddData(
                key: nameof(User.LastName),
                values: "Text is required");

            invalidUserException.AddData(
                key: nameof(User.Email),
                values: "Text is required");

            invalidUserException.AddData(
                key: nameof(User.Address),
                values: "Text is required");

            invalidUserException.AddData(
                key: nameof(User.PasswordHash),
                values: "Text is required");

            invalidUserException.AddData(
                key: nameof(User.CreatedDate),
                values: "Date is required");

            invalidUserException.AddData(
                key: nameof(User.UpdatedDate),
                values: "Date is required");

            invalidUserException.AddData(
                key: nameof(User.IsActive),
                values: "IsActive must be true");

            var expectedUserValidationException =
                new UserValidationException(invalidUserException);

            //when
            ValueTask<User> modifyUserTask =
                this.userService.ModifyUserAsync(invalidUser);
            
            UserValidationException actualUserValidationException =
                await Assert.ThrowsAsync<UserValidationException>(
                    modifyUserTask.AsTask);
            
            //then
            actualUserValidationException.Should().BeEquivalentTo(
                expectedUserValidationException);
            
            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedUserValidationException))),
                        Times.Once);
            
            this.storageBrokerMock.Verify(broker =>
                broker.UpdateUserAsync(It.IsAny<User>()),
                    Times.Never);
            
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfRoleIsInvalidAndLogItAsync()
        {
            //given
            User randomUser = CreateRandomUser();
            User invalidUser = randomUser;
            invalidUser.Role = GetInvalidEnum<Role>();
            var invalidUserException = new InvalidUserException();

            invalidUserException.AddData(
                key: nameof(User.Role),
                values: "Role is required");

            var expectedUserValidationException =
                new UserValidationException(invalidUserException);

            //when
            ValueTask<User> addUserTask =
                this.userService.ModifyUserAsync(invalidUser);

            //then
            await Assert.ThrowsAsync<UserValidationException>(() =>
                addUserTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedUserValidationException))),
                        Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateUserAsync(It.IsAny<User>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
