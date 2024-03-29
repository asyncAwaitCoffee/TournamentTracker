﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
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

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines)
        {
            //id,team name,list of ids separated by |
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> persons = GlobalConfig.PersonsFile.FullFilePath().LoadFile().ConvertToPersonModels();

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
            this List<string> lines)
        {
            //id,TournamentName,EntryFee,(id|id|id - entered teams),(id|id|id - prizes),(id^id^id|id^id^id - rounds)
            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels();
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();
            List<MatchupModel> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().ConvertToMatchupModels();

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

                if (cols[4].Length > 0)
                {
                    string[] prizeIds = cols[4].Split('|');
                    foreach (string id in teamIds)
                    {
                        tournament.Prizes.Add(prizes.Where(p => int.Parse(id) == p.Id).First());
                    } 
                }

                // capture rounds info
                string[] rounds = cols[5].Split('|');

                foreach (string r in rounds)
                {
                    string[] matchupText = r.Split('^'); // matchups from text file
                    List<MatchupModel> matchupModel = new List<MatchupModel>();

                    foreach (string matchupModelTextId in matchupText)
                    {
                        if (int.TryParse(matchupModelTextId, out int parsedId))
                        {
                            List<MatchupModel> found = matchups.Where(m => m.Id == parsedId).ToList();
                            if (found.Count > 0)
                            {
                                matchupModel.Add(found.First());
                            }
                        }                        
                    }

                    tournament.Rounds.Add(matchupModel);
                }

                output.Add(tournament);

            }

            return output;
        }

        public static void SaveToPrizesFile(this List<PrizeModel> models)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel m in models)
            {
                lines.Add($"{ m.Id },{ m.PlaceNumber },{m.PlaceName },{m.PrizeAmout},{m.PrizePercentage}");
            }
            
            File.WriteAllLines(GlobalConfig.PrizesFile.FullFilePath(), lines);
        }

        public static void SaveToPersonsFile(this List<PersonModel> models)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel m in models)
            {
                lines.Add($"{ m.Id },{ m.FirstName },{ m.LastName },{ m.EmailAdress },{ m.CellphoneNumber }");
            }

            File.WriteAllLines(GlobalConfig.PersonsFile.FullFilePath(), lines);
        }

        public static void SaveToTeamsFile(this List<TeamModel> models)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel t in models)
            {
                lines.Add($"{t.Id},{t.TeamName},{ConvertPersonsListRoString(t.TeamMembers)}");
            }

            File.WriteAllLines(GlobalConfig.TeamsFile.FullFilePath(), lines);
        }

        public static void SaveRoundsToFile(this TournamentModel model)
        {
            // loop through each round
            // loop through each machup
            // get the id for the new matchup and save record
            // loop through each entry, get the id, and save it

            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    // load all of the matchups from file
                    // get the top id
                    // store the id
                    // save the matchup record
                    matchup.SaveMatchupToFile();                    
                }
                
            }
        }
        public static List<MatchupEntryModel> ConvertToMatchupEntryModels(this List<string> lines)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupEntryModel model = new MatchupEntryModel();
                model.Id = int.Parse(cols[0]);
                if (cols[1].Length == 0 )
                {
                    model.TeamCompeting = null;
                }
                else
                {
                    model.TeamCompeting = LookupTeamById(int.Parse(cols[1]));
                }                
                model.Score = double.Parse(cols[2]);
                if (int.TryParse(cols[3], out int parentId))
                {
                    model.ParentMatchup = LookupMatchupById(parentId);
                }
                else
                {
                    model.ParentMatchup = null;
                }

                output.Add(model);
                
            }

            return output;
        }
        private static List<MatchupEntryModel> ConvertStringToMatchupEntryModels(string input)
        {
            string[] ids = input.Split('|');
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();
            List<string> entries = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile();
            List<string> matchingEntries = new List<string>();

            foreach (string id in ids)
            {
                foreach (string entry in entries)
                {
                    string[] cols = entry.Split(',');

                    if (cols[0] == id)
                    {
                        matchingEntries.Add(entry);
                    }
                }
            }

            output = matchingEntries.ConvertToMatchupEntryModels();

            return output;
        }
        private static TeamModel LookupTeamById(int id)
        {
            List<string> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile();

            foreach (string team in teams)
            {
                string[] cols = team.Split(',');

                if (cols[0] == id.ToString())
                {
                    List<string> matchingTeams = new List<string>();
                    matchingTeams.Add(team);

                    return matchingTeams.ConvertToTeamModels().First();
                }
            }

            return null;
        }
        private static MatchupModel LookupMatchupById(int id)
        {
            List<string> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile();

            foreach (string matchup in matchups)
            {
                string[] cols = matchup.Split(',');
                if (cols[0] == id.ToString())
                {
                    List<string> matchingMatchups = new List<string>();
                    matchingMatchups.Add(matchup);

                    return matchingMatchups.ConvertToMatchupModels().First();
                }
            }

            return null;
        }
        public static List<MatchupModel> ConvertToMatchupModels(this List<string> lines)
        {
            // id=0,entries=1(pipe delimited by id),winner=2,matchupRounds=3
            List<MatchupModel> output = new List<MatchupModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupModel p = new MatchupModel();
                p.Id = int.Parse(cols[0]);
                p.Entries = ConvertStringToMatchupEntryModels(cols[1]);
                if (cols[2].Length == 0)
                {
                    p.Winner = null;
                }
                else
                {
                    p.Winner = LookupTeamById(int.Parse(cols[2]));
                }
                
                p.MatchupRound = int.Parse(cols[3]);

                output.Add(p);
            }

            return output;
        }
        public static void SaveMatchupToFile(this MatchupModel matchup)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupsFile
                .FullFilePath()
                .LoadFile()
                .ConvertToMatchupModels();

            int currentId = 1;
            if (matchups.Count > 0)
            {
                currentId = matchups.OrderByDescending(x => x.Id).First().Id + 1;
            }
            matchup.Id = currentId;

            matchups.Add(matchup);

            foreach (MatchupEntryModel entry in matchup.Entries)
            {
                entry.SaveEntryToFile();
            }

            // save to file
            List<string> lines = new List<string>();
            foreach (MatchupModel m in matchups)
            {

                lines.Add($"{ m.Id },{ ConvertMatchupEntriesListToString(m.Entries) },{ m.Winner?.Id },{ m.MatchupRound }");
            }

            File.WriteAllLines(GlobalConfig.MatchupsFile.FullFilePath(), lines);
        }

        public static void UpdateMatchupFile(this MatchupModel matchup)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupsFile
                .FullFilePath()
                .LoadFile()
                .ConvertToMatchupModels();

            MatchupModel oldMatchup = new MatchupModel();

            foreach (MatchupModel model in matchups)
            {
                if (model.Id == matchup.Id)
                {
                    oldMatchup = model;
                }
            }

            matchups.Remove(oldMatchup);
            matchups.Add(matchup);

            foreach (MatchupEntryModel entry in matchup.Entries)
            {
                entry.UpdateEntryFile();
            }

            // save to file
            List<string> lines = new List<string>();
            foreach (MatchupModel m in matchups)
            {

                lines.Add($"{m.Id},{ConvertMatchupEntriesListToString(m.Entries)},{m.Winner?.Id},{m.MatchupRound}");
            }

            File.WriteAllLines(GlobalConfig.MatchupsFile.FullFilePath(), lines);
        }
        public static void SaveEntryToFile(this MatchupEntryModel entry)
        {
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntriesFile
                .FullFilePath()
                .LoadFile()
                .ConvertToMatchupEntryModels();

            int currentId = 1;
            if (entries.Count > 0)
            {
                currentId = entries.OrderByDescending(e => e.Id).First().Id + 1;
            }

            entry.Id = currentId;
            entries.Add(entry);

            List<string> lines = new List<string>();

            foreach (MatchupEntryModel e in entries)
            {

                lines.Add($"{ e.Id },{e.TeamCompeting?.Id },{ e.Score },{ e.ParentMatchup?.Id }");
            }

            File.WriteAllLines(GlobalConfig.MatchupEntriesFile.FullFilePath(), lines);
        }
        public static void UpdateEntryFile(this MatchupEntryModel entry)
        {
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntriesFile
                .FullFilePath()
                .LoadFile()
                .ConvertToMatchupEntryModels();

            MatchupEntryModel oldEntry = new MatchupEntryModel();

            foreach (MatchupEntryModel e in entries)
            {
               if (e.Id == entry.Id)
                {
                    oldEntry = e;
                }
            }

            entries.Remove(oldEntry);
            entries.Add(entry);

            List<string> lines = new List<string>();

            foreach (MatchupEntryModel e in entries)
            {

                lines.Add($"{e.Id},{e.TeamCompeting?.Id},{e.Score},{e.ParentMatchup?.Id}");
            }

            File.WriteAllLines(GlobalConfig.MatchupEntriesFile.FullFilePath(), lines);
        }
        public static void SaveToTournamentsFile(this List<TournamentModel> models)
        {
            List<string> lines = new List<string>();

            foreach (TournamentModel t in models)
            {
                lines.Add($"{ t.Id },{ t.TournamentName },{ t.EntryFee },{ ConvertTeamsListRoString(t.EnteredTeams) },{ ConvertPrizesListToString(t.Prizes) },{ ConvertRoundsListRoString(t.Rounds) }");
            }

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
        }

        //TODO - refactor Converts to single generic method

        private static string ConvertRoundsListRoString(List<List<MatchupModel>> rounds)
        {
            string output = "";

            foreach (List<MatchupModel> r in rounds)
            {
                output += $"{ ConvertMatchupsListToString(r) }|";
            }
            output = output.TrimEnd('|');

            return output;
        }

        private static string ConvertMatchupsListToString(List<MatchupModel> matchups)
        {
            string output = "";

            foreach (MatchupModel m in matchups)
            {
                output += $"{m.Id}^";
            }
            output = output.TrimEnd('^');

            return output;
        }
        private static string ConvertMatchupEntriesListToString(List<MatchupEntryModel> entries)
        {
            string output = "";

            foreach (MatchupEntryModel p in entries)
            {
                output += $"{p.Id}|";
            }
            output = output.TrimEnd('|');

            return output;
        }

        private static string ConvertPrizesListToString(List<PrizeModel> prizes)
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
