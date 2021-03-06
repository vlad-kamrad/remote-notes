﻿using MediatR;
using RN.Domain.Entities;
using System.Collections.Generic;

namespace RN.Application.UseCases.User.Commands.ChangeRole
{

    // Use for add, update or delete role list
    public class ChangeRoleCommand : IRequest<List<Role>>
    {
        public string UserId { get; set; }
        public List<string> DesiredRoles { get; set; }
    }
}
