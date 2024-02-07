using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availibleTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        private ITeamRequester callingForm;
        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            //CreateSampleData();

            WireUpLists();
        }
        private bool ValidateForm()
        {
            // TODO - return false immediately VS accumulate all "errors" and display information to user
            if (firstNameValue.Text.Length == 0)
            {
                return false;
            }

            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }

            if (emailValue.Text.Length == 0)
            {
                return false;
            }

            if (cellphoneValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates the sample data for testing purposes
        /// </summary>
        private void CreateSampleData()
        {
            availibleTeamMembers.Add(new PersonModel { FirstName = "Test", LastName = "Testovich" });
            availibleTeamMembers.Add(new PersonModel { FirstName = "Check", LastName = "Checkovich" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Wow", LastName = "Wowovich" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "What", LastName = "Whatovich" });
        }

        private void WireUpLists()
        {
            // TODO - rework refresh solution
            selectTeamMemberDropDown.DataSource = null;

            selectTeamMemberDropDown.DataSource = availibleTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            // TODO - rework refresh solution
            teamMembersListBox.DataSource = null;

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if (p == null)
            {
                return;
            }

            availibleTeamMembers.Remove(p);
            selectedTeamMembers.Add(p);

            WireUpLists();
            //selectTeamMemberDropDown.Refresh();
            //teamMembersListBox.Refresh();
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel person = new PersonModel();
                person.FirstName = firstNameValue.Text;
                person.LastName = lastNameValue.Text;
                person.EmailAdress = emailValue.Text;
                person.CellphoneNumber = cellphoneValue.Text;

                GlobalConfig.Connection.CreatePerson(person);

                selectedTeamMembers.Add(person);
                WireUpLists();

                firstNameValue.Text = "";

                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("You need to fill all of the fields!");
            }
        }


        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            if (p == null)
            {
                return;
            }

            selectedTeamMembers.Remove(p);
            availibleTeamMembers.Add(p);

            WireUpLists();
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel team = new TeamModel();

            team.TeamName = teamNameValue.Text;
            team.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(team);

            callingForm.TeamComplete(team);

            this.Close();
        }
    }
}
