using RN.Application.Common.Boundaries.Interfaces;
using RN.Domain.Common;

namespace RN.Application.Common.Boundaries.User
{
    public sealed class UpdateAccessTokenInput
    {
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public UpdateAccessTokenInput(string refreshToken, string username)
        {
            RefreshToken = refreshToken;
            UserName = username;
        }
    }
    public sealed class UpdateAccessTokenOutput
    {
        public string AccessToken { get; }
        public UpdateAccessTokenOutput(string accessToken) => AccessToken = accessToken;
    }
    public interface IUpdateAccessTokenOutputPort : IOutputPortStandard<UpdateAccessTokenOutput>, IOutputPortError { }
    public interface IUpdateAccessTokenUseCase : IUseCase<UpdateAccessTokenInput> { }
}
