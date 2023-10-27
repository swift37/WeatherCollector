using WeatherCollector.Interfaces;

namespace WeatherCollector.Domain
{
    public class Page<T> : IPage<T>
    {
        public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();

        public int Index { get; init; }

        public int Size { get; init; }

        public int TotalItemsCount { get; init; }

        public int TotalPagesCount => (int)Math.Ceiling((double)TotalItemsCount / Size);
    }
}
