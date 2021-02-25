using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Contracts
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}
