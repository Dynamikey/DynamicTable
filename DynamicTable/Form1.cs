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

        XmlTextReader reader = new XmlTextReader("Z:\\Downloads\\RN-EJ-412-1009-03.xml");
        //XmlTextReader reader = new XmlTextReader("Z:\Downloads\\RR\\RN-EJ-412-1008-04.xml");
        private void WriteValues()
        {
            using (var writer = new CsvFileWriter("Z:\\Documents\\WriteTest.csv"))
            {
                List<string> columns = new List<string>();

                for (int i = 0; i < list.Count; i++)
                {
                    columns.Add(list[i].headingNumber);
                    columns.Add(list[i].headingName);
                    if (list[i].useableLimits != null) columns.Add(list[i].useableLimits);
                    else columns.Add(" ");
                    if (list[i].repairableLimits != null) columns.Add(list[i].repairableLimits);
                    else columns.Add(" ");
                    if (list[i].correctiveAction != null) columns.Add(list[i].correctiveAction);
                    else columns.Add(" ");
                    if (list[i].relatedFigures != null) columns.Add(list[i].relatedFigures);
                    else columns.Add(" ");
                    writer.WriteRow(columns);
                    columns.Clear();
                }

            }
        }

        private void FigFinder()
        {
            for (int i = 0; i < list.Count; i++)
            {
                RepairData temp = new RepairData();
                temp = list[i]; //Copy list data into temporary repairData struct

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

                list[i] = temp;
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
                        data.headingNumber = reader.GetAttribute("id").Substring(1); //Extract heading number "4.4" (minus the first character which is a F)
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
                        data.headingName = featureText;

                        AddToList(list, ref data);
                    }

                    if (reader.Name == "damageAndActions")
                    {
                        do
                        {
                            data.headingNumber = reader.GetAttribute("id").Substring(1); //Extract sub-heading number "4.4.1" (minus the first character which is a D)

                            XmlReader fdsub = reader.ReadSubtree();

                            while (fdsub.Read())
                            {
                                if (fdsub.Name == "damage")
                                {

                                    string damageHeading = Regex.Replace(fdsub.ReadInnerXml(), "<[^>]+>", string.Empty);
                                    if (damageHeading.StartsWith(".")) //Bit of clean up code to move sub-sub-heading number (e.g. "4.2.3.1") from headingName to headingNumber
                                    {
                                        data.headingNumber += damageHeading.Substring(0, damageHeading.IndexOf(" "));
                                        damageHeading = damageHeading.Remove(0, damageHeading.IndexOf(" ") + 1);
                                    }

                                    data.headingName = damageHeading;
                                }

                                if (fdsub.Name == "useableLimits")
                                {
                                    string useableLimitsText = Regex.Replace(fdsub.ReadInnerXml(), "\"([A-Z0-9a-z]+)\"", ">$1 <"); // Extracts figure attributes from useable limits
                                    useableLimitsText = Regex.Replace(useableLimitsText, "figure", "Fig.");
                                    useableLimitsText = Regex.Replace(useableLimitsText, "F0+[^1-9]", string.Empty);
                                    data.useableLimits = Regex.Replace(useableLimitsText, "<[^>]+>", string.Empty);
                                }

                                if (fdsub.Name == "repairableLimits")
                                {
                                    data.repairableLimits = Regex.Replace(fdsub.ReadInnerXml(), "<[^>]+>", string.Empty);
                                }

                                if (fdsub.Name == "correctiveAction")
                                {
                                    data.correctiveAction = Regex.Replace(fdsub.ReadInnerXml(), "<[^>]+>", string.Empty);
                                }

                            }

                            AddToList(list, ref data);

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



        

        List<RepairData> list = new List<RepairData>();
        RepairData data = new RepairData();

        /*public void AddToList()
        {
            list.Add(data);
            data = default(RepairData);
        }*/

        public void AddToList(List<RepairData> l, ref RepairData d)
        {
            l.Add(d);
            d = default(RepairData);
        }


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
            string imagePath = "Z:\\Downloads\\hi.png";
            DataGridViewImageColumn newDataGridViewImageColumn = new DataGridViewImageColumn();
            Image image = Image.FromFile(imagePath);
            newDataGridViewImageColumn.Image = image;
            dataGridView1.Columns.Add(newDataGridViewImageColumn);
            newDataGridViewImageColumn.HeaderText = "Image";
            //dataGridView1.AutoResizeRows();
        }

        private void CreateButtonsColumn()
        {

            DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
            dataGridViewButtonColumn.HeaderText = "Done?";
            //dataGridViewButtonColumn.Text = "Done?";
            dataGridViewButtonColumn.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Add(dataGridViewButtonColumn);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < list.Count; i++)
            {
                DataRow newDataRow = dataTable.NewRow();
                newDataRow[0] = list[i].headingNumber;
                newDataRow[1] = list[i].headingName;
                newDataRow[2] = list[i].useableLimits;
                newDataRow[3] = list[i].repairableLimits;
                newDataRow[4] = list[i].correctiveAction;
                newDataRow[5] = list[i].relatedFigures;
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
                data.headingNumber = (i+1).ToString();
                data.headingName = "blah";
                data.useableLimits = "blah";
                data.repairableLimits = "blah";
                data.correctiveAction = "blah blah blah blah blah blah blah blah blah blah blah";
                data.relatedFigures = "blah";
                data.checkComplete = false;
                AddToList(list, ref data);

            }
            PrintList(list);
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
            PrintList(list);
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

        /*
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                bool isSelect = senderGrid["Completed", e.RowIndex].Value as bool? ?? false;
                //Console.WriteLine(dataGridView1["Completed", e.RowIndex].Value);
                //bool completed = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                var row = senderGrid.Rows[e.RowIndex];
                if (isSelect)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                Console.WriteLine("Button clicked");
                // Do something

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

            if (e.RowIndex >= 0)
            {
                var row = senderGrid.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.Red;
            }

            senderGrid.EndEdit();
        }
        */
    }
}
