﻿
namespace ScorersReporter.Services
{
    public interface IScorersReporterService
    {
        public IEnumerable<T> SaveToDatabase<T>(Stream file);

    }
}