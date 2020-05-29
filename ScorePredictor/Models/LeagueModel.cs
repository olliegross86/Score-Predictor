using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScorePredictor.Models
{
    public class LeagueModel
    {
        public int TeamNumber { get; set; }
        public IList<JToken> Teams { get; set; }
        public IList<string> DBTeams { get; set; }
        public string TeamName { get; set; }
        public IList<JToken> TeamLogo { get; set; }
    }
}
