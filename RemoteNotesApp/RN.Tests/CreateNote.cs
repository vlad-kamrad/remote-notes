using IFramework.Infrastructure;
using Moq;
using RN.Application.Common.Boundaries.Note;
using RN.Application.Common.Interfaces;
using RN.Application.UseCases.Notes.Commands;
using RN.WebApi.Presenters.Note;
using System.Threading.Tasks;
using Xunit;

namespace RN.UnitTests
{
    public class CreateNote
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public async Task CreateNoteWithWrongTitleLength_NotNull(string title)
        {
            var presenter = new CreateNotePresenter();
            var useCase = new CreateNoteUseCase(
                new Mock<IApplicationDbContext>().Object,
                new Mock<ICurrentUserService>().Object,
                presenter);

            await useCase.Execute(new CreateNoteInput(title, "text"));
            var value = presenter.ViewModel.GetPropertyValue("Value");
            Assert.True(value == "Title length must be more than 1 character");
        }
    }
}
