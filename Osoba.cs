using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KaminskiAdrianBazaKsiazek
{
    [XmlRoot("Autorzy")]
    public class Osoba
    {
        [XmlElement("Imie")]
        public string Imie_osoby
        {
            get;
            set;
        }
        [XmlElement("Nazwisko")]
        public string Nazwisko_osoby
        {
            get;
            set;
        }

        [XmlElement("Ulica")]
        public string Ulica
        {
            get;
            set;
        }

        [XmlElement("Kod_pocztowy")]
        public string Kod_pocztowy
        {
            get;
            set;
        }

        [XmlElement("Miasto")]
        public string Miasto
        {
            get;
            set;
        }

        public Osoba()
        {
        }
        public Osoba(string _Imie_osoby, string _Nazwisko_osoby, string _Ulica, string _Kod_pocztowy, string _Miasto)
        {
            this.Imie_osoby = _Imie_osoby;
            this.Nazwisko_osoby = _Nazwisko_osoby;
            this.Ulica = _Ulica;
            this.Kod_pocztowy = _Kod_pocztowy;
            this.Miasto = _Miasto;
        }
    }
}
