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
    public partial class SAP_popup : Form
    {
        UI_Base ui = new UI_Base();
        public List<RepairData> repairDataList2 { get; set; }
        int rowIndex;
        public SAP_popup(int rowIndexTemp, List<RepairData> list)
        {
            InitializeComponent();
            repairDataList2 = list;
            rowIndex = rowIndexTemp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Font = new Font(button1.Font, FontStyle.Bold);
            button2.Font = new Font(button2.Font, FontStyle.Regular);
            button3.Font = new Font(button3.Font, FontStyle.Regular);

            repairDataList2[rowIndex] = new RepairData(repairDataList2[rowIndex], "Serviceable");
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            button3.Font = new Font(button3.Font, FontStyle.Bold);
            button1.Font = new Font(button1.Font, FontStyle.Regular);
            button2.Font = new Font(button2.Font, FontStyle.Regular);

            repairDataList2[rowIndex] = new RepairData(repairDataList2[rowIndex], "Salvageable");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button2.Font = new Font(button2.Font, FontStyle.Bold);
            button1.Font = new Font(button1.Font, FontStyle.Regular);
            button3.Font = new Font(button3.Font, FontStyle.Regular);

            repairDataList2[rowIndex] = new RepairData(repairDataList2[rowIndex], "Unsalvageable");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }


    }
}
