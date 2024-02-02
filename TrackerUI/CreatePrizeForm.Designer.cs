namespace TrackerUI
{
    partial class CreatePrizeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePrizeForm));
            headerLabel = new Label();
            placeNumberValue = new TextBox();
            placeNumberLabel = new Label();
            prizePercentageValue = new TextBox();
            prizePercentageLabel = new Label();
            prizeAmountValue = new TextBox();
            prizeAmountLable = new Label();
            placeNameValue = new TextBox();
            placeNameLabel = new Label();
            orLabel = new Label();
            createPrizeButton = new Button();
            SuspendLayout();
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI Semilight", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            headerLabel.ForeColor = Color.IndianRed;
            headerLabel.Location = new Point(12, 9);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(214, 50);
            headerLabel.TabIndex = 3;
            headerLabel.Text = "Create Prize";
            // 
            // placeNumberValue
            // 
            placeNumberValue.Location = new Point(240, 95);
            placeNumberValue.Name = "placeNumberValue";
            placeNumberValue.Size = new Size(261, 35);
            placeNumberValue.TabIndex = 12;
            // 
            // placeNumberLabel
            // 
            placeNumberLabel.AutoSize = true;
            placeNumberLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            placeNumberLabel.ForeColor = Color.IndianRed;
            placeNumberLabel.Location = new Point(14, 92);
            placeNumberLabel.Name = "placeNumberLabel";
            placeNumberLabel.Size = new Size(183, 37);
            placeNumberLabel.TabIndex = 11;
            placeNumberLabel.Text = "Place Number";
            // 
            // prizePercentageValue
            // 
            prizePercentageValue.Location = new Point(240, 316);
            prizePercentageValue.Name = "prizePercentageValue";
            prizePercentageValue.Size = new Size(261, 35);
            prizePercentageValue.TabIndex = 14;
            prizePercentageValue.Text = "0";
            // 
            // prizePercentageLabel
            // 
            prizePercentageLabel.AutoSize = true;
            prizePercentageLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            prizePercentageLabel.ForeColor = Color.IndianRed;
            prizePercentageLabel.Location = new Point(14, 313);
            prizePercentageLabel.Name = "prizePercentageLabel";
            prizePercentageLabel.Size = new Size(212, 37);
            prizePercentageLabel.TabIndex = 13;
            prizePercentageLabel.Text = "Prize Percentage";
            // 
            // prizeAmountValue
            // 
            prizeAmountValue.Location = new Point(240, 199);
            prizeAmountValue.Name = "prizeAmountValue";
            prizeAmountValue.Size = new Size(261, 35);
            prizeAmountValue.TabIndex = 16;
            prizeAmountValue.Text = "0";
            // 
            // prizeAmountLable
            // 
            prizeAmountLable.AutoSize = true;
            prizeAmountLable.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            prizeAmountLable.ForeColor = Color.IndianRed;
            prizeAmountLable.Location = new Point(14, 196);
            prizeAmountLable.Name = "prizeAmountLable";
            prizeAmountLable.Size = new Size(176, 37);
            prizeAmountLable.TabIndex = 15;
            prizeAmountLable.Text = "Prize Amount";
            // 
            // placeNameValue
            // 
            placeNameValue.Location = new Point(240, 147);
            placeNameValue.Name = "placeNameValue";
            placeNameValue.Size = new Size(261, 35);
            placeNameValue.TabIndex = 18;
            // 
            // placeNameLabel
            // 
            placeNameLabel.AutoSize = true;
            placeNameLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            placeNameLabel.ForeColor = Color.IndianRed;
            placeNameLabel.Location = new Point(14, 144);
            placeNameLabel.Name = "placeNameLabel";
            placeNameLabel.Size = new Size(157, 37);
            placeNameLabel.TabIndex = 17;
            placeNameLabel.Text = "Place Name";
            // 
            // orLabel
            // 
            orLabel.AutoSize = true;
            orLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            orLabel.ForeColor = Color.IndianRed;
            orLabel.Location = new Point(193, 255);
            orLabel.Name = "orLabel";
            orLabel.Size = new Size(78, 37);
            orLabel.TabIndex = 19;
            orLabel.Text = "- or -";
            // 
            // createPrizeButton
            // 
            createPrizeButton.FlatAppearance.BorderColor = Color.Silver;
            createPrizeButton.FlatAppearance.MouseDownBackColor = Color.DimGray;
            createPrizeButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            createPrizeButton.FlatStyle = FlatStyle.Flat;
            createPrizeButton.ForeColor = Color.IndianRed;
            createPrizeButton.Location = new Point(148, 382);
            createPrizeButton.Name = "createPrizeButton";
            createPrizeButton.Size = new Size(209, 52);
            createPrizeButton.TabIndex = 26;
            createPrizeButton.Text = "Create Prize";
            createPrizeButton.UseVisualStyleBackColor = true;
            createPrizeButton.Click += createPrizeButton_Click;
            // 
            // CreatePrizeForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(547, 463);
            Controls.Add(createPrizeButton);
            Controls.Add(orLabel);
            Controls.Add(placeNameValue);
            Controls.Add(placeNameLabel);
            Controls.Add(prizeAmountValue);
            Controls.Add(prizeAmountLable);
            Controls.Add(prizePercentageValue);
            Controls.Add(prizePercentageLabel);
            Controls.Add(placeNumberValue);
            Controls.Add(placeNumberLabel);
            Controls.Add(headerLabel);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ForeColor = Color.IndianRed;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 6, 5, 6);
            Name = "CreatePrizeForm";
            Text = "Create Prize Form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label headerLabel;
        private TextBox placeNumberValue;
        private Label placeNumberLabel;
        private TextBox prizePercentageValue;
        private Label prizePercentageLabel;
        private TextBox prizeAmountValue;
        private Label prizeAmountLable;
        private TextBox placeNameValue;
        private Label placeNameLabel;
        private Label orLabel;
        private Button createPrizeButton;
    }
}