using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
namespace PhanMemXemPhimSongNgu
{
    public partial class Form1 : Form
    {
        static bool kt = false;
        static string filesub1;
        static string filesub2;
        static int index=0;
        static long timeStart = 0;
        static long timeEnd = 0;
        static double temp;
        static string sub;
        static List<Match> list1 =new List<Match>();
        static List<Match> list2 = new List<Match>();
        private static Regex unit = new Regex(
           @"(?<sequence>\d+)\r\n(?<start>\d{2}\:\d{2}\:\d{2},\d{3}) --\> (?<end>\d{2}\:\d{2}\:\d{2},\d{3})\r\n(?<text>[\s\S]*?\r\n\r\n)",
           RegexOptions.Compiled | RegexOptions.ECMAScript);
        public Form1()
        {
            InitializeComponent();
        }

        private void btnchoosefile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfileDialog1 = new OpenFileDialog();
            if (openfileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtPath.Text = openfileDialog1.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = this.txtPath.Text;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            kt = true;
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
               this.txt1.Text = list1[index].Groups[4].Value;
            
        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
               this.textBox1.Text = list2[index].Groups[4].Value;
        }


        static void readData1()
        {   
            using (StreamReader r = new StreamReader( Form1.filesub1 ))
            {
                string line,ans = "";
                while ((line = r.ReadLine()) != null )
                {
                    if (line == "")
                    {
                        ans = ans + "\r\n";
                        Match m = unit.Match(ans);
                        if (m.Success)
                            list1.Add(m);
                        ans = "";
                    }
                    else ans = ans + line + "\r\n";
                }
            }

           
        }
        static void readData2()
        {
            using (StreamReader r = new StreamReader(Form1.filesub2))
            {
                
                string line, ans = "";
                while ((line = r.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        ans = ans + "\r\n";
                        Match m = unit.Match(ans);
                        if (m.Success)
                            list2.Add(m);
                        ans = "";
                    }
                    else ans = ans + line + "\r\n";
                }
            }
        }
        static private bool isReady()
        {
            if ((Form1.filesub1 != null) && (Form1.filesub2!=null) && (kt==true)) return true;
            return false;
        }
         static bool isNext()
        {
            if (temp >= timeStart && temp <= timeEnd)
                return false;
            else return true;
           /* {
                index++;
                timeStart = convertMs(list[index].Groups[2].Value);
                timeEnd = convertMs(list[index].Groups[3].Value);

                if (temp >= timeStart && temp <= timeEnd)
                    return false;
            }*/
          
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // this.txtPath.Text = index.ToString();
            if (isReady())
            {
              
                temp = 1000 * axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                timeStart = convertMs(list1[index].Groups[2].Value);
                timeEnd = convertMs(list1[index].Groups[3].Value);
                textBox1_TextChanged(sender, e);
                textBox1_TextChanged_1(sender, e);
                if (temp > timeEnd)
                    index = index + 1;
            }
               // timer1.Start();
        }
        static long convertMs(string time)
        {
            long ans = 0;
            string h, m, s, ms;

            h = time.Substring(0, 2);
            m = time.Substring(3, 2);
            s = time.Substring(6, 2);
            ms = time.Substring(9, 3);

            ans = Int32.Parse(h) * 60 * 60 * 1000 + Int32.Parse(m) * 60 * 1000 + Int32.Parse(s) * 1000 + Int32.Parse(m);
            return ans;
        }

    
        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog openfileDialog1 = new OpenFileDialog();
            if (openfileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                 filesub2 = openfileDialog1.FileName;
                 readData2();
            }

        }

        private void btnSub1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfileDialog1 = new OpenFileDialog();
            if (openfileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                 filesub1 = openfileDialog1.FileName;
                 readData1();
            }

        }
    }
}
