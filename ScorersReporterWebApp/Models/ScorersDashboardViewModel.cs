using System;
namespace ScorersReporterWebApp.Models
{
	public class ScorersDashboardViewModel
	{
        public IEnumerable<ScorerViewModel> Scorers { get; set; }
		public IEnumerable<CanadianScorerViewModel> Top5CanadianScorers { get; set; }
		public TopScorerViewModel TopScorer { get; set; }
	}
}

