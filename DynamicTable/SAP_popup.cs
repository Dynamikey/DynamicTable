using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebEye.Controls.WinForms.WebCameraControl;

namespace DynamicTable
{
    public partial class SAP_popup : Form
    {
        UI_Base ui = new UI_Base();
        public List<RepairData> repairDataList2 { get; set; }
        int rowIndex;
        WebCameraControl webCameraControl1 = new WebCameraControl();
        
        public SAP_popup(int rowIndexTemp, List<RepairData> list)
        {
            InitializeComponent();
            repairDataList2 = list;
            rowIndex = rowIndexTemp;

            webCameraControl1.Location = new System.Drawing.Point(801, 13);
            webCameraControl1.Size = new System.Drawing.Size(532, 368);
            this.Controls.Add(this.webCameraControl1);


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
            if (webCameraControl1.IsCapturing)
            {
                webCameraControl1.StopCapture();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (!webCameraControl1.IsCapturing)
            {
                webCameraControl1.BringToFront();
                List<WebCameraId> cameras = new List<WebCameraId>(webCameraControl1.GetVideoCaptureDevices());
                if (cameras.Count > 0)
                    webCameraControl1.StartCapture(cameras[0]);
            }
            else if (webCameraControl1.IsCapturing)
            {
                Bitmap image = webCameraControl1.GetCurrentImage();
                CameraPreview.Image = image;
                webCameraControl1.StopCapture();
                CameraPreview.BringToFront();
            }

            
        }
    }
}
