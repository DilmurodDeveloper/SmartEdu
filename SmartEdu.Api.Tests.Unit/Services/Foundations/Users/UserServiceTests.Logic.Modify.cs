using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SmartEdu.Api.Models.Foundations.Users;

namespace SmartEdu.Api.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async Task ShouldModifyUserAsync()
        {
            //given
            User randomUser = CreateRandomUser();
            User inputUser = randomUser;
            User storageUser = inputUser.DeepClone();
            User updatedUser = inputUser;
            User expectedUser = updatedUser.DeepClone();
            Guid userId = inputUser.Id;
            
            this.storageBrokerMock.Setup(broker =>
                broker.SelectUserByIdAsync(userId))
                    .ReturnsAsync(storageUser);
            
            this.storageBrokerMock.Setup(broker =>
                broker.UpdateUserAsync(inputUser))
                .ReturnsAsync(updatedUser);
            
            //when
            User actualUser = 
                await this.userService.ModifyUserAsync(inputUser);
            
            //then
            actualUser.Should().BeEquivalentTo(expectedUser);
            
            this.storageBrokerMock.Verify(broker =>
                broker.SelectUserByIdAsync(userId),
                    Times.Once);
            
            this.storageBrokerMock.Verify(broker =>
                broker.UpdateUserAsync(inputUser),
                    Times.Once);
            
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}