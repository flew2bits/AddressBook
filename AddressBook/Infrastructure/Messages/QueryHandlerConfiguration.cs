namespace AddressBook.Infrastructure.Messages;

public static class QueryHandlerConfiguration
{
    public static IServiceCollection AddQueryHandler<TQuery, TResult, TQueryHandler>(this IServiceCollection services)
        where TQueryHandler : class, IQueryHandler<TQuery, TResult>
        => services
            .AddTransient<IQueryHandler<TQuery, TResult>, TQueryHandler>()
            .AddTransient<QueryHandler<TQuery, TResult>>(sp =>
                sp.GetRequiredService<IQueryHandler<TQuery, TResult>>().Handle);
}

public delegate Task<TResult> QueryHandler<in TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default);