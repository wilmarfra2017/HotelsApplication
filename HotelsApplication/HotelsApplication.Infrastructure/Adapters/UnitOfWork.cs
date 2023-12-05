using HotelsApplication.Domain.Ports;
using HotelsApplication.Infrastructure.DataSource;
using Microsoft.EntityFrameworkCore;

namespace HotelsApplication.Infrastructure.Adapters
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private async Task InternalSaveAsync(CancellationToken cancellationToken)
        {
            _context.ChangeTracker.DetectChanges();
            var entryStatus = new Dictionary<EntityState, string>
            {
                { EntityState.Added, "CreatedOn" },
                { EntityState.Modified, "LastModifiedOn" }
            };

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                if (entryStatus.TryGetValue(entry.State, out var propertyName))
                {
                    entry.Property(propertyName).CurrentValue = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await InternalSaveAsync(cancellationToken);
        }


        public async Task SaveAsync()
        {
            await InternalSaveAsync(CancellationToken.None);
        }
    }
}
