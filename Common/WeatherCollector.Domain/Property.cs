using WeatherCollector.Interfaces.Entities;

namespace WeatherCollector.Domain
{
    public class Property: INamedEntity, IEquatable<Property>
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }

        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

        public string? Value { get; set; }

        public City? City { get; set; }

        public Source? Source { get; set; }

        public bool IsFault { get; set; }

        public bool Equals(Property property)
        {
            if (ReferenceEquals(null, property)) return false;
            if (ReferenceEquals(this, property)) return true;
            return Id == property.Id && Name == property.Name && Value == property.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Property)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Property left, Property right) => Equals(left, right);

        public static bool operator !=(Property left, Property right) => !Equals(left, right);
    }
}
