using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using System.Data.SqlClient;
using Dapper;
//@PLACE_NUMBER INT,
//@PLACE_NAME NVARCHAR(50),
//@PRIZE_AMOUNT MONEY,
//@PRIZE_PERCENTAGE FLOAT,
//@ID INT = 0 OUTPUT
namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "COURSES_DB";
        public PersonModel CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FIRST_NAME", model.FirstName);
                p.Add("@LAST_NAME", model.LastName);
                p.Add("@EMAIL_ADRESS", model.EmailAdress);
                p.Add("@CELLPHONE_NUMBER", model.CellphoneNumber);
                p.Add("@ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("TOURNAMENT_TRACKER.USP_ADD_PERSON", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@ID");

                return model;
            }
        }

        //TODO - implement CreatePrize method
        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information with id</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@PLACE_NUMBER", model.PlaceNumber);
                p.Add("@PLACE_NAME", model.PlaceName);
                p.Add("@PRIZE_AMOUNT", model.PrizeAmout);
                p.Add("@PRIZE_PERCENTAGE", model.PrizePercentage);
                p.Add("@ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("TOURNAMENT_TRACKER.USP_ADD_PRIZE", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@ID");

                return model;
            }
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@TEAM_NAME", model.TeamName);
                p.Add("@ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("TOURNAMENT_TRACKER.USP_ADD_TEAM", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@ID");

                foreach (PersonModel tm in model.TeamMembers)
                {
                    p = new DynamicParameters();
                    p.Add("@TEAM_ID", model.Id);
                    p.Add("@PERSON_ID", tm.Id);

                    connection.Execute("TOURNAMENT_TRACKER.USP_ADD_TEAM_MEMBER", p, commandType: CommandType.StoredProcedure);
                }

                return model;
            }
        }

        public void CreateTournament(TournamentModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                SaveTournament(connection, model);
                SaveTournamentPrizes(connection, model);
                SaveTournamentEntries(connection, model);
                SaveTournamentRounds(connection, model);
            }
        }

        private void SaveTournament(IDbConnection connection, TournamentModel model)
        {
            var p = new DynamicParameters();
            p.Add("@TOURNAMENT_NAME", model.TournamentName);
            p.Add("@ENTRY_FEE", model.EntryFee);
            p.Add("@ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            connection.Execute("TOURNAMENT_TRACKER.USP_ADD_TOURNAMENT", p, commandType: CommandType.StoredProcedure);

            model.Id = p.Get<int>("@ID");
        }

        private void SaveTournamentPrizes(IDbConnection connection, TournamentModel model)
        {
            foreach (PrizeModel pz in model.Prizes)
            {
                var p = new DynamicParameters();
                p.Add("@TOURNAMENT_ID", model.Id);
                p.Add("@PRIZE_ID", pz.Id);
                p.Add("@ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("TOURNAMENT_TRACKER.USP_ADD_TOURNAMENT_PRIZE", p, commandType: CommandType.StoredProcedure);
            }
        }
        private void SaveTournamentEntries(IDbConnection connection, TournamentModel model)
        {
            foreach (TeamModel tm in model.EnteredTeams)
            {
                var p = new DynamicParameters();
                p.Add("@TOURNAMENT_ID", model.Id);
                p.Add("@TEAM_ID", tm.Id);
                p.Add("@ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("TOURNAMENT_TRACKER.USP_ADD_TOURNAMENT_ENTRY", p, commandType: CommandType.StoredProcedure);
            }
        }
        private void SaveTournamentRounds(IDbConnection connection, TournamentModel model)
        {
            // loop throug the rounds
            // loop throug the matchups
            // save the matchup
            // loop through the entries and save them

            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    var p = new DynamicParameters();
                    p.Add("@TOURNAMENT_ID", model.Id);
                    p.Add("@MATCHUP_ROUND", matchup.MatchupRound);
                    p.Add("@ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute("TOURNAMENT_TRACKER.USP_ADD_MATCHUP", p, commandType: CommandType.StoredProcedure);

                    matchup.Id = p.Get<int>("@ID");

                    foreach (MatchupEntryModel entry in matchup.Entries)
                    {
                        p = new DynamicParameters();
                        p.Add("@MATCHUP_ID", matchup.Id);
                        p.Add("@PARENT_MATCHUP_ID", entry.ParentMatchup?.Id);                        
                        p.Add("@TEAM_COMPETING_ID", entry.TeamCompeting?.Id);
                        p.Add("@ID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("TOURNAMENT_TRACKER.USP_ADD_MATCHUP_ENTRY", p, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }
        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;

            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<PersonModel>("TOURNAMENT_TRACKER.USP_GET_PERSON_ALL").ToList();
            }

            return output;
        }

        public List<TeamModel> GetTeam_All()
        {
            List<TeamModel> output;

            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<TeamModel>("TOURNAMENT_TRACKER.USP_GET_TEAM_ALL").ToList();

                foreach (TeamModel team in output)
                {
                    var p = new DynamicParameters();
                    p.Add("@TEAM_ID", team.Id);
                    team.TeamMembers = connection.Query<PersonModel>("TOURNAMENT_TRACKER.USP_GET_TEAM_MEMBERS_BY_TEAM", p, commandType: CommandType.StoredProcedure).ToList();
                }

            }

            return output;
        }

        public List<TournamentModel> GetTournament_All()
        {
            List<TournamentModel> output;

            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<TournamentModel>("TOURNAMENT_TRACKER.USP_GET_TOURNAMENT_ALL").ToList();

                foreach (TournamentModel tournament in output)
                {
                    var p = new DynamicParameters();
                    p.Add("@TOURNAMENT_ID", tournament.Id);

                    // populate prizes
                    tournament.Prizes = connection.Query<PrizeModel>("TOURNAMENT_TRACKER.USP_GET_PRIZES_BY_TOURNAMENT", p, commandType: CommandType.StoredProcedure).ToList();
                    // populate teams
                    tournament.EnteredTeams = connection.Query<TeamModel>("TOURNAMENT_TRACKER.USP_GET_TEAMS_BY_TOURNAMENT", p, commandType: CommandType.StoredProcedure).ToList();
                    // get all matchups for this tournament
                    List<MatchupModel> matchups = connection.Query<MatchupModel>("TOURNAMENT_TRACKER.USP_GET_MATCHUPS_BY_TOURNAMENT", p, commandType: CommandType.StoredProcedure).ToList();

                    foreach (TeamModel team in tournament.EnteredTeams)
                    {
                        p = new DynamicParameters();
                        p.Add("@TEAM_ID", team.Id);
                        team.TeamMembers = connection.Query<PersonModel>("TOURNAMENT_TRACKER.USP_GET_TEAM_MEMBERS_BY_TEAM", p, commandType: CommandType.StoredProcedure).ToList();
                    }

                    // populate rounds

                    foreach (MatchupModel matchup in matchups)
                    {
                        p = new DynamicParameters();
                        p.Add("@MATCHUP_ID", matchup.Id);

                        matchup.Entries = connection.Query<MatchupEntryModel>("TOURNAMENT_TRACKER.USP_GET_MATCHUP_ENTRIES_BY_MATCHUP", p, commandType: CommandType.StoredProcedure).ToList();

                        // populate each matchup entry
                        // populate each matchup

                        List<TeamModel> allTeams = GetTeam_All();

                        if (matchup.WinnerId > 0)
                        {
                            matchup.Winner = allTeams.Where(t => t.Id == matchup.WinnerId).First();
                        }

                        foreach (MatchupEntryModel entry in matchup.Entries)
                        {
                            if (entry.TeamCompetingId > 0)
                            {
                                entry.TeamCompeting = allTeams.Where(t => t.Id == entry.TeamCompetingId).First();
                            }

                            if (entry.ParentMatchupId > 0)
                            {
                                entry.ParentMatchup = matchups.Where(m => m.Id == entry.ParentMatchupId).First();
                            }
                        }
                    }

                    // TODO - same loop twice, refactor
                    List<MatchupModel> currentRow = new List<MatchupModel>();
                    int currentRound = 1;

                    foreach (MatchupModel matchup in matchups)
                    {
                        if (matchup.MatchupRound > currentRound)
                        {
                            tournament.Rounds.Add(currentRow);
                            currentRow = new List<MatchupModel>();
                            currentRound++;
                        }

                        currentRow.Add(matchup);
                    }

                    tournament.Rounds.Add(currentRow);

                }
            }
            return output;
        }

        public void UpdateMatchup(MatchupModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@ID", model.Id);
                p.Add("@WINNER_ID", model.Winner.Id);

                connection.Execute("TOURNAMENT_TRACKER.USP_UPDATE_MATCHUPS", p, commandType: CommandType.StoredProcedure);

                foreach (MatchupEntryModel entry in model.Entries)
                {
                    p = new DynamicParameters();
                    p.Add("@ID", entry.Id);
                    p.Add("@TEAM_COMPETING_ID", entry.TeamCompetingId);
                    p.Add("@SCORE", entry.Score);

                    connection.Execute("TOURNAMENT_TRACKER.USP_UPDATE_MATCHUP_ENTRIES", p, commandType: CommandType.StoredProcedure);
                }
            }
        }
    }
}
