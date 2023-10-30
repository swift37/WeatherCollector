using WeatherCollector.Interfaces.Entities;

namespace WeatherCollector.Domain
{
    public class Source : INamedEntity, IEquatable<Source>
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Details { get; set; }

        public bool Equals(Source source)
        {
            if (ReferenceEquals(null, source)) return false;
            if (ReferenceEquals(this, source)) return true;
            return Id == source.Id && Name == source.Name && Details == source.Details;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Source)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Details != null ? Details.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Source left, Source right) => Equals(left, right);

        public static bool operator !=(Source left, Source right) => !Equals(left, right);
    }
}
