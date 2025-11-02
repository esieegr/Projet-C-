namespace Projet_C_
{
    partial class form_cr_offres
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
            label_description_titre = new Label();
            label_description = new Label();
            button_faire_offre = new Button();
            textBox_recherche = new TextBox();
            button_recherche = new Button();
            SuspendLayout();
            // 
            // listBox_marche
            // 
            listBox_marche.FormattingEnabled = true;
            listBox_marche.Location = new Point(38, 63);
            listBox_marche.Name = "listBox_marche";
            listBox_marche.Size = new Size(260, 304);
            listBox_marche.TabIndex = 1;
            // 
            // label_description_titre
            // 
            label_description_titre.AutoSize = true;
            label_description_titre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label_description_titre.Location = new Point(318, 63);
            label_description_titre.Name = "label_description_titre";
            label_description_titre.Size = new Size(89, 20);
            label_description_titre.TabIndex = 2;
            label_description_titre.Text = "Description";
            // 
            // label_description
            // 
            label_description.BorderStyle = BorderStyle.FixedSingle;
            label_description.Location = new Point(318, 90);
            label_description.Name = "label_description";
            label_description.Size = new Size(270, 235);
            label_description.TabIndex = 6;
            label_description.Text = "Sélectionnez un objet pour voir sa description";
            label_description.Padding = new Padding(5);
            // 
            // button_faire_offre
            // 
            button_faire_offre.Location = new Point(318, 338);
            button_faire_offre.Name = "button_faire_offre";
            button_faire_offre.Size = new Size(121, 29);
            button_faire_offre.TabIndex = 3;
            button_faire_offre.Text = "Faire une offre";
            button_faire_offre.UseVisualStyleBackColor = true;
            // 
            // textBox_recherche
            // 
            textBox_recherche.Location = new Point(38, 12);
            textBox_recherche.Name = "textBox_recherche";
            textBox_recherche.Size = new Size(260, 27);
            textBox_recherche.TabIndex = 4;
            // 
            // button_recherche
            // 
            button_recherche.Location = new Point(318, 12);
            button_recherche.Name = "button_recherche";
            button_recherche.Size = new Size(94, 29);
            button_recherche.TabIndex = 5;
            button_recherche.Text = "Recherche";
            button_recherche.UseVisualStyleBackColor = true;
            // 
            // form_cr_offres
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(621, 408);
            Controls.Add(label_description);
            Controls.Add(button_recherche);
            Controls.Add(textBox_recherche);
            Controls.Add(button_faire_offre);
            Controls.Add(label_description_titre);
            Controls.Add(listBox_marche);
            Name = "form_cr_offres";
            Text = "Créer une offre";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox listBox_marche;
        private Label label_description_titre;
        private Label label_description;
        private Button button_recherche;
        private TextBox textBox_recherche;
        private Button button_faire_offre;
    }
}