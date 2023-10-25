using Microsoft.EntityFrameworkCore;
using WeatherCollector.DAL.Context;
using WeatherCollector.DAL.Entities.Base;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.DAL.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly AppDbContext _context;

        protected DbSet<T> DbSet { get; }

        protected virtual IQueryable<T> Entities => DbSet;

        public DbRepository(AppDbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public async Task<T?> Add(T? entity, CancellationToken cancellation = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            DbSet.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            return entity;
        }

        public async Task<T?> Delete(T? entity, CancellationToken cancellation = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            if (!await Exist(entity, cancellation)) return null;

            DbSet.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            return entity;
        }

        public async Task<T?> DeleteById(int id, CancellationToken cancellation = default)
        {
            var entity = DbSet.Local.FirstOrDefault(e => e.Id == id);
            if (entity is null) await DbSet
                    .Select(e => new T { Id = e.Id })
                    .FirstOrDefaultAsync(e => e.Id == id, cancellation)
                    .ConfigureAwait(false);
            if (entity is null) return null;

            return await Delete(entity).ConfigureAwait(false);
        }

        public async Task<bool> Exist(T? entity, CancellationToken cancellation = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return await Entities.AnyAsync(e => e.Id == entity.Id, cancellation).ConfigureAwait(false);
        }

        public async Task<bool> ExistById(int id, CancellationToken cancellation = default)
        {
            return await Entities.AnyAsync(e => e.Id == id, cancellation).ConfigureAwait(false);
        }

        public async Task<T?> Get(int id, CancellationToken cancellation = default)
        {
            switch (Entities)
            {
                case DbSet<T> dbSet:
                    return await dbSet.FindAsync(new object[] { id }, cancellation).ConfigureAwait(false);
                case { } entities:
                    return await entities.FirstOrDefaultAsync(e => e.Id == id, cancellation).ConfigureAwait(false);
                default:
                    throw new InvalidOperationException("Data source defenition failed.");
            }
        }

        public async Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancellation = default)
        {
            var entitiesCount = await GetCount();

            if (count <= 0 || skip >= entitiesCount || entitiesCount - skip < count) Enumerable.Empty<T>();

            IQueryable<T> query = Entities switch
            {
                IOrderedQueryable<T> orderedQuery => orderedQuery,
                { } q => q.OrderBy(e => e.Id),
            };

            if (skip > 0) query = query.Skip(skip);

            return await query.Take(count).ToArrayAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellation = default)
        {
            return await Entities.ToArrayAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<int> GetCount(CancellationToken cancellation = default)
        {
            return await Entities.CountAsync();
        }
        
        protected record Page(IEnumerable<T> Entities, int Index, int Size, int TotalEntitiesCount) : IPage<T>
        {
            public int TotalPagesCount => (int)Math.Ceiling((double)TotalEntitiesCount / Size);
        }

        public async Task<IPage<T>> GetPage(int index, int size, CancellationToken cancellation = default)
        {
            if (size <= 0) return new Page(Enumerable.Empty<T>(), index, size, size);

            var query = Entities;
            var totalEntitiesCount = await query.CountAsync().ConfigureAwait(false);
            if (totalEntitiesCount == 0) new Page(Enumerable.Empty<T>(), index, 0, totalEntitiesCount);
            if (index * size > totalEntitiesCount) new Page(Enumerable.Empty<T>(), index, 0, totalEntitiesCount);

            if (index > 0) query = query.Skip(index * size);
            query = query.Take(size);
            var entities = await query.ToArrayAsync(cancellation).ConfigureAwait(false);

            return new Page(entities, index, size, totalEntitiesCount);
        }

        public async Task<T?> Update(T? entity, CancellationToken cancellation = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            DbSet.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            return entity;
        }
    }
}
