
namespace ThaiIMEBasic
{
    partial class Main
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
            this.txbInput = new System.Windows.Forms.TextBox();
            this.lbCandidates = new System.Windows.Forms.ListBox();
            this.txbOutput = new System.Windows.Forms.TextBox();
            this.cbAdvanced = new System.Windows.Forms.CheckBox();
            this.lblFoundCount = new System.Windows.Forms.Label();
            this.cbFrequency = new System.Windows.Forms.CheckBox();
            this.cbLimit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txbInput
            // 
            this.txbInput.Location = new System.Drawing.Point(18, 18);
            this.txbInput.Margin = new System.Windows.Forms.Padding(4);
            this.txbInput.MaxLength = 100;
            this.txbInput.Name = "txbInput";
            this.txbInput.Size = new System.Drawing.Size(232, 27);
            this.txbInput.TabIndex = 0;
            this.txbInput.TextChanged += new System.EventHandler(this.txbInput_TextChanged);
            this.txbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbInput_KeyDown);
            // 
            // lbCandidates
            // 
            this.lbCandidates.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCandidates.FormattingEnabled = true;
            this.lbCandidates.ItemHeight = 23;
            this.lbCandidates.Location = new System.Drawing.Point(18, 56);
            this.lbCandidates.Margin = new System.Windows.Forms.Padding(4);
            this.lbCandidates.Name = "lbCandidates";
            this.lbCandidates.Size = new System.Drawing.Size(232, 257);
            this.lbCandidates.TabIndex = 2;
            this.lbCandidates.DoubleClick += new System.EventHandler(this.lbCandidates_DoubleClick);
            this.lbCandidates.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbCandidates_KeyDown);
            // 
            // txbOutput
            // 
            this.txbOutput.Location = new System.Drawing.Point(261, 18);
            this.txbOutput.Margin = new System.Windows.Forms.Padding(4);
            this.txbOutput.Multiline = true;
            this.txbOutput.Name = "txbOutput";
            this.txbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbOutput.Size = new System.Drawing.Size(258, 295);
            this.txbOutput.TabIndex = 1;
            // 
            // cbAdvanced
            // 
            this.cbAdvanced.AutoSize = true;
            this.cbAdvanced.Enabled = false;
            this.cbAdvanced.Location = new System.Drawing.Point(18, 345);
            this.cbAdvanced.Name = "cbAdvanced";
            this.cbAdvanced.Size = new System.Drawing.Size(403, 23);
            this.cbAdvanced.TabIndex = 3;
            this.cbAdvanced.Text = "&Advanced Orthography (Pali && Sanskrit orthography)";
            this.cbAdvanced.UseVisualStyleBackColor = true;
            this.cbAdvanced.Visible = false;
            // 
            // lblFoundCount
            // 
            this.lblFoundCount.AutoSize = true;
            this.lblFoundCount.Location = new System.Drawing.Point(14, 323);
            this.lblFoundCount.Name = "lblFoundCount";
            this.lblFoundCount.Size = new System.Drawing.Size(211, 19);
            this.lblFoundCount.TabIndex = 4;
            this.lblFoundCount.Text = "Found N words in T seconds";
            // 
            // cbFrequency
            // 
            this.cbFrequency.AutoSize = true;
            this.cbFrequency.Location = new System.Drawing.Point(18, 374);
            this.cbFrequency.Name = "cbFrequency";
            this.cbFrequency.Size = new System.Drawing.Size(182, 23);
            this.cbFrequency.TabIndex = 5;
            this.cbFrequency.Text = "Show word frequency";
            this.cbFrequency.UseVisualStyleBackColor = true;
            // 
            // cbLimit
            // 
            this.cbLimit.AutoSize = true;
            this.cbLimit.Checked = true;
            this.cbLimit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLimit.Location = new System.Drawing.Point(261, 374);
            this.cbLimit.Name = "cbLimit";
            this.cbLimit.Size = new System.Drawing.Size(169, 23);
            this.cbLimit.TabIndex = 6;
            this.cbLimit.Text = "Limit List Size (100)";
            this.cbLimit.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 410);
            this.Controls.Add(this.cbLimit);
            this.Controls.Add(this.cbFrequency);
            this.Controls.Add(this.lblFoundCount);
            this.Controls.Add(this.cbAdvanced);
            this.Controls.Add(this.txbOutput);
            this.Controls.Add(this.lbCandidates);
            this.Controls.Add(this.txbInput);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Basic Thai IME - By Hevanafa (23-11-2022)";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbInput;
        private System.Windows.Forms.ListBox lbCandidates;
        private System.Windows.Forms.TextBox txbOutput;
        private System.Windows.Forms.CheckBox cbAdvanced;
        private System.Windows.Forms.Label lblFoundCount;
        private System.Windows.Forms.CheckBox cbFrequency;
        private System.Windows.Forms.CheckBox cbLimit;
    }
}

