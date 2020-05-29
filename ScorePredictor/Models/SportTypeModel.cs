using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScorePredictor.Models
{
    public class SportTypeModel
    {
        public IList<JToken> LeagueName { get; set; }
        public IList<String> LeagueLogo { get; set; }
        public List<string> SportType { get; set; }
        public List<int> SportCode { get; set; }
    }
}
