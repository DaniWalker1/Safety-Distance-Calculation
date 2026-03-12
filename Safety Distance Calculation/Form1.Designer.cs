namespace Safety_Distance_Calculation
{
    partial class Form1
    {

        ///  Required designer variable.

        private System.ComponentModel.IContainer components = null;


        ///  Clean up any resources being used.

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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbApproachType = new ComboBox();
            txtT = new TextBox();
            txtD = new TextBox();
            txtExtra = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblExtra = new Label();
            btnCalculate = new Button();
            lblResult = new Label();
            chkValidateReachOver = new CheckBox();
            txtHazardHeight = new TextBox();
            label4 = new Label();
            txtGuardHeight = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // cmbApproachType
            // 
            cmbApproachType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbApproachType.FormattingEnabled = true;
            cmbApproachType.Items.AddRange(new object[] { "Perpendicular", "Parallel", "Angle" });
            cmbApproachType.Location = new Point(20, 14);
            cmbApproachType.Name = "cmbApproachType";
            cmbApproachType.Size = new Size(177, 23);
            cmbApproachType.TabIndex = 0;
            cmbApproachType.SelectedIndexChanged += cmbApproachType_SelectedIndexChanged;
            // 
            // txtT
            // 
            txtT.Location = new Point(20, 51);
            txtT.Name = "txtT";
            txtT.Size = new Size(121, 23);
            txtT.TabIndex = 1;
            // 
            // txtD
            // 
            txtD.Location = new Point(20, 80);
            txtD.Name = "txtD";
            txtD.Size = new Size(121, 23);
            txtD.TabIndex = 2;
            // 
            // txtExtra
            // 
            txtExtra.Location = new Point(20, 109);
            txtExtra.Name = "txtExtra";
            txtExtra.Size = new Size(121, 23);
            txtExtra.TabIndex = 3;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(147, 54);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 4;
            label2.Text = "Time Stop (S)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(147, 83);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 5;
            label3.Text = "Resolution (mm)";
            // 
            // lblExtra
            // 
            lblExtra.AutoSize = true;
            lblExtra.Location = new Point(147, 112);
            lblExtra.Name = "lblExtra";
            lblExtra.Size = new Size(79, 15);
            lblExtra.TabIndex = 6;
            lblExtra.Text = "Height/Angle";
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(20, 151);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(131, 37);
            btnCalculate.TabIndex = 7;
            btnCalculate.Text = "Calculate Distance";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblResult.Location = new Point(20, 206);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(0, 25);
            lblResult.TabIndex = 8;
            // 
            // chkValidateReachOver
            // 
            chkValidateReachOver.AutoSize = true;
            chkValidateReachOver.Location = new Point(273, 18);
            chkValidateReachOver.Name = "chkValidateReachOver";
            chkValidateReachOver.Size = new Size(192, 19);
            chkValidateReachOver.TabIndex = 9;
            chkValidateReachOver.Text = "Validate ISO 13857 (Reach Over)";
            chkValidateReachOver.UseVisualStyleBackColor = true;
            chkValidateReachOver.CheckedChanged += chkValidateReachOver_CheckedChanged;
            // 
            // txtHazardHeight
            // 
            txtHazardHeight.Location = new Point(273, 43);
            txtHazardHeight.Name = "txtHazardHeight";
            txtHazardHeight.Size = new Size(121, 23);
            txtHazardHeight.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(399, 46);
            label4.Name = "label4";
            label4.Size = new Size(101, 15);
            label4.TabIndex = 11;
            label4.Text = "Danger Height (a)";
            // 
            // txtGuardHeight
            // 
            txtGuardHeight.Location = new Point(273, 72);
            txtGuardHeight.Name = "txtGuardHeight";
            txtGuardHeight.Size = new Size(121, 23);
            txtGuardHeight.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(399, 75);
            label5.Name = "label5";
            label5.Size = new Size(140, 15);
            label5.TabIndex = 11;
            label5.Text = "Guard/Curtain Height (b)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(643, 346);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtGuardHeight);
            Controls.Add(txtHazardHeight);
            Controls.Add(chkValidateReachOver);
            Controls.Add(lblResult);
            Controls.Add(btnCalculate);
            Controls.Add(lblExtra);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtExtra);
            Controls.Add(txtD);
            Controls.Add(txtT);
            Controls.Add(cmbApproachType);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbApproachType;
        private TextBox txtT;
        private TextBox txtD;
        private TextBox txtExtra;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblExtra;
        private Button btnCalculate;
        private Label lblResult;
        private CheckBox chkValidateReachOver;
        private TextBox txtHazardHeight;
        private Label label4;
        private TextBox txtGuardHeight;
        private Label label5;
    }
}
