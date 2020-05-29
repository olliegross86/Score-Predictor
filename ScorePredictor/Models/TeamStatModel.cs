using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScorePredictor.Models
{
    public class TeamStatModel
    {
        public int TeamNumber { get; set; }
        public string TeamId { get; set; }
        public String TeamName { get; set; }
        public String TeamLogo { get; set; }
        public String TeamShirt { get; set; }
        public String HomeTeam { get; set; }
        public int HomeScore { get; set; }
        public String AwayTeam { get; set; }
        public int AwayScore { get; set; }
        public IList<JToken> Results { get; set; }
        public IList<string> DBResults { get; set; }
        public int ResultsCount { get; set; }
        public IList<JToken> Fixtures { get; set; } 
        public IList<int> HomeWinsList { get; set; }
        public int HomeWins { get; set; }
        public int HomeLosses { get; set; }
        public int HomeDraws { get; set; }
        public float HomeWinPercentage { get; set; }
        public IList <int> HomeScoredList { get; set; }
        public int HomeScored { get; set; }
        public IList<int> HomeConcededList { get; set; }
        public int HomeConceded { get; set; }
        public IList<int> AwayScoredList { get; set; }
        public int AwayScored { get; set; }
        public IList<int> AwayConcededList { get; set; }
        public int AwayConceded { get; set; }
        public IList<int> HomeMarginList { get; set; }
        public float HomeMargin { get; set; }
        public IList<int> AwayWinsList { get; set; }
        public int AwayWins { get; set; }
        public int AwayLosses { get; set; }
        public int AwayDraws { get; set; }
        public float AwayWinPercentage{ get; set; }
        public IList<int> AwayMarginList { get; set; }
        public float AwayMargin { get; set; }
        public int OverallWins { get; set; }
        public float OverallWinPercentage { get; set; }
        public int OverallMargin { get; set; }


        public string NextFixtureHomeTeam { get; set; }
        public string NextFixtureAwayTeam { get; set; }


        public int OppTeamNumber { get; set; }
        public string OppTeamId { get; set; }
        public String OppTeamName { get; set; }
        public String OppTeamLogo { get; set; }
        public String OppTeamShirt { get; set; }
        public IList<JToken> OppResults { get; set; }
        public IList<JToken> OppFixtures { get; set; }
        public IList<int> OppHomeWinsList { get; set; }
        public int OppHomeWins { get; set; }
        public int OppHomeLosses { get; set; }
        public int OppHomeDraws { get; set; }
        public float OppHomeWinPercentage { get; set; }
        public IList<int> OppHomeScoredList { get; set; }
        public int OppHomeScored { get; set; }
        public IList<int> OppHomeConcededList { get; set; }
        public int OppHomeConceded { get; set; }
        public IList<int> OppAwayScoredList { get; set; }
        public int OppAwayScored { get; set; }
        public IList<int> OppAwayConcededList { get; set; }
        public int OppAwayConceded { get; set; }
        public IList<int> OppHomeMarginList { get; set; }
        public float OppHomeMargin { get; set; }
        public IList<int> OppAwayWinsList { get; set; }
        public int OppAwayWins { get; set; }
        public int OppAwayLosses { get; set; }
        public int OppAwayDraws { get; set; }
        public float OppAwayWinPercentage { get; set; }
        public IList<int> OppAwayMarginList { get; set; }
        public float OppwayMargin { get; set; }
        public int OppOverallWins { get; set; }
        public float OppOverallWinPercentage { get; set; }
        public int OppOverallMargin { get; set; }

    }
}
