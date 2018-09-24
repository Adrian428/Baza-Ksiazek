using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaminskiAdrianBazaKsiazek
{
    class Autorzy
    {

        public string Imieautora
        {
            get;set;
        }

        public string Nazwiskoautora
        {
            get;set;
        }

        public Autorzy()
        {
        }

        public Autorzy(string _Imieautora, string _Nazwiskoautora)
        {
            this.Imieautora = _Imieautora;
            this.Nazwiskoautora = _Nazwiskoautora;
        }
    }
}
