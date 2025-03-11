namespace CMPT291PROJECT
{
    partial class Edit
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
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.edit_plate = new System.Windows.Forms.TextBox();
            this.edit_model = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.edit_branch = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.edit_type = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.edit_colour = new System.Windows.Forms.TextBox();
            this.edit_milage = new System.Windows.Forms.TextBox();
            this.addcar_submit = new System.Windows.Forms.Button();
            this.original_values = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(191, 214);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(71, 13);
            this.label27.TabIndex = 26;
            this.label27.Text = "Plate Number";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(214, 166);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(36, 13);
            this.label26.TabIndex = 25;
            this.label26.Text = "Model";
            // 
            // edit_plate
            // 
            this.edit_plate.Location = new System.Drawing.Point(268, 211);
            this.edit_plate.Name = "edit_plate";
            this.edit_plate.Size = new System.Drawing.Size(121, 20);
            this.edit_plate.TabIndex = 24;
            // 
            // edit_model
            // 
            this.edit_model.Location = new System.Drawing.Point(268, 160);
            this.edit_model.Name = "edit_model";
            this.edit_model.Size = new System.Drawing.Size(121, 20);
            this.edit_model.TabIndex = 23;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(16, 214);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(37, 13);
            this.label24.TabIndex = 22;
            this.label24.Text = "Colour";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(214, 112);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 13);
            this.label20.TabIndex = 21;
            this.label20.Text = "Branch";
            // 
            // edit_branch
            // 
            this.edit_branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.edit_branch.FormattingEnabled = true;
            this.edit_branch.Location = new System.Drawing.Point(268, 106);
            this.edit_branch.Name = "edit_branch";
            this.edit_branch.Size = new System.Drawing.Size(121, 21);
            this.edit_branch.TabIndex = 20;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(13, 163);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 13);
            this.label19.TabIndex = 19;
            this.label19.Text = "Milage";
            // 
            // edit_type
            // 
            this.edit_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.edit_type.FormattingEnabled = true;
            this.edit_type.Location = new System.Drawing.Point(60, 109);
            this.edit_type.Name = "edit_type";
            this.edit_type.Size = new System.Drawing.Size(121, 21);
            this.edit_type.TabIndex = 18;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 109);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 17;
            this.label18.Text = "Type";
            // 
            // edit_colour
            // 
            this.edit_colour.Location = new System.Drawing.Point(60, 211);
            this.edit_colour.Name = "edit_colour";
            this.edit_colour.Size = new System.Drawing.Size(121, 20);
            this.edit_colour.TabIndex = 16;
            // 
            // edit_milage
            // 
            this.edit_milage.Location = new System.Drawing.Point(60, 160);
            this.edit_milage.Name = "edit_milage";
            this.edit_milage.Size = new System.Drawing.Size(121, 20);
            this.edit_milage.TabIndex = 15;
            // 
            // addcar_submit
            // 
            this.addcar_submit.Location = new System.Drawing.Point(60, 263);
            this.addcar_submit.Name = "addcar_submit";
            this.addcar_submit.Size = new System.Drawing.Size(75, 23);
            this.addcar_submit.TabIndex = 14;
            this.addcar_submit.Text = "Submit";
            this.addcar_submit.UseVisualStyleBackColor = true;
            this.addcar_submit.Click += new System.EventHandler(this.editSubmit);
            // 
            // original_values
            // 
            this.original_values.AutoSize = true;
            this.original_values.Location = new System.Drawing.Point(22, 38);
            this.original_values.Name = "original_values";
            this.original_values.Size = new System.Drawing.Size(37, 13);
            this.original_values.TabIndex = 27;
            this.original_values.Text = "TEMP";
            // 
            // Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 299);
            this.Controls.Add(this.original_values);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.edit_plate);
            this.Controls.Add(this.edit_model);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.edit_branch);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.edit_type);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.edit_colour);
            this.Controls.Add(this.edit_milage);
            this.Controls.Add(this.addcar_submit);
            this.Name = "Edit";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Edit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox edit_plate;
        private System.Windows.Forms.TextBox edit_model;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox edit_branch;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox edit_type;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox edit_colour;
        private System.Windows.Forms.TextBox edit_milage;
        private System.Windows.Forms.Button addcar_submit;
        private System.Windows.Forms.Label original_values;
    }
}