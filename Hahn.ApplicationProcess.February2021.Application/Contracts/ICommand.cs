using MediatR;

namespace Hahn.ApplicationProcess.February2021.Application.Contracts
{
    public interface ICommand<out T>: IRequest<T>
    {

    }

    public interface ICommand : IRequest
    {
    }
}
