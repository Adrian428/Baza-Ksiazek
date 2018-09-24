using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaminskiAdrianBazaKsiazek
{
    class Ksiazki:Autorzy
    {
        public string Tytul
        {
            get;
            set;
        }

        public string Rok_wydania
        {
            get;
            set;
        }

        public string Nr_ISBN
        {
            get;
            set;
        }

        public Ksiazki(string _Nazwiskoautora, string _Tytul,string _Rok_wydania, string _Nr_ISBN)
        {
            Nazwiskoautora = _Nazwiskoautora;
            Tytul = _Tytul;
            Rok_wydania = _Rok_wydania;
            Nr_ISBN = _Nr_ISBN;
        }
    }
}
