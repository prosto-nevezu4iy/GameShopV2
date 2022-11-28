using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Send<TCommand>(TCommand command, CancellationToken ct = default) where TCommand : notnull
        {
            var commandHandler =
                       _serviceProvider.GetService<ICommandHandler<TCommand>>()
                       ?? throw new InvalidOperationException($"Unable to find handler for command '{command.GetType().Name}'");

            return commandHandler.Handle(command, ct);
        }
    }
}
