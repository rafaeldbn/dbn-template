using $ext_safeprojectname$.SharedKernel;
using Ardalis.EFCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Infrastructure.Data;

#nullable enable

public class AppDbContext : DbContext
{
    private readonly IMediator? _mediator;

    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator? mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

        modelBuilder.ConvertToSnakeCase();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_mediator == null) return result;

        // dispatch events only if save was successful
        var domainEvents = new List<BaseDomainEvent>();

        //Caso tenha algum model com Id com tipos diferentes de int e Guid devem ser adicionados aqui
        domainEvents.AddRange(GetEventsFromBaseEntity<int>());
        domainEvents.AddRange(GetEventsFromBaseEntity<Guid>());

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent).ConfigureAwait(false);
        }

        return result;
    }

    private BaseDomainEvent[] GetEventsFromBaseEntity<T>() where T : struct
    {
        var events = new List<BaseDomainEvent>();

        var entitiesWithEvents = ChangeTracker
            .Entries<BaseEntity<T>>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            events.AddRange(entity.Events.ToArray());
            entity.Events.Clear();
        }

        return events.ToArray();
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}