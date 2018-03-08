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
using System.Threading;
using System.Drawing.Imaging;

namespace DynamicTable
{
    public partial class SAP_popup : Form
    {
        UI_Base ui = new UI_Base();
        public List<RepairData> repairDataList2 { get; set; }
        int rowIndex;
       // WebCameraControl webCameraControl1 = new WebCameraControl();
        List<WebCameraId> cameras;
        string condition = "";
        string imagePath;
        public Bitmap image;
        string datetime;

        public SAP_popup(int rowIndexTemp, List<RepairData> list)
        {
            InitializeComponent();
            repairDataList2 = list;
            rowIndex = rowIndexTemp;

            //webCameraControl1.Location = new System.Drawing.Point(801, 13);
            // webCameraControl1.Size = new System.Drawing.Size(532, 368);
            // this.Controls.Add(this.webCameraControl1);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Font = new Font(button1.Font, FontStyle.Bold);
            button2.Font = new Font(button2.Font, FontStyle.Regular);
            button3.Font = new Font(button3.Font, FontStyle.Regular);

            condition =  "Serviceable";
            updateSAPComment(this, EventArgs.Empty);
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            button3.Font = new Font(button3.Font, FontStyle.Bold);
            button1.Font = new Font(button1.Font, FontStyle.Regular);
            button2.Font = new Font(button2.Font, FontStyle.Regular);

            condition = "Salvageable";
            updateSAPComment(this, EventArgs.Empty);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button2.Font = new Font(button2.Font, FontStyle.Bold);
            button1.Font = new Font(button1.Font, FontStyle.Regular);
            button3.Font = new Font(button3.Font, FontStyle.Regular);

            condition = "Unsalvageable";
            updateSAPComment(this, EventArgs.Empty);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (condition != "")
            {
                if (webCameraControl1.IsCapturing)
                {
                    webCameraControl1.StopCapture();
                    webCameraControl1.Visible = false;
                }

                try
                {
                    datetime = $"{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}_{DateTime.Now.Year.ToString()}_{DateTime.Now.Hour.ToString()}_{DateTime.Now.Minute.ToString()}_{DateTime.Now.Second.ToString()}";
                    imagePath = $"{Program.path}CameraPicsFolder\\{repairDataList2[rowIndex].headingNumber}_{condition}_{datetime}.jpeg";
                    image.Save(imagePath, ImageFormat.Jpeg);
                }
                catch (Exception)
                {
                    Console.WriteLine("Can't save file");
                }


                repairDataList2[rowIndex] = new RepairData(repairDataList2[rowIndex], condition, comboBox1.Text, textBox1.Text + comboBox2.Text, textBox2.Text, SAP_Preview.Text, imagePath);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }

        void button6_Click(object sender, EventArgs e)
        {

            if (!webCameraControl1.IsCapturing)
            {
                cameras = new List<WebCameraId>(webCameraControl1.GetVideoCaptureDevices());
                webCameraControl1.Visible = false;
                if (cameras.Count > 0)
                    webCameraControl1.StartCapture(cameras[0]);

                webCameraControl1.Visible = true;
                webCameraControl1.BringToFront();
            }
            else if (webCameraControl1.IsCapturing)
            {
                image = webCameraControl1.GetCurrentImage();
                CameraPreview.Image = image;
                
                CameraPreview.BringToFront();
                webCameraControl1.Visible = false;
                webCameraControl1.StopCapture();
            }

            
        }

        private void updateSAPComment(object sender, EventArgs e)
        {

            SAP_Preview.Text = $@"{repairDataList2[rowIndex].headingNumber} {repairDataList2[rowIndex].headingName} 
Part is: {condition}
Damage: {comboBox1.Text}
Amount: {textBox1.Text + comboBox2.Text}
Further Comment: {textBox2.Text}";
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
