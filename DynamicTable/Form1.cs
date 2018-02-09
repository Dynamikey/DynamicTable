using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DynamicTable
{
    public partial class Form1 : Form
    {

        //XmlTextReader reader = new XmlTextReader("Z:\\Downloads\\RN-EJ-412-1009-03.xml");
        //XmlTextReader reader = new XmlTextReader("Z:\Downloads\\RR\\RN-EJ-412-1008-04.xml");
        XmlTextReader reader = new XmlTextReader("C:\\Users\\Fin\\Documents\\RR\\RN-EJ-412-1009-03.xml");
        //XmlTextReader reader = new XmlTextReader("C:\\Users\\Fin\\Documents\\RR\\RN-EJ-412-1008-04.xml");
        private void WriteValues()
        {
            using (var writer = new CsvFileWriter("C:\\Users\\Fin\\Documents\\RR\\WriteTest.csv"))
            {
                List<string> columns = new List<string>();

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

        List<RepairData> repairDataList = new List<RepairData>();
        RepairData repairData = new RepairData();
        DataTable dataTable;
       
        public Form1()
        {
            InitializeDataTable();
            InitializeComponent();
            GenerateRepairData();
            //dataGridView1.SelectAll();
        }

        private void InitializeDataTable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("Number", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Useable Limits", typeof(string));
            dataTable.Columns.Add("Repairable Limits", typeof(string));
            dataTable.Columns.Add("Corrective Action", typeof(string));
            dataTable.Columns.Add("Related Figures", typeof(string));
            //dataTable.Columns.Add("Completed", typeof(bool));
        }

        private void CreateGraphicsColumn()
        {
            //string imagePath = "Z:\\Downloads\\hi.png";
            string imagePath = "C:\\Users\\Fin\\Documents\\RR\\hi.jpg";
            DataGridViewImageColumn newDataGridViewImageColumn = new DataGridViewImageColumn();
            Image image = Image.FromFile(imagePath);
            //hello mikey
            Size newsize = new Size(Convert.ToInt32(image.Width*0.1), Convert.ToInt32(image.Height * 0.1));
            image = new Bitmap(image, newsize);

            newDataGridViewImageColumn.Image = image;
            newDataGridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.Columns.Add(newDataGridViewImageColumn);
            newDataGridViewImageColumn.HeaderText = "Image";
            //dataGridView1.AutoResizeRows();
        }

        private void CreateButtonsColumn()
        {

            DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
            dataGridViewButtonColumn.HeaderText = "Comments";
            dataGridViewButtonColumn.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Add(dataGridViewButtonColumn);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < repairDataList.Count; i++)
            {
                DataRow newDataRow = dataTable.NewRow();
                newDataRow[0] = repairDataList[i].headingNumber;
                newDataRow[1] = repairDataList[i].headingName;
                newDataRow[2] = repairDataList[i].useableLimits;
                newDataRow[3] = repairDataList[i].repairableLimits;
                newDataRow[4] = repairDataList[i].correctiveAction;
                newDataRow[5] = repairDataList[i].relatedFigures;
                //newDataRow[6] = list[i].checkComplete;
                dataTable.Rows.Add(newDataRow);

                /*dataTable.Rows[i].SetField(0, list[i].headingNumber);
                dataTable.Rows[i].SetField(1, list[i].headingName);
                dataTable.Rows[i].SetField(2, list[i].useableLimits);
                dataTable.Rows[i].SetField(3, list[i].repairableLimits);
                dataTable.Rows[i].SetField(4, list[i].correctiveAction);
                dataTable.Rows[i].SetField(5, list[i].relatedFigures);
                dataTable.Rows[i].SetField(6, list[i].checkComplete);*/

            }
            dataGridView1.DataSource = dataTable;
            //dataGridView1.Columns.Add(new DataGridViewButtonColumn());


            // Resize "Number" column
            dataGridView1.AutoResizeColumn(0);
            // Resize "Completed" column
            dataGridView1.AutoResizeColumn(5);
            
            //dataGridView1.Columns.Add(new DataGridViewImageColumn());
            CreateGraphicsColumn();
            CreateButtonsColumn();
        }

        private void GenerateExampleRepairData()
        {
            for (int i = 0; i < 10; i++)
            {
                repairData.headingNumber = (i+1).ToString();
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
        /*
        void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //DataGridView dataGridView1 = (DataGridView)sender;
            //DataGridViewCell dataGridViewCell = dataGridView1.CurrentCell;
            //bool isSelected = dataGridViewCell.EditedFormattedValue.Equals(true);
            //Console.Write(isSelected);
            Console.WriteLine("Dirty State Changed");
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("Cell Value Changed");
            bool isSelect = dataGridView1["Completed", e.RowIndex].Value as bool? ?? false;
            Console.WriteLine(dataGridView1["Completed", e.RowIndex].Value);
            //bool completed = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            var row = dataGridView1.Rows[e.RowIndex];
            if (isSelect)
            {
                row.DefaultCellStyle.BackColor = Color.Red;
            } else
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
            dataGridView1.EndEdit();
        }
        */

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
                if (row.DefaultCellStyle.BackColor == Color.White)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
            senderGrid.EndEdit();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Selection changed");
            dataGridView1.ClearSelection();
        }

        
    }
}
