using System.Windows.Forms;

namespace DynamicTable
{
    partial class UI_Base
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_Base));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.Slide_Panel = new System.Windows.Forms.Panel();
            this.Part_Name = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Inspector_ID_Label = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Inspector_ID_Nxt_btn = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Engine_nb_lbl = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape4 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape3 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer3 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape6 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape5 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.generalDataGridView = new System.Windows.Forms.DataGridView();
            this.button5 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Main_Picture_Box = new System.Windows.Forms.PictureBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Slide_Panel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.generalDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_Picture_Box)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ExitBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitBtn.Font = new System.Drawing.Font("Segoe UI Symbol", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitBtn.Location = new System.Drawing.Point(1856, 2);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(64, 64);
            this.ExitBtn.TabIndex = 0;
            this.ExitBtn.Text = "x";
            this.ExitBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ExitBtn.UseVisualStyleBackColor = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.MidnightBlue;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 1080);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Slide_Panel
            // 
            this.Slide_Panel.BackColor = System.Drawing.SystemColors.Control;
            this.Slide_Panel.Controls.Add(this.Part_Name);
            this.Slide_Panel.Location = new System.Drawing.Point(96, 0);
            this.Slide_Panel.Name = "Slide_Panel";
            this.Slide_Panel.Size = new System.Drawing.Size(400, 1080);
            this.Slide_Panel.TabIndex = 9;
            // 
            // Part_Name
            // 
            this.Part_Name.AutoSize = true;
            this.Part_Name.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Part_Name.Location = new System.Drawing.Point(5, 9);
            this.Part_Name.Name = "Part_Name";
            this.Part_Name.Size = new System.Drawing.Size(191, 42);
            this.Part_Name.TabIndex = 0;
            this.Part_Name.Text = "U1111 > Engine XXYYZZ \r\nPart XXYYZZ > RN XXYYZZ";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 20);
            this.tabControl1.Location = new System.Drawing.Point(48, -25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(48, 1);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1924, 1113);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.Inspector_ID_Label);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.Inspector_ID_Nxt_btn);
            this.tabPage1.Controls.Add(this.shapeContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1916, 1085);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // Inspector_ID_Label
            // 
            this.Inspector_ID_Label.AutoSize = true;
            this.Inspector_ID_Label.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.Inspector_ID_Label.Location = new System.Drawing.Point(462, 194);
            this.Inspector_ID_Label.Name = "Inspector_ID_Label";
            this.Inspector_ID_Label.Size = new System.Drawing.Size(200, 45);
            this.Inspector_ID_Label.TabIndex = 3;
            this.Inspector_ID_Label.Text = "Inspector ID:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.textBox1.Location = new System.Drawing.Point(470, 250);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(386, 43);
            this.textBox1.TabIndex = 1;
            // 
            // Inspector_ID_Nxt_btn
            // 
            this.Inspector_ID_Nxt_btn.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Inspector_ID_Nxt_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.Inspector_ID_Nxt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Inspector_ID_Nxt_btn.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.Inspector_ID_Nxt_btn.ForeColor = System.Drawing.Color.White;
            this.Inspector_ID_Nxt_btn.Location = new System.Drawing.Point(1028, 227);
            this.Inspector_ID_Nxt_btn.Name = "Inspector_ID_Nxt_btn";
            this.Inspector_ID_Nxt_btn.Size = new System.Drawing.Size(358, 92);
            this.Inspector_ID_Nxt_btn.TabIndex = 0;
            this.Inspector_ID_Nxt_btn.Text = "Next";
            this.Inspector_ID_Nxt_btn.UseVisualStyleBackColor = false;
            this.Inspector_ID_Nxt_btn.Click += new System.EventHandler(this.Inspector_ID_Nxt_btn_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 3);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape2,
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1910, 1079);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape2
            // 
            this.rectangleShape2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.rectangleShape2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape2.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.rectangleShape2.CornerRadius = 5;
            this.rectangleShape2.Location = new System.Drawing.Point(1020, 220);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.Size = new System.Drawing.Size(368, 100);
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape1.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape1.CornerRadius = 5;
            this.rectangleShape1.Location = new System.Drawing.Point(460, 240);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(410, 56);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.Engine_nb_lbl);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.shapeContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1916, 1085);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // Engine_nb_lbl
            // 
            this.Engine_nb_lbl.AutoSize = true;
            this.Engine_nb_lbl.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.Engine_nb_lbl.Location = new System.Drawing.Point(462, 194);
            this.Engine_nb_lbl.Name = "Engine_nb_lbl";
            this.Engine_nb_lbl.Size = new System.Drawing.Size(242, 45);
            this.Engine_nb_lbl.TabIndex = 4;
            this.Engine_nb_lbl.Text = "Engine Number";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1028, 227);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(358, 92);
            this.button2.TabIndex = 3;
            this.button2.Text = "Next";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.textBox2.Location = new System.Drawing.Point(470, 250);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(386, 43);
            this.textBox2.TabIndex = 2;
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(3, 3);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape4,
            this.rectangleShape3});
            this.shapeContainer2.Size = new System.Drawing.Size(1910, 1079);
            this.shapeContainer2.TabIndex = 0;
            this.shapeContainer2.TabStop = false;
            // 
            // rectangleShape4
            // 
            this.rectangleShape4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape4.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape4.CornerRadius = 5;
            this.rectangleShape4.Location = new System.Drawing.Point(460, 240);
            this.rectangleShape4.Name = "rectangleShape4";
            this.rectangleShape4.Size = new System.Drawing.Size(410, 56);
            // 
            // rectangleShape3
            // 
            this.rectangleShape3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.rectangleShape3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape3.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.rectangleShape3.CornerRadius = 5;
            this.rectangleShape3.Location = new System.Drawing.Point(1020, 220);
            this.rectangleShape3.Name = "rectangleShape3";
            this.rectangleShape3.Size = new System.Drawing.Size(368, 100);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.textBox3);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.shapeContainer3);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1916, 1085);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.textBox3.Location = new System.Drawing.Point(470, 250);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(386, 43);
            this.textBox3.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(1028, 227);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(358, 92);
            this.button3.TabIndex = 6;
            this.button3.Text = "Next";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.label1.Location = new System.Drawing.Point(462, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 45);
            this.label1.TabIndex = 5;
            this.label1.Text = "Part Number";
            // 
            // shapeContainer3
            // 
            this.shapeContainer3.Location = new System.Drawing.Point(3, 3);
            this.shapeContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer3.Name = "shapeContainer3";
            this.shapeContainer3.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape6,
            this.rectangleShape5});
            this.shapeContainer3.Size = new System.Drawing.Size(1910, 1079);
            this.shapeContainer3.TabIndex = 8;
            this.shapeContainer3.TabStop = false;
            // 
            // rectangleShape6
            // 
            this.rectangleShape6.BackColor = System.Drawing.SystemColors.ControlDark;
            this.rectangleShape6.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape6.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.rectangleShape6.CornerRadius = 5;
            this.rectangleShape6.Location = new System.Drawing.Point(1020, 220);
            this.rectangleShape6.Name = "rectangleShape6";
            this.rectangleShape6.Size = new System.Drawing.Size(368, 100);
            // 
            // rectangleShape5
            // 
            this.rectangleShape5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape5.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape5.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape5.CornerRadius = 5;
            this.rectangleShape5.Location = new System.Drawing.Point(460, 240);
            this.rectangleShape5.Name = "rectangleShape5";
            this.rectangleShape5.Size = new System.Drawing.Size(410, 56);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.Controls.Add(this.tableLayoutPanel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1916, 1085);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 644F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 825F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(76, 45);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1743, 982);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 35F);
            this.label6.Location = new System.Drawing.Point(789, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.label6.Size = new System.Drawing.Size(126, 112);
            this.label6.TabIndex = 6;
            this.label6.Text = "P.C.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 35F);
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.label4.Size = new System.Drawing.Size(638, 112);
            this.label4.TabIndex = 4;
            this.label4.Text = "Description";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 35F);
            this.label7.Location = new System.Drawing.Point(921, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.label7.Size = new System.Drawing.Size(819, 112);
            this.label7.TabIndex = 7;
            this.label7.Text = "Repair Note";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 35F);
            this.label5.Location = new System.Drawing.Point(647, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.label5.Size = new System.Drawing.Size(136, 112);
            this.label5.TabIndex = 5;
            this.label5.Text = "ML";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.White;
            this.tabPage5.Controls.Add(this.listView1);
            this.tabPage5.Controls.Add(this.generalDataGridView);
            this.tabPage5.Controls.Add(this.button5);
            this.tabPage5.Controls.Add(this.pictureBox1);
            this.tabPage5.Controls.Add(this.dataGridView1);
            this.tabPage5.Controls.Add(this.Main_Picture_Box);
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1916, 1085);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "tabPage5";
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Location = new System.Drawing.Point(989, 873);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(844, 171);
            this.listView1.TabIndex = 16;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // generalDataGridView
            // 
            this.generalDataGridView.AllowUserToAddRows = false;
            this.generalDataGridView.AllowUserToDeleteRows = false;
            this.generalDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.generalDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.generalDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.generalDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.generalDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.generalDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.generalDataGridView.DefaultCellStyle = dataGridViewCellStyle14;
            this.generalDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.generalDataGridView.GridColor = System.Drawing.Color.LightGray;
            this.generalDataGridView.Location = new System.Drawing.Point(48, 3);
            this.generalDataGridView.MaximumSize = new System.Drawing.Size(912, 1080);
            this.generalDataGridView.MinimumSize = new System.Drawing.Size(912, 1080);
            this.generalDataGridView.MultiSelect = false;
            this.generalDataGridView.Name = "generalDataGridView";
            this.generalDataGridView.ReadOnly = true;
            this.generalDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generalDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.generalDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.generalDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.generalDataGridView.Size = new System.Drawing.Size(912, 1080);
            this.generalDataGridView.TabIndex = 13;
            this.generalDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.generalDataGridView_CellClick);
            this.generalDataGridView.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.generalDataGridView_ColumnAdded);
            this.generalDataGridView.SelectionChanged += new System.EventHandler(this.generalDataGridView_SelectionChanged);
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(989, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(68, 45);
            this.button5.TabIndex = 12;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(989, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(854, 839);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.GridColor = System.Drawing.Color.LightGray;
            this.dataGridView1.Location = new System.Drawing.Point(44, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(919, 1080);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded_1);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Main_Picture_Box
            // 
            this.Main_Picture_Box.Location = new System.Drawing.Point(15, -12);
            this.Main_Picture_Box.Name = "Main_Picture_Box";
            this.Main_Picture_Box.Size = new System.Drawing.Size(399, 516);
            this.Main_Picture_Box.TabIndex = 0;
            this.Main_Picture_Box.TabStop = false;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.White;
            this.tabPage6.Controls.Add(this.pictureBox2);
            this.tabPage6.Location = new System.Drawing.Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1916, 1085);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "tabPage6";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(499, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(399, 516);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.MidnightBlue;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, -1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(95, 113);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.MidnightBlue;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(52, 540);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 40);
            this.label2.TabIndex = 12;
            this.label2.Text = ">";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UI_Base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.Slide_Panel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UI_Base";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Slide_Panel.ResumeLayout(false);
            this.Slide_Panel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.generalDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_Picture_Box)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel Slide_Panel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button Inspector_ID_Nxt_btn;
        private System.Windows.Forms.Label Inspector_ID_Label;
        private System.Windows.Forms.TextBox textBox1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private System.Windows.Forms.Label Engine_nb_lbl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer3;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape6;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape5;
        private System.Windows.Forms.PictureBox Main_Picture_Box;
        private System.Windows.Forms.Label Part_Name;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView generalDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listView1;
        private PictureBox pictureBox3;
        private Label label2;
    }
}

