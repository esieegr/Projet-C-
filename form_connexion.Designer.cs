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
            this.comboBox_utilisateurs = new System.Windows.Forms.ComboBox();
            this.button_connexion = new System.Windows.Forms.Button();
            this.button_administration = new System.Windows.Forms.Button();
            this.label_titre = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // 
            // comboBox_utilisateurs
            // 
            this.comboBox_utilisateurs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_utilisateurs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox_utilisateurs.FormattingEnabled = true;
            this.comboBox_utilisateurs.Location = new System.Drawing.Point(300, 200);
            this.comboBox_utilisateurs.Name = "comboBox_utilisateurs";
            this.comboBox_utilisateurs.Size = new System.Drawing.Size(200, 28);
            this.comboBox_utilisateurs.TabIndex = 0;
            
            // 
            // button_connexion
            // 
            this.button_connexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_connexion.Location = new System.Drawing.Point(520, 200);
            this.button_connexion.Name = "button_connexion";
            this.button_connexion.Size = new System.Drawing.Size(100, 30);
            this.button_connexion.TabIndex = 1;
            this.button_connexion.Text = "Connexion";
            this.button_connexion.UseVisualStyleBackColor = true;
            this.button_connexion.Click += new System.EventHandler(this.button_connexion_Click);
            
            // 
            // button_administration
            // 
            this.button_administration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_administration.Location = new System.Drawing.Point(700, 20);
            this.button_administration.Name = "button_administration";
            this.button_administration.Size = new System.Drawing.Size(100, 30);
            this.button_administration.TabIndex = 2;
            this.button_administration.Text = "Administration";
            this.button_administration.UseVisualStyleBackColor = true;
            this.button_administration.Click += new System.EventHandler(this.button_administration_Click);
            
            // 
            // label_titre
            // 
            this.label_titre.AutoSize = true;
            this.label_titre.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_titre.Location = new System.Drawing.Point(350, 120);
            this.label_titre.Name = "label_titre";
            this.label_titre.Size = new System.Drawing.Size(100, 26);
            this.label_titre.TabIndex = 3;
            this.label_titre.Text = "Connexion";
            
            // 
            // form_connexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_titre);
            this.Controls.Add(this.button_administration);
            this.Controls.Add(this.button_connexion);
            this.Controls.Add(this.comboBox_utilisateurs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "form_connexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connexion";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox comboBox_utilisateurs;
        private System.Windows.Forms.Button button_connexion;
        private System.Windows.Forms.Button button_administration;
        private System.Windows.Forms.Label label_titre;
    }
}
