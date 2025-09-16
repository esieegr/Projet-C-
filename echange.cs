namespace Projet_C_
{
    public partial class form_echange : Form
    {
        menu m;
        public form_echange(menu m)
        {
            InitializeComponent();

            this.m = m;

            listbox_type.Items.Add("Utilisateur");
            listbox_type.Items.Add("Type");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
