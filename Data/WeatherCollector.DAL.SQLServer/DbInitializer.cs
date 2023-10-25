using Microsoft.EntityFrameworkCore;
using WeatherCollector.DAL.Context;
using WeatherCollector.DAL.Entities;

namespace WeatherCollector.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();

            if (context.DataSources.Any()) return;

            var rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                var source = new DataSource
                {
                    Name = $"Source #{i}",
                    Details = $"Test data source #{i}"
                };

                context.DataSources.Add(source);


                var values = new DataValue[rnd.Next(10, 25)]; 
                for (var j = 0; j < values.Length; j++)
                {
                    var value = new DataValue
                    {
                        Time = DateTimeOffset.Now.AddDays(-i * j),
                        Value = $"Test value #{j}",
                        Source = source,
                        IsFault = false
                    };
                    values[j] = value;
                }

                context.DataValues.AddRange(values);
            }

            context.SaveChanges();
        }
    }
}
