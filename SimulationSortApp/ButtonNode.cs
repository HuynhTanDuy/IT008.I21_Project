using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;


namespace SimulationSortApp
{
    class ButtonNode:Button
    {

        public int giaTri;
        public int vitriHienTai;
        public TextBox nhapTayTexbox;

        public ButtonNode(int vitrihientai, int giatri)
        {

            // ButtonNode : property + event

            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = System.Drawing.Color.White;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.Size = new Size(40, 40);
            this.Padding = new Padding(0);
            this.Font = new Font("Consolas", 40 / 3.2f, FontStyle.Bold);
            this.UseCompatibleTextRendering = true;

            this.Text = giatri.ToString();
            giaTri = giatri;
            vitriHienTai = vitrihientai;
            this.GotFocus += new EventHandler(Node_GotFocus);

            // NhapTay TextBox : property + event
            nhapTayTexbox = new TextBox();
            nhapTayTexbox.MaxLength = 2;
            nhapTayTexbox.TextAlign = HorizontalAlignment.Center;
            nhapTayTexbox.BorderStyle = BorderStyle.Fixed3D;
            nhapTayTexbox.Visible = false;
            nhapTayTexbox.Size = new Size(40, 40);
            nhapTayTexbox.Font = new Font("Consolas", 40 / 2, FontStyle.Bold);
            this.Controls.Add(nhapTayTexbox);
                      
            nhapTayTexbox.KeyPress += new KeyPressEventHandler(nhapTayTexbox_KeyPress);
            nhapTayTexbox.KeyDown += new KeyEventHandler(nhapTayTexbox_KeyDown);
            nhapTayTexbox.TextChanged += new EventHandler(nhapTayTexbox_TextChanged);
            nhapTayTexbox.LostFocus += new EventHandler(nhapTayTexbox_LostFocus);
        }

        private void Node_GotFocus(object sender, EventArgs e)
        {
            if (nhapTayTexbox.Enabled == true)   // Nếu textbox bị tắt (khi node đang sắp xếp) thì texbox không đc bật lên để sửa
            {
                nhapTayTexbox.BackColor = this.BackColor;
                nhapTayTexbox.Visible = true;
                nhapTayTexbox.Text = this.Text;
                nhapTayTexbox.SelectAll();
                nhapTayTexbox.Focus();
            }
        }
       
        //Sau khi kết thúc nhập textbox, ghi nhận giá trị.
        private void nhapTayTexbox_LostFocus(object sender, EventArgs e)
        {
            nhapTayTexbox.Visible = false;
            this.Text = nhapTayTexbox.Text;
            this.giaTri = int.Parse(nhapTayTexbox.Text);
             
        }

        private void nhapTayTexbox_TextChanged(object sender, EventArgs e)
        {
            if (nhapTayTexbox.Text.Count() == 0)
            {
                nhapTayTexbox.Text = "0";
                nhapTayTexbox.SelectAll();
                nhapTayTexbox.Focus();
                this.giaTri=int.Parse(nhapTayTexbox.Text);
            }
        }

        private void nhapTayTexbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nhapTayTexbox.Visible = false;
                this.Text = nhapTayTexbox.Text;
            }

        }

        private void nhapTayTexbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')    // nếu là nút BackSpace thì bỏ qua bước check này --> cho phép nút Backspace hoạt động
            {
                e.Handled = !char.IsNumber(e.KeyChar);
                // Nếu   : Handled == true thì event bị hủy
                // Nhưng : [isNumber(True) + not] = false --> Handled = false --> cho phép nhập --> nếu là số thì cho phép nhập
                if (nhapTayTexbox.Text.Count() == 0)
                {
                    nhapTayTexbox.Text = "0";
                }
            }
        }

    }
}
