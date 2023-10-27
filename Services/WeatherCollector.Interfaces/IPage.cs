namespace WeatherCollector.Interfaces
{
    public interface IPage<out T>
    {
        IEnumerable<T> Items { get; }

        int Index { get; }

        int Size { get; }

        int TotalItemsCount { get; }

        int TotalPagesCount { get; }
    }
}
