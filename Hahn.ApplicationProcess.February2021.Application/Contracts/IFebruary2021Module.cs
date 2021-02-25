using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Contracts
{
    public interface IFebruary2021Module
    {
        Task<T> ExecuteCommandAsync<T>(ICommand<T> command);
        Task ExecuteCommandAsync(ICommand command);
        Task<T> ExecuteQueryAsync<T>(IQuery<T> query);
    }
}
