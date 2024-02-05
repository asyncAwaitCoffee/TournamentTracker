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
    }
}
