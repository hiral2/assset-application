using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Contracts
{
    public interface ICommandHandler<in T>: IRequestHandler<T> where T: ICommand
    {
    }

    public interface ICommandHandler<in T, R> : IRequestHandler<T,R> where T : ICommand<R>
    {
    }
}
