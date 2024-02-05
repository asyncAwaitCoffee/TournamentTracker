using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProceesor
    {
        public static string FullFilePath(this string fileName)
        {
            return $"{ ConfigurationManager.AppSettings["filepath"] }\\{ fileName }";
        }

        public static List<string> LoadFile(this string file)
        {
            if(!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmout = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);

                output.Add(p);
            }

            return output;
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (var line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel p = new PersonModel();

                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAdress = cols[3];
                p.CellphoneNumber = cols[4];

                output.Add(p);
            }

            return output;
        }

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string personsFileName)
        {
            //id,team name,list of ids separated by |
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> persons = personsFileName.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel team = new TeamModel();
                team.Id = int.Parse(cols[0]);
                team.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    team.TeamMembers.Add(persons.Where(p => int.Parse(id) == p.Id).First());
                }

                output.Add(team);
            }

            return output;
        }

        public static List<TournamentModel> ConvertToTournamentModels(
            this List<string> lines,
            string teamsFileName,
            string personsFileName,
            string prizesFileName)
        {
            //id,TournamentName,EntryFee,(id|id|id - entered teams),(id|id|id - prizes),(id^id^id|id^id^id - rounds)
            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = teamsFileName.FullFilePath().LoadFile().ConvertToTeamModels(personsFileName);
            List<PrizeModel> prizes = prizesFileName.FullFilePath().LoadFile().ConvertToPrizeModels();

            foreach (var line in lines)
            {
                string[] cols = line.Split(',');

                TournamentModel tournament = new TournamentModel();
                tournament.Id = int.Parse(cols[0]);
                tournament.TournamentName = cols[1];
                tournament.EntryFee = decimal.Parse(cols[2]);

                string[] teamIds = cols[3].Split('|');

                foreach (string id in teamIds)
                {
                    tournament.EnteredTeams.Add(teams.Where(t => int.Parse(id) == t.Id).First());
                }

                string[] prizeIds = cols[4].Split('|');
                foreach (string id in teamIds)
                {
                    tournament.Prizes.Add(prizes.Where(p => int.Parse(id) == p.Id).First());
                }

                output.Add(tournament);

                // TODO - capture rounds info
            }

            return output;
        }

        public static void SaveToPrizesFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel m in models)
            {
                lines.Add($"{ m.Id },{ m.PlaceNumber },{m.PlaceName },{m.PrizeAmout},{m.PrizePercentage}");
            }
            
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPersonsFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel m in models)
            {
                lines.Add($"{ m.Id },{ m.FirstName },{ m.LastName },{ m.EmailAdress },{ m.CellphoneNumber }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamsFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel t in models)
            {
                lines.Add($"{t.Id},{t.TeamName},{ConvertPersonsListRoString(t.TeamMembers)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTournamentsFile(this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TournamentModel t in models)
            {
                lines.Add($@"
                    { t.Id },
                    { t.TournamentName },
                    { t.EntryFee },
                    { ConvertTeamsListRoString(t.EnteredTeams) },
                    { ConvertPrizesListRoString(t.Prizes) },
                    { ConvertRoundsListRoString(t.Rounds) }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //TODO - refactor Converts to single generic method

        private static string ConvertRoundsListRoString(List<List<MatchupModel>> rounds)
        {
            string output = "";

            foreach (List<MatchupModel> r in rounds)
            {
                output += $"{ ConvertMatchupsListRoString(r) }|";
            }
            output = output.TrimEnd('|');

            return output;
        }

        private static string ConvertMatchupsListRoString(List<MatchupModel> matchups)
        {
            string output = "";

            foreach (MatchupModel m in matchups)
            {
                output += $"{m.Id}^";
            }
            output = output.TrimEnd('^');

            return output;
        }

        private static string ConvertPrizesListRoString(List<PrizeModel> prizes)
        {
            string output = "";

            foreach (PrizeModel p in prizes)
            {
                output += $"{p.Id}|";
            }
            output = output.TrimEnd('|');

            return output;
        }

        private static string ConvertTeamsListRoString(List<TeamModel> teams)
        {
            string output = "";

            foreach (TeamModel t in teams)
            {
                output += $"{t.Id}|";
            }
            output = output.TrimEnd('|');

            return output;
        }

        private static string ConvertPersonsListRoString(List<PersonModel> persons)
        {
            string output = "";

            foreach (PersonModel p in persons)
            {
                output += $"{ p.Id }|";
            }
            output = output.TrimEnd('|');

            return output;
        }
    }
}
