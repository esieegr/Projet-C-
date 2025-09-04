namespace Projet_C_
{
    partial class cr_offres
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
            label1 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            button2 = new Button();
            SuspendLayout();
            // 
            // listBox_inventaire
            // 
            listBox_inventaire.FormattingEnabled = true;
            listBox_inventaire.Location = new Point(38, 63);
            listBox_inventaire.Name = "listBox_inventaire";
            listBox_inventaire.Size = new Size(260, 304);
            listBox_inventaire.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(318, 63);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 2;
            label1.Text = "Caractéristiques";
            // 
            // button1
            // 
            button1.Location = new Point(318, 338);
            button1.Name = "button1";
            button1.Size = new Size(121, 29);
            button1.TabIndex = 3;
            button1.Text = "Faire une offre";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(38, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(260, 27);
            textBox1.TabIndex = 4;
            // 
            // button2
            // 
            button2.Location = new Point(318, 12);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 5;
            button2.Text = "Recherche";
            button2.UseVisualStyleBackColor = true;
            // 
            // cr_offres
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(621, 408);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(listBox_inventaire);
            Name = "cr_offres";
            Text = "Créer une offre";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox listBox_inventaire;
        private Label label1;
        private Button button1;
        private TextBox textBox1;
        private Button button2;
    }
}