using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SmartEdu.Api.Models.Foundations.Users;

namespace SmartEdu.Api.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async Task ShouldRemoveUserAsync()
        {
            //given
            Guid randomUserId = Guid.NewGuid();
            Guid inputUserId = randomUserId;
            User randomUser = CreateRandomUser();
            User storageUser = randomUser;
            User expectedInputUser = storageUser;
            User deleteUser = expectedInputUser;
            User expectedUser = deleteUser.DeepClone();
            
            this.storageBrokerMock.Setup(broker =>
                broker.SelectUserByIdAsync(inputUserId))
                    .ReturnsAsync(storageUser);
            
            this.storageBrokerMock.Setup(broker =>
                broker.DeleteUserAsync(expectedInputUser))
                    .ReturnsAsync(deleteUser);
            
            //when
            User actualUser =
                await this.userService
                    .RemoveUserByIdAsync(inputUserId);
            
            //then
            actualUser.Should().BeEquivalentTo(expectedUser);
            
            this.storageBrokerMock.Verify(broker =>
                broker.SelectUserByIdAsync(inputUserId),
                    Times.Once);
            
            this.storageBrokerMock.Verify(broker =>
                broker.DeleteUserAsync(expectedInputUser),
                    Times.Once);
            
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
