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
            label1 = new Label();
            button_recherche = new Button();
            textBox_recherche = new TextBox();
            button_rechercher = new Button();
            label2 = new Label();
            button_faire_offre = new Button();
            SuspendLayout();
            // 
            // listBox_mes_objets
            // 
            listBox_mes_objets.FormattingEnabled = true;
            listBox_mes_objets.Location = new Point(57, 60);
            listBox_mes_objets.Name = "listBox_mes_objets";
            listBox_mes_objets.Size = new Size(257, 284);
            listBox_mes_objets.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(567, 60);
            label1.Name = "label1";
            label1.Size = new Size(122, 20);
            label1.TabIndex = 1;
            label1.Text = "Offre sélectionée";
            // 
            // button_recherche
            // 
            button_recherche.Location = new Point(330, 315);
            button_recherche.Name = "button_recherche";
            button_recherche.Size = new Size(124, 29);
            button_recherche.TabIndex = 5;
            button_recherche.Text = "Valider l'offre";
            button_recherche.UseVisualStyleBackColor = true;
            // 
            // textBox_recherche
            // 
            textBox_recherche.Location = new Point(57, 18);
            textBox_recherche.Name = "textBox_recherche";
            textBox_recherche.Size = new Size(178, 27);
            textBox_recherche.TabIndex = 6;
            // 
            // button_rechercher
            // 
            button_rechercher.Location = new Point(241, 17);
            button_rechercher.Name = "button_rechercher";
            button_rechercher.Size = new Size(94, 29);
            button_rechercher.TabIndex = 7;
            button_rechercher.Text = "Rechercher";
            button_rechercher.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(330, 60);
            label2.Name = "label2";
            label2.Size = new Size(114, 20);
            label2.TabIndex = 8;
            label2.Text = "Caractéristiques";
            // 
            // button_faire_offre
            // 
            button_faire_offre.Location = new Point(485, 315);
            button_faire_offre.Name = "button_faire_offre";
            button_faire_offre.Size = new Size(124, 29);
            button_faire_offre.TabIndex = 9;
            button_faire_offre.Text = "Annuler";
            button_faire_offre.UseVisualStyleBackColor = true;
            // 
            // mon_offre
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(794, 387);
            Controls.Add(button_faire_offre);
            Controls.Add(label2);
            Controls.Add(button_rechercher);
            Controls.Add(textBox_recherche);
            Controls.Add(button_recherche);
            Controls.Add(label1);
            Controls.Add(listBox_mes_objets);
            Name = "mon_offre";
            Text = "Mon offre";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox_mes_objets;
        private Label label1;
        private Button button_recherche;
        private TextBox textBox_recherche;
        private Button button_rechercher;
        private Label label2;
        private Button button_faire_offre;
    }
}