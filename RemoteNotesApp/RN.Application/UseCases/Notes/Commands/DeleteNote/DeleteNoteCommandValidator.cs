using FluentValidation;

namespace RN.Application.UseCases.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
        }
    }
}
