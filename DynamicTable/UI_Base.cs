using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Drawing.Imaging;
using Excel = Microsoft.Office.Interop.Excel;

namespace DynamicTable
{
    public partial class UI_Base : Form
    {
        //hello
        int PW;
        bool Hiden;
        String InspectorID;
        String EngineID;
        String PartNumber;
        String RepairNoteNumber;
        List<RepairNoteInformation> repairNoteList = new List<RepairNoteInformation>();
        RepairNoteInformation repairNoteInformation = new RepairNoteInformation();

        public UI_Base()
        {
            InitializeComponent();
            PW = Slide_Panel.Width;
            Hiden = true;
            Slide_Panel.Width = 0;
            InitializeSubrowDataTable();
            InitializeGeneralDataTable();
            //GenerateRepairData(); Moved to later when switching to table tab
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Start();
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
            Environment.Exit(0);
        }

        private void Inspector_ID_Nxt_btn_Click(object sender, EventArgs e)
        {
            InspectorID = textBox1.Text;
            tabControl1.SelectedTab = tabPage2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EngineID = textBox2.Text;
            tabControl1.SelectedTab = tabPage3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PartNumber = textBox3.Text;
            
            repairNoteSearch();

            tabControl1.SelectedTab = tabPage4;
            int top = 80;
            int left = 19;

            for (int i = 0; i < repairNoteList.Count; i++)
            {
                RowStyle temp = tableLayoutPanel1.RowStyles[0];
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                Button button = new Button();
                button.Left = left;
                button.Top = top;
                button.Height = 40;
                button.Width = 200;
                button.Name = "Option" + i;
                button.Text = repairNoteList[i].rn;
                button.Click += button4_Click;//function
                tableLayoutPanel1.Controls.Add(button,3, i+1);

                Label label = new Label();
                label.Left = 300;
                label.Height = 40;
                label.Top = top + label.Height/4;
                label.Width = 700; 
                label.Font = new Font("Segoe UI", 12);
                label.Text = repairNoteList[i].description;
                tableLayoutPanel1.Controls.Add(label, 0, i + 1);

                Label label2 = new Label();
                label2.Left = 300;
                label2.Height = 40;
                label2.Top = top + label.Height / 4;
                label2.Width = 700;
                label2.Font = new Font("Segoe UI", 12);
                label2.Text = repairNoteList[i].ml;
                tableLayoutPanel1.Controls.Add(label2, 1, i + 1);

                Label label3 = new Label();
                label3.Left = 300;
                label3.Height = 40;
                label3.Top = top + label.Height / 4;
                label3.Width = 700;
                label3.Font = new Font("Segoe UI", 12);
                label3.Text = repairNoteList[i].pc;
                tableLayoutPanel1.Controls.Add(label3, 2, i + 1);



                top += button.Height + 5;
            }
        }

        private void optionbutton_Click(object sender, EventArgs e)
        {
            RepairNoteNumber = sender.ToString();
            Console.WriteLine("testing  " + RepairNoteNumber);
            //tabControl1.SelectedTab = tabPage3;
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
            GenerateRepairData();
            tabControl1.SelectedTab = tabPage5;

            // *******

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

        }


        static string path = "C:\\Users\\Fin\\Documents\\RR\\";
        //static string path = "C:\\Users\\METIIB\\Documents\\RR\\";
        //static string path = "Z:\\Downloads\\";
        //static string path = "C:\\Users\\RRCATablet\\Documents\\RR\\";
        XmlTextReader reader = new XmlTextReader($"{path}RN-EJ-412-1009-03.xml");
        //XmlTextReader reader = new XmlTextReader($"{path}RN-EJ-412-1008-04.xml");
        public List<RepairData> repairDataList = new List<RepairData>();
        RepairData repairData = new RepairData();
        DataTable subrowDataTable;
        DataTable generalDataTable;

