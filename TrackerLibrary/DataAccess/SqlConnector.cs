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
        public PersonModel CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("COURSES_DB")))
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
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("COURSES_DB")))
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
    }
}
