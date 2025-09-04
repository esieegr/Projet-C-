namespace Projet_C_
{
    partial class Inventaire
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
            listBox_inventaire = new ListBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // listBox_inventaire
            // 
            listBox_inventaire.FormattingEnabled = true;
            listBox_inventaire.Location = new Point(64, 171);
            listBox_inventaire.Name = "listBox_inventaire";
            listBox_inventaire.Size = new Size(319, 284);
            listBox_inventaire.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(257, 127);
            button1.Name = "button1";
            button1.Size = new Size(126, 29);
            button1.TabIndex = 1;
            button1.Text = "Ajouter l'objet";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(156, 461);
            button2.Name = "button2";
            button2.Size = new Size(146, 29);
            button2.TabIndex = 2;
            button2.Text = "Supprimer un objet";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(156, 496);
            button3.Name = "button3";
            button3.Size = new Size(146, 29);
            button3.TabIndex = 3;
            button3.Text = "Sauvegarder";
            button3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(64, 41);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(238, 27);
            textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(64, 94);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(319, 27);
            textBox2.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(64, 71);
            label1.Name = "label1";
            label1.Size = new Size(85, 20);
            label1.TabIndex = 6;
            label1.Text = "Déscription";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(64, 13);
            label2.Name = "label2";
            label2.Size = new Size(109, 20);
            label2.TabIndex = 7;
            label2.Text = "Nom de l'objet";
            // 
            // Inventaire
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(502, 537);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listBox_inventaire);
            Name = "Inventaire";
            Text = "Inventaire";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox_inventaire;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
    }
}