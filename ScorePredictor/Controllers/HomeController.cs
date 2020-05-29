using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using ScorePredictor.Models;

namespace ScorePredictor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to the Score Predictor...your one stop shop for sport results";
            var api = new Utils.API();
            api.GetLeagues();

            IList<JToken> listofLeagues = api.GetLeagues();
            IList<String> listofleaguelogos = new List<String>();
            listofleaguelogos.Add("http://www.pngall.com/wp-content/uploads/4/Premier-League-Logo.png");
            listofleaguelogos.Add("https://berugbymagazine.com/wp-content/uploads/2019/07/Gallagher-Prem-logo.jpg.png");
            listofleaguelogos.Add("https://static1.squarespace.com/static/5919c80cb3db2b3cdad80039/t/5a760368085229a6163816fe/1517683840523/Six+Nations.jpg?format=1500w");
            var sportsType = new SportTypeModel();
            sportsType.LeagueName = listofLeagues;
            sportsType.LeagueLogo = listofleaguelogos;

            return View(sportsType);
        }

        public IActionResult PremierLeague()
        {
            LeagueId = "4328";
            var teamsmodel = new LeagueModel();
            var api = new Utils.API();
            teamsmodel.Teams = api.GetTeams(LeagueId);

            var teamnumber = 0;
            teamsmodel.TeamNumber = teamnumber;


            return View(teamsmodel);
        }

        public IActionResult GallagherPremiership()
        {
            LeagueId = "4414";
            var teamsmodel = new LeagueModel();
            var api = new Utils.API();
            teamsmodel.Teams = api.GetTeams(LeagueId);
            var teamnumber = 0;
            teamsmodel.TeamNumber = teamnumber;


            return View(teamsmodel);
        }
        public IActionResult SixNations()
        {
            var teamsmodel = new LeagueModel();

            var sixnationteams = new List<string>();

            sixnationteams.Add("England" + "*" + "https://cdn.soticservers.net/tools/images/teams/logos/RUGBY969513/d/ENGL6288.svg" + "*" + "0");
            sixnationteams.Add("France" + "*" + "https://cdn.soticservers.net/tools/images/teams/logos/RUGBY969513/d/FRAN5896.svg" + "*" + "1");
            sixnationteams.Add("Ireland" + "*" + "https://cdn.soticservers.net/tools/images/teams/logos/RUGBY969513/d/IREL3260.svg" + "*" + "2");
            sixnationteams.Add("Italy" + "*" + "https://upload.wikimedia.org/wikipedia/en/thumb/b/bb/Italian_Rugby_Federation_logo.svg/1200px-Italian_Rugby_Federation_logo.svg.png" + "*" + "3");
            sixnationteams.Add("Scotland" + "*" + "https://cdn.soticservers.net/tools/images/teams/logos/RUGBY969513/d/SCOT3017.svg" + "*" + "4");
            sixnationteams.Add("Wales" + "*" + "https://cdn.soticservers.net/tools/images/teams/logos/RUGBY969513/d/WALE6240.svg" + "*" + "5");

            teamsmodel.DBTeams = sixnationteams;
            var teamnumber = 0;
            teamsmodel.TeamNumber = teamnumber;


            return View(teamsmodel);
        }


        public string LeagueId { get; set; }
        public string HomeTeam { get; set; }
        public int HomeScore { get; set; }
        public string AwayTeam { get; set; }
        public int AwayScore { get; set; }
        public int ResultsCount { get; set; }
        public int TeamNumber { get; set; }

        [HttpPost]
        public IActionResult TeamStats(int teamnumber, string leagueId, int oppteamnumber)
        {
            var api = new Utils.API();
            var teamstatmodel = new TeamStatModel();
            teamstatmodel.TeamNumber = teamnumber;
            var conn = new Utils.Connection();
            if (leagueId != "9999")
            {
                teamstatmodel.TeamId = teamstatmodel.TeamShirt = api.GetTeams(leagueId)[teamnumber].ToString().Split("*")[9];
                teamstatmodel.TeamName = api.GetTeams(leagueId)[teamnumber].ToString().Split("*")[0];
                teamstatmodel.TeamLogo = api.GetTeams(leagueId)[teamnumber].ToString().Split("*")[7];
                teamstatmodel.TeamShirt = api.GetTeams(leagueId)[teamnumber].ToString().Split("*")[8];
                teamstatmodel.Results = api.GetResults(leagueId);
                teamstatmodel.Fixtures = api.GetFixtures(teamstatmodel.TeamId);
            }
            else
            {
                teamstatmodel.TeamName = conn.SixNationsTeams()[teamnumber].ToString().Split("*")[1];
                teamstatmodel.TeamLogo = conn.SixNationsTeams()[teamnumber].ToString().Split("*")[2];
                teamstatmodel.Results = api.GetResults(leagueId);
                teamstatmodel.Fixtures = api.GetResults(leagueId);
                teamstatmodel.DBResults = conn.Scores();
            }
            
            teamstatmodel.DBResults = conn.Scores();

            if (teamstatmodel.Fixtures.Count() > 0)
            {
                for (int f = 0; f < teamstatmodel.Fixtures.Count(); f++)
                {
                    var nextfixturehometeam = teamstatmodel.Fixtures[f].ToString().Split("*")[0];
                    var nextfixtureawayteam = teamstatmodel.Fixtures[f].ToString().Split("*")[1];

                    if (nextfixturehometeam.Contains(teamstatmodel.TeamName))
                    {
                        teamstatmodel.NextFixtureHomeTeam = nextfixturehometeam;
                        teamstatmodel.NextFixtureAwayTeam = nextfixtureawayteam;
                        teamstatmodel.OppTeamName = nextfixtureawayteam;
                        break;
                    }
                    else if (nextfixtureawayteam.Contains(teamstatmodel.TeamName))
                    {
                        teamstatmodel.NextFixtureHomeTeam = nextfixturehometeam;
                        teamstatmodel.NextFixtureAwayTeam = nextfixtureawayteam;
                        teamstatmodel.OppTeamName = nextfixturehometeam;
                        break;
                    }
                    else
                    {

                        continue;
                    }

                }

            }

            else
            {
                teamstatmodel.NextFixtureHomeTeam = "";
                teamstatmodel.NextFixtureAwayTeam = "";
            }

            var homegamesList = new List<int>();
            var homewinsList = new List<int>();
            var homelossesList = new List<int>();
            var homedrawsList = new List<int>();
            var homescoredList = new List<int>();
            var homeconcededList = new List<int>();
            var homemarginList = new List<int>();

            var opphomegamesList = new List<int>();
            var opphomewinsList = new List<int>();
            var opphomelossesList = new List<int>();
            var opphomedrawsList = new List<int>();
            var opphomescoredList = new List<int>();
            var opphomeconcededList = new List<int>();
            var opphomemarginList = new List<int>();

            var awaywinsList = new List<int>();
            var awaylossesList = new List<int>();
            var awaydrawsList = new List<int>();
            var awaygamesList = new List<int>();
            var awayscoredList = new List<int>();
            var awayconcededList = new List<int>();
            var awaymarginList = new List<int>();

            var oppawaywinsList = new List<int>();
            var oppawaylossesList = new List<int>();
            var oppawaydrawsList = new List<int>();
            var oppawaygamesList = new List<int>();
            var oppawayscoredList = new List<int>();
            var oppawayconcededList = new List<int>();
            var oppawaymarginList = new List<int>();


            if (teamstatmodel.Results.Count != 0)
            {
                for (int i = 0; i < teamstatmodel.Results.Count; i++)
                {
                    teamstatmodel.ResultsCount = teamstatmodel.Results.Count();
                    HomeTeam = teamstatmodel.Results[i].ToString().Split("*")[0];
                    teamstatmodel.HomeTeam = teamstatmodel.Results[i].ToString().Split("*")[0];
                    HomeScore = Int32.Parse(teamstatmodel.Results[i].ToString().Split("*")[1]);
                    teamstatmodel.HomeScore = Int32.Parse(teamstatmodel.Results[i].ToString().Split("*")[1]);
                    AwayTeam = teamstatmodel.Results[i].ToString().Split("*")[2];
                    teamstatmodel.AwayTeam = teamstatmodel.Results[i].ToString().Split("*")[2];
                    AwayScore = Int32.Parse(teamstatmodel.Results[i].ToString().Split("*")[3]);
                    teamstatmodel.AwayScore = Int32.Parse(teamstatmodel.Results[i].ToString().Split("*")[3]);

                    if (HomeTeam.Equals(teamstatmodel.TeamName))
                    {
                        homegamesList.Add(1);
                        homescoredList.Add(HomeScore);
                        homeconcededList.Add(AwayScore);
                        homemarginList.Add(HomeScore - AwayScore);

                        if (HomeScore > AwayScore)
                        {
                            homewinsList.Add(1);
                        }
                        else if (HomeScore < AwayScore)
                        {
                            homelossesList.Add(1);
                        }
                        else
                        {
                            homedrawsList.Add(1);
                        }
                    }

                    else if (HomeTeam.Equals(teamstatmodel.NextFixtureHomeTeam) || HomeTeam.Equals(teamstatmodel.NextFixtureAwayTeam))
                    {
                        opphomegamesList.Add(1);
                        opphomescoredList.Add(HomeScore);
                        opphomeconcededList.Add(AwayScore);
                        opphomemarginList.Add(HomeScore - AwayScore);

                        if (HomeScore > AwayScore)
                        {
                            opphomewinsList.Add(1);
                        }
                        else if (HomeScore < AwayScore)
                        {
                            opphomelossesList.Add(1);
                        }
                        else
                        {
                            opphomedrawsList.Add(1);
                        }
                    }
                    else if (AwayTeam.Equals(teamstatmodel.TeamName))
                    {
                        awaygamesList.Add(1);
                        awayscoredList.Add(AwayScore);
                        awayconcededList.Add(HomeScore);
                        awaymarginList.Add(AwayScore - HomeScore);
                        if (AwayScore > HomeScore)
                        {
                            awaywinsList.Add(1);
                        }
                        else if (HomeScore > AwayScore)
                        {
                            awaylossesList.Add(1);
                        }
                        else
                        {
                            awaydrawsList.Add(1);
                        }
                    }

                    else if (AwayTeam.Equals(teamstatmodel.NextFixtureHomeTeam) || AwayTeam.Equals(teamstatmodel.NextFixtureAwayTeam))
                    {
                        oppawaygamesList.Add(1);
                        oppawayscoredList.Add(AwayScore);
                        oppawayconcededList.Add(HomeScore);
                        oppawaymarginList.Add(AwayScore - HomeScore);
                        if (AwayScore > HomeScore)
                        {
                            oppawaywinsList.Add(1);
                        }
                        else if (HomeScore > AwayScore)
                        {
                            oppawaylossesList.Add(1);
                        }
                        else
                        {
                            oppawaydrawsList.Add(1);
                        }
                    }

                    else
                    {
                    }
                }
            }
            else
            {

                for (int i = 0; i < teamstatmodel.DBResults.Count; i++)
                {
                    teamstatmodel.ResultsCount = teamstatmodel.DBResults.Count();
                    HomeTeam = conn.Results[i].ToString().Split("*")[5];
                    teamstatmodel.HomeTeam = conn.Results[i].ToString().Split("*")[5];
                    HomeScore = Int32.Parse(conn.Results[i].ToString().Split("*")[6]);
                    teamstatmodel.HomeScore = Int32.Parse(conn.Results[i].ToString().Split("*")[6]);
                    AwayTeam = conn.Results[i].ToString().Split("*")[7];
                    teamstatmodel.AwayTeam = conn.Results[i].ToString().Split("*")[7];
                    AwayScore = Int32.Parse(conn.Results[i].ToString().Split("*")[8]);
                    teamstatmodel.AwayScore = Int32.Parse(conn.Results[i].ToString().Split("*")[8]);


                    if (HomeTeam.Equals(teamstatmodel.TeamName))
                    {
                        homegamesList.Add(1);
                        homescoredList.Add(HomeScore);
                        homeconcededList.Add(AwayScore);
                        homemarginList.Add(HomeScore - AwayScore);

                        if (HomeScore > AwayScore)
                        {
                            homewinsList.Add(1);
                        }
                        else if (HomeScore < AwayScore)
                        {
                            homelossesList.Add(1);
                        }
                        else
                        {
                            homedrawsList.Add(1);
                        }
                    }

                    else if (HomeTeam.Equals(teamstatmodel.NextFixtureHomeTeam) || HomeTeam.Equals(teamstatmodel.NextFixtureAwayTeam))
                    {
                        opphomegamesList.Add(1);
                        opphomescoredList.Add(HomeScore);
                        opphomeconcededList.Add(AwayScore);
                        opphomemarginList.Add(HomeScore - AwayScore);

                        if (HomeScore > AwayScore)
                        {
                            opphomewinsList.Add(1);
                        }
                        else if (HomeScore < AwayScore)
                        {
                            opphomelossesList.Add(1);
                        }
                        else
                        {
                            opphomedrawsList.Add(1);
                        }
                    }
                    else if (AwayTeam.Equals(teamstatmodel.TeamName))
                    {
                        awaygamesList.Add(1);
                        awayscoredList.Add(AwayScore);
                        awayconcededList.Add(HomeScore);
                        awaymarginList.Add(AwayScore - HomeScore);
                        if (AwayScore > HomeScore)
                        {
                            awaywinsList.Add(1);
                        }
                        else if (HomeScore > AwayScore)
                        {
                            awaylossesList.Add(1);
                        }
                        else
                        {
                            awaydrawsList.Add(1);
                        }
                    }

                    else if (AwayTeam.Equals(teamstatmodel.NextFixtureHomeTeam) || AwayTeam.Equals(teamstatmodel.NextFixtureAwayTeam))
                    {
                        oppawaygamesList.Add(1);
                        oppawayscoredList.Add(AwayScore);
                        oppawayconcededList.Add(HomeScore);
                        oppawaymarginList.Add(AwayScore - HomeScore);
                        if (AwayScore > HomeScore)
                        {
                            oppawaywinsList.Add(1);
                        }
                        else if (HomeScore > AwayScore)
                        {
                            oppawaylossesList.Add(1);
                        }
                        else
                        {
                            oppawaydrawsList.Add(1);
                        }
                    }

                    else
                    {
                    }
                }
            }


            teamstatmodel.HomeWins = homewinsList.Count();
            teamstatmodel.HomeLosses = homelossesList.Count();
            teamstatmodel.HomeDraws = homedrawsList.Count();
            teamstatmodel.HomeScoredList = homescoredList;
            teamstatmodel.HomeScored = homescoredList.Sum();
            teamstatmodel.HomeMarginList = homemarginList;
            teamstatmodel.HomeConcededList = homeconcededList;
            teamstatmodel.HomeConceded = homeconcededList.Sum();
            teamstatmodel.AwayWins = awaywinsList.Count();
            teamstatmodel.AwayLosses = awaylossesList.Count();
            teamstatmodel.AwayDraws = awaydrawsList.Count();
            teamstatmodel.AwayScoredList = awayscoredList;
            teamstatmodel.AwayScored = awayscoredList.Sum();
            teamstatmodel.AwayMarginList = awaymarginList;
            teamstatmodel.AwayConcededList = awayconcededList;
            teamstatmodel.AwayConceded = awayconcededList.Sum();

            teamstatmodel.OppHomeWins = opphomewinsList.Count();
            teamstatmodel.OppHomeLosses = opphomelossesList.Count();
            teamstatmodel.OppHomeDraws = opphomedrawsList.Count();
            teamstatmodel.OppHomeScoredList = opphomescoredList;
            teamstatmodel.OppHomeScored = opphomescoredList.Sum();
            teamstatmodel.OppHomeMarginList = opphomemarginList;
            teamstatmodel.OppHomeConcededList = opphomeconcededList;
            teamstatmodel.OppHomeConceded = opphomeconcededList.Sum();
            teamstatmodel.OppAwayWins = oppawaywinsList.Count();
            teamstatmodel.OppAwayLosses = oppawaylossesList.Count();
            teamstatmodel.OppAwayDraws = oppawaydrawsList.Count();
            teamstatmodel.OppAwayScoredList = oppawayscoredList;
            teamstatmodel.OppAwayScored = oppawayscoredList.Sum();
            teamstatmodel.OppAwayMarginList = oppawaymarginList;
            teamstatmodel.OppAwayConcededList = oppawayconcededList;
            teamstatmodel.OppAwayConceded = oppawayconcededList.Sum();

            float totalhomeMargin = homemarginList.Sum();
            float totalhomeGames = homegamesList.Count();
            try { teamstatmodel.HomeMargin = totalhomeMargin / totalhomeGames; }
            catch { teamstatmodel.HomeMargin = 0; }

            try { teamstatmodel.HomeWinPercentage = ((homewinsList.Count() * 100) / homegamesList.Count()); }
            catch { teamstatmodel.HomeWinPercentage = 0; }


            teamstatmodel.AwayWins = awaywinsList.Count();
            float totalawayMargin = awaymarginList.Sum();
            float totalawayGames = awaygamesList.Count();
            try { teamstatmodel.AwayMargin = totalawayMargin / totalawayGames; }
            catch { teamstatmodel.AwayMargin = 0; }

            try { teamstatmodel.AwayWinPercentage = ((awaywinsList.Count() * 100) / awaygamesList.Count()); }
            catch { teamstatmodel.AwayWinPercentage = 0; }

            try { teamstatmodel.OverallWinPercentage = ((homewinsList.Count() + awaywinsList.Count()) * 100) / (homegamesList.Count + awaygamesList.Count()); }
            catch { teamstatmodel.OverallWinPercentage = 0; }


            return View(teamstatmodel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
