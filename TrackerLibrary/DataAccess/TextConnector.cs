using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        // TODO - files constants moved to GlobalConfig
        private const string PrizesFile = "PrizeModels.csv";
        private const string PersonsFile = "PersonModels.csv";
        private const string TeamsFile = "TeamModels.csv";
        private const string TournamentsFile = "TournamentModels.csv";
        private const string MatchupsFile = "MatchupModels.csv";
        private const string MatchupEntriesFile = "MatchupEntryModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> persons = PersonsFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;
            if (persons.Count > 0)
            {
                currentId = persons.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            persons.Add(model);

            persons.SaveToPersonsFile(PersonsFile);

            return model;
        }

        //TODO - implement CreatePrize method
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // Load the text file and convert the text to List<PrizeModel>
            List<PrizeModel> prizes =  PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();
            // Get next Id
            int currentId = 1;
            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            // Add the new record with the new Id
            prizes.Add(model);

            // Convert the prizes to Lisr<string>
            // Save the List<string> ti the text file

            prizes.SaveToPrizesFile(PrizesFile);

            return model;

        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels(PersonsFile);
            
            int currentId = 1;
            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            teams.Add(model);

            teams.SaveToTeamsFile(TeamsFile);

            return model;
        }

        // TODO - refactor other methods to void
        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = TournamentsFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels(TeamsFile, PersonsFile, PrizesFile);

            int currentId = 1;
            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            model.SaveRoundsToFile(MatchupsFile, MatchupEntriesFile);

            tournaments.Add(model);

            tournaments.SaveToTournamentsFile(TournamentsFile);
        }

        public List<PersonModel> GetPerson_All()
        {
            return PersonsFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeam_All()
        {
            return TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels(PersonsFile);
        }

        public List<TournamentModel> GetTournament_All()
        {
            return TournamentsFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels(TeamsFile, PersonsFile, PrizesFile);
        }

        public void UpdateMatchup(MatchupModel model)
        {
            throw new NotImplementedException();
        }
    }
}
