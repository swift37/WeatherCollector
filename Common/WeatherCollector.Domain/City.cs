using WeatherCollector.Interfaces.Entities;

namespace WeatherCollector.Domain
{
    public class City : INamedEntity, IEquatable<City>
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Country { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool Equals(City city)
        {
            if (ReferenceEquals(null, city)) return false;
            if (ReferenceEquals(this, city)) return true;
            return Id == city.Id 
                && Name == city.Name 
                && Country == city.Country 
                && Latitude == city.Latitude
                && Longitude == city.Longitude;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((City)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Country != null ? Country.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(City left, City right) => Equals(left, right);

        public static bool operator !=(City left, City right) => !Equals(left, right);
    }
}
