using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using SmartEdu.Api.Models.Foundations.Users;
using SmartEdu.Api.Models.Foundations.Users.Exceptions;

namespace SmartEdu.Api.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            User someUser = CreateRandomUser();
            SqlException sqlException = GetSqlError();
            var failedUserStorageException = new FailedUserStorageException(sqlException);

            var expectedUserDependencyException =
                new UserDependencyException(failedUserStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertUserAsync(someUser))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<User> addUserTask =
                this.userService.AddUserAsync(someUser);

            // then
            await Assert.ThrowsAsync<UserDependencyException>(() =>
                addUserTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertUserAsync(someUser),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedUserDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDublicateKeyErrorOccursAndLogItAsync()
        {
            //given
            User someUser = CreateRandomUser();
            string someMessage = GetRandomString();
            var dublicateKeyException = new DuplicateKeyException(someMessage);

            var alreadyExistsUserException =
                new AlreadyExistsUserException(dublicateKeyException);

            var expectedUserDependencyValidationException =
                new UserDependencyValidationException(alreadyExistsUserException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertUserAsync(someUser))
                    .ThrowsAsync(dublicateKeyException);

            //when
            ValueTask<User> addUserTask =
                this.userService.AddUserAsync(someUser);

            //then
            await Assert.ThrowsAsync<UserDependencyValidationException>(() =>
                addUserTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertUserAsync(someUser),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedUserDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            User someUser = CreateRandomUser();
            var serviceException = new Exception();

            var failedUserServiceException =
                new FailedUserServiceException(serviceException);

            var expectedUserServiceException =
                new UserServiceException(failedUserServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertUserAsync(someUser))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<User> addUserTask =
                this.userService.AddUserAsync(someUser);

            // then
            await Assert.ThrowsAsync<UserServiceException>(() =>
                addUserTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertUserAsync(someUser),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedUserServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}