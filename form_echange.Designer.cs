namespace Projet_C_
{
    partial class form_echange
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Libérer le contexte DB
                _db?.Dispose();
                _bs?.Dispose();
                
                // Libérer les composants
                if (components != null)
                {
                    components.Dispose();
                }
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
            Rechercher = new TextBox();
            button_refuser = new Button();
            button_accepter = new Button();
            listbox_type = new ComboBox();
            button_rechercher = new Button();
            listBox_offres = new ListBox();
            label_nbr_offree = new Label();
            label_details_titre = new Label();
            label_details = new Label();
            button_faire_offre = new Button();
            SuspendLayout();
            // 
            // Rechercher
            // 
            Rechercher.Location = new Point(20, 15);
            Rechercher.Name = "Rechercher";
            Rechercher.Size = new Size(200, 27);
            Rechercher.TabIndex = 4;
            // 
            // button_refuser
            // 
            button_refuser.Location = new Point(420, 360);
            button_refuser.Name = "button_refuser";
            button_refuser.Size = new Size(100, 29);
            button_refuser.TabIndex = 8;
            button_refuser.Text = "Refuser";
            button_refuser.UseVisualStyleBackColor = true;
            // 
            // button_accepter
            // 
            button_accepter.Location = new Point(20, 360);
            button_accepter.Name = "button_accepter";
            button_accepter.Size = new Size(100, 29);
            button_accepter.TabIndex = 9;
            button_accepter.Text = "Accepter";
            button_accepter.UseVisualStyleBackColor = true;
            // 
            // listbox_type
            // 
            listbox_type.FormattingEnabled = true;
            listbox_type.Location = new Point(230, 15);
            listbox_type.Name = "listbox_type";
            listbox_type.Size = new Size(130, 28);
            listbox_type.TabIndex = 10;
            listbox_type.SelectedIndexChanged += type_SelectedIndexChanged;
            // 
            // button_rechercher
            // 
            button_rechercher.Location = new Point(370, 14);
            button_rechercher.Name = "button_rechercher";
            button_rechercher.Size = new Size(100, 29);
            button_rechercher.TabIndex = 11;
            button_rechercher.Text = "Rechercher";
            button_rechercher.UseVisualStyleBackColor = true;
            button_rechercher.Click += button_rechercher_Click;
            // 
            // listBox_offres
            // 
            listBox_offres.FormattingEnabled = true;
            listBox_offres.Location = new Point(20, 75);
            listBox_offres.Name = "listBox_offres";
            listBox_offres.Size = new Size(330, 264);
            listBox_offres.TabIndex = 12;
            // 
            // label_nbr_offree
            // 
            label_nbr_offree.AutoSize = true;
            label_nbr_offree.Location = new Point(20, 52);
            label_nbr_offree.Name = "label_nbr_offree";
            label_nbr_offree.Size = new Size(118, 20);
            label_nbr_offree.TabIndex = 15;
            label_nbr_offree.Text = "Offres actives : 0";
            // 
            // label_details_titre
            // 
            label_details_titre.AutoSize = true;
            label_details_titre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label_details_titre.Location = new Point(370, 75);
            label_details_titre.Name = "label_details_titre";
            label_details_titre.Size = new Size(131, 20);
            label_details_titre.TabIndex = 17;
            label_details_titre.Text = "Détails de l'offre";
            // 
            // label_details
            // 
            label_details.BorderStyle = BorderStyle.FixedSingle;
            label_details.Location = new Point(370, 100);
            label_details.Name = "label_details";
            label_details.Size = new Size(350, 239);
            label_details.TabIndex = 18;
            label_details.Text = "Sélectionnez une offre pour voir les détails";
            label_details.Padding = new Padding(5);
            // 
            // button_faire_offre
            // 
            button_faire_offre.Location = new Point(220, 360);
            button_faire_offre.Name = "button_faire_offre";
            button_faire_offre.Size = new Size(120, 29);
            button_faire_offre.TabIndex = 14;
            button_faire_offre.Text = "Contre-offre";
            button_faire_offre.UseVisualStyleBackColor = true;
            // 
            // form_echange
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(750, 400);
            Controls.Add(label_details);
            Controls.Add(label_details_titre);
            Controls.Add(label_nbr_offree);
            Controls.Add(button_faire_offre);
            Controls.Add(listBox_offres);
            Controls.Add(button_rechercher);
            Controls.Add(listbox_type);
            Controls.Add(button_accepter);
            Controls.Add(button_refuser);
            Controls.Add(Rechercher);
            Name = "form_echange";
            Text = "Échange";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox Rechercher;
        private Button button_refuser;
        private Button button_accepter;
        private ComboBox listbox_type;
        private Button button_rechercher;
        private ListBox listBox_offres;
        private Label label_nbr_offree;
        private Label label_details_titre;
        private Label label_details;
        private Button button_faire_offre;
    }
}
