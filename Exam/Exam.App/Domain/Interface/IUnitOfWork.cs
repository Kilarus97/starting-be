namespace Exam.App.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IAnimalRepository AnimalRepository { get; set; }
        ICagesRepository CagesRepository { get; set; }
        Task<int> CompleteAsync();
    }
}
