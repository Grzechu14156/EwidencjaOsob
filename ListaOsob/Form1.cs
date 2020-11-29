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
        string search;
        public List<Osoba> records;
        private FileDataSource<Osoba> fileDataSource;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            //Get records
            fileDataSource = new FileDataSource<Osoba>();
            var records = fileDataSource.GetAll();
            FillInListView(records);


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
            List<Osoba> results = new List<Osoba>();
            var records = fileDataSource.GetAll();

            if (search != null)
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
            FillInListView(results);
        }

        public void FillInListView(List<Osoba> options)
        {
            //tutaj updatuje list view
            listView.Items.Clear();
            foreach (Osoba person in options)
            {
                ListViewItem viewItem = new ListViewItem(person.Imie);
                viewItem.SubItems.Add(person.Nazwisko);
                viewItem.SubItems.Add(person.Wiek.ToString());
                viewItem.SubItems.Add(person.MiejsceZamieszkania);
                viewItem.SubItems.Add(person.Status);
                viewItem.Tag = person;
                listView.Items.Add(viewItem);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var win = new AddPersonWindow(this);
            win.Show();
        }

        private void RemovePerson(object sender, EventArgs e)
        {
            Osoba personToRemove = (Osoba) listView.SelectedItems[0].Tag;
            fileDataSource.Remove(personToRemove.Id);
            var records = fileDataSource.GetAll();
            FillInListView(records);
        }

        public void AddPerson(Osoba osoba)
        {
            fileDataSource.Add(osoba);
        }
    }
}
