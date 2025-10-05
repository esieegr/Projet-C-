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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            label1 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // Rechercher
            // 
            Rechercher.Location = new Point(66, 34);
            Rechercher.Name = "Rechercher";
            Rechercher.Size = new Size(230, 27);
            Rechercher.TabIndex = 4;
            // 
            // button_refuser
            // 
            button_refuser.Location = new Point(442, 480);
            button_refuser.Name = "button_refuser";
            button_refuser.Size = new Size(134, 29);
            button_refuser.TabIndex = 8;
            button_refuser.Text = "Refuser";
            button_refuser.UseVisualStyleBackColor = true;
            // 
            // button_accepter
            // 
            button_accepter.Location = new Point(66, 480);
            button_accepter.Name = "button_accepter";
            button_accepter.Size = new Size(134, 29);
            button_accepter.TabIndex = 9;
            button_accepter.Text = "Accepter";
            button_accepter.UseVisualStyleBackColor = true;
            // 
            // listbox_type
            // 
            listbox_type.FormattingEnabled = true;
            listbox_type.Location = new Point(346, 34);
            listbox_type.Name = "listbox_type";
            listbox_type.Size = new Size(119, 28);
            listbox_type.TabIndex = 10;
            listbox_type.SelectedIndexChanged += type_SelectedIndexChanged;
            // 
            // button_rechercher
            // 
            button_rechercher.Location = new Point(482, 32);
            button_rechercher.Name = "button_rechercher";
            button_rechercher.Size = new Size(94, 29);
            button_rechercher.TabIndex = 11;
            button_rechercher.Text = "Rechercher";
            button_rechercher.UseVisualStyleBackColor = true;
            // 
            // listBox_offres
            // 
            listBox_offres.FormattingEnabled = true;
            listBox_offres.Location = new Point(66, 108);
            listBox_offres.Name = "listBox_offres";
            listBox_offres.Size = new Size(270, 350);
            listBox_offres.TabIndex = 12;
            // 
            // label_nbr_offree
            // 
            label_nbr_offree.AutoSize = true;
            label_nbr_offree.Location = new Point(66, 85);
            label_nbr_offree.Name = "label_nbr_offree";
            label_nbr_offree.Size = new Size(80, 20);
            label_nbr_offree.TabIndex = 15;
            label_nbr_offree.Text = "Nombre d'offres";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(383, 108);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 17;
            label1.Text = "Caractéristiques";
            // 
            // button1
            // 
            button1.Location = new Point(265, 480);
            button1.Name = "button1";
            button1.Size = new Size(120, 29);
            button1.TabIndex = 14;
            button1.Text = "Contre offre";
            button1.UseVisualStyleBackColor = true;
            // 
            // form_echange
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(635, 671);
            Controls.Add(label1);
            Controls.Add(label_nbr_offree);
            Controls.Add(button1);
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
        private Label label1;
        private Button button1;
    }
}
