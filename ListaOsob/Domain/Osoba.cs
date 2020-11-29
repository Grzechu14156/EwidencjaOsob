using ListaOsob.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaOsob
{
    public class Osoba : IDomainItem
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }
        public string MiejsceZamieszkania { get; set; }
        public string Status { get; set; }
    }
}
