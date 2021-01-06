using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            using (StreamReader csvFile = System.IO.File.OpenText("C:\\Temp\\population.csv"))
            {
                CsvReader csv = new CsvReader(csvFile);
                csv.Configuration.IgnoreHeaderWhiteSpace = true;
                personList = csv.GetRecords<Person>().ToList();
                dataGridView.DataSource = personList;
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
    }
}
