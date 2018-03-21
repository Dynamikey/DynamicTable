using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace RollsRoyceRNApp
{
    public partial class OpeningScreen : Form
    {
        

        public OpeningScreen()
        {
            InitializeComponent();
            this.Close();
        }

        public void CloseForm()
        {
            this.Close();
        }
    }
}
