using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        // Order our teams list randomly
        // Check if its lenghth is even and add dummy if needed
        // Create the first round of matchups
        // Create every round after that, ex: 8 -> 4 -> 2 -> 1

        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomizedTeams = RandomizeTeamsOrder(model.EnteredTeams);
            int rounds = FindNumberOfRounds(randomizedTeams.Count);
            int dummies = NumberOfDummies(rounds, randomizedTeams.Count);

            model.Rounds.Add(CreateFirstRound(dummies, randomizedTeams));

            CreateOtherRounds(model, rounds);
        }

        private static void CreateOtherRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel currentMatchup = new MatchupModel();

            while (round <= rounds)
            {
                foreach (MatchupModel match in previousRound)
                {
                    currentMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = match });

                    if (currentMatchup.Entries.Count > 1)
                    {
                        currentMatchup.MatchupRound = round;
                        currentRound.Add(currentMatchup);
                        currentMatchup = new MatchupModel();
                    }
                }

                model.Rounds.Add(currentRound);
                previousRound = currentRound;
                currentRound = new List<MatchupModel>();
                round++;
            }
        }

        private static List<MatchupModel> CreateFirstRound(int dummies, List<TeamModel> teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel current = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                current.Entries.Add(new MatchupEntryModel { TeamCompeting = team });
                if (dummies > 0 || current.Entries.Count > 1)
                {
                    current.MatchupRound = 1;
                    output.Add(current);
                    current = new MatchupModel();

                    if (dummies > 0)
                    {
                        dummies--;
                    }
                }
            }
            return output;
        }

        private static int NumberOfDummies(int rounds, int teamsCount)
        {
            int totalTeams = 1;

            for (int i = 1; i <= rounds; i++)
            {
                totalTeams *= 2;
            }

            return totalTeams - teamsCount;
        }

        private static int FindNumberOfRounds(int teamsCount)
        {
            int output = 1;
            int val = 2;

            while (val < teamsCount)
            {
                output++;
                val *= 2;
            }

            return output;
        }

        private static List<TeamModel> RandomizeTeamsOrder(List<TeamModel> teams)
        {
            return teams.OrderBy(t => Guid.NewGuid()).ToList();
        }
    }
}
