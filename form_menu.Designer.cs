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
            button_administration = new Button();
            button2 = new Button();
            button3 = new Button();
            comboBox_login = new ComboBox();
            button_creer_offre = new Button();
            SuspendLayout();
            // 
            // button_administration
            // 
            button_administration.Location = new Point(123, 187);
            button_administration.Name = "button_administration";
            button_administration.Size = new Size(134, 29);
            button_administration.TabIndex = 0;
            button_administration.Text = "Administration";
            button_administration.UseVisualStyleBackColor = true;
            button_administration.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(347, 187);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 1;
            button2.Text = "Échange";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(541, 187);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 2;
            button3.Text = "Inventaire";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // comboBox_login
            // 
            comboBox_login.FormattingEnabled = true;
            comboBox_login.Location = new Point(320, 43);
            comboBox_login.Name = "comboBox_login";
            comboBox_login.Size = new Size(151, 28);
            comboBox_login.TabIndex = 3;
            // 
            // button_creer_offre
            // 
            button_creer_offre.Location = new Point(127, 304);
            button_creer_offre.Name = "button_creer_offre";
            button_creer_offre.Size = new Size(130, 29);
            button_creer_offre.TabIndex = 4;
            button_creer_offre.Text = "Créer une offre";
            button_creer_offre.UseVisualStyleBackColor = true;
            button_creer_offre.Click += button_creer_offre_Click_1;
            // 
            // menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button_creer_offre);
            Controls.Add(comboBox_login);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button_administration);
            Name = "menu";
            Text = "menu";
            ResumeLayout(false);
        }

        #endregion

        private Button button_administration;
        private Button button2;
        private Button button3;
        private ComboBox comboBox_login;
        private Button button_creer_offre;
    }
}