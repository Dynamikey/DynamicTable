namespace RollsRoyceRNApp
{
    partial class SAP_popup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SAP_popup));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serviceableButton = new System.Windows.Forms.Button();
            this.unsalvageableButton = new System.Windows.Forms.Button();
            this.salvageableButton = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape5 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape6 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape4 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape3 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SAP_Preview = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.webCameraControl1 = new WebEye.Controls.WinForms.WebCameraControl.WebCameraControl();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.CameraPreview = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.raiseRDRButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CameraPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Dent",
            "Wear",
            "Blockage",
            "Crack",
            "Corrosion"});
            this.comboBox1.Location = new System.Drawing.Point(18, 57);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(346, 38);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.TextChanged += new System.EventHandler(this.updateSAPComment);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.label1.Location = new System.Drawing.Point(14, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 45);
            this.label1.TabIndex = 1;
            this.label1.Text = "Damage Type";
            // 
            // serviceableButton
            // 
            this.serviceableButton.BackColor = System.Drawing.Color.LimeGreen;
            this.serviceableButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.serviceableButton.FlatAppearance.BorderSize = 0;
            this.serviceableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.serviceableButton.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.serviceableButton.Location = new System.Drawing.Point(394, 12);
            this.serviceableButton.Name = "serviceableButton";
            this.serviceableButton.Size = new System.Drawing.Size(376, 108);
            this.serviceableButton.TabIndex = 2;
            this.serviceableButton.Text = "Serviceable";
            this.serviceableButton.UseVisualStyleBackColor = false;
            this.serviceableButton.Click += new System.EventHandler(this.serviceableButton_Click);
            // 
            // unsalvageableButton
            // 
            this.unsalvageableButton.BackColor = System.Drawing.Color.Tomato;
            this.unsalvageableButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.unsalvageableButton.FlatAppearance.BorderSize = 0;
            this.unsalvageableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.unsalvageableButton.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.unsalvageableButton.Location = new System.Drawing.Point(394, 273);
            this.unsalvageableButton.Name = "unsalvageableButton";
            this.unsalvageableButton.Size = new System.Drawing.Size(376, 108);
            this.unsalvageableButton.TabIndex = 3;
            this.unsalvageableButton.Text = "Unsalvageable";
            this.unsalvageableButton.UseVisualStyleBackColor = false;
            this.unsalvageableButton.Click += new System.EventHandler(this.unsalvageableButton_Click_1);
            // 
            // salvageableButton
            // 
            this.salvageableButton.BackColor = System.Drawing.Color.Gold;
            this.salvageableButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.salvageableButton.FlatAppearance.BorderSize = 0;
            this.salvageableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.salvageableButton.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.salvageableButton.Location = new System.Drawing.Point(394, 143);
            this.salvageableButton.Name = "salvageableButton";
            this.salvageableButton.Size = new System.Drawing.Size(376, 108);
            this.salvageableButton.TabIndex = 4;
            this.salvageableButton.Text = "Salvageable";
            this.salvageableButton.UseVisualStyleBackColor = false;
            this.salvageableButton.Click += new System.EventHandler(this.salvageableButton_Click_1);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape5,
            this.rectangleShape6,
            this.rectangleShape4,
            this.rectangleShape3,
            this.rectangleShape2,
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1830, 400);
            this.shapeContainer1.TabIndex = 5;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape5
            // 
            this.rectangleShape5.BackColor = System.Drawing.SystemColors.Control;
            this.rectangleShape5.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape5.CornerRadius = 5;
            this.rectangleShape5.Cursor = System.Windows.Forms.Cursors.Default;
            this.rectangleShape5.FillColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape5.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape5.Location = new System.Drawing.Point(1606, 12);
            this.rectangleShape5.Name = "rectangleShape6";
            this.rectangleShape5.Size = new System.Drawing.Size(204, 166);
            // 
            // rectangleShape6
            // 
            this.rectangleShape6.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape6.CornerRadius = 5;
            this.rectangleShape6.FillColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape6.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape6.Location = new System.Drawing.Point(1360, 12);
            this.rectangleShape6.Name = "rectangleShape6";
            this.rectangleShape6.Size = new System.Drawing.Size(217, 166);
            // 
            // rectangleShape4
            // 
            this.rectangleShape4.BorderColor = System.Drawing.Color.MidnightBlue;
            this.rectangleShape4.CornerRadius = 5;
            this.rectangleShape4.FillColor = System.Drawing.Color.MidnightBlue;
            this.rectangleShape4.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape4.Location = new System.Drawing.Point(1606, 202);
            this.rectangleShape4.Name = "rectangleShape4";
            this.rectangleShape4.Size = new System.Drawing.Size(204, 180);
            // 
            // rectangleShape3
            // 
            this.rectangleShape3.BackColor = System.Drawing.Color.Tomato;
            this.rectangleShape3.BorderColor = System.Drawing.Color.Tomato;
            this.rectangleShape3.CornerRadius = 5;
            this.rectangleShape3.FillColor = System.Drawing.Color.Tomato;
            this.rectangleShape3.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape3.Location = new System.Drawing.Point(390, 267);
            this.rectangleShape3.Name = "rectangleShape3";
            this.rectangleShape3.Size = new System.Drawing.Size(383, 114);
            // 
            // rectangleShape2
            // 
            this.rectangleShape2.BackColor = System.Drawing.Color.Gold;
            this.rectangleShape2.BorderColor = System.Drawing.Color.Gold;
            this.rectangleShape2.CornerRadius = 5;
            this.rectangleShape2.FillColor = System.Drawing.Color.Gold;
            this.rectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape2.Location = new System.Drawing.Point(394, 137);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.Size = new System.Drawing.Size(378, 117);
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.Color.LimeGreen;
            this.rectangleShape1.BorderColor = System.Drawing.Color.LimeGreen;
            this.rectangleShape1.CornerRadius = 5;
            this.rectangleShape1.FillColor = System.Drawing.Color.LimeGreen;
            this.rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape1.Location = new System.Drawing.Point(391, 8);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(382, 115);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.MidnightBlue;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(1610, 202);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(192, 175);
            this.button5.TabIndex = 7;
            this.button5.Text = "Generate SAP Note";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.generateSAPCommentButton_Click);
            // 
            // inspectorIDTextbox
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(22, 153);
            this.textBox1.Name = "inspectorIDTextbox";
            this.textBox1.Size = new System.Drawing.Size(207, 35);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextChanged += new System.EventHandler(this.updateSAPComment);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.label2.Location = new System.Drawing.Point(14, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 45);
            this.label2.TabIndex = 9;
            this.label2.Text = "Measurement";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.label3.Location = new System.Drawing.Point(14, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(286, 45);
            this.label3.TabIndex = 11;
            this.label3.Text = "Further Comments";
            // 
            // engineIDTextbox
            // 
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(19, 238);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "engineIDTextbox";
            this.textBox2.Size = new System.Drawing.Size(345, 144);
            this.textBox2.TabIndex = 10;
            this.textBox2.TextChanged += new System.EventHandler(this.updateSAPComment);
            // 
            // SAP_Preview
            // 
            this.SAP_Preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SAP_Preview.Location = new System.Drawing.Point(801, 13);
            this.SAP_Preview.Multiline = true;
            this.SAP_Preview.Name = "SAP_Preview";
            this.SAP_Preview.Size = new System.Drawing.Size(532, 369);
            this.SAP_Preview.TabIndex = 12;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button6.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(1367, 19);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(206, 157);
            this.button6.TabIndex = 13;
            this.button6.Text = "Take photo";
            this.button6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.cameraButton_Click);
            // 
            // webCameraControl1
            // 
            this.webCameraControl1.Location = new System.Drawing.Point(801, 13);
            this.webCameraControl1.Name = "webCameraControl1";
            this.webCameraControl1.Size = new System.Drawing.Size(532, 368);
            this.webCameraControl1.TabIndex = 16;
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.ItemHeight = 30;
            this.comboBox2.Items.AddRange(new object[] {
            "µm",
            "mm",
            "cm"});
            this.comboBox2.Location = new System.Drawing.Point(236, 153);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(124, 38);
            this.comboBox2.TabIndex = 17;
            this.comboBox2.TextChanged += new System.EventHandler(this.updateSAPComment);
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ExitBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ExitBtn.FlatAppearance.BorderSize = 0;
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitBtn.Font = new System.Drawing.Font("Segoe UI Symbol", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitBtn.Location = new System.Drawing.Point(1788, 0);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(36, 48);
            this.ExitBtn.TabIndex = 18;
            this.ExitBtn.Text = "x";
            this.ExitBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ExitBtn.UseVisualStyleBackColor = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // CameraPreview
            // 
            this.CameraPreview.Location = new System.Drawing.Point(1360, 202);
            this.CameraPreview.Name = "CameraPreview";
            this.CameraPreview.Size = new System.Drawing.Size(218, 181);
            this.CameraPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CameraPreview.TabIndex = 15;
            this.CameraPreview.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1418, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(104, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // raiseRDRButton
            // 
            this.raiseRDRButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.raiseRDRButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.raiseRDRButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raiseRDRButton.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.raiseRDRButton.Location = new System.Drawing.Point(1611, 19);
            this.raiseRDRButton.Name = "raiseRDRButton";
            this.raiseRDRButton.Size = new System.Drawing.Size(194, 155);
            this.raiseRDRButton.TabIndex = 19;
            this.raiseRDRButton.Text = "Raise RDR";
            this.raiseRDRButton.UseVisualStyleBackColor = false;
            this.raiseRDRButton.Click += new System.EventHandler(this.raiseRDRButton_Click);
            // 
            // SAP_popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1830, 400);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.raiseRDRButton);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.CameraPreview);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.SAP_Preview);
            this.Controls.Add(this.webCameraControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.salvageableButton);
            this.Controls.Add(this.unsalvageableButton);
            this.Controls.Add(this.serviceableButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(96, 180);
            this.Name = "SAP_popup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SAP_Comment_pop";
            this.Load += new System.EventHandler(this.updateSAPComment);
            ((System.ComponentModel.ISupportInitialize)(this.CameraPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button serviceableButton;
        private System.Windows.Forms.Button unsalvageableButton;
        private System.Windows.Forms.Button salvageableButton;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape3;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox SAP_Preview;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape6;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox CameraPreview;
        private WebEye.Controls.WinForms.WebCameraControl.WebCameraControl webCameraControl1;
        private System.Windows.Forms.ComboBox comboBox2;
        protected System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Button raiseRDRButton;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape5;
    }
}