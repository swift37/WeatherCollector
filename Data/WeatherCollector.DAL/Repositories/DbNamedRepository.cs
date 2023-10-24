using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherCollector.DAL.Context;
using WeatherCollector.DAL.Entities.Base;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.DAL.Repositories
{
    public class DbNamedRepository<T> : DbRepository<T>, INamedRepository<T> where T : NamedEntity, new()
    {
        public DbNamedRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistName(string? name, CancellationToken cancellation = default)
        {
            if (name is null) throw new ArgumentNullException(nameof(name));

            return await Entities.AnyAsync(e => e.Name == name).ConfigureAwait(false);
        }

        public async Task<T?> GetByName(string? name, CancellationToken cancellation = default)
        {
            if (name is null) throw new ArgumentNullException(nameof(name));
            
            return await Entities.FirstOrDefaultAsync(e => e.Name == name, cancellation).ConfigureAwait(false);
        }

        public async Task<T?> DeleteByName(string? name, CancellationToken cancellation = default)
        {
            if (name is null) throw new ArgumentNullException(nameof(name));

            var entity = DbSet.Local.FirstOrDefault(e => e.Name == name);
            if (entity is null) await DbSet
                    .Select(e => new T { Id = e.Id, Name = e.Name })
                    .FirstOrDefaultAsync(e => e.Name == name, cancellation)
                    .ConfigureAwait(false);
            if (entity is null) return null;

            return await Delete(entity).ConfigureAwait(false);
        }
    }
}
