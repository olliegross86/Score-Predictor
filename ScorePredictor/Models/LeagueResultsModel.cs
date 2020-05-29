using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScorePredictor.Models
{
    public class LeagueResultsModel
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public string LeagueName { get; set; }
        public ICollection<Scores> Results { get; set; }
    }
}
