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
    public partial class OpeningScreen : Form
    {
        public OpeningScreen()
        {
            InitializeComponent();
            Op = pictureBox1.this.opacity;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start;
        }
    }
}
