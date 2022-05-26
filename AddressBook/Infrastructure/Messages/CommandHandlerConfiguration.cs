namespace AddressBook.Infrastructure.Messages;

public static class CommandHandlerConfiguration
{
    public static IServiceCollection AddCommandHandler<TCommand, TCommandHandler>(this IServiceCollection services)
        where TCommandHandler : class, ICommandHandler<TCommand> =>
        services
            .AddTransient<ICommandHandler<TCommand>, TCommandHandler>()
            .AddTransient<CommandHandler<TCommand>>(sp => sp.GetRequiredService<ICommandHandler<TCommand>>().Handle);
}

public delegate Task CommandHandler<in TCommand>(TCommand command, CancellationToken cancellationToken = default);