namespace RN.Application.Common.Boundaries.Interfaces
{
    public interface IOutputPortStandard<in TUseCaseOutput>
    {
        void Standart(TUseCaseOutput output);
    }
}
