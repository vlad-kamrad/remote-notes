using FluentValidation;

namespace RN.Application.UseCases.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(x => x.Text)
              .MaximumLength(300);

            RuleFor(x => x.Title)
                .MinimumLength(1)
                .MaximumLength(80);
        }
    }
}
