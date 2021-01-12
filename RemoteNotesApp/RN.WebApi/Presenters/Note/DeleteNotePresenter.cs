using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RN.WebApi.Presenters.Note
{
    public class DeleteNotePresenter : IDeleteNoteOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Standart(DeleteNoteOutput output) => ViewModel = new OkObjectResult(output.Success);
        public void WriteError(string message) => ViewModel = new BadRequestObjectResult(message);
    }
}
