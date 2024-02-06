using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentViewForm : Form
    {
        private TournamentModel tournament;
        private BindingList<int> rounds = new BindingList<int>();
        private BindingList<MatchupModel> selectedMatchups = new BindingList<MatchupModel>();
        public TournamentViewForm(TournamentModel tournamentModel)
        {
            InitializeComponent();

            tournament = tournamentModel;

            WireUpLists();

            LoadFormData();
            LoadRounds();
        }

        private void LoadFormData()
        {
            tournamentName.Text = tournament.TournamentName;
        }

        private void WireUpLists()
        {
            roundDropDown.DataSource = rounds;

            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = "DisplayName";
        }

        private void LoadRounds()
        {
            rounds.Clear();

            rounds.Add(1);
            int currentRound = 1;

            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.Count > 0 && matchups.First().MatchupRound > currentRound)
                {
                    currentRound = matchups.First().MatchupRound;
                    rounds.Add(currentRound);
                }
            }

            LoadMatchups(1);
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void LoadMatchups(int round)
        {
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.Count > 0 && matchups.First().MatchupRound == round)
                {
                    selectedMatchups.Clear();
                    foreach (MatchupModel machup in matchups)
                    {
                        if (machup.Winner == null || !unplayedOnlyCheckbox.Checked)
                        {
                            selectedMatchups.Add(machup);

                        }
                    }
                }
            }

            if (selectedMatchups.Count > 0)
            {
                LoadMatchup(selectedMatchups.First());
            }

            DisplayMatchupInfo();
        }

        private void DisplayMatchupInfo()
        {
            bool isVisible = (selectedMatchups.Count > 0);

            teamOneName.Visible = isVisible;
            teamOneScoreLabel.Visible = isVisible;
            teamOneScoreValue.Visible = isVisible;

            teamTwoName.Visible = isVisible;
            teamTwoScoreLabel.Visible = isVisible;
            teamTwoScoreValue.Visible = isVisible;

            versusLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;
        }

        private void LoadMatchup(MatchupModel matchup)
        {
            if (matchup == null)
            {
                return;
            }

            for (int i = 0; i < matchup.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (matchup.Entries[0].TeamCompeting != null)
                    {
                        teamOneName.Text = matchup.Entries[0].TeamCompeting.TeamName;
                        teamOneScoreValue.Text = matchup.Entries[0].Score.ToString();

                        teamTwoName.Text = "<dummy>";
                        teamTwoScoreValue.Text = "0";
                    }
                    else
                    {
                        teamOneName.Text = "Not Yet Set";
                        teamOneScoreValue.Text = "";
                    }

                }

                if (i == 1)
                {
                    if (matchup.Entries[1].TeamCompeting != null)
                    {
                        teamTwoName.Text = matchup.Entries[1].TeamCompeting.TeamName;
                        teamTwoScoreValue.Text = matchup.Entries[1].Score.ToString();
                    }
                    else
                    {
                        teamTwoName.Text = "Not Yet Set";
                        teamTwoScoreValue.Text = "";
                    }

                }
            }
        }

        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchup((MatchupModel)matchupListBox.SelectedItem);
        }

        private void unplayedOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            MatchupModel matchup = (MatchupModel)matchupListBox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;

            if (matchup == null)
            {
                return;
            }

            for (int i = 0; i < matchup.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (matchup.Entries[0].TeamCompeting != null)
                    {
                        if (double.TryParse(teamOneScoreValue.Text, out teamOneScore))
                        {
                            matchup.Entries[0].Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score for team #1");
                            return;
                        }
                    }

                }

                if (i == 1)
                {
                    if (matchup.Entries[1].TeamCompeting != null)
                    {
                        if (double.TryParse(teamTwoScoreValue.Text, out teamTwoScore))
                        {
                            matchup.Entries[1].Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score for team #2");
                            return;
                        }
                    }

                }
            }

            if (teamOneScore > teamTwoScore)
            {
                // team one wins
                matchup.Winner = matchup.Entries[0].TeamCompeting;
            }
            else if (teamTwoScore > teamOneScore)
            {
                // team two wins
                matchup.Winner = matchup.Entries[1].TeamCompeting;
            }
            else
            {
                MessageBox.Show("I do not handle tie games");
                return;
            }

            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                foreach (MatchupModel mm in round)
                {
                    foreach (MatchupEntryModel me in mm.Entries)
                    {
                        if (me.ParentMatchup?.Id == matchup.Id)
                        {
                            me.TeamCompeting = matchup.Winner;
                            GlobalConfig.Connection.UpdateMatchup(mm);
                        }
                    }
                }
            }

            LoadMatchups((int)roundDropDown.SelectedItem);

            GlobalConfig.Connection.UpdateMatchup(matchup);
        }
    }
}
