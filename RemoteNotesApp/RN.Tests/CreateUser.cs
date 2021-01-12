using IFramework.Infrastructure;
using Moq;
using RN.Application.Common.Boundaries.User;
using RN.Application.Common.Interfaces;
using RN.Application.UseCases.User;
using RN.WebApi.Presenters.User;
using System.Threading.Tasks;
using Xunit;

namespace RN.Tests
{
    public class CreateUser
    {
        private const string message1 = "Name must be between 2 and 40 characters";
        private const string message2 = "Password must be more than 6 characters";

        [Fact]
        public async Task CreateUser_Returns_NotNull()
        {
            var presenter = new CreateUserPresenter();
            var useCase = new CreateUserUseCase(
                new Mock<IApplicationDbContext>().Object,
                new Mock<IAuthService>().Object,
                new Mock<IIdentityService>().Object,
                presenter);

            await useCase.Execute(new CreateUserInput(
                "username", "surname", "email@email.email", "password#123"));

            Assert.NotNull(presenter.ViewModel);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("There----are----40----characters----here")]
        public async Task CreateUserWithWrongInputData_Returns_ErrorMessage(string name)
        {
            var presenter = new CreateUserPresenter();
            var useCase = new CreateUserUseCase(
                new Mock<IApplicationDbContext>().Object,
                new Mock<IAuthService>().Object,
                new Mock<IIdentityService>().Object,
                presenter);

            await useCase.Execute(new CreateUserInput(
                name, "surname", "email@email.email", "password#123"));

            var value = presenter.ViewModel.GetPropertyValue("Value");

            Assert.True(value == message1);
        }


        [Fact]
        public async Task PasswordShouldBeMore6chars()
        {
            var presenter = new CreateUserPresenter();
            var useCase = new CreateUserUseCase(
                new Mock<IApplicationDbContext>().Object,
                new Mock<IAuthService>().Object,
                new Mock<IIdentityService>().Object,
                presenter);

            var shortPassword = "short";

            await useCase.Execute(new CreateUserInput(
                "username", "surname", "email@email.email", shortPassword));

            var value = presenter.ViewModel.GetPropertyValue("Value");

            Assert.True(value == message2);
        }

    }
}
