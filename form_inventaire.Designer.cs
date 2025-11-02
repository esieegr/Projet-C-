namespace Projet_C_
{
    partial class form_inventaire
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
            listBox_marche = new ListBox();
            button_ajouter_objet = new Button();
            button_supprimer_objet = new Button();
            button_sauvegarder = new Button();
            textBox_recherche = new TextBox();
            textBox2 = new TextBox();
            label_description = new Label();
            label_nom = new Label();
            comboBox_type = new ComboBox();
            comboBox_etat = new ComboBox();
            button4 = new Button();
            label_type = new Label();
            label_etat = new Label();
            SuspendLayout();
            // 
            // listBox_marche
            // 
            listBox_marche.FormattingEnabled = true;
            listBox_marche.Location = new Point(20, 130);
            listBox_marche.Name = "listBox_marche";
            listBox_marche.Size = new Size(380, 204);
            listBox_marche.TabIndex = 0;
            // 
            // button_ajouter_objet
            // 
            button_ajouter_objet.Location = new Point(420, 30);
            button_ajouter_objet.Name = "button_ajouter_objet";
            button_ajouter_objet.Size = new Size(150, 88);
            button_ajouter_objet.TabIndex = 1;
            button_ajouter_objet.Text = "➕ Ajouter\nl'objet";
            button_ajouter_objet.UseVisualStyleBackColor = true;
            button_ajouter_objet.Click += button_add_Click;
            // 
            // button_supprimer_objet
            // 
            button_supprimer_objet.Location = new Point(20, 350);
            button_supprimer_objet.Name = "button_supprimer_objet";
            button_supprimer_objet.Size = new Size(146, 29);
            button_supprimer_objet.TabIndex = 2;
            button_supprimer_objet.Text = "Supprimer l'objet";
            button_supprimer_objet.UseVisualStyleBackColor = true;
            button_supprimer_objet.Click += button_supprimer_objet_Click;
            // 
            // button_sauvegarder
            // 
            button_sauvegarder.Location = new Point(254, 350);
            button_sauvegarder.Name = "button_sauvegarder";
            button_sauvegarder.Size = new Size(146, 29);
            button_sauvegarder.TabIndex = 3;
            button_sauvegarder.Text = "Sauvegarder";
            button_sauvegarder.UseVisualStyleBackColor = true;
            button_sauvegarder.Click += button_sauvegarder_Click;
            // 
            // textBox_recherche
            // 
            textBox_recherche.Location = new Point(20, 30);
            textBox_recherche.Name = "textBox_recherche";
            textBox_recherche.Size = new Size(220, 27);
            textBox_recherche.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(20, 90);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(220, 27);
            textBox2.TabIndex = 5;
            // 
            // label_description
            // 
            label_description.AutoSize = true;
            label_description.Location = new Point(20, 67);
            label_description.Name = "label_description";
            label_description.Size = new Size(85, 20);
            label_description.TabIndex = 6;
            label_description.Text = "Description";
            // 
            // label_nom
            // 
            label_nom.AutoSize = true;
            label_nom.Location = new Point(20, 7);
            label_nom.Name = "label_nom";
            label_nom.Size = new Size(109, 20);
            label_nom.TabIndex = 7;
            label_nom.Text = "Nom de l'objet";
            // 
            // comboBox_type
            // 
            comboBox_type.FormattingEnabled = true;
            comboBox_type.Location = new Point(270, 30);
            comboBox_type.Name = "comboBox_type";
            comboBox_type.Size = new Size(130, 28);
            comboBox_type.TabIndex = 8;
            // 
            // comboBox_etat
            // 
            comboBox_etat.FormattingEnabled = true;
            comboBox_etat.Location = new Point(270, 90);
            comboBox_etat.Name = "comboBox_etat";
            comboBox_etat.Size = new Size(130, 28);
            comboBox_etat.TabIndex = 9;
            // 
            // button4
            // 
            button4.Location = new Point(420, 200);
            button4.Name = "button4";
            button4.Size = new Size(150, 60);
            button4.TabIndex = 10;
            button4.Text = "Disponible/\nIndisponible";
            button4.UseVisualStyleBackColor = true;
            // 
            // label_type
            // 
            label_type.AutoSize = true;
            label_type.Location = new Point(270, 7);
            label_type.Name = "label_type";
            label_type.Size = new Size(40, 20);
            label_type.TabIndex = 11;
            label_type.Text = "Type";
            // 
            // label_etat
            // 
            label_etat.AutoSize = true;
            label_etat.Location = new Point(270, 67);
            label_etat.Name = "label_etat";
            label_etat.Size = new Size(35, 20);
            label_etat.TabIndex = 12;
            label_etat.Text = "État";
            // 
            // form_inventaire
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 400);
            Controls.Add(label_etat);
            Controls.Add(label_type);
            Controls.Add(button4);
            Controls.Add(comboBox_etat);
            Controls.Add(comboBox_type);
            Controls.Add(label_nom);
            Controls.Add(label_description);
            Controls.Add(textBox2);
            Controls.Add(textBox_recherche);
            Controls.Add(button_sauvegarder);
            Controls.Add(button_supprimer_objet);
            Controls.Add(button_ajouter_objet);
            Controls.Add(listBox_marche);
            Name = "form_inventaire";
            Text = "Inventaire";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox_marche;
        private Button button_ajouter_objet;
        private Button button_supprimer_objet;
        private Button button_sauvegarder;
        private TextBox textBox_recherche;
        private TextBox textBox2;
        private Label label_description;
        private Label label_nom;
        private ComboBox comboBox_type;
        private ComboBox comboBox_etat;
        private Button button4;
        private Label label_type;
        private Label label_etat;
    }
}