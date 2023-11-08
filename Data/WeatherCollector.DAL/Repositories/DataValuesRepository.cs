using Microsoft.EntityFrameworkCore;
using WeatherCollector.DAL.Context;
using WeatherCollector.DAL.Entities;

namespace WeatherCollector.DAL.Repositories
{
    public class DataValuesRepository : DbRepository<DataValue>
    {
        public DataValuesRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<DataValue>> GetAllMatches(string? name, CancellationToken cancellation = default)
        {
            if (name is null) throw new ArgumentNullException(nameof(name));
            
            return await Entities.Where(e => e.Object != null && e.Object.Name == name).ToArrayAsync(cancellation); ;
        }
    }
}
