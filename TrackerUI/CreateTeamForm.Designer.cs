namespace TrackerUI
{
    partial class CreateTeamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTeamForm));
            headerLabel = new Label();
            teamNameValue = new TextBox();
            teamNameLabel = new Label();
            addMemberButton = new Button();
            selectTeamMemberDropDown = new ComboBox();
            selectTeamMemberLabel = new Label();
            addNewMemberGroupBox = new GroupBox();
            cellphoneValue = new TextBox();
            createMemberButton = new Button();
            emailValue = new TextBox();
            lastNameValue = new TextBox();
            cellphoneLabel = new Label();
            emailLabel = new Label();
            lastNameLabel = new Label();
            firstNameValue = new TextBox();
            firstNameLabel = new Label();
            teamMembersListBox = new ListBox();
            removeSelectedMemberButton = new Button();
            createTeamButton = new Button();
            addNewMemberGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI Semilight", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            headerLabel.ForeColor = Color.IndianRed;
            headerLabel.Location = new Point(12, 9);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(221, 50);
            headerLabel.TabIndex = 2;
            headerLabel.Text = "Create Team";
            // 
            // teamNameValue
            // 
            teamNameValue.Location = new Point(31, 110);
            teamNameValue.Name = "teamNameValue";
            teamNameValue.Size = new Size(424, 35);
            teamNameValue.TabIndex = 12;
            // 
            // teamNameLabel
            // 
            teamNameLabel.AutoSize = true;
            teamNameLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            teamNameLabel.ForeColor = Color.IndianRed;
            teamNameLabel.Location = new Point(22, 70);
            teamNameLabel.Name = "teamNameLabel";
            teamNameLabel.Size = new Size(163, 37);
            teamNameLabel.TabIndex = 11;
            teamNameLabel.Text = "Team Name:";
            // 
            // addMemberButton
            // 
            addMemberButton.FlatAppearance.BorderColor = Color.Silver;
            addMemberButton.FlatAppearance.MouseDownBackColor = Color.DimGray;
            addMemberButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            addMemberButton.FlatStyle = FlatStyle.Flat;
            addMemberButton.ForeColor = Color.IndianRed;
            addMemberButton.Location = new Point(130, 252);
            addMemberButton.Name = "addMemberButton";
            addMemberButton.Size = new Size(209, 41);
            addMemberButton.TabIndex = 19;
            addMemberButton.Text = "Add Member";
            addMemberButton.UseVisualStyleBackColor = true;
            addMemberButton.Click += addMemberButton_Click;
            // 
            // selectTeamMemberDropDown
            // 
            selectTeamMemberDropDown.FormattingEnabled = true;
            selectTeamMemberDropDown.Location = new Point(31, 199);
            selectTeamMemberDropDown.Name = "selectTeamMemberDropDown";
            selectTeamMemberDropDown.Size = new Size(424, 38);
            selectTeamMemberDropDown.TabIndex = 18;
            // 
            // selectTeamMemberLabel
            // 
            selectTeamMemberLabel.AutoSize = true;
            selectTeamMemberLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            selectTeamMemberLabel.ForeColor = Color.IndianRed;
            selectTeamMemberLabel.Location = new Point(31, 159);
            selectTeamMemberLabel.Name = "selectTeamMemberLabel";
            selectTeamMemberLabel.Size = new Size(263, 37);
            selectTeamMemberLabel.TabIndex = 17;
            selectTeamMemberLabel.Text = "Select Team Member";
            // 
            // addNewMemberGroupBox
            // 
            addNewMemberGroupBox.Controls.Add(cellphoneValue);
            addNewMemberGroupBox.Controls.Add(createMemberButton);
            addNewMemberGroupBox.Controls.Add(emailValue);
            addNewMemberGroupBox.Controls.Add(lastNameValue);
            addNewMemberGroupBox.Controls.Add(cellphoneLabel);
            addNewMemberGroupBox.Controls.Add(emailLabel);
            addNewMemberGroupBox.Controls.Add(lastNameLabel);
            addNewMemberGroupBox.Controls.Add(firstNameValue);
            addNewMemberGroupBox.Controls.Add(firstNameLabel);
            addNewMemberGroupBox.ForeColor = Color.IndianRed;
            addNewMemberGroupBox.Location = new Point(31, 319);
            addNewMemberGroupBox.Name = "addNewMemberGroupBox";
            addNewMemberGroupBox.Size = new Size(452, 303);
            addNewMemberGroupBox.TabIndex = 20;
            addNewMemberGroupBox.TabStop = false;
            addNewMemberGroupBox.Text = "Add New Member";
            // 
            // cellphoneValue
            // 
            cellphoneValue.Location = new Point(163, 190);
            cellphoneValue.Name = "cellphoneValue";
            cellphoneValue.Size = new Size(261, 35);
            cellphoneValue.TabIndex = 4;
            // 
            // createMemberButton
            // 
            createMemberButton.FlatAppearance.BorderColor = Color.Silver;
            createMemberButton.FlatAppearance.MouseDownBackColor = Color.DimGray;
            createMemberButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            createMemberButton.FlatStyle = FlatStyle.Flat;
            createMemberButton.ForeColor = Color.IndianRed;
            createMemberButton.Location = new Point(99, 242);
            createMemberButton.Name = "createMemberButton";
            createMemberButton.Size = new Size(209, 41);
            createMemberButton.TabIndex = 5;
            createMemberButton.Text = "Create Member";
            createMemberButton.UseVisualStyleBackColor = true;
            createMemberButton.Click += createMemberButton_Click;
            // 
            // emailValue
            // 
            emailValue.Location = new Point(163, 141);
            emailValue.Name = "emailValue";
            emailValue.Size = new Size(261, 35);
            emailValue.TabIndex = 3;
            // 
            // lastNameValue
            // 
            lastNameValue.Location = new Point(163, 92);
            lastNameValue.Name = "lastNameValue";
            lastNameValue.Size = new Size(261, 35);
            lastNameValue.TabIndex = 2;
            // 
            // cellphoneLabel
            // 
            cellphoneLabel.AutoSize = true;
            cellphoneLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cellphoneLabel.ForeColor = Color.IndianRed;
            cellphoneLabel.Location = new Point(8, 190);
            cellphoneLabel.Name = "cellphoneLabel";
            cellphoneLabel.Size = new Size(144, 37);
            cellphoneLabel.TabIndex = 11;
            cellphoneLabel.Text = "Cellphone:";
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            emailLabel.ForeColor = Color.IndianRed;
            emailLabel.Location = new Point(8, 141);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(88, 37);
            emailLabel.TabIndex = 11;
            emailLabel.Text = "Email:";
            // 
            // lastNameLabel
            // 
            lastNameLabel.AutoSize = true;
            lastNameLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lastNameLabel.ForeColor = Color.IndianRed;
            lastNameLabel.Location = new Point(6, 92);
            lastNameLabel.Name = "lastNameLabel";
            lastNameLabel.Size = new Size(148, 37);
            lastNameLabel.TabIndex = 11;
            lastNameLabel.Text = "Last Name:";
            // 
            // firstNameValue
            // 
            firstNameValue.Location = new Point(163, 43);
            firstNameValue.Name = "firstNameValue";
            firstNameValue.Size = new Size(261, 35);
            firstNameValue.TabIndex = 1;
            // 
            // firstNameLabel
            // 
            firstNameLabel.AutoSize = true;
            firstNameLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            firstNameLabel.ForeColor = Color.IndianRed;
            firstNameLabel.Location = new Point(6, 43);
            firstNameLabel.Name = "firstNameLabel";
            firstNameLabel.Size = new Size(150, 37);
            firstNameLabel.TabIndex = 9;
            firstNameLabel.Text = "First Name:";
            // 
            // teamMembersListBox
            // 
            teamMembersListBox.BorderStyle = BorderStyle.FixedSingle;
            teamMembersListBox.FormattingEnabled = true;
            teamMembersListBox.ItemHeight = 30;
            teamMembersListBox.Location = new Point(545, 110);
            teamMembersListBox.Name = "teamMembersListBox";
            teamMembersListBox.Size = new Size(326, 512);
            teamMembersListBox.TabIndex = 21;
            // 
            // removeSelectedMemberButton
            // 
            removeSelectedMemberButton.FlatAppearance.BorderColor = Color.Silver;
            removeSelectedMemberButton.FlatAppearance.MouseDownBackColor = Color.DimGray;
            removeSelectedMemberButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            removeSelectedMemberButton.FlatStyle = FlatStyle.Flat;
            removeSelectedMemberButton.ForeColor = Color.IndianRed;
            removeSelectedMemberButton.Location = new Point(886, 319);
            removeSelectedMemberButton.Name = "removeSelectedMemberButton";
            removeSelectedMemberButton.Size = new Size(133, 78);
            removeSelectedMemberButton.TabIndex = 22;
            removeSelectedMemberButton.Text = "Remove Selected";
            removeSelectedMemberButton.UseVisualStyleBackColor = true;
            removeSelectedMemberButton.Click += removeSelectedMemberButton_Click;
            // 
            // createTeamButton
            // 
            createTeamButton.FlatAppearance.BorderColor = Color.Silver;
            createTeamButton.FlatAppearance.MouseDownBackColor = Color.DimGray;
            createTeamButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            createTeamButton.FlatStyle = FlatStyle.Flat;
            createTeamButton.ForeColor = Color.IndianRed;
            createTeamButton.Location = new Point(422, 638);
            createTeamButton.Name = "createTeamButton";
            createTeamButton.Size = new Size(209, 52);
            createTeamButton.TabIndex = 25;
            createTeamButton.Text = "Create Team";
            createTeamButton.UseVisualStyleBackColor = true;
            createTeamButton.Click += createTeamButton_Click;
            // 
            // CreateTeamForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1030, 702);
            Controls.Add(createTeamButton);
            Controls.Add(removeSelectedMemberButton);
            Controls.Add(teamMembersListBox);
            Controls.Add(addNewMemberGroupBox);
            Controls.Add(addMemberButton);
            Controls.Add(selectTeamMemberDropDown);
            Controls.Add(selectTeamMemberLabel);
            Controls.Add(teamNameValue);
            Controls.Add(teamNameLabel);
            Controls.Add(headerLabel);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 6, 5, 6);
            Name = "CreateTeamForm";
            Text = "Create Team";
            addNewMemberGroupBox.ResumeLayout(false);
            addNewMemberGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label headerLabel;
        private TextBox teamNameValue;
        private Label teamNameLabel;
        private Button addMemberButton;
        private ComboBox selectTeamMemberDropDown;
        private Label selectTeamMemberLabel;
        private GroupBox addNewMemberGroupBox;
        private TextBox firstNameValue;
        private Label firstNameLabel;
        private TextBox emailValue;
        private TextBox lastNameValue;
        private Label emailLabel;
        private Label lastNameLabel;
        private TextBox cellphoneValue;
        private Button createMemberButton;
        private Label cellphoneLabel;
        private ListBox teamMembersListBox;
        private Button removeSelectedMemberButton;
        private Button createTeamButton;
    }
}