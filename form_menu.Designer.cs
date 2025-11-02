namespace Projet_C_
{
    partial class form_menu
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
            tabControl_menu = new TabControl();
            tabPage_echange = new TabPage();
            tabPage_inventaire = new TabPage();
            tabPage_creer_offre = new TabPage();
            button_deconnexion = new Button();
            tabControl_menu.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl_menu
            // 
            tabControl_menu.Controls.Add(tabPage_echange);
            tabControl_menu.Controls.Add(tabPage_inventaire);
            tabControl_menu.Controls.Add(tabPage_creer_offre);
            tabControl_menu.Location = new Point(0, 45);
            tabControl_menu.Name = "tabControl_menu";
            tabControl_menu.SelectedIndex = 0;
            tabControl_menu.Size = new Size(800, 455);
            tabControl_menu.TabIndex = 0;
            tabControl_menu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // tabPage_echange
            // 
            tabPage_echange.Location = new Point(4, 29);
            tabPage_echange.Name = "tabPage_echange";
            tabPage_echange.Padding = new Padding(3);
            tabPage_echange.Size = new Size(792, 422);
            tabPage_echange.TabIndex = 0;
            tabPage_echange.Text = "Échange";
            tabPage_echange.UseVisualStyleBackColor = true;
            // 
            // tabPage_inventaire
            // 
            tabPage_inventaire.Location = new Point(4, 29);
            tabPage_inventaire.Name = "tabPage_inventaire";
            tabPage_inventaire.Padding = new Padding(3);
            tabPage_inventaire.Size = new Size(792, 422);
            tabPage_inventaire.TabIndex = 1;
            tabPage_inventaire.Text = "Inventaire";
            tabPage_inventaire.UseVisualStyleBackColor = true;
            // 
            // tabPage_creer_offre
            // 
            tabPage_creer_offre.Location = new Point(4, 29);
            tabPage_creer_offre.Name = "tabPage_creer_offre";
            tabPage_creer_offre.Size = new Size(792, 422);
            tabPage_creer_offre.TabIndex = 2;
            tabPage_creer_offre.Text = "Créer une offre";
            tabPage_creer_offre.UseVisualStyleBackColor = true;
            // 
            // button_deconnexion
            // 
            button_deconnexion.Location = new Point(655, 8);
            button_deconnexion.Name = "button_deconnexion";
            button_deconnexion.Size = new Size(133, 32);
            button_deconnexion.TabIndex = 1;
            button_deconnexion.Text = "🚪 Déconnexion";
            button_deconnexion.UseVisualStyleBackColor = true;
            button_deconnexion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_deconnexion.Click += button_deconnexion_Click;
            // 
            // form_menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 500);
            Controls.Add(button_deconnexion);
            Controls.Add(tabControl_menu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "form_menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            tabControl_menu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl_menu;
        private TabPage tabPage_echange;
        private TabPage tabPage_inventaire;
        private TabPage tabPage_creer_offre;
        private Button button_deconnexion;

        private void button_deconnexion_Click(object sender, EventArgs e)
        {
            Deconnexion();
        }
    }
}