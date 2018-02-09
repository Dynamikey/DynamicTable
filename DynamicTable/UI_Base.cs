using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicTable
{
    public partial class UI_Base : Form
    {

        int PW;
        bool Hiden;
        public UI_Base()
        {
            InitializeComponent();
            PW = Slide_Panel.Width;
            Hiden = false;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Start();
        }



        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (Hiden)
            {
                Slide_Panel.Width = Slide_Panel.Width + 20;
                if (Slide_Panel.Width >= PW)
                {
                    timer1.Stop();
                    Hiden = false;
                    this.Refresh();
                }

            }
            else
            {
                Slide_Panel.Width = Slide_Panel.Width - 20;
                if (Slide_Panel.Width <= 0)
                {
                    timer1.Stop();
                    Hiden = true;
                    this.Refresh();
                }

            }
        }
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Inspector_ID_Nxt_btn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
        }
    }
}
