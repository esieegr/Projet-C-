namespace Projet_C_
{
    partial class administration
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
            textBox1 = new TextBox();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            listBox_utilisateurs = new ListBox();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(65, 32);
            label2.Name = "label2";
            label2.Size = new Size(141, 20);
            label2.TabIndex = 13;
            label2.Text = "Nom de l'Utilisateur";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(65, 60);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(238, 27);
            textBox1.TabIndex = 12;
            // 
            // button3
            // 
            button3.Location = new Point(157, 473);
            button3.Name = "button3";
            button3.Size = new Size(146, 29);
            button3.TabIndex = 11;
            button3.Text = "Sauvegarder";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(140, 438);
            button2.Name = "button2";
            button2.Size = new Size(179, 29);
            button2.TabIndex = 10;
            button2.Text = "Supprimer l'utilisateur";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(231, 93);
            button1.Name = "button1";
            button1.Size = new Size(153, 29);
            button1.TabIndex = 9;
            button1.Text = "Ajouter l'utilisateur";
            button1.UseVisualStyleBackColor = true;
            // 
            // listBox_utilisateurs
            // 
            listBox_utilisateurs.FormattingEnabled = true;
            listBox_utilisateurs.Location = new Point(65, 148);
            listBox_utilisateurs.Name = "listBox_utilisateurs";
            listBox_utilisateurs.Size = new Size(319, 284);
            listBox_utilisateurs.TabIndex = 8;
            // 
            // administration
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(447, 520);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listBox_utilisateurs);
            Name = "administration";
            Text = "Administration";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox textBox1;
        private Button button3;
        private Button button2;
        private Button button1;
        private ListBox listBox_utilisateurs;
    }
}