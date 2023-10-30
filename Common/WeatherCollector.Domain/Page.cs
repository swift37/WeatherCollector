using WeatherCollector.Interfaces;

namespace WeatherCollector.Domain
{
    public class Page<T> : IPage<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        public int Index { get; set; }

        public int Size { get; set; }

        public int TotalItemsCount { get; set; }

        public int TotalPagesCount => (int)Math.Ceiling((double)TotalItemsCount / Size);
    }
}
