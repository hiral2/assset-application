using Hahn.ApplicationProcess.February2021.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.MediatR
{
    public class MediatRFebruary2021Module : IFebruary2021Module
    {
        private readonly IMediator mediator;

        public MediatRFebruary2021Module(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public Task<T> ExecuteCommandAsync<T>(ICommand<T> command)
        {
            return mediator.Send<T>(command);
        }

        public Task ExecuteCommandAsync(ICommand command)
        {
            return mediator.Send(command);
        }

        public Task<T> ExecuteQueryAsync<T>(IQuery<T> query)
        {
            return mediator.Send(query);
        }
    }
}
