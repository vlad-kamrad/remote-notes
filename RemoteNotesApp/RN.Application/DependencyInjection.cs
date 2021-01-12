using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using RN.Application.Common.Behaviours;
using RN.Application.Common.Boundaries.Note;
using RN.Application.UseCases.Notes.Commands;
using RN.Application.UseCases.Notes;
using RN.Application.Common.Boundaries.User;
using RN.Application.UseCases.User;

namespace RN.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICreateNoteUseCase, CreateNoteUseCase>();
            services.AddScoped<IDeleteNoteUseCase, DeleteNoteUseCase>();
            services.AddScoped<IGetNotesUseCase, GetNotesUseCase>();
            services.AddScoped<IUpdateNoteUseCase, UpdateNoteUseCase>();

            services.AddScoped<IChangeRoleUseCase, ChangeRoleUseCase>();
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<IGetRolesUseCase, GetRolesUseCase>();
            services.AddScoped<IGetUserInfoUseCase, GetUserInfoUseCase>();
            services.AddScoped<IGetUsersUseCase, GetUsersUseCase>();
            services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
            services.AddScoped<IUpdateAccessTokenUseCase, UpdateAccessTokenUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();

            // TODO: implement performance logger

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
