using Microsoft.Extensions.DependencyInjection;
using RN.Application.Common.Boundaries.Note;
using RN.Application.Common.Boundaries.User;
using RN.WebApi.Presenters.Note;
using RN.WebApi.Presenters.User;

namespace RN.WebApi.Presenters
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<CreateNotePresenter>();
            services.AddScoped<ICreateNoteOutputPort>(x => x.GetRequiredService<CreateNotePresenter>());
            services.AddScoped<DeleteNotePresenter>();
            services.AddScoped<IDeleteNoteOutputPort>(x => x.GetRequiredService<DeleteNotePresenter>());
            services.AddScoped<UpdateNotePresenter>();
            services.AddScoped<IUpdateNoteOutputPort>(x => x.GetRequiredService<UpdateNotePresenter>());
            services.AddScoped<GetNotesPresenter>();
            services.AddScoped<IGetNotesOutputPort>(x => x.GetRequiredService<GetNotesPresenter>());

            services.AddScoped<ChangeRolePresenter>();
            services.AddScoped<IChangeRoleOutputPort>(x => x.GetRequiredService<ChangeRolePresenter>());
            services.AddScoped<CreateUserPresenter>();
            services.AddScoped<ICreateUserOutputPort>(x => x.GetRequiredService<CreateUserPresenter>());
            services.AddScoped<GetRolesPresenter>();
            services.AddScoped<IGetRolesOutputPort>(x => x.GetRequiredService<GetRolesPresenter>());
            services.AddScoped<GetUserInfoPresenter>();
            services.AddScoped<IGetUserInfoOutputPort>(x => x.GetRequiredService<GetUserInfoPresenter>());
            services.AddScoped<GetUsersPresenter>();
            services.AddScoped<IGetUsersOutputPort>(x => x.GetRequiredService<GetUsersPresenter>());
            services.AddScoped<LoginUserPresenter>();
            services.AddScoped<ILoginUserOutputPort>(x => x.GetRequiredService<LoginUserPresenter>());
            services.AddScoped<UpdateAccessTokenPresenter>();
            services.AddScoped<IUpdateAccessTokenOutputPort>(x => x.GetRequiredService<UpdateAccessTokenPresenter>());
            services.AddScoped<UpdateUserPresenter>();
            services.AddScoped<IUpdateUserOutputPort>(x => x.GetRequiredService<UpdateUserPresenter>());

            return services;
        }
    }
}
