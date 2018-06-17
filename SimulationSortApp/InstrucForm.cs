using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationSortApp
{
    public partial class InstrucForm : Form
    {
        public InstrucForm()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        Boolean flag;
        int x, y;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == true)
            {
                this.SetDesktopLocation(Cursor.Position.X - x, Cursor.Position.Y - y);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            flag = true;
            x = e.X;
            y = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            flag = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
