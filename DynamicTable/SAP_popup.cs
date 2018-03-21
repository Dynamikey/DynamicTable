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

namespace RollsRoyceRNApp
{
    public partial class SAP_popup : Form
    {
        UI_Base ui = new UI_Base();
        public List<RepairData> repairDataList2 { get; set; }
        int rowIndex;
        List<WebCameraId> cameras;
        string condition = "";
        bool raiseRDR;
        string imagePath;
        public Bitmap image;
        string datetime;

        //Called when SAP_popup form run
        public SAP_popup(int rowIndexTemp, List<RepairData> list)
        {
            InitializeComponent();
            repairDataList2 = list;
            rowIndex = rowIndexTemp;
        }

        //Handles condition button clicks
        private void serviceableButton_Click(object sender, EventArgs e)
        {
            serviceableButton.Font = new Font(serviceableButton.Font, FontStyle.Bold);
            unsalvageableButton.Font = new Font(unsalvageableButton.Font, FontStyle.Regular);
            salvageableButton.Font = new Font(salvageableButton.Font, FontStyle.Regular);

            condition =  "Serviceable";
            updateSAPComment(this, EventArgs.Empty);
            
        }
        private void salvageableButton_Click_1(object sender, EventArgs e)
        {
            salvageableButton.Font = new Font(salvageableButton.Font, FontStyle.Bold);
            serviceableButton.Font = new Font(serviceableButton.Font, FontStyle.Regular);
            unsalvageableButton.Font = new Font(unsalvageableButton.Font, FontStyle.Regular);

            condition = "Salvageable";
            updateSAPComment(this, EventArgs.Empty);
        }
        private void unsalvageableButton_Click_1(object sender, EventArgs e)
        {
            unsalvageableButton.Font = new Font(unsalvageableButton.Font, FontStyle.Bold);
            serviceableButton.Font = new Font(serviceableButton.Font, FontStyle.Regular);
            salvageableButton.Font = new Font(salvageableButton.Font, FontStyle.Regular);

            condition = "Unsalvageable";
            updateSAPComment(this, EventArgs.Empty);

        }

        //When user clicks 'generate sap comment# button
        private void generateSAPCommentButton_Click(object sender, EventArgs e)
        {
            if (condition != "") //Only do the following if a condition has been provided
            {
                if (webCameraControl1.IsCapturing)
                {
                    webCameraControl1.StopCapture();
                    webCameraControl1.Visible = false;
                }

                try
                {
                    //save image
                    datetime = $"{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}_{DateTime.Now.Year.ToString()}_{DateTime.Now.Hour.ToString()}_{DateTime.Now.Minute.ToString()}_{DateTime.Now.Second.ToString()}";
                    imagePath = $"{UI_Base.path}CameraPicsFolder\\{repairDataList2[rowIndex].headingNumber}_{condition}_{datetime}.jpeg";
                    image.Save(imagePath, ImageFormat.Jpeg);
                }
                catch (Exception)
                {
                    Console.WriteLine("Can't save file");
                }

                //update repair data list
                repairDataList2[rowIndex] = new RepairData(repairDataList2[rowIndex], condition, comboBox1.Text, textBox1.Text + comboBox2.Text, textBox2.Text, SAP_Preview.Text, imagePath, raiseRDR);
                repairDataList2[rowIndex] = new RepairData(repairDataList2[rowIndex], true);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }

        //Handles taking picture, does not save it yet (CURRENTLY ISSUES ON TABLET, WORKS FINE ON TEST PC) - we think it's a hardware issue, although it seems to work when you turn the tablet screen off for 5+ seconds or rotate screen.
        void cameraButton_Click(object sender, EventArgs e)
        {

            if (!webCameraControl1.IsCapturing) //Start showing video feed
            {
                cameras = new List<WebCameraId>(webCameraControl1.GetVideoCaptureDevices());
                webCameraControl1.Visible = false;
                if (cameras.Count > 0)
                    webCameraControl1.StartCapture(cameras[0]); // <---- Change to [1] for rear camera

                webCameraControl1.Visible = true;
                webCameraControl1.BringToFront();
            }
            else if (webCameraControl1.IsCapturing) //Take still image
            {
                image = webCameraControl1.GetCurrentImage();
                CameraPreview.Image = image;
                
                CameraPreview.BringToFront();
                webCameraControl1.Visible = false;
                webCameraControl1.StopCapture();
            }            
        }

        //Updates SAP preview text
        private void updateSAPComment(object sender, EventArgs e)
        {
            SAP_Preview.Text = $@"{repairDataList2[rowIndex].headingNumber} {repairDataList2[rowIndex].headingName} 
Part is: {condition}
Damage: {comboBox1.Text}
Amount: {textBox1.Text + comboBox2.Text}
Further Comment: {textBox2.Text}
Raise RDR: {raiseRDR}";
        }

        //Handles X button click
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Handles raise RDR button click
        private void raiseRDRButton_Click(object sender, EventArgs e)
        {
            raiseRDR = !raiseRDR;
            if(raiseRDR) raiseRDRButton.Font = new Font(raiseRDRButton.Font, FontStyle.Bold);
            else raiseRDRButton.Font = new Font(raiseRDRButton.Font, FontStyle.Strikeout);
            updateSAPComment(this, EventArgs.Empty);
        }
    }
}
