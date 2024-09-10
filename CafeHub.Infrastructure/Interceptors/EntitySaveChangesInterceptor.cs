using CafeHub.Application.Common.Contracts;
using CafeHub.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CafeHub.Infrastructure.Interceptors
{
    public class EntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILoggedInUserService _loggedInUserService;
        public EntitySaveChangesInterceptor(IDateTimeProvider dateTimeProvider, ILoggedInUserService loggedInUserService)
        {
            _dateTimeProvider = dateTimeProvider;
            _loggedInUserService = loggedInUserService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            // handle soft delete
            foreach (var entry in context.ChangeTracker.Entries<ISoftDeleteEntity>().Where(e => e.State == EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
            }

            // handle audit record info
            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _loggedInUserService.UserId ?? Guid.Empty;
                    entry.Entity.CreatedAt = _dateTimeProvider.Now;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedBy = _loggedInUserService.UserId;
                    entry.Entity.UpdatedAt = _dateTimeProvider.Now;
                }
            }
        }
    }
}