using ScorePredictor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScorePredictor.Utils
{
    public class Connection
    {
        public IList<string> Results { get; set; }
        public IList<string> Teams { get; set; }

        public IList<string> Scores()
        {

            string connectionString = @"Data Source=olliegross.database.windows.net; Initial Catalog=SportsResults; User id=ollie.gross@uwe.ac.uk@olliegross; Password=Merlin&Bryn86;";

            using (var connection = new SqlConnection(connectionString))

            using (var command = connection.CreateCommand())

            {
                connection.Open();

                //command.CommandType = CommandType.StoredProcedure;

                command.CommandText = "SELECT * FROM[dbo].[Results]";

                //command.Parameters.AddWithValue("intFrIndex", 0);

                using (var reader = command.ExecuteReader())

                {
                    List<string> results = new List<string>();
                    var scores = new Scores();
                    {
                        while (reader.Read())
                        {
                            scores.Id = Int32.Parse(reader.GetValue(0).ToString());
                            scores.LeagueId = Int32.Parse(reader.GetValue(1).ToString());
                            scores.SeasonId = Int32.Parse(reader.GetValue(2).ToString());
                            scores.Date = DateTime.Parse(reader.GetValue(3).ToString());
                            scores.RoundId = Int32.Parse(reader.GetValue(4).ToString());
                            scores.HomeTeam = reader.GetValue(5).ToString();
                            scores.HomeScore = Int32.Parse(reader.GetValue(6).ToString());
                            scores.AwayTeam = reader.GetValue(7).ToString();
                            scores.AwayScore = Int32.Parse(reader.GetValue(8).ToString());


                            results.Add(scores.Id + "*" + scores.LeagueId + "*" + scores.SeasonId + "*" + scores.Date + "*" + scores.RoundId + "*" + scores.HomeTeam + "*" + scores.HomeScore + "*" + scores.AwayTeam + "*" + scores.AwayScore);

                        }
                    }

                    Results = results;
                }

            }

            return Results;
        }

        public IList<string> SixNationsTeams()
        {

            string connectionString = @"Data Source=olliegross.database.windows.net; Initial Catalog=SportsResults; User id=ollie.gross@uwe.ac.uk@olliegross; Password=Merlin&Bryn86;";

            using (var connection = new SqlConnection(connectionString))

            using (var command = connection.CreateCommand())

            {
                connection.Open();

                //command.CommandType = CommandType.StoredProcedure;

                command.CommandText = "SELECT * FROM[dbo].[SixNationsTeams]";

                //command.Parameters.AddWithValue("intFrIndex", 0);

                using (var reader = command.ExecuteReader())

                {
                    var teams = new TeamStatModel();
                    List<string> sixnationsteams = new List<string>();
                    {
                        while (reader.Read())
                        {
                            teams.TeamId = reader.GetValue(0).ToString();
                            teams.TeamName = reader.GetValue(1).ToString();
                            teams.TeamLogo = reader.GetValue(2).ToString();

                            sixnationsteams.Add(teams.TeamId + "*" + teams.TeamName + "*" + teams.TeamLogo);
                        }

                        Teams = sixnationsteams;
                    }

                }

            }

            return Teams;
        }

    }
}
