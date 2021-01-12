using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RN.WebApi.Presenters.User
{
    public class ChangeRolePresenter : IChangeRoleOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Standart(ChangeRoleOutput output) => ViewModel = new OkObjectResult(output.Roles);
        public void WriteError(string message) => ViewModel = new BadRequestObjectResult(message);
    }
}
