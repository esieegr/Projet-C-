namespace Projet_C_
{
    partial class form_mon_offre
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
            listBox_mes_objets = new ListBox();
            label_objet_demande = new Label();
            button_proposer = new Button();
            textBox_recherche = new TextBox();
            button_rechercher = new Button();
            label_description_titre = new Label();
            label_description = new Label();
            button_annuler = new Button();
            SuspendLayout();
            // 
            // listBox_mes_objets
            // 
            listBox_mes_objets.FormattingEnabled = true;
            listBox_mes_objets.Location = new Point(20, 85);
            listBox_mes_objets.Name = "listBox_mes_objets";
            listBox_mes_objets.Size = new Size(300, 264);
            listBox_mes_objets.TabIndex = 0;
            // 
            // label_objet_demande
            // 
            label_objet_demande.AutoSize = true;
            label_objet_demande.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label_objet_demande.Location = new Point(340, 20);
            label_objet_demande.Name = "label_objet_demande";
            label_objet_demande.Size = new Size(240, 23);
            label_objet_demande.TabIndex = 1;
            label_objet_demande.Text = "Objet demandé : Dragon Ball Z (de alice)";
            // 
            // button_proposer
            // 
            button_proposer.BackColor = Color.FromArgb(76, 175, 80);
            button_proposer.FlatStyle = FlatStyle.Flat;
            button_proposer.ForeColor = Color.White;
            button_proposer.Location = new Point(340, 320);
            button_proposer.Name = "button_proposer";
            button_proposer.Size = new Size(140, 35);
            button_proposer.TabIndex = 5;
            button_proposer.Text = "✓ Valider l'offre";
            button_proposer.UseVisualStyleBackColor = false;
            // 
            // textBox_recherche
            // 
            textBox_recherche.Location = new Point(20, 20);
            textBox_recherche.Name = "textBox_recherche";
            textBox_recherche.PlaceholderText = "Rechercher un objet...";
            textBox_recherche.Size = new Size(200, 27);
            textBox_recherche.TabIndex = 6;
            // 
            // button_rechercher
            // 
            button_rechercher.Location = new Point(226, 19);
            button_rechercher.Name = "button_rechercher";
            button_rechercher.Size = new Size(94, 29);
            button_rechercher.TabIndex = 7;
            button_rechercher.Text = "🔍 Rechercher";
            button_rechercher.UseVisualStyleBackColor = true;
            // 
            // label_description_titre
            // 
            label_description_titre.AutoSize = true;
            label_description_titre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label_description_titre.Location = new Point(340, 60);
            label_description_titre.Name = "label_description_titre";
            label_description_titre.Size = new Size(165, 20);
            label_description_titre.TabIndex = 8;
            label_description_titre.Text = "Description de l'objet";
            // 
            // label_description
            // 
            label_description.BorderStyle = BorderStyle.FixedSingle;
            label_description.Location = new Point(340, 85);
            label_description.Name = "label_description";
            label_description.Size = new Size(420, 220);
            label_description.TabIndex = 10;
            label_description.Text = "Sélectionnez un objet pour voir sa description";
            label_description.Padding = new Padding(10);
            // 
            // button_annuler
            // 
            button_annuler.FlatStyle = FlatStyle.Flat;
            button_annuler.Location = new Point(510, 320);
            button_annuler.Name = "button_annuler";
            button_annuler.Size = new Size(140, 35);
            button_annuler.TabIndex = 9;
            button_annuler.Text = "✗ Annuler";
            button_annuler.UseVisualStyleBackColor = true;
            // 
            // form_mon_offre
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 375);
            Controls.Add(label_description);
            Controls.Add(button_annuler);
            Controls.Add(label_description_titre);
            Controls.Add(button_rechercher);
            Controls.Add(textBox_recherche);
            Controls.Add(button_proposer);
            Controls.Add(label_objet_demande);
            Controls.Add(listBox_mes_objets);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "form_mon_offre";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Créer une offre d'échange";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox_mes_objets;
        private Label label_objet_demande;
        private Button button_proposer;
        private TextBox textBox_recherche;
        private Button button_rechercher;
        private Label label_description_titre;
        private Label label_description;
        private Button button_annuler;
    }
}