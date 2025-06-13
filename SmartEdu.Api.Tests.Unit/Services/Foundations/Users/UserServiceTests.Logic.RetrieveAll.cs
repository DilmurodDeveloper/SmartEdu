using FluentAssertions;
using Moq;
using SmartEdu.Api.Models.Foundations.Users;

namespace SmartEdu.Api.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllUsers()
        {
            //given
            IQueryable<User> randomUsers = CreateRandomUsers();
            IQueryable<User> storageUsers = randomUsers;
            IQueryable<User> expectedUsers = storageUsers;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllUsers())
                    .Returns(storageUsers);

            //when
            IQueryable<User> actualUsers =
                this.userService.RetrieveAllUsers();

            //then
            actualUsers.Should().BeEquivalentTo(expectedUsers);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllUsers(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