        private void WriteValues()
        {
            using (var writer = new CsvFileWriter($"{path}test.csv"))
            {
               

                List<string> columns = new List<string>();

                columns.Add(Inspector_ID_Label.Text);
                columns.Add(InspectorID);
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
                    //Console.WriteLine("Found '{0}' at position {1} in {2}", match.Value, match.Index, temp.headingName);
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
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element) // If the node is an element.
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
        }

        private void PrintList(List<RepairData> l)
        {
            for (int i = 0; i < l.Count; i++)
                Console.WriteLine($"{i} = {l[i].headingNumber} {l[i].headingName} {l[i].useableLimits} {l[i].repairableLimits} {l[i].correctiveAction} {l[i].relatedFigures}");
        }

        public void AddToList(List<RepairData> l, ref RepairData d)
        {
            l.Add(d);
            d = default(RepairData);
        }

        private void InitializeSubrowDataTable()
        {
            subrowDataTable = new DataTable();
            subrowDataTable.Columns.Add("Number", typeof(string));
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
            generalDataTable.Columns.Add("Number", typeof(string));
            generalDataTable.Columns.Add("Name", typeof(string));
            generalDataTable.Columns.Add("Related Figures", typeof(string));
        }

        private void CreateGraphicsColumn(ref DataGridView dataGridView)
        {
            DataGridViewImageColumn newDataGridViewImageColumn = new DataGridViewImageColumn();
            newDataGridViewImageColumn.Width = 120;
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
                        string[] relatedFiguresArr = convertToRelatedFiguresArr(repairDataList[i].relatedFigures);
                        string imagePath = $"{path}figfolder\\RN-EJ-412-1009-03\\{relatedFiguresArr[0]}.png";
                        Image image = Image.FromFile(imagePath);
                        Size newsize = new Size(newDataGridViewImageColumn.Width, Convert.ToInt32((newDataGridViewImageColumn.Width) * image.Height / image.Width));
                        //Size newsize = new Size(Convert.ToInt32(120*image.Width/image.Height), 120);
                        Bitmap resizedimage = new Bitmap(image, newsize);
                        dataGridView.Rows[row].Cells[col].Value = resizedimage;
                    }
                    row++;
                    
                }
            }
            newDataGridViewImageColumn.DefaultCellStyle.NullValue = null;
            //dataGridView.Rows[0].Cells[6].Value = resizedimage;

            //dataGridView.AutoResizeRows();
        }

        int count = 0;
        private void CreateButtonsColumn(ref DataGridView dataGridView)
        {

            //if (!dataGridView.Columns.Contains("Comments"))
            //{
            if (count == 0)
            {
                DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
                dataGridViewButtonColumn.HeaderText = "Comments";
                dataGridViewButtonColumn.FlatStyle = FlatStyle.Flat;
                dataGridView.Columns.Add(dataGridViewButtonColumn);
            }
            count = 1;
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
            WriteValues();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("Cell clicked");
            var senderGrid = (DataGridView)sender;
            senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (e.RowIndex >= 0)
            {
                var row = senderGrid.Rows[e.RowIndex];
                if (!(senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn))
                {
                    if (row.DefaultCellStyle.BackColor == Color.White)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    launchSAPcomment();
                }
            }
            senderGrid.EndEdit();
        }

        int globalSubRowNumber = -1;
        private void GenerateSubrowDataGridView(int row)
        {
            subrowDataTable.Clear();
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
            //dataGridView1.Columns.Add(new DataGridViewButtonColumn());

            // Resize "Number" column
            dataGridView1.AutoResizeColumn(0);
            // Resize "Completed" column
            //dataGridView1.AutoResizeColumn(5);
            CreateButtonsColumn(ref dataGridView1);

            for (int i = globalSubRowNumber; i < repairDataList.Count; i++)
            {
                if (isSubRow(repairDataList[i].headingNumber))
                {
                    if (repairDataList[i].checkComplete)
                    {
                        dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor = Color.LightGreen;
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
                    string tempImagePath = $"{path}figfolder\\RN-EJ-412-1009-03\\{arr[j]}.png";
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
            Console.WriteLine("Cell clicked");
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

                    string[] relatedFiguresArr = convertToRelatedFiguresArr(repairDataList[rowIndex].relatedFigures);
                    GenerateImageListView(relatedFiguresArr);
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

                        Console.WriteLine("Image clicked");


                        // Cast to image
                        string[] relatedFiguresArr = convertToRelatedFiguresArr(repairDataList[rowIndex].relatedFigures);
                        string imagePath = $"{path}figfolder\\RN-EJ-412-1009-03\\{relatedFiguresArr[0]}.png";
                        Image img = Image.FromFile(imagePath);
                        // Load image data in memory stream
                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, ImageFormat.Png);
                        pictureBox1.Image = Image.FromStream(ms);
                        //originalPictureBoxSize = pictureBox1.Size;


                        GenerateImageListView(relatedFiguresArr);
                    }

                }
            }
            senderGrid.EndEdit();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Selection changed");
            dataGridView1.ClearSelection();
        }

        private void generalDataGridView_SelectionChanged(object sender, EventArgs e)                                                            
        {
            Console.WriteLine("Selection changed");
            generalDataGridView.ClearSelection();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible == true)
            {
                if (globalSubRowNumber != -1)
                {
                    bool isFullyComplete = true;
                    for (int i = globalSubRowNumber; i < repairDataList.Count; i++)
                    {
                        if (isSubRow(repairDataList[i].headingNumber))
                        {
                            Console.WriteLine(i);
                            //Console.WriteLine(globalSubRowNumber);
                            //repairDataList[i].checkComplete = true
                            if (dataGridView1.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor == Color.LightGreen)
                            {
                                /*repairDataList[i] = new RepairData(repairDataList[i].headingNumber,
                                        repairDataList[i].headingName,
                                        repairDataList[i].useableLimits,
                                        repairDataList[i].repairableLimits,
                                        repairDataList[i].correctiveAction,
                                        repairDataList[i].relatedFigures,
                                        true);*/
                                repairDataList[i] = new RepairData(repairDataList[i], true);
                            }
                            else
                            {
                                /*repairDataList[i] = new RepairData(repairDataList[i].headingNumber,
                                        repairDataList[i].headingName,
                                        repairDataList[i].useableLimits,
                                        repairDataList[i].repairableLimits,
                                        repairDataList[i].correctiveAction,
                                        repairDataList[i].relatedFigures,
                                        false);*/
                                repairDataList[i] = new RepairData(repairDataList[i], false);
                                isFullyComplete = false;
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
                    if (isFullyComplete)
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.White;
                    }
                }
                dataGridView1.Visible = false;
                generalDataGridView.Visible = true;
            }
            else
            {
                dataGridView1.Visible = true;
                generalDataGridView.Visible = false;
            }
        }

        private void repairNoteSearch()
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(@"C:\Users\Fin\Documents\RR\\EJ200 Repair Note Finder.xlsx");
            //Excel.Workbook wb = app.Workbooks.Open($"C:\\Users\\Fin\\Documents\\RR\\{EngineID} Repair Note Finder.xlsx");
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)wb.Worksheets.get_Item(1);

            Excel.Range currentFind = null;
            Excel.Range firstFind = null;

            Excel.Range PartNameRange = xlWorkSheet.UsedRange.Columns["A:E", Type.Missing];

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

        private void launchSAPcomment()
        {
            using (SAP_popup sapopup = new SAP_popup(repairDataList))
            {
                var result = sapopup.ShowDialog();
                if (result == DialogResult.OK)
                {
                    repairDataList = sapopup.repairDataList2;
                    Console.WriteLine("HI");
                    Console.WriteLine(repairDataList[0].condition);
                }
            }
                
        }

    }
}
