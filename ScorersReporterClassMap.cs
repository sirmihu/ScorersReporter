using System;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using ScorersReporter.Models;


namespace ScorersReporter
{
    public class ScorersReporterClassMap : ClassMap<ScorersReport>
    {
        public ScorersReporterClassMap()
        {
            Map(m => m.FirstName).Name("FirstName");
            Map(m => m.LastName).Name("LastName");
            Map(m => m.DateOfBirth).Name("DateOfBirth");
            Map(m => m.Country).Name("Country");
            Map(m => m.Goals).Name("Goals");
            Map(m => m.Assists).Name("Assists");
            Map(m => m.Club).Name("Club");
            Map(m => m.League).Name("League");
            Map(m => m.MarketValue).Name("MarketValue");
        }
    }
}
