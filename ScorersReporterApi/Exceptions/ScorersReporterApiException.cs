using System;
namespace ScorersReporterApi.Exceptions
{
	public class ScorersReporterApiException : Exception
    {
        public ScorersReporterApiException(string? message)
            : base($"Scorers reporter api exception: {message}.") { }
    }
}

