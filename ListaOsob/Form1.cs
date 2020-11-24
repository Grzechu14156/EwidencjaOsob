using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;

namespace ListaOsob
{
    public partial class Form1 : Form
    {
        private bool isVisible = true;
        string search;
        public List<Options> records;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;

            using (var reader = new StreamReader("C:\\Users\\Grzegorz\\Desktop\\ewidencja.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                records = csv.GetRecords<Options>().ToList();
                // wywolac FillInListView()
                FillInListView(records);

            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Clear(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        public void Search()
        {
            search = textBox1.Text;
            String option = comboBox1.Text;
            List<Options> results = new List<Options>();

            if (search == null)
            {
                ReReadFile();
            }
            else
            {
                switch (option)
                            {
                                case "Imię":
                                    results = records.Where(x => x.Imie.StartsWith(search)).ToList();
                                    break;
                                case "Nazwisko":
                                    results = records.Where(x => x.Nazwisko.StartsWith(search)).ToList();
                                    break;
                                case "Wiek":
                                    results = records.Where(x => x.Wiek.Equals(search)).ToList();
                                    break;
                                case "Miejsce Zamieszkania":
                                    results = records.Where(x => x.MiejsceZamieszkania.StartsWith(search)).ToList();
                                    break;
                                case "Status":
                                    results = records.Where(x => x.Status.StartsWith(search)).ToList();
                                    break;
                            }
            }
            
            //var results = records.Where(x => x.Imie.StartsWith(search)).ToList();
            FillInListView(results);
        }

        public void FillInListView(List<Options> options)
        {
            //tutaj updatuje list view
            listView.Items.Clear();
            foreach (Options person in options)
            {
                ListViewItem viewItem = new ListViewItem(person.Imie);
                viewItem.SubItems.Add(person.Nazwisko);
                viewItem.SubItems.Add(person.Wiek.ToString());
                viewItem.SubItems.Add(person.MiejsceZamieszkania);
                viewItem.SubItems.Add(person.Status);
                listView.Items.Add(viewItem);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var win = new AddPersonWindow(records, this);
            win.Show();
        }

        public void ReReadFile()
        {
            using (var reader = new StreamReader("C:\\Users\\Grzegorz\\Desktop\\ewidencja.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                records = csv.GetRecords<Options>().ToList();
                // wywolac FillInListView()
                FillInListView(records);

            }
        }

        public void SaveFile()
        {
            using (var writer = new StreamWriter("C:\\Users\\Grzegorz\\Desktop\\ewidencja.csv"))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.Delimiter = ";";
                csvWriter.Configuration.HasHeaderRecord = true;
                csvWriter.WriteRecords(records);
            }
        }
        private void RemovePerson(object sender, EventArgs e)
        {
            Options personToRemove = new Options();
            personToRemove.Imie = listView.SelectedItems[0].SubItems[0].Text;
            personToRemove.Nazwisko = listView.SelectedItems[0].SubItems[1].Text;
            personToRemove.Wiek = int.Parse(listView.SelectedItems[0].SubItems[2].Text);
            personToRemove.MiejsceZamieszkania = listView.SelectedItems[0].SubItems[3].Text;
            personToRemove.Status = listView.SelectedItems[0].SubItems[4].Text;
            var removeRecord = records.FirstOrDefault(x => x.Imie == personToRemove.Imie && x.Nazwisko == personToRemove.Nazwisko && x.Wiek == personToRemove.Wiek);
            if (removeRecord != null)
                records.Remove(removeRecord);
            //var item = records.Find(x => x. == );
            //for (int i = 0; i == records.Count(); i++ )
            //{
            //    if ()
            //}
            //records.RemoveAt(ID);
            SaveFile();
            FillInListView(records);
        }
    }
}
