using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsSort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Go_Click(object sender, EventArgs e)
        {
            if (nud_amt.Value > 0)
            {
                frm_visualiser form = new frm_visualiser((int)nud_amt.Value);
                form.Show();
            }
            else
            {
                MessageBox.Show("Cannot visualise sorting 0 elements");
            }
        }

        private void btn_how_Click(object sender, EventArgs e)
        {
            MessageBox.Show("backspace to scramble, arrow keys to change sorting method, enter to sort");
        }
    }
}
