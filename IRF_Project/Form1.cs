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
    }
}
