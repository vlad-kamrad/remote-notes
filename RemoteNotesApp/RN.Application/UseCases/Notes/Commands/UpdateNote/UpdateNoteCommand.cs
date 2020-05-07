using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RN.Application.UseCases.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
