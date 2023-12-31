﻿using WeatherCollector.DAL.Entities.Base;

namespace WeatherCollector.DAL.Entities
{
    public class DataValue : NamedEntity
    {
        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

        public string? Value { get; set; }

        public DataObject? Object { get; set; }

        public DataSource? Source { get; set; }

        public bool IsFault { get; set; }
    }
}
