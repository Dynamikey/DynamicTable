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
using RollsRoyceRNApp.Properties;
using System.Diagnostics;
using System.Threading;
using System.Configuration;

namespace RollsRoyceRNApp
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
        public static string path = ConfigurationManager.AppSettings["path"];


        XmlTextReader reader = new XmlTextReader($"{path}XMLFolder\\RN-EJ-412-1009-03.xml"); //XML file (from within path folder). Can be changed to any XML from within RNOptionClick()
        //Declare repair data, repair data list and datatables;
        RepairData repairData = new RepairData();
        public List<RepairData> repairDataList = new List<RepairData>();
        DataTable subrowDataTable;
        DataTable generalDataTable;

        //For displaying/zooming images
        Size originalPictureBoxSize;
        Image img;
        const double maxScale = 2.0; // The scale factor when it is at its max
        const double minScale = 1.0;
        const double scaleIncrement = 0.2;
        double currentScale = 1.0;

        //For accessing Excel part finder
        public static Excel.Application app;
        public static Excel.Workbook wb;
        public static Excel.Worksheet xlWorkSheet;
        Excel.Range PartNameRange;

        string datetime;

        //bool for resetting app, not yet implemented
        bool isFinsFirstTime = true;

        //Form start up code
        public UI_Base()
        {
            InitializeComponent();
            Console.WriteLine(path);
            //Show loading GIF
            showGIF();
            //Load up Excel doc in background
            ThreadStart myThreadStart = new ThreadStart(loadXL);
            Thread myThread = new Thread(myThreadStart);
            myThread.Start();        
        }

        //Function for reseting app after finalising an inspection - doesn't work fully yet.
        public void ResetApp()
        {
            RepairNoteNumber = "";
            PartNumber = "";
            EngineID = "";
            updatetoolbartext();
            //tableLayoutPanel1.Controls.Clear();
            //tableLayoutPanel1.RowCount = 0;

            tableLayoutPanel1.SuspendLayout();

            while (tableLayoutPanel1.RowCount > 1)
            {
                int row = tableLayoutPanel1.RowCount - 1;
                for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
                {
                    Control c = tableLayoutPanel1.GetControlFromPosition(i, row);
                    tableLayoutPanel1.Controls.Remove(c);
                    c.Dispose();
                }

                tableLayoutPanel1.RowStyles.RemoveAt(row);
                tableLayoutPanel1.RowCount--;
            }

            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();

            repairNoteList = new List<RepairNoteInformation>();



            //tableLayoutPanel1 = new TableLayoutPanel();
            repairDataList = new List<RepairData>();
            //LowLevelGridView = new DataGridView();
            //generalDataGridView = new DataGridView();

            generalDataGridView.Visible = true;
            LowLevelGridView.Visible = false;
            button5.Visible = false;
            listView1.Items.Clear();

            plusButton.Visible = false;
            minusButton.Visible = false;
            panel1.Visible = false;

            //generalDataGridView
            generalDataTable = new DataTable();
            subrowDataTable = new DataTable();
        }

        //For opening left hand slide panel
        private void SlidePanelClick(object sender, EventArgs e)
        {
            //changePercentComplete();
            SliderTimer.Start();
            SliderArrow.Text = "<";
        }

        //Slide panel animation
        private void SliderTimer_Tick(object sender, EventArgs e)
        {
            if (Hiden)
            {
                Slide_Panel.Width = Slide_Panel.Width + 10;
                if (Slide_Panel.Width >= PW)
                {
                    SliderTimer.Stop();
                    Hiden = false;
                    this.Refresh();
                }

            }
            else
            {
                SliderArrow.Text = ">";
                Slide_Panel.Width = Slide_Panel.Width - 10;
                if (Slide_Panel.Width <= 0)
                {
                    SliderTimer.Stop();
                    Hiden = true;
                    
                    this.Refresh();
                }

            }
        }

        //For handling exit button click
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Close excel workbook
                wb.Close(false);
            }
            catch (Exception)
            {
                Console.WriteLine("Excel file is already closed");
            }
            Environment.Exit(0);          
        }

        //When Inspector ID next button has been clicked
        private void Inspector_ID_Nxt_btn_Click(object sender, EventArgs e)
        {
            InspectorID = inspectorIDTextbox.Text;

            if (InspectorID != "")
            {
                tabControl1.SelectedTab = engineNumberTabPage;
                updatetoolbartext();
            }
            else inspectorIDTextbox.Focus();
        }

        //When Engine ID next button has been clicked
        private void Engine_No_Nxt_btn_Click(object sender, EventArgs e)
        {
            EngineID = engineIDTextbox.Text;

            if(EngineID != "")
            {
                tabControl1.SelectedTab = partNumberTabPage;
                this.ActiveControl = partNumberTextbox;
                updatetoolbartext();
            }
            else engineIDTextbox.Focus();
        }

        //When Part No. next button has been clicked
        private void Part_No_Nxt_btn_Click(object sender, EventArgs e)
        {

            PartNumber = partNumberTextbox.Text;

            if (PartNumber != "")
            {
                repairNoteSearch();
                tabControl1.SelectedTab = ChooseRNTabpage;
                updatetoolbartext();
                
                //Dynamically generate list of repair notes to choose from
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
                    button.Click += RNOptionClick;//function
                    tableLayoutPanel1.Controls.Add(button, 3, i + 1);

                    Label RNDescriptionLabel = new Label();
                    //RNDescriptionLabel.Left = 300;
                    RNDescriptionLabel.Height = 60;
                    RNDescriptionLabel.Margin = new Padding(0, 10, 0, 0);
                    //RNDescriptionLabel.Top = top + RNDescriptionLabel.Height / 4;
                    RNDescriptionLabel.Width = 700;
                    RNDescriptionLabel.Font = new Font("Segoe UI", 20);
                    RNDescriptionLabel.Text = repairNoteList[i].description;
                    tableLayoutPanel1.Controls.Add(RNDescriptionLabel, 0, i + 1);

                    Label RNMLLabel = new Label();
                    //RNMLLabel.Left = 300;
                    RNMLLabel.Height = 60;
                    RNMLLabel.Margin = new Padding(0, 10, 0, 0);
                    //RNMLLabel.Top = top + RNDescriptionLabel.Height / 4;
                    RNMLLabel.Width = 700;
                    RNMLLabel.Font = new Font("Segoe UI", 20);
                    RNMLLabel.Text = repairNoteList[i].ml;
                    tableLayoutPanel1.Controls.Add(RNMLLabel, 1, i + 1);

                    Label RNPCLabel = new Label();
                    //RNPCLabel.Left = 300;
                    RNPCLabel.Height = 60;
                    RNPCLabel.Margin = new Padding(0, 10, 0, 0);
                    //RNPCLabel.Top = top + RNDescriptionLabel.Height / 4;
                    RNPCLabel.Width = 700;
                    RNPCLabel.Font = new Font("Segoe UI", 20);
                    RNPCLabel.Text = repairNoteList[i].pc;
                    tableLayoutPanel1.Controls.Add(RNPCLabel, 2, i + 1);

                    //top += button.Height + 5;
                }

            }
            else partNumberTextbox.Focus();
        }

        //Check to see if data in list is heading or sub-heading
        bool isSubRow(string str)
        {
            string[] numbers = str.Split('.');
            return (numbers.Length > 2) ? true : false;
        }

        //Splits string of related figures into string array
        string[] convertToRelatedFiguresArr(string str)
        {
            return str.Split(' ');
        }

        //Handles choice of RN option
        private void RNOptionClick(object sender, EventArgs e)
        {
            RepairNoteNumber = sender.ToString();
            RepairNoteNumber = RepairNoteNumber.Substring(RepairNoteNumber.IndexOf(":") + 2);

            if (ConfigurationManager.AppSettings["AnyXML?"] == "No")
            {
                RepairNoteNumber = "RN-EJ-412-1009-03"; //Comment to use user selected RN XML file (from within path folder), currently always loads the same file 
            }

            reader = new XmlTextReader($"{path}XMLFolder\\{RepairNoteNumber}.xml");
            GenerateRepairData();
            tabControl1.SelectedTab = MainDataTabpage;
            PresentenceChecksBody.Text = preSentenceChecks;
            updatetoolbartext();
        }
       
        //For writing final inspection data into CSV file
        private void WriteValues()
        {
            datetime = $"{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}_{DateTime.Now.Year.ToString()}_{DateTime.Now.Hour.ToString()}_{DateTime.Now.Minute.ToString()}_{DateTime.Now.Second.ToString()}"; //Puts date and time into a saveable format
            using (var writer = new CsvFileWriter($"{path}CSVFolder//{PartNumber}_{RepairNoteNumber}_{InspectorID}_{datetime}.csv")) //<- CSV filename declared here
            {


                List<string> columns = new List<string>();

                //Add introductory info to top of CSV
                columns.Add(RepairNoteNumber);
                writer.WriteRow(columns);
                columns.Clear();

                columns.Add(Inspector_ID_Label.Text);
                columns.Add(InspectorID);
                writer.WriteRow(columns);
                columns.Clear();

                columns.Add(Engine_nb_lbl.Text);
                columns.Add(EngineID);
                writer.WriteRow(columns);
                columns.Clear();

                columns.Add(partNumberLabel.Text);
                columns.Add(PartNumber);
                writer.WriteRow(columns);
                columns.Clear();

                //Add column headers
                columns.Add("Feature Number");
                columns.Add("Feature Name");
                columns.Add("Usable Limits");
                columns.Add("Repairable Limits");
                columns.Add("Corrective Action");
                columns.Add("Associated Figures");
                columns.Add("Condition");
                columns.Add("Damage Type");
                columns.Add("Damage Amount");
                columns.Add("Further Comments");
                columns.Add("Raise RDR");
                columns.Add("Image Path");

                writer.WriteRow(columns);
                columns.Clear();

                //Add repair info
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
                    if (repairDataList[i].raiseRDR != null) columns.Add(repairDataList[i].raiseRDR.ToString());
                    else columns.Add(" ");
                    if (repairDataList[i].imagePath != null) columns.Add(repairDataList[i].imagePath);
                    else columns.Add(" ");


                    writer.WriteRow(columns);
                    columns.Clear();
                }

            }
        }

        //Trys to find related figures from heading name and useable limits
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

        //Parses XML data into repair data format. Works for two xml docs provided. May require further work as XML content is very variable.
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
            reader = new XmlTextReader($"{path}XMLFolder\\{RepairNoteNumber}.xml");
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
                    repairData.raiseRDR = false;
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
                        repairData.raiseRDR = false;
                        AddToList(repairDataList, ref repairData);

                    } while (reader.ReadToNextSibling("damageAndActions")); //Multiple sub-headings
                }
            }
        }

        //Print repair list to console
        private void PrintList(List<RepairData> l)
        {
            for (int i = 0; i < l.Count; i++)
                Console.WriteLine($"{i} = {l[i].headingNumber} {l[i].headingName} {l[i].useableLimits} {l[i].repairableLimits} {l[i].correctiveAction} {l[i].relatedFigures} {l[i].conditionInput} {l[i].damageTypeInput} {l[i].damageMeasurementInput} {l[i].damageFurtherCommentsInput}");
        }

        //Adds repair data to repair data list
        public void AddToList(List<RepairData> l, ref RepairData d)
        {
            l.Add(d);
            d = default(RepairData);
        }

        //Initialise subrow data table headings
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

        //Initialise general data table headings
        private void InitializeGeneralDataTable()
        {
            generalDataTable = new DataTable();
            generalDataTable.Columns.Add("No.", typeof(string));
            generalDataTable.Columns.Add("Name", typeof(string));
            generalDataTable.Columns.Add("Related Figures", typeof(string));

        }

        //Creates graphics column in general datagridview
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
                        string imagePathTemp = $"{path}RNFigures\\{RepairNoteNumber}\\{relatedFiguresArrTemp[0]}.png"; //Change this to work with other XMLs


                        Image imageTemp = Image.FromFile(imagePathTemp);
                        Size newsizeTemp = new Size(newDataGridViewImageColumn.Width, Convert.ToInt32((newDataGridViewImageColumn.Width) * imageTemp.Height / imageTemp.Width));
                        Bitmap resizedImageTemp = new Bitmap(imageTemp, newsizeTemp);
                        dataGridView.Rows[row].Cells[col].Value = resizedImageTemp;
                    } 
                    row++;

                }
            }
            string imagePath = $"{path}RNFigures\\default.png"; //If no figures associated, use default white image.
            Image image = Image.FromFile(imagePath);
            Size newsize = new Size(newDataGridViewImageColumn.Width, Convert.ToInt32((newDataGridViewImageColumn.Width) * image.Height / image.Width));
            Bitmap resizedImage = new Bitmap(image, newsize);
            newDataGridViewImageColumn.DefaultCellStyle.NullValue = resizedImage;
        }

        //Creates 'pencil' button column for SAP form
        private void CreateButtonsColumn(ref DataGridView dataGridView)
        {
                DataGridViewImageColumn dataGridViewButtonColumn = new DataGridViewImageColumn();
                Image image = Resources.pencil;
                dataGridViewButtonColumn.Image = image;
                dataGridView.Columns.Add(dataGridViewButtonColumn);
        }

        //Parse XML, find figures and output to console
        private void GenerateRepairData()
        {
            ParseXML();
            FigFinder();
            PrintList(repairDataList);
        }

        //If a cell is clicked in sub data grid view
        private void subDataGridView_Click(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (e.RowIndex >= 0)
            {
                var row = senderGrid.Rows[e.RowIndex];
                
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn) //If pencil icon is clicked
                {
                    launchSAPcomment(e.RowIndex + globalSubRowNumber); //Launch SAP comment
                    switch (repairDataList[e.RowIndex + globalSubRowNumber].conditionInput)
                    {
                        case "Serviceable":
                            row.DefaultCellStyle.BackColor = Color.LimeGreen;
                            break;

                        case "Salvageable":
                            row.DefaultCellStyle.BackColor = Color.Gold;
                            break;

                        case "Unsalvageable":
                            row.DefaultCellStyle.BackColor = Color.Tomato;
                            break;

                        case "":
                            break;
                    }

                }
                else if (!(senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)) //if not pencil icon, turn green or undo
                {
                    if (row.DefaultCellStyle.BackColor == Color.White || row.DefaultCellStyle.BackColor == Color.Empty) //If not green
                    {
                        row.DefaultCellStyle.BackColor = Color.LimeGreen;
                        repairDataList[e.RowIndex + globalSubRowNumber] = new RepairData(repairDataList[e.RowIndex + globalSubRowNumber], "Serviceable", "", "", "", "", "", false);
                        repairDataList[e.RowIndex + globalSubRowNumber] = new RepairData(repairDataList[e.RowIndex + globalSubRowNumber], true);

                        //If we want servicable in SAP comment:
                        /*$@"{repairDataList[e.RowIndex + globalSubRowNumber].headingNumber} {repairDataList[e.RowIndex + globalSubRowNumber].headingName} 
Part is: Serviceable
Damage: N/A
Amount: N/A
Further Comment: N/A"*/

                        PrintList(repairDataList);
                    }
                    else if(row.DefaultCellStyle.BackColor != Color.White && row.DefaultCellStyle.BackColor != Color.Empty) //If already green
                        {
                        if (launchUndoConfirmation())
                        {
                            row.DefaultCellStyle.BackColor = Color.White;
                            repairDataList[e.RowIndex + globalSubRowNumber] = new RepairData(repairDataList[e.RowIndex + globalSubRowNumber], "", "", "", "", "", "", false);
                            repairDataList[e.RowIndex + globalSubRowNumber] = new RepairData(repairDataList[e.RowIndex + globalSubRowNumber], false);
                            PrintList(repairDataList);
                        }
                            
                        }
                        // TODO: Add some sort of confirmation to deselect
                    
                }

            }
            changePercentComplete();
            senderGrid.EndEdit();
        }

        //Launch undo confirmation form
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

        //Generate the subDataGridView
        private void GenerateSubrowDataGridView(int row)
        {
            subrowDataTable.Clear();
            //Console.WriteLine("Column count at start of GenerateSubrowDataGridView = " + LowLevelGridView.ColumnCount);

            LowLevelGridView.DataSource = null; //Gets rid of datasource
            LowLevelGridView.Columns.Clear(); //Gets rid of button
            LowLevelGridView.Refresh(); 

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
            LowLevelGridView.DataSource = subrowDataTable;
            
            CreateButtonsColumn(ref LowLevelGridView);

            //Set widths of columns
            DataGridViewColumn column0 = LowLevelGridView.Columns[0];
            column0.Width = 80;
            DataGridViewColumn column1 = LowLevelGridView.Columns[1];
            column1.Width = 140;
            DataGridViewColumn column2 = LowLevelGridView.Columns[2];
            column2.Width = 200;
            DataGridViewColumn column3 = LowLevelGridView.Columns[3];
            column3.Width = 142;
            DataGridViewColumn column4 = LowLevelGridView.Columns[4];
            column4.Width = 300;
            DataGridViewColumn column5 = LowLevelGridView.Columns[5];
            column5.Width = 50;


            for (int i = globalSubRowNumber; i < repairDataList.Count; i++)
            {
                if (isSubRow(repairDataList[i].headingNumber))
                {
                    if (repairDataList[i].checkComplete)
                    {
                        switch (repairDataList[i].conditionInput)
                        {
                            case "Serviceable":
                                LowLevelGridView.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor = Color.LimeGreen;
                                break;

                            case "Salvageable":
                                LowLevelGridView.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor = Color.Gold;
                                break;

                            case "Unsalvageable":
                                LowLevelGridView.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor = Color.Tomato;
                                break;

                            case "":
                                break;
                        }
                    }
                    else
                    {
                        LowLevelGridView.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor = Color.White;
                    }
                }
                else
                {
                    break;
                }
            }

            

        }

        //Generate choice of figures
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
                    string tempImagePath = $"{path}RNFigures\\{RepairNoteNumber}\\{arr[j]}.png";
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

        //If general datagridview cell has been clicked
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
                        string imagePath = $"{path}RNFigures\\{RepairNoteNumber}\\{relatedFiguresArr[0]}.png";
                        img = Image.FromFile(imagePath);
                        // Load image data in memory stream
                        //MemoryStream ms = new MemoryStream();
                        //img.Save(ms, ImageFormat.Png);
                        //pictureBox1.Image = Image.FromStream(ms);
                        pictureBox1.Image = new Bitmap(img);
                        originalPictureBoxSize = pictureBox1.Size;
                    }
                    button5.Visible = true;
                    LowLevelGridView.Visible = true;
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
                        string imagePath = $"{path}RNFigures\\{RepairNoteNumber}\\{relatedFiguresArr[0]}.png";
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

        //If low level datagridview cell has been clicked
        private void LowLevelGridView_SelectionChanged(object sender, EventArgs e)
        {
            LowLevelGridView.ClearSelection();
        }

        private void generalDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            generalDataGridView.ClearSelection();
        }

        //If back button is clicked in main data tab page
        private void backbuttonClick(object sender, EventArgs e)
        {
            if (LowLevelGridView.Visible == true)
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
                            if (LowLevelGridView.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor == Color.LimeGreen)
                            {
                                repairDataList[i] = new RepairData(repairDataList[i], true);
                            }
                            else if (LowLevelGridView.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor == Color.Gold)
                            {
                                repairDataList[i] = new RepairData(repairDataList[i], true);
                                containsSalvageable = true;
                            }
                            else if (LowLevelGridView.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor == Color.Tomato)
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
                            // = (LowLevelGridView.Rows[i - globalSubRowNumber].DefaultCellStyle.BackColor == Color.White) ? false : true;
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
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.White;
                    }*/

                        if (containsUnsalvageable)
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.Tomato;
                    }
                    else if (containsSalvageable)
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gold;
                    }
                    else if (isFullyComplete)
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        generalDataGridView.Rows[rowCount].DefaultCellStyle.BackColor = Color.White;
                    }
                }
                button5.Visible = false;
                LowLevelGridView.Visible = false;
                generalDataGridView.Visible = true;
            }
        }

        //Searches repair note file for part number
        private void repairNoteSearch()
        {
            Excel.Range currentFind = null;
            Excel.Range firstFind = null;

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

        //Launces SAP cmomment form
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

        //Handles changing figure displayed
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            int selectedIndex = listView1.SelectedIndices[0];
            if (selectedIndex >= 0)
            {
              
                currentScale = 1.0;
                string imagePath = $"{path}RNFigures\\{RepairNoteNumber}\\{relatedFiguresArr[selectedIndex]}.png";
                img = Image.FromFile(imagePath);
                // Load image data in memory stream
                //MemoryStream ms = new MemoryStream();
                ///img.Save(ms, ImageFormat.Png);
                //pictureBox1.Image = Image.FromStream(ms);
                pictureBox1.Image = new Bitmap(img);
            }

        }

        //Ensures added columns are correctly formatted
        private void LowLevelGridView_ColumnAdded_1(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }
        private void generalDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        //Timer for welcome screen
        System.Windows.Forms.Timer sw = new System.Windows.Forms.Timer(); // sw cotructor

        //When UI base form loads up
        public void UI_Base_Load(object sender, EventArgs e)
        {
            var asForm = System.Windows.Automation.AutomationElement.FromHandle(this.Handle); //To auto bring up OSK

            //Slide panel setup
            PW = Slide_Panel.Width;
            Hiden = true;
            Slide_Panel.Width = 0;
            
            //Initialise datatables
            InitializeSubrowDataTable();
            InitializeGeneralDataTable();

            updatetoolbartext(); //Update slide panel text 

            sw.Interval = 3000; //3 second timer
            sw.Start(); // starts the stopwatch
            sw.Tick += new EventHandler(exitOpeningScreen); //Exits welcome screen after timer interval
            
           

        }

        //For handling exiting of welcome screen
        void exitOpeningScreen(Object myObject, EventArgs myEventArgs)
        {
            sw.Stop();

            //For handling the non-finished app reset
            if (isFinsFirstTime)
            {
                tabControl1.SelectedTab = tabPage1;
                this.ActiveControl = inspectorIDTextbox;
            } else
            {
                tabControl1.SelectedTab = engineNumberTabPage;
                this.ActiveControl = engineIDTextbox;
            }
            
        }

        //Loads up Excel EJ200 Repair Note Finder file
        void loadXL()
        {
            app = new Excel.Application();
            wb = app.Workbooks.Open($"{path}EJ200 Repair Note Finder.xlsx");
            xlWorkSheet = (Excel.Worksheet)wb.Worksheets.get_Item(1);
            PartNameRange = xlWorkSheet.UsedRange.Columns["A:E", Type.Missing];
        }

        //Adds loading GIF
        void showGIF()
        {

            tabControl1.SelectedTab = OpeningTab;
            PictureBox LoadingGIF = new PictureBox();
            LoadingGIF.Image = Resources.ajax_loader;
            LoadingGIF.Location = new System.Drawing.Point(850, 647);
            LoadingGIF.Name = "LoadingGIF";
            LoadingGIF.Size = new System.Drawing.Size(220, 220);
            LoadingGIF.TabIndex = 2;
            LoadingGIF.TabStop = false;
            OpeningTab.Controls.Add(LoadingGIF);
        }

        //Generates final SAP comment on finish button click
        private void finishbutton_Click_1(object sender, EventArgs e)
        {
            
            WriteValues();
            tabControl1.SelectedTab = FinalSAPPreviewPage;
            string OverallSAPComment = "";

            OverallSAPComment = DateTime.Now.ToString() + System.Environment.NewLine + System.Environment.NewLine;
            OverallSAPComment += "Inspector ID: " + InspectorID + System.Environment.NewLine + System.Environment.NewLine;

            for (int i = 0; i < repairDataList.Count; i++)
            {
                if(repairDataList[i].SAPcomment != null && repairDataList[i].SAPcomment != "") OverallSAPComment += repairDataList[i].SAPcomment + System.Environment.NewLine + System.Environment.NewLine;
            }

            OverallSAPComment += "Inspection checks satisfactory to " + RepairNoteNumber;
            textBox4.Text = OverallSAPComment;
        }

        //Handles non-functioning app reset
        private void button4_Click_1(object sender, EventArgs e)
        {
            isFinsFirstTime = false;

            ExitBtn_Click(this, EventArgs.Empty);
            /*
            // Reset variables after exporting to CSV
            ResetApp();

            showGIF();
            ThreadStart myThreadStart = new ThreadStart(loadXL);
            Thread myThread = new Thread(myThreadStart);
            myThread.Start();
            sw.Interval = 3000;
            sw.Start(); // starts the stopwatch
            sw.Tick += new EventHandler(exitOpeningScreen);*/
        }

        //Updates slide bar text based on input
        private void updatetoolbartext()
        {
            if (RepairNoteNumber != "") Part_Name.Text = $"{InspectorID} > Engine: {EngineID} > Part: {PartNumber} > {RepairNoteNumber}";
            else if (PartNumber != "") Part_Name.Text = $"{InspectorID} > Engine: {EngineID} > Part: {PartNumber}";
            else if (EngineID != "") Part_Name.Text = $"{InspectorID} > Engine: {EngineID}";
            else if (InspectorID != "") Part_Name.Text = $"{InspectorID}";
            else Part_Name.Text = "Welcome to the Rolls Royce Interactive Repair Note App";
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = MainDataTab;
        }

        //Debug earlier column width problems
        private void dataGridView1_ColumnWidthChanged_1(object sender, DataGridViewColumnEventArgs e)
        {
            //Console.WriteLine("Width has been changed to" + LowLevelGridView.Columns[0].Width + LowLevelGridView.Columns[1].Width + LowLevelGridView.Columns[2].Width + LowLevelGridView.Columns[3].Width + LowLevelGridView.Columns[4].Width + LowLevelGridView.Columns[5].Width);
        }

        //Creates datagridviews on start inspection button click
        private void StartInspectionButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = MainDataTab;

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

        //Figure zooming functionality
        private void plusButton_Click(object sender, EventArgs e)
        {
            currentScale += scaleIncrement;
            if (currentScale > maxScale)
                currentScale = 2.0;
            Console.WriteLine(currentScale);
            Size newSize = new Size(Convert.ToInt32((double)originalPictureBoxSize.Width * (double)currentScale),
                          Convert.ToInt32((double)originalPictureBoxSize.Height * (double)currentScale));

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
            Size newSize = new Size(Convert.ToInt32((double)originalPictureBoxSize.Width * (double)currentScale),
                          Convert.ToInt32((double)originalPictureBoxSize.Height * (double)currentScale));

            Console.WriteLine(newSize.Width);
            Console.WriteLine(newSize.Height);
            Bitmap bmp = new Bitmap(img, newSize);
            pictureBox1.Image = bmp;
        }

        //Handles pressing of 'Enter' key instead of button clicks
        private void textBox3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Part_No_Nxt_btn_Click(this, EventArgs.Empty);
            }
        }
        private void textBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Engine_No_Nxt_btn_Click(this, EventArgs.Empty);
            }
        }
        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Inspector_ID_Nxt_btn_Click(this, EventArgs.Empty);
            }
        }

        //Updates percentage through RN number in slide panel
        private void changePercentComplete()
        {
            int NoOfComplete = 0;
            for (int i = 0; i < repairDataList.Count; i++)
            {
                if (repairDataList[i].checkComplete == true)
                {
                    Console.WriteLine("check");
                    NoOfComplete += 1;
                }
            }
            Console.WriteLine(NoOfComplete);
            if(repairDataList.Count > 0)
            {
                Console.WriteLine((float)NoOfComplete / (float)repairDataList.Count);
                float percentCompletefloat =  (float) NoOfComplete * 100f / (float) repairDataList.Count;
                int percentComplete = (int)percentCompletefloat;
                percentCompleteLabel.Text = percentComplete.ToString() + "%";
            }

        }

    }
}