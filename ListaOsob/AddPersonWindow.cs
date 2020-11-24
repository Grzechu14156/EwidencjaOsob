using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using System.IO;
using System.Globalization;

namespace ListaOsob
{
    public partial class AddPersonWindow : Form
    {
        List<Options> record;
        Form1 form;

        public AddPersonWindow(List<Options> records, Form1 form)
        {
            InitializeComponent();
            record = records;
            this.form = form;
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            record = new List<Options>
                {
                    new Options { Imie = NameInput.Text, Nazwisko = Surname.Text, Wiek = Int16.Parse(Age.Text), MiejsceZamieszkania = Place.Text, Status = Status.Text },
                };
            using (var stream = File.Open("C:\\Users\\Grzegorz\\Desktop\\ewidencja.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = false;
                csv.WriteRecords(record);
            }
            form.ReReadFile();
            this.Close();
        }
    }
}
