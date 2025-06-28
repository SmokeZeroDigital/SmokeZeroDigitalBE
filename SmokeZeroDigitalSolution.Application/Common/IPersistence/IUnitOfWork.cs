namespace SmokeZeroDigitalSolution.Application.Common.IPersistence
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken = default);
        void Save();
    }
}
