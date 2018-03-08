using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Drawing.Imaging;
using Excel = Microsoft.Office.Interop.Excel;
using DynamicTable.Properties;
using System.Diagnostics;
using System.Threading;


//using MySql.Data.MySqlClient;

namespace DynamicTable
{
    public partial class UI_Base : Form
    {

        int PW;
        bool Hiden;
        String InspectorID = "";
        String EngineID = "";
        String PartNumber = "";
        String RepairNoteNumber = "";
        string preSentenceChecks = "";
        List<RepairNoteInformation> repairNoteList = new List<RepairNoteInformation>();
        RepairNoteInformation repairNoteInformation = new RepairNoteInformation();
        string[] relatedFiguresArr;
        Size originalPictureBoxSize;
        Image img;
        const double maxScale = 2.0; // The scale factor when it is at its max
        const double minScale = 1.0;
        const double scaleIncrement = 0.2;
        double currentScale = 1.0;

        public static Excel.Application app;
        public static Excel.Workbook wb;
        public static Excel.Worksheet xlWorkSheet;
        Excel.Range PartNameRange;

        void ResetSession()
        {

        }

        public UI_Base()
        {
            InitializeComponent();
            showGIF();
            ThreadStart myThreadStart = new ThreadStart(loadXL);
            Thread myThread = new Thread(myThreadStart);
            myThread.Start();
            //loadXL();
           

            //GenerateRepairData(); Moved to later when switching to table tab
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Start();
            label2.Text = "<";
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (Hiden)
            {
                Slide_Panel.Width = Slide_Panel.Width + 40;
                if (Slide_Panel.Width >= PW)
                {
                    timer1.Stop();
                    Hiden = false;
                    this.Refresh();
                }

            }
            else
            {
                label2.Text = ">";
                Slide_Panel.Width = Slide_Panel.Width - 40;
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
            try
            {
                wb.Close(false);
            }
            catch (Exception)
            {
                Console.WriteLine("Excel file is already closed");
            }
            Environment.Exit(0);          
        }


        private void Inspector_ID_Nxt_btn_Click(object sender, EventArgs e)
        {
            InspectorID = textBox1.Text;

            if (InspectorID != "")
            {
                tabControl1.SelectedTab = tabPage2;
                updatetoolbartext();
            }
            else textBox1.Focus();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EngineID = textBox2.Text;

            if(EngineID != "")
            {
                tabControl1.SelectedTab = tabPage3;
                this.ActiveControl = textBox3;
                updatetoolbartext();
            }
            else textBox2.Focus();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            PartNumber = textBox3.Text;

            if (PartNumber != "")
            {
                repairNoteSearch();
                tabControl1.SelectedTab = tabPage4;
                updatetoolbartext();

                for (int i = 0; i < repairNoteList.Count; i++)
                {
                    RowStyle temp = tableLayoutPanel1.RowStyles[0];
                    tableLayoutPanel1.RowCount++;
                   

                    RowStyle style = new RowStyle();
                    style.SizeType = SizeType.Absolute;
                    style.Height = 60;

                    tableLayoutPanel1.RowStyles.Add(style);

                    Button button = new Button();
                    //button.Left = left;
                    //button.Top = top;
                    button.Height = 60;
                    button.Width = 800;
                    button.Name = "Option" + i;
                    button.FlatStyle = FlatStyle.Flat;
                    button.BackColor = SystemColors.Control;
                    button.FlatAppearance.BorderSize = 0;
                    button.Text = repairNoteList[i].rn;
                    button.Click += button4_Click;//function
                    tableLayoutPanel1.Controls.Add(button, 3, i + 1);

                    Label label = new Label();
                    //label.Left = 300;
                    label.Height = 60;
                    label.Margin = new Padding(0, 10, 0, 0);
                    //label.Top = top + label.Height / 4;
                    label.Width = 700;
                    label.Font = new Font("Segoe UI", 20);
                    label.Text = repairNoteList[i].description;
                    tableLayoutPanel1.Controls.Add(label, 0, i + 1);

                    Label label2 = new Label();
                    //label2.Left = 300;
                    label2.Height = 60;
                    label2.Margin = new Padding(0, 10, 0, 0);
                    //label2.Top = top + label.Height / 4;
                    label2.Width = 700;
                    label2.Font = new Font("Segoe UI", 20);
                    label2.Text = repairNoteList[i].ml;
                    tableLayoutPanel1.Controls.Add(label2, 1, i + 1);

                    Label label3 = new Label();
                    //label3.Left = 300;
                    label3.Height = 60;
                    label3.Margin = new Padding(0, 10, 0, 0);
                    //label3.Top = top + label.Height / 4;
                    label3.Width = 700;
                    label3.Font = new Font("Segoe UI", 20);
                    label3.Text = repairNoteList[i].pc;
                    tableLayoutPanel1.Controls.Add(label3, 2, i + 1);

                    //top += button.Height + 5;
                }

            }
            else textBox3.Focus();

        }


        bool isSubRow(string str)
        {
            string[] numbers = str.Split('.');
            return (numbers.Length > 2) ? true : false;
        }

        string[] convertToRelatedFiguresArr(string str)
        {
            return str.Split(' ');
        }

        private void button4_Click(object sender, EventArgs e)
        {

            RepairNoteNumber = sender.ToString();
            RepairNoteNumber = RepairNoteNumber.Substring(RepairNoteNumber.IndexOf(":") + 2);
            GenerateRepairData();
            tabControl1.SelectedTab = tabPage7;
            PresentenceChecksBody.Text = preSentenceChecks;
            updatetoolbartext();

        }



        XmlTextReader reader = new XmlTextReader($"{Program.path}RN-EJ-412-1009-03.xml");
        //XmlTextReader reader = new XmlTextReader($"{path}RN-EJ-412-1008-04.xml");
        public List<RepairData> repairDataList = new List<RepairData>();
        RepairData repairData = new RepairData();
        DataTable subrowDataTable;
        DataTable generalDataTable;

        private void WriteValues()
        {
            using (var writer = new CsvFileWriter($"{Program.path}CSVoutput.csv"))
            {


                List<string> columns = new List<string>();


                Console.WriteLine(RepairNoteNumber);
                Console.WriteLine(Inspector_ID_Label.Text);
                Console.WriteLine(InspectorID);
                Console.WriteLine(Engine_nb_lbl.Text);
                Console.WriteLine(EngineID);
                Console.WriteLine(label1.Text);
                Console.WriteLine(PartNumber);

                columns.Add(RepairNoteNumber);
                columns.Add(Inspector_ID_Label.Text);
                columns.Add(InspectorID);
                columns.Add(Engine_nb_lbl.Text);
                columns.Add(EngineID);
                columns.Add(label1.Text);
                columns.Add(PartNumber);
                writer.WriteRow(columns);
                columns.Clear();


                for (int i = 0; i < repairDataList.Count; i++)
                {
                    columns.Add(repairDataList[i].headingNumber);
                    columns.Add(repairDataList[i].headingName);
                    if (repairDataList[i].useableLimits != null) columns.Add(repairDataList[i].useableLimits);
                    else columns.Add(" ");
                    if (repairDataList[i].repairableLimits != null) columns.Add(repairDataList[i].repairableLimits);
                    else columns.Add(" ");
                    if (repairDataList[i].correctiveAction != null) columns.Add(repairDataList[i].correctiveAction);
                    else columns.Add(" ");
                    if (repairDataList[i].relatedFigures != null) columns.Add(repairDataList[i].relatedFigures);
                    else columns.Add(" ");
                    if (repairDataList[i].conditionInput != null) columns.Add(repairDataList[i].conditionInput);
                    else columns.Add(" ");
                    if (repairDataList[i].damageTypeInput != null) columns.Add(repairDataList[i].damageTypeInput);
                    else columns.Add(" ");
                    if (repairDataList[i].damageMeasurementInput != null) columns.Add(repairDataList[i].damageMeasurementInput);
                    else columns.Add(" ");
                    if (repairDataList[i].damageFurtherCommentsInput != null) columns.Add(repairDataList[i].damageFurtherCommentsInput);
                    else columns.Add(" ");


                    writer.WriteRow(columns);
                    columns.Clear();
                }

            }
        }

        private void FigFinder()
        {
            for (int i = 0; i < repairDataList.Count; i++)
            {
                RepairData temp = new RepairData();
                temp = repairDataList[i]; //Copy list data into temporary repairData struct

                foreach (Match match in Regex.Matches(temp.headingName, @"\d+[^,.]")) //@"\d+[a-zA-Z0-9]"
                {
                    temp.relatedFigures += match.Value + " ";
                }

                try
                {
                    if (temp.useableLimits.Contains("Fig"))
                    {
                        foreach (Match match in Regex.Matches(temp.useableLimits, @"\d+[^,.]")) //@"\d+[a-zA-Z0-9]"
                        {
                            temp.relatedFigures += match.Value + " ";
                        }
                    }
                }
                catch
                {
                    NullReferenceException e;
                }

                repairDataList[i] = temp;
            }
        }

        private void ParseXML()
        {
            int preSentenceCount = 0;
            //reader.Read();
            while (!reader.EOF)
            {
                if (reader.NodeType == XmlNodeType.Element) // If the node is an element.
                {
                    Console.WriteLine(reader.Name);

                    if (reader.Name == "step1" && preSentenceCount < 3)
                    {
                        //preSentenceChecks += Regex.Replace(reader.ReadInnerXml(), "<[^>]+>", " ") + "\r" + "\r";
                        string preSentenceChecks1 = reader.ReadInnerXml().Replace("<para>", " <para>");
                        Console.WriteLine(preSentenceChecks1);
                        preSentenceChecks1 = preSentenceChecks1.Replace("General", "General \r");
                        preSentenceChecks1 = preSentenceChecks1.Replace("Cleaning Procedures", "Cleaning Procedures \r");
                        preSentenceChecks1 = preSentenceChecks1.Replace("Examination Procedure", "Examination Procedure \r");

                        preSentenceChecks += Regex.Replace(preSentenceChecks1, "<[^>]+>", "") + "\r" + "\r";
                        preSentenceCount += 1;
                    } else if (preSentenceCount == 3) {
                        break;
                    }
                    else
                    {
                        reader.Read();
                    }
                } else
                {
                    reader.Read();
                }
            }

            // temporary solution to start reading at beginning
            reader = new XmlTextReader($"{Program.path}RN-EJ-412-1009-03.xml");
            while (reader.Read())
            {
                if (reader.Name == "featureDamage") //If XML line is a feature damage heading (e.g. "4.4 Heatshield")
                {
                    repairData.headingNumber = reader.GetAttribute("id").Substring(1); //Extract heading number "4.4" (minus the first character which is a F)
                                                                                       //Console.WriteLine($"Found a heading number: {data.headingNumber}");
                }

                if (reader.Name == "feature")
                {
                    string featureText = Regex.Replace(reader.ReadInnerXml(), "\"([A-Z0-9a-z]+)\"", ">$1 <"); // Extracts figure attributes from useable limits

                    featureText = Regex.Replace(featureText, "figure", "Fig.");
                    featureText = Regex.Replace(featureText, "F0+[^1-9]", string.Empty);
                    featureText = Regex.Replace(featureText, "bold", string.Empty);

                    featureText = Regex.Replace(featureText, "<[^>]+>", string.Empty);
                    featureText = Regex.Replace(featureText, "<>", string.Empty);
                    featureText = featureText.Replace("&amp;", "&");
                    featureText = featureText.Replace("(.", " (");
                    featureText = Regex.Replace(featureText, @"\s+", " ");
                    if (char.IsWhiteSpace(featureText, 0))
                    {
                        featureText = featureText.Remove(0, 1);
                    }
                    repairData.headingName = featureText;

                    AddToList(repairDataList, ref repairData);
                }

                if (reader.Name == "damageAndActions")
                {
                    do
                    {
                        repairData.headingNumber = reader.GetAttribute("id").Substring(1); //Extract sub-heading number "4.4.1" (minus the first character which is a D)

                        XmlReader fdsub = reader.ReadSubtree();

                        while (fdsub.Read())
                        {
                            if (fdsub.Name == "damage")
                            {

                                string damageHeading = Regex.Replace(fdsub.ReadInnerXml(), "<[^>]+>", string.Empty);
                                if (damageHeading.StartsWith(".")) //Bit of clean up code to move sub-sub-heading number (e.g. "4.2.3.1") from headingName to headingNumber
                                {
                                    repairData.headingNumber += damageHeading.Substring(0, damageHeading.IndexOf(" "));
                                    damageHeading = damageHeading.Remove(0, damageHeading.IndexOf(" ") + 1);
                                }

                                repairData.headingName = damageHeading;
                            }

                            if (fdsub.Name == "useableLimits")
                            {
                                string useableLimitsText = Regex.Replace(fdsub.ReadInnerXml(), "\"([A-Z0-9a-z]+)\"", ">$1 <"); // Extracts figure attributes from useable limits
                                useableLimitsText = Regex.Replace(useableLimitsText, "figure", "Fig.");
                                useableLimitsText = Regex.Replace(useableLimitsText, "F0+[^1-9]", string.Empty);
                                repairData.useableLimits = Regex.Replace(useableLimitsText, "<[^>]+>", string.Empty);
                            }

                            if (fdsub.Name == "repairableLimits")
                            {
                                repairData.repairableLimits = Regex.Replace(fdsub.ReadInnerXml(), "<[^>]+>", string.Empty);
                            }

                            if (fdsub.Name == "correctiveAction")
                            {
                                repairData.correctiveAction = Regex.Replace(fdsub.ReadInnerXml(), "<[^>]+>", string.Empty);
                            }

                        }

                        AddToList(repairDataList, ref repairData);

                    } while (reader.ReadToNextSibling("damageAndActions")); //Multiple sub-headings
                }
            }
        }

        private void PrintList(List<RepairData> l)
        {
            for (int i = 0; i < l.Count; i++)
                Console.WriteLine($"{i} = {l[i].headingNumber} {l[i].headingName} {l[i].useableLimits} {l[i].repairableLimits} {l[i].correctiveAction} {l[i].relatedFigures} {l[i].conditionInput} {l[i].damageTypeInput} {l[i].damageMeasurementInput} {l[i].damageFurtherCommentsInput}");
        }

        public void AddToList(List<RepairData> l, ref RepairData d)
        {
            l.Add(d);
            d = default(RepairData);
        }

        private void InitializeSubrowDataTable()
        {
            subrowDataTable = new DataTable();
            subrowDataTable.Columns.Add("No.", typeof(string));
            subrowDataTable.Columns.Add("Name", typeof(string));
            subrowDataTable.Columns.Add("Useable Limits", typeof(string));
            subrowDataTable.Columns.Add("Repairable Limits", typeof(string));
            subrowDataTable.Columns.Add("Corrective Action", typeof(string));
            //subrowDataTable.Columns.Add("Related Figures", typeof(string));
            //dataTable.Columns.Add("Completed", typeof(bool));

        }

        private void InitializeGeneralDataTable()
        {
            generalDataTable = new DataTable();
            generalDataTable.Columns.Add("No.", typeof(string));
            generalDataTable.Columns.Add("Name", typeof(string));
            generalDataTable.Columns.Add("Related Figures", typeof(string));

        }

        private void CreateGraphicsColumn(ref DataGridView dataGridView)
        {
            DataGridViewImageColumn newDataGridViewImageColumn = new DataGridViewImageColumn();
            newDataGridViewImageColumn.Width = 200;
            newDataGridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            newDataGridViewImageColumn.HeaderText = "Images";
            dataGridView.Columns.Add(newDataGridViewImageColumn);
            
            int col = dataGridView.ColumnCount - 1;
            int row = 0;

            for (int i = 0; i < repairDataList.Count; i++)
            {
                if (!isSubRow(repairDataList[i].headingNumber))
                {
                    if (repairDataList[i].relatedFigures != null)
                    {
                        string[] relatedFiguresArrTemp = convertToRelatedFiguresArr(repairDataList[i].relatedFigures);
                        string imagePathTemp = $"{Program.path}figfolder\\RN-EJ-412-1009-03\\{relatedFiguresArrTemp[0]}.png";


                        Image imageTemp = Image.FromFile(imagePathTemp);
                        Size newsizeTemp = new Size(newDataGridViewImageColumn.Width, Convert.ToInt32((newDataGridViewImageColumn.Width) * imageTemp.Height / imageTemp.Width));
                        //Size newsize = new Size(Convert.ToInt32(120*image.Width/image.Height), 120);
                        Bitmap resizedImageTemp = new Bitmap(imageTemp, newsizeTemp);
                        dataGridView.Rows[row].Cells[col].Value = resizedImageTemp;
                    } 
                    row++;

                }
            }
            string imagePath = $"{Program.path}figfolder\\default.png";
            Image image = Image.FromFile(imagePath);
            Size newsize = new Size(newDataGridViewImageColumn.Width, Convert.ToInt32((newDataGridViewImageColumn.Width) * image.Height / image.Width));
            Bitmap resizedImage = new Bitmap(image, newsize);
            newDataGridViewImageColumn.DefaultCellStyle.NullValue = resizedImage;
            //dataGridView.Rows[0].Cells[6].Value = resizedimage;

            //dataGridView.AutoResizeRows();
        }

        //int count = 0;
        private void CreateButtonsColumn(ref DataGridView dataGridView)
        {

            //if (!dataGridView.Columns.Contains("Comments"))
            //{
            //if (count == 0)
            //{
                DataGridViewImageColumn dataGridViewButtonColumn = new DataGridViewImageColumn();
                Image image = Resources.pencil;
                dataGridViewButtonColumn.Image = image;
                dataGridView.Columns.Add(dataGridViewButtonColumn);
                
            //}
            //count = 1;
            //}
        }

        private void GenerateExampleRepairData()
        {
            for (int i = 0; i < 10; i++)
            {
                repairData.headingNumber = (i + 1).ToString();
                repairData.headingName = "blah";
                repairData.useableLimits = "blah";
                repairData.repairableLimits = "blah";
                repairData.correctiveAction = "blah blah blah blah blah blah blah blah blah blah blah";
                repairData.relatedFigures = "blah";
                repairData.checkComplete = false;
                AddToList(repairDataList, ref repairData);

            }
            PrintList(repairDataList);
        }

        private void GenerateRepairData()
        {
            ParseXML();
            FigFinder();
            PrintList(repairDataList);
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Console.WriteLine("Cell clicked");
            var senderGrid = (DataGridView)sender;
            senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (e.RowIndex >= 0)
            {
                var row = senderGrid.Rows[e.RowIndex];
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn)
                {
                    launchSAPcomment(e.RowIndex + globalSubRowNumber);
                    switch (repairDataList[e.RowIndex + globalSubRowNumber].conditionInput)
                    {
                        case "Serviceable":
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            break;

                        case "Salvageable":
                            row.DefaultCellStyle.BackColor = Color.Orange;
                            break;

                        case "Unsalvageable":
                            row.DefaultCellStyle.BackColor = Color.PaleVioletRed;
                            break;

                        case "":
                            break;
                    }

                }
                else if (!(senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn))
                {
                    if (row.DefaultCellStyle.BackColor == Color.White || row.DefaultCellStyle.BackColor == Color.Empty)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        repairDataList[e.RowIndex + globalSubRowNumber] = new RepairData(repairDataList[e.RowIndex + globalSubRowNumber], "Serviceable", "", "","","");
                        PrintList(repairDataList);
                    }
                    else if(row.DefaultCellStyle.BackColor != Color.White && row.DefaultCellStyle.BackColor != Color.Empty)
                        {
                        if (launchUndoConfirmation())
                        {
                            row.DefaultCellStyle.BackColor = Color.White;
                            repairDataList[e.RowIndex + globalSubRowNumber] = new RepairData(repairDataList[e.RowIndex + globalSubRowNumber], "", "", "", "", "");
                            PrintList(repairDataList);
                        }
                            
                        }
                        // TODO: Add some sort of confirmation to deselect
                    
                }

            }
            senderGrid.EndEdit();
        }

