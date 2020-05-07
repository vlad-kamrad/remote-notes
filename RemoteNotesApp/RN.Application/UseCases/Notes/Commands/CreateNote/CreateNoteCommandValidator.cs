using FluentValidation;

namespace RN.Application.UseCases.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(x => x.Text)
                .MaximumLength(300);

            RuleFor(x => x.Title)
                .MinimumLength(1)
                .MaximumLength(80);
        }
    }
}
