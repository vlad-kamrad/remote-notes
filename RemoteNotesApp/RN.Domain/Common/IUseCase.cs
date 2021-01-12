using System.Threading.Tasks;

namespace RN.Domain.Common
{
    public interface IUseCase<in TUseCaseInput>
    {
        Task Execute(TUseCaseInput input);
    }
}
