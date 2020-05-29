using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScorePredictor.Utils
{
    public class API
    {
        public IRestResponse response;
        public IRestResponse Leagues()
        {
            var client = new RestClient("https://www.thesportsdb.com/api/v1/json/1/all_leagues.php");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "www.thesportsdb.com");
            request.AddHeader("Postman-Token", "27bae8b6-06cd-47ab-961b-2447955bc599,6fd48979-2288-4685-b2ef-1f0392456218");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            response = client.Execute(request);

            return (response);
        }

        public IList<JToken> GetLeagues()
        {
            Leagues();

            var jsonData = JsonConvert.DeserializeObject(response.Content);

            JObject json = JObject.Parse(response.Content);

            IList<JToken> leagues = json["leagues"].Children().ToList();
            IList<JToken> leagueList = new List<JToken>();
         
            for (int i = 0; i < leagues.Count; i++)
            {
                if (leagues[i]["idLeague"].ToString() == "4328")
                {
                    var premierLeague = leagues[i]["strLeagueAlternate"].ToString();
                    
                    leagueList.Add(premierLeague);
                }
                else if (leagues[i]["idLeague"].ToString() == "4414")
                {
                    var guinessPremiership = leagues[i]["strLeagueAlternate"].ToString();

                    leagueList.Add(guinessPremiership);
                }
                else
                {
                    var sixnations = "Six Nations";

                    leagueList.Add(sixnations);
                }
            }

            return (leagueList);

        }
        public IRestResponse Teams(string leaguecode)
        {
            var client = new RestClient("https://www.thesportsdb.com/api/v1/json/1/lookup_all_teams.php?id=" + leaguecode);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "www.thesportsdb.com");
            request.AddHeader("Postman-Token", "27bae8b6-06cd-47ab-961b-2447955bc599,6fd48979-2288-4685-b2ef-1f0392456218");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            response = client.Execute(request);

            return (response);
        }


        public IList<JToken> GetTeams(string leagueId)
        {
            Teams(leagueId);

            var jsonData = JsonConvert.DeserializeObject(response.Content);

            JObject json = JObject.Parse(response.Content);

            IList<JToken> teams = json["teams"].Children().ToList();
            IList<JToken> teamList = new List<JToken>();


            for (int i = 0; i < teams.Count; i++)
            {
                var teamId = teams[i]["idTeam"].ToString();
                if (!teamId.Contains("135504") && !teamId.Contains("135204"))
                {
                    var teamname = teams[i]["strTeam"].ToString();
                    var teamlogo = teams[i]["strTeamLogo"].ToString();
                    var manager = teams[i]["strManager"].ToString();
                    var stadium = teams[i]["strStadium"].ToString();
                    var description = teams[i]["strDescriptionEN"].ToString();
                    var stadiumImage = teams[i]["strStadiumThumb"].ToString();
                    var stadiumCapacity = teams[i]["intStadiumCapacity"].ToString();
                    var teamBadge = teams[i]["strTeamBadge"].ToString();
                    var teamKit = teams[i]["strTeamJersey"].ToString();
                    var teamID = teams[i]["idTeam"].ToString();

                    teamList.Add(teamname + "*" + teamlogo + "*" + manager + "*" + stadium + "*" + description + "*" + stadiumImage + "*" + stadiumCapacity + "*" + teamBadge + "*" + teamKit + "*" + teamId);

                }
                else 
                {
                    continue;
                }
               
            }

            return (teamList);
        }

        public IRestResponse Results(string leagueId)
        {
            var client = new RestClient("https://www.thesportsdb.com/api/v1/json/1/eventsseason.php?id=" + leagueId + "&s=1920");
            //var client = new RestClient("https://www.thesportsdb.com/api/v1/json/1/eventspastleague.php?id=" + leagueId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cookie", "__cfduid=d1f3626759c8cec7aa66e1e29cda70e5f1574253953");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "www.thesportsdb.com");
            request.AddHeader("Postman-Token", "ed91ca45-50ce-47a4-8b82-01aed88a37fc,d2a50eb1-d2c6-4205-9fb9-091db56005f5");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            response = client.Execute(request);

            return (response);
        }
        public IList<JToken> GetResults(string leagueId)
        {
            Results(leagueId);

            var jsonData = JsonConvert.DeserializeObject(response.Content);

            JObject json = JObject.Parse(response.Content);

            IList<JToken> results = json["events"].Children().ToList();
            IList<JToken> resultList = new List<JToken>();

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i]["intHomeScore"].ToString() != "")
                {
                    var homeTeam = results[i]["strHomeTeam"].ToString();
                    var awayTeam = results[i]["strAwayTeam"].ToString();
                    var homeScore = results[i]["intHomeScore"].ToString();
                    var awayScore = results[i]["intAwayScore"].ToString();
                    var date = results[i]["dateEvent"].ToString();
                    var video = results[i]["strVideo"].ToString().Replace("https://www.youtube.com/watch?v=", "https://www.youtube.com/embed/");
                    resultList.Add(homeTeam + "*" + homeScore + "*" + awayTeam + "*" + awayScore + "*" + date + "*" + video);
                }
            }

            return (resultList);
        }

        public IRestResponse Fixtures(string teamId)
        {
            var client = new RestClient("https://www.thesportsdb.com/api/v1/json/1/eventsnext.php?id=" + teamId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cookie", "__cfduid=d1f3626759c8cec7aa66e1e29cda70e5f1574253953");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "www.thesportsdb.com");
            request.AddHeader("Postman-Token", "ed91ca45-50ce-47a4-8b82-01aed88a37fc,d2a50eb1-d2c6-4205-9fb9-091db56005f5");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            response = client.Execute(request);

            return (response);
        }
        public IList<JToken> GetFixtures(string teamId)
        {
            Fixtures(teamId);

            var jsonData = JsonConvert.DeserializeObject(response.Content);

            JObject json = JObject.Parse(response.Content);

            IList<JToken> fixtures = json["events"].Children().ToList();
            IList<JToken> fixtureList = new List<JToken>();
            IList<JToken> homeFixtureList = new List<JToken>();
            IList<JToken> awayFixtureList = new List<JToken>();


            for (int i = 0; i < fixtures.Count; i++)
            {
                if (fixtures[i]["intHomeScore"].ToString() == "")
                {
                    var homeTeam = fixtures[i]["strHomeTeam"].ToString();
                    var awayTeam = fixtures[i]["strAwayTeam"].ToString();
                    var date = fixtures[i]["dateEvent"].ToString();


                    fixtureList.Add(homeTeam + "*" + awayTeam + "*" + date);
                }
            }

            return (fixtureList);

        }
    }
    
}
