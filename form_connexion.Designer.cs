namespace Projet_C_
{
    partial class form_connexion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            comboBox_utilisateurs = new ComboBox();
            button_connexion = new Button();
            button_administration = new Button();
            label_titre = new Label();
            SuspendLayout();
            // 
            // comboBox_utilisateurs
            // 
            comboBox_utilisateurs.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_utilisateurs.Font = new Font("Microsoft Sans Serif", 12F);
            comboBox_utilisateurs.FormattingEnabled = true;
            comboBox_utilisateurs.Location = new Point(343, 267);
            comboBox_utilisateurs.Margin = new Padding(3, 4, 3, 4);
            comboBox_utilisateurs.Name = "comboBox_utilisateurs";
            comboBox_utilisateurs.Size = new Size(228, 33);
            comboBox_utilisateurs.TabIndex = 0;
            // 
            // button_connexion
            // 
            button_connexion.Font = new Font("Microsoft Sans Serif", 12F);
            button_connexion.Location = new Point(594, 267);
            button_connexion.Margin = new Padding(3, 4, 3, 4);
            button_connexion.Name = "button_connexion";
            button_connexion.Size = new Size(133, 40);
            button_connexion.TabIndex = 1;
            button_connexion.Text = "Connexion";
            button_connexion.UseVisualStyleBackColor = true;
            button_connexion.Click += button_connexion_Click;
            // 
            // button_administration
            // 
            button_administration.Font = new Font("Microsoft Sans Serif", 10F);
            button_administration.Location = new Point(766, 27);
            button_administration.Margin = new Padding(3, 4, 3, 4);
            button_administration.Name = "button_administration";
            button_administration.Size = new Size(136, 40);
            button_administration.TabIndex = 2;
            button_administration.Text = "Administration";
            button_administration.UseVisualStyleBackColor = true;
            button_administration.Click += button_administration_Click;
            // 
            // label_titre
            // 
            label_titre.AutoSize = true;
            label_titre.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            label_titre.Location = new Point(400, 160);
            label_titre.Name = "label_titre";
            label_titre.Size = new Size(152, 31);
            label_titre.TabIndex = 3;
            label_titre.Text = "Connexion";
            // 
            // form_connexion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(label_titre);
            Controls.Add(button_administration);
            Controls.Add(button_connexion);
            Controls.Add(comboBox_utilisateurs);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "form_connexion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connexion";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ComboBox comboBox_utilisateurs;
        private System.Windows.Forms.Button button_connexion;
        private System.Windows.Forms.Button button_administration;
        private System.Windows.Forms.Label label_titre;
    }
}