        private bool launchUndoConfirmation()
        {
            using (Undo undoConfirmation = new Undo())
            {
                var result = undoConfirmation.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return true;
                }
                else return false;
            }
        }

        public int globalSubRowNumber = -1;
        private void GenerateSubrowDataGridView(int row)
        {
            subrowDataTable.Clear();
            //Console.WriteLine("Column count at start of GenerateSubrowDataGridView = " + dataGridView1.ColumnCount);

            dataGridView1.DataSource = null; //Gets rid of datasource
            dataGridView1.Columns.Clear(); //Gets rid of button
            dataGridView1.Refresh(); 

            globalSubRowNumber = row + 1;
            for (int i = row + 1; i < repairDataList.Count; i++)
            {
                if (isSubRow(repairDataList[i].headingNumber))
                {
                    DataRow newDataRow = subrowDataTable.NewRow();
                    newDataRow[0] = repairDataList[i].headingNumber;
                    
                    newDataRow[1] = repairDataList[i].headingName;
                    newDataRow[2] = repairDataList[i].useableLimits;
                    newDataRow[3] = repairDataList[i].repairableLimits;
                    newDataRow[4] = repairDataList[i].correctiveAction;
                    subrowDataTable.Rows.Add(newDataRow);
                    
                }
                else break;

            }
            dataGridView1.DataSource = subrowDataTable;
            //Console.WriteLine(subrowDataTable.Rows[0].ToString());
            //dataGridView1.Columns.Add(new DataGridViewButtonColumn());

            // Resize "Number" column
            //dataGridView1.AutoResizeColumn(0);
            // Resize "Completed" column
            //dataGridView1.AutoResizeColumn(5);
            
            CreateButtonsColumn(ref dataGridView1);

            Console.WriteLine("Column count before resize = " + dataGridView1.ColumnCount);

            DataGridViewColumn column0 = dataGridView1.Columns[0];
            column0.Width = 80;
            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 140;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 200;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 142;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 300;
            DataGridViewColumn column5 = dataGridView1.Columns[5];
            column5.Width = 50;
            Console.WriteLine("Column count at end of GenerateSubrowDataGridView = " + dataGridView1.ColumnCount);


            for (int i = globalSubRowNumber; i < repairDataList.Count; i++)
            {
                if (isSubRow(repairDataList[i].headingNumber))
                {
                    if (repairDataList[i].checkComplete)
                    {
                        //dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor = Color.LightGreen;

                        switch (repairDataList[i].conditionInput)
                        {
                            case "Serviceable":
                                dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor = Color.LightGreen;
                                break;

                            case "Salvageable":
                                dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor = Color.Orange;
                                break;

                            case "Unsalvageable":
                                dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                                break;

                            case "":
                                break;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor = Color.White;
                    }
                }
                else
                {
                    break;
                }
            }

            

        }

        private void GenerateImageListView(string[] arr)
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(140, 140);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            listView1.Clear();
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j] != "")
                {
                    string tempImagePath = $"{Program.path}figfolder\\RN-EJ-412-1009-03\\{arr[j]}.png";
                    Image tempImage = Image.FromFile(tempImagePath);

                    Console.WriteLine(tempImagePath);
                    imageList.Images.Add(tempImage);
                }
            }

            listView1.View = View.LargeIcon;
            listView1.LargeImageList = imageList;
            for (int j = 0; j < imageList.Images.Count; j++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = j;
                listView1.Items.Add(item);
            }
            Console.WriteLine(listView1.Items.Count);
        }

        private void generalDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Console.WriteLine("Cell clicked");
            var senderGrid = (DataGridView)sender;
            senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (e.RowIndex >= 0)
            {
                var row = senderGrid.Rows[e.RowIndex];
                if (!(senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn) && !(senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn))
                {
                    int rowIndex;
                    int i = 0;
                    for (rowIndex = 0; i <= e.RowIndex; rowIndex++)
                    {
                        if (!isSubRow(repairDataList[rowIndex].headingNumber)) i++;
                    }
                    if (rowIndex != 0) rowIndex--;

                    GenerateSubrowDataGridView(rowIndex);
                    if (repairDataList[rowIndex].relatedFigures != null) { 
                        relatedFiguresArr = convertToRelatedFiguresArr(repairDataList[rowIndex].relatedFigures);


                        GenerateImageListView(relatedFiguresArr);
                        pictureBox1.Visible = true;
                       
                        plusButton.Visible = true;
                        minusButton.Visible = true;
                        currentScale = 1.0;
                       
                        // Cast to image
                        string imagePath = $"{Program.path}figfolder\\RN-EJ-412-1009-03\\{relatedFiguresArr[0]}.png";
                        img = Image.FromFile(imagePath);
                        // Load image data in memory stream
                        //MemoryStream ms = new MemoryStream();
                        //img.Save(ms, ImageFormat.Png);
                        //pictureBox1.Image = Image.FromStream(ms);
                        pictureBox1.Image = new Bitmap(img);
                        originalPictureBoxSize = pictureBox1.Size;
                    }
                    button5.Visible = true;
                    dataGridView1.Visible = true;
                    generalDataGridView.Visible = false;
                }
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn)
                {
                    int rowIndex;
                    int i = 0;

                    for (rowIndex = 0; i <= e.RowIndex; rowIndex++)
                    {
                        if (!isSubRow(repairDataList[rowIndex].headingNumber)) i++;
                    }
                    if (rowIndex != 0) rowIndex--;

                    if (repairDataList[rowIndex].relatedFigures != null)
                    {

                        //Console.WriteLine("Image clicked");

                        pictureBox1.Visible = true;
                        
                        plusButton.Visible = true;
                        minusButton.Visible = true;
                        currentScale = 1.0;
                        
                        // Cast to image
                        relatedFiguresArr = convertToRelatedFiguresArr(repairDataList[rowIndex].relatedFigures);
                        string imagePath = $"{Program.path}figfolder\\RN-EJ-412-1009-03\\{relatedFiguresArr[0]}.png";
                        img = Image.FromFile(imagePath);
                        // Load image data in memory stream
                        //MemoryStream ms = new MemoryStream();
                        //img.Save(ms, ImageFormat.Png);
                        //pictureBox1.Image = Image.FromStream(ms);
                        pictureBox1.Image = new Bitmap(img);
                        originalPictureBoxSize = pictureBox1.Size;


                        GenerateImageListView(relatedFiguresArr);
                    }

                }
            }
            senderGrid.EndEdit();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("Selection changed");
            dataGridView1.ClearSelection();
        }

        private void generalDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("Selection changed");
            generalDataGridView.ClearSelection();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible == true)
            {
                if (globalSubRowNumber != -1)
                {
                    bool isFullyComplete = true;
                    bool containsSalvageable = false;
                    bool containsUnsalvageable = false;
                    for (int i = globalSubRowNumber; i < repairDataList.Count; i++)
                    {
                        if (isSubRow(repairDataList[i].headingNumber))
                        {
                            Console.WriteLine(i);
                            //Console.WriteLine(globalSubRowNumber);
                            //repairDataList[i].checkComplete = true
                            if (dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor == Color.LightGreen)
                            {
                                repairDataList[i] = new RepairData(repairDataList[i], true);
                            }
                            else if (dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor == Color.Orange)
                            {
                                repairDataList[i] = new RepairData(repairDataList[i], true);
                                containsSalvageable = true;
                            }
                            else if (dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor == Color.PaleVioletRed)
                            {
                                repairDataList[i] = new RepairData(repairDataList[i], true);
                                containsUnsalvageable = true;
                            }
                            else
                            {
                                repairDataList[i] = new RepairData(repairDataList[i], false);
                                isFullyComplete = false;
                                containsSalvageable = false;
                                containsUnsalvageable = false;
                            }
                            // = (dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor == Color.White) ? false : true;
                        }
                        else
                        {
                            break;
                        }
                    }
                    repairDataList[globalSubRowNumber - 1] = (isFullyComplete) ? new RepairData(repairDataList[globalSubRowNumber - 1], true) : new RepairData(repairDataList[globalSubRowNumber - 1], false);
                    int rowCount = -1;
                    for (int i = 0; i <= globalSubRowNumber; i++)
                    {
                        if (!isSubRow(repairDataList[i].headingNumber))
                        {
                            rowCount++;
                        }
                    }
                    /*
                    if (isFullyComplete)
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.White;
                    }*/

                    if (containsUnsalvageable)
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                    }
                    else if (containsSalvageable)
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.Orange;
                    }
                    else if (isFullyComplete)
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.White;
                    }
                }
                button5.Visible = false;
                dataGridView1.Visible = false;
                generalDataGridView.Visible = true;
            }
        }

        private void repairNoteSearch()
        {
            //Excel.Application app = new Excel.Application();
            //Excel.Workbook wb = app.Workbooks.Open($"{path}EJ200 Repair Note Finder.xlsx");
           // Excel.Worksheet xlWorkSheet = (Excel.Worksheet)wb.Worksheets.get_Item(1);

            Excel.Range currentFind = null;
            Excel.Range firstFind = null;

           // Excel.Range PartNameRange = xlWorkSheet.UsedRange.Columns["A:E", Type.Missing];

            currentFind = PartNameRange.Find(PartNumber, Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);

            while (currentFind != null)
            {
                // Keep track of the first range you find. 
                if (firstFind == null)
                {
                    firstFind = currentFind;
                }

                // If you didn't move to a new range, you are done.
                else if (currentFind.get_Address(Excel.XlReferenceStyle.xlA1)
                      == firstFind.get_Address(Excel.XlReferenceStyle.xlA1))
                {
                    break;
                }

                Console.WriteLine("Found Part Number on Row No.: " + currentFind.Row);
                repairNoteInformation.description = xlWorkSheet.Cells[currentFind.Row, 2].Value.ToString();
                repairNoteInformation.ml = xlWorkSheet.Cells[currentFind.Row, 3].Value.ToString();
                repairNoteInformation.pc = xlWorkSheet.Cells[currentFind.Row, 4].Value.ToString();
                repairNoteInformation.rn = xlWorkSheet.Cells[currentFind.Row, 5].Value.ToString();
                repairNoteList.Add(repairNoteInformation);
                repairNoteInformation = default(RepairNoteInformation);

                //Console.WriteLine(RepairNoteNumber);

                currentFind = PartNameRange.FindNext(currentFind);
            }

            wb.Close(false);
        }

        private void launchSAPcomment(int rowIndex)
        {
            using (SAP_popup sapopup = new SAP_popup(rowIndex, repairDataList))
            {
                var result = sapopup.ShowDialog();
                if (result == DialogResult.OK)
                {
                    repairDataList = sapopup.repairDataList2;
                    PrintList(repairDataList);
                }
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            int selectedIndex = listView1.SelectedIndices[0];
            if (selectedIndex >= 0)
            {
              
                currentScale = 1.0;
                string imagePath = $"{Program.path}figfolder\\RN-EJ-412-1009-03\\{relatedFiguresArr[selectedIndex]}.png";
                img = Image.FromFile(imagePath);
                // Load image data in memory stream
                //MemoryStream ms = new MemoryStream();
                ///img.Save(ms, ImageFormat.Png);
                //pictureBox1.Image = Image.FromStream(ms);
                pictureBox1.Image = new Bitmap(img);
            }

        }

        private void dataGridView1_ColumnAdded_1(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        private void generalDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        System.Windows.Forms.Timer sw = new System.Windows.Forms.Timer(); // sw cotructor

        public void UI_Base_Load(object sender, EventArgs e)
        {
            var asForm = System.Windows.Automation.AutomationElement.FromHandle(this.Handle);
            PW = Slide_Panel.Width;
            Hiden = true;
            Slide_Panel.Width = 0;
            


            InitializeSubrowDataTable();
            InitializeGeneralDataTable();

            updatetoolbartext();
            
            
            sw.Interval = 3000;
            sw.Start(); // starts the stopwatch
            sw.Tick += new EventHandler(exitOpeningScreen);
            
           

        }

        void exitOpeningScreen(Object myObject, EventArgs myEventArgs)
        {
            sw.Stop();
            tabControl1.SelectedTab = tabPage1;
            this.ActiveControl = textBox1;
            
        }

        void loadXL()
        {
            app = new Excel.Application();
            wb = app.Workbooks.Open($"{Program.path}EJ200 Repair Note Finder.xlsx");
            xlWorkSheet = (Excel.Worksheet)wb.Worksheets.get_Item(1);
            PartNameRange = xlWorkSheet.UsedRange.Columns["A:E", Type.Missing];

        }

        void showGIF()
        {
            // 
            // pictureBox5
            // 
            tabControl1.SelectedTab = tabPage0;
            PictureBox pictureBox5 = new PictureBox();
            pictureBox5.Image = Resources.ajax_loader;
            pictureBox5.Location = new System.Drawing.Point(850, 647);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new System.Drawing.Size(220, 220);
            pictureBox5.TabIndex = 2;
            pictureBox5.TabStop = false;
            tabPage0.Controls.Add(pictureBox5);
        }


        private void finishbutton_Click_1(object sender, EventArgs e)
        {
            WriteValues();
            tabControl1.SelectedTab = tabPage6;
            string OverallSAPComment = "";

            for (int i = 0; i < repairDataList.Count; i++)
            {
                if(repairDataList[i].SAPcomment != null) OverallSAPComment += repairDataList[i].SAPcomment + "\r";
            }
            textBox4.Text = OverallSAPComment;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;


        }

        private void updatetoolbartext()
        {
            if (RepairNoteNumber != "") Part_Name.Text = $"{InspectorID} > Engine: {EngineID} > Part: {PartNumber} > {RepairNoteNumber}";
            else if (PartNumber != "") Part_Name.Text = $"{InspectorID} > Engine: {EngineID} > Part: {PartNumber}";
            else if (EngineID != "") Part_Name.Text = $"{InspectorID} > Engine: {EngineID}";
            else if (InspectorID != "") Part_Name.Text = $"{InspectorID}";
            else Part_Name.Text = "Welcome to the Rolls Royce Interactive Repair Note App";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
        }

        private void dataGridView1_ColumnWidthChanged_1(object sender, DataGridViewColumnEventArgs e)
        {
            //Console.WriteLine("Width has been changed to" + dataGridView1.Columns[0].Width + dataGridView1.Columns[1].Width + dataGridView1.Columns[2].Width + dataGridView1.Columns[3].Width + dataGridView1.Columns[4].Width + dataGridView1.Columns[5].Width);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;

            for (int i = 0; i < repairDataList.Count; i++)
            {
                if (isSubRow(repairDataList[i].headingNumber) == false)
                {
                    DataRow newDataRow = generalDataTable.NewRow();
                    newDataRow[0] = repairDataList[i].headingNumber;
                    newDataRow[1] = repairDataList[i].headingName;
                    newDataRow[2] = repairDataList[i].relatedFigures;
                    generalDataTable.Rows.Add(newDataRow);
                }

            }
            generalDataGridView.DataSource = generalDataTable;


        
            CreateGraphicsColumn(ref generalDataGridView);

            DataGridViewColumn column = generalDataGridView.Columns[0];
            column.Width = 60;
            column = generalDataGridView.Columns[1];
            column.Width = 542;
            column = generalDataGridView.Columns[2];
            column.Width = 110;
            column = generalDataGridView.Columns[3];
            column.Width = 200;
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            currentScale += scaleIncrement;
            if (currentScale > maxScale)
                currentScale = 2.0;
            //double scale = Math.Pow(MaxScale, trackBar1.Value / trackBar1.Maximum);
            //double scale = (((double)trackBar1.Value / (double)trackBar1.Maximum) * maxScale) + 1.0;
            //Console.WriteLine(trackBar1.Value);
            Console.WriteLine(currentScale);
            Size newSize = new Size(Convert.ToInt32((double)originalPictureBoxSize.Width * (double)currentScale),
                          Convert.ToInt32((double)originalPictureBoxSize.Height * (double)currentScale));

            //pictureBox1.Size = newSize;
            //pictureBox1.Image.Size = newSize;
            Console.WriteLine(newSize.Width);
            Console.WriteLine(newSize.Height);
            Bitmap bmp = new Bitmap(img, newSize);
            pictureBox1.Image = bmp;
        }

        private void minusButton_Click(object sender, EventArgs e)
        {
            currentScale -= scaleIncrement;
            if (currentScale < minScale)
                currentScale = 1.0;
            //double scale = Math.Pow(MaxScale, trackBar1.Value / trackBar1.Maximum);
            //double scale = (((double)trackBar1.Value / (double)trackBar1.Maximum) * maxScale) + 1.0;
            //Console.WriteLine(trackBar1.Value);
            //Console.WriteLine(scale);
            Size newSize = new Size(Convert.ToInt32((double)originalPictureBoxSize.Width * (double)currentScale),
                          Convert.ToInt32((double)originalPictureBoxSize.Height * (double)currentScale));

            //pictureBox1.Size = newSize;
            //pictureBox1.Image.Size = newSize;
            Console.WriteLine(newSize.Width);
            Console.WriteLine(newSize.Height);
            Bitmap bmp = new Bitmap(img, newSize);
            pictureBox1.Image = bmp;
        }


        private void textBox3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(this, EventArgs.Empty);
            }
        }

        private void textBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(this, EventArgs.Empty);
            }
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Inspector_ID_Nxt_btn_Click(this, EventArgs.Empty);
            }
        }
    }
}