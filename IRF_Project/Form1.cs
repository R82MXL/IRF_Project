using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using CsvHelper;
using IRF_Project.Entities;

namespace IRF_Project
{
    public partial class Form1 : Form
    {
        List<Person> personList = new List<Person>();
        public Form1()
        {
            InitializeComponent();
            SetTimer();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (dataGridView.Rows.Count == 0)
            {
                readCsvFromLocalhost();
            }
        }

        public void SetTimer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Population_Analysis_CSV.csv";
                sfd.ValidateNames = true;

                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dataGridView.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dataGridView.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dataGridView.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += dataGridView.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Your data has been successfully saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !", "Info");
            }
        }

        private void fillGenderList()
        {
            genderCmBox.Items.Clear();

            List<Gender> genderList = new List<Gender>();
            genderList.Add(new Gender() { id = 2, genderName = "all" });
            genderList.Add(new Gender() { id = 1, genderName = "male" });
            genderList.Add(new Gender() { id = 0, genderName = "female" });

            genderCmBox.DataSource = genderList;
            genderCmBox.ValueMember = "id";
            genderCmBox.DisplayMember = "genderName";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView.DataSource = new List<Person>();

            fillGenderList();
        }

        public void readCsv()
        {

            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader csvFile = System.IO.File.OpenText(ofd.FileName))
                    {
                        CsvReader csv = new CsvReader(csvFile);
                        csv.Configuration.IgnoreHeaderWhiteSpace = true;
                        personList = csv.GetRecords<Person>().ToList();
                        dataGridView.DataSource = personList;
                    }
                }
            }
        }

        public void readCsvFromLocalhost()
        {
            using (StreamReader csvFile = System.IO.File.OpenText("C:\\Temp\\population.csv"))
            {
                CsvReader csv = new CsvReader(csvFile);
                csv.Configuration.IgnoreHeaderWhiteSpace = true;
                personList = csv.GetRecords<Person>().ToList();
                //dataGridView.DataSource = personList;
                dataGridView.Invoke(new Action(() => dataGridView.DataSource = personList));
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            readCsv();
        }

        private void genderCmBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView.Refresh();
            var genderElement = genderCmBox.SelectedItem as Gender;

            if (personList.Count != 0)
            {
                List<Person> list = new List<Person>();

                if (genderElement.id == 1 || genderElement.id == 0)
                {
                    list = personList.Where(p => p.Gender == genderElement.id).ToList();
                }

                if (genderElement.id == 2)
                {
                    list = personList;
                }

                refreshDataGrid(list);
            }
            else
            {
                MessageBox.Show("There is no data in the data grid.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void refreshDataGrid(List<Person> pList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Age");
            dt.Columns.Add("Gender");
            dt.Columns.Add("County");
            dt.Columns.Add("Country");

            foreach (Person p in pList)
            {
                DataRow dRow = dt.NewRow();
                dRow["FirstName"] = p.FirstName;
                dRow["LastName"] = p.LastName;
                dRow["Age"] = p.Age;
                dRow["Gender"] = p.Gender;
                dRow["County"] = p.County;
                dRow["Country"] = p.County;
                dt.Rows.Add(dRow);
            }

            dataGridView.DataSource = dt;
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                ExcelExport ex = new ExcelExport();
                ex.SaveExcel(dataGridView);
            }
            else
            {
                MessageBox.Show("No Record To Export !", "Info");
            }

        }
    }
}