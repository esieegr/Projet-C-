namespace Projet_C_
{
    partial class form_administration
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
            label2 = new Label();
            textBox_nom = new TextBox();
            button3 = new Button();
            button2 = new Button();
            button_ajouter_utilisateur = new Button();
            listBox_utilisateurs = new ListBox();
            textBox_pseudo = new TextBox();
            label_pseudo = new Label();
            textBox_prenom = new TextBox();
            label_prenom = new Label();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(248, 29);
            label2.Name = "label2";
            label2.Size = new Size(42, 20);
            label2.TabIndex = 13;
            label2.Text = "Nom";
            // 
            // textBox_nom
            // 
            textBox_nom.Location = new Point(248, 52);
            textBox_nom.Name = "textBox_nom";
            textBox_nom.Size = new Size(169, 27);
            textBox_nom.TabIndex = 12;
            // 
            // button3
            // 
            button3.Location = new Point(157, 473);
            button3.Name = "button3";
            button3.Size = new Size(146, 29);
            button3.TabIndex = 11;
            button3.Text = "Sauvegarder";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(140, 438);
            button2.Name = "button2";
            button2.Size = new Size(179, 29);
            button2.TabIndex = 10;
            button2.Text = "Supprimer l'utilisateur";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button_ajouter_utilisateur
            // 
            button_ajouter_utilisateur.Location = new Point(248, 112);
            button_ajouter_utilisateur.Name = "button_ajouter_utilisateur";
            button_ajouter_utilisateur.Size = new Size(147, 29);
            button_ajouter_utilisateur.TabIndex = 9;
            button_ajouter_utilisateur.Text = "Ajouter l'utilisateur";
            button_ajouter_utilisateur.UseVisualStyleBackColor = true;
            button_ajouter_utilisateur.Click += button1_Click;
            // 
            // listBox_utilisateurs
            // 
            listBox_utilisateurs.FormattingEnabled = true;
            listBox_utilisateurs.Location = new Point(65, 168);
            listBox_utilisateurs.Name = "listBox_utilisateurs";
            listBox_utilisateurs.Size = new Size(319, 264);
            listBox_utilisateurs.TabIndex = 8;
            // 
            // textBox_pseudo
            // 
            textBox_pseudo.Location = new Point(62, 112);
            textBox_pseudo.Name = "textBox_pseudo";
            textBox_pseudo.Size = new Size(169, 27);
            textBox_pseudo.TabIndex = 14;
            // 
            // label_pseudo
            // 
            label_pseudo.AutoSize = true;
            label_pseudo.Location = new Point(65, 89);
            label_pseudo.Name = "label_pseudo";
            label_pseudo.Size = new Size(57, 20);
            label_pseudo.TabIndex = 15;
            label_pseudo.Text = "Pseudo";
            // 
            // textBox_prenom
            // 
            textBox_prenom.Location = new Point(62, 52);
            textBox_prenom.Name = "textBox_prenom";
            textBox_prenom.Size = new Size(169, 27);
            textBox_prenom.TabIndex = 16;
            // 
            // label_prenom
            // 
            label_prenom.AutoSize = true;
            label_prenom.Location = new Point(62, 29);
            label_prenom.Name = "label_prenom";
            label_prenom.Size = new Size(60, 20);
            label_prenom.TabIndex = 17;
            label_prenom.Text = "Prénom";
            // 
            // form_administration
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(447, 520);
            Controls.Add(label_prenom);
            Controls.Add(textBox_prenom);
            Controls.Add(label_pseudo);
            Controls.Add(textBox_pseudo);
            Controls.Add(label2);
            Controls.Add(textBox_nom);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button_ajouter_utilisateur);
            Controls.Add(listBox_utilisateurs);
            Name = "form_administration";
            Text = "Administration";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox textBox_nom;
        private Button button3;
        private Button button2;
        private Button button_ajouter_utilisateur;
        private ListBox listBox_utilisateurs;
        private TextBox textBox_pseudo;
        private Label label_pseudo;
        private TextBox textBox_prenom;
        private Label label_prenom;
    }
}