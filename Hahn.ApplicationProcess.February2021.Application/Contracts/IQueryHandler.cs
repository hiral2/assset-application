using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Contracts
{
    public interface IQueryHandler<in T, R> : IRequestHandler<T, R> where T : IQuery<R>
    {
    }
}
