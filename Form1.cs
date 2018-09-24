using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace KaminskiAdrianBazaKsiazek
{
    public partial class Form1 : Form
    {
        List<Osoba> ListaOsob; // tworzymy listę osób (czytelników), która będzie zapisywana do pliku xml
        List<Autorzy> ListaAutorow; // tworzymy listę autorów
        List<Ksiazki> ListaKsiazek; // tworzymy liste uslug, do ktorej bedziemy dodwac pozycje z listview 
        public Form1()
        {
            // inicjacja list
            ListaOsob = new List<Osoba>();
            ListaAutorow = new List<Autorzy>();
            ListaKsiazek = new List<Ksiazki>(); 
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // dodaje osobe (czytelnika)
        {
            // konstruktor dodaje do listy nową osobę
            ListaOsob.Add(new Osoba(textBox1.Text, textBox2.Text, textBox9.Text, textBox12.Text, textBox11.Text));
            // po każdym kliknięciu cała lista nabywców wyprowadzana jest do comBoxa
            // dlatego przed każdymwyprowadzeniem czyścimy comboxa, żeby dane się nie powtarzały
            comboBox1.Items.Clear();
            if (ListaOsob != null)
                foreach (Osoba a in ListaOsob)
                {
                    comboBox1.Items.Add(a.Nazwisko_osoby);
                }
        }

        // zdarzenie, które po każdym kliknięciu na pozycji w comboBox, wypełnia textboxy
        // zgodnie z danymi
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                textBox1.Text = ListaOsob[comboBox1.SelectedIndex].Imie_osoby;
                textBox2.Text = ListaOsob[comboBox1.SelectedIndex].Nazwisko_osoby;
                textBox9.Text = ListaOsob[comboBox1.SelectedIndex].Ulica;
                textBox12.Text = ListaOsob[comboBox1.SelectedIndex].Kod_pocztowy;
                textBox11.Text = ListaOsob[comboBox1.SelectedIndex].Miasto;
            }
        }


        private void button9_Click(object sender, EventArgs e) // dodanie autora
        {
            ListaAutorow.Add(new Autorzy(textBox10.Text, textBox13.Text));
            comboBox2.Items.Clear();
            if(ListaAutorow != null)
                foreach(Autorzy aut in ListaAutorow)
                {
                    comboBox2.Items.Add(aut.Nazwiskoautora);
                }
        }

        // wypełnienie textboxów zgodnie z danymi
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0)
            {
                textBox10.Text = ListaAutorow[comboBox2.SelectedIndex].Imieautora;
                textBox13.Text = ListaAutorow[comboBox2.SelectedIndex].Nazwiskoautora;
            }
        }

        // zapisanie listy osób do pliku xml korzystając z serializacji danych XMLSerializer
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                TextWriter textWriter = new StreamWriter(@"C:/baza_ksiazek/osoby.xml");
                XmlSerializer s = new XmlSerializer(typeof(List<Osoba>));
                s.Serialize(textWriter, ListaOsob);
                textWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // wczytanie osób zapisanych w pliku xml i wyprowadzenie danych do listy
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                TextReader textReader = new StreamReader(@"C:/baza_ksiazek/osoby.xml");
                XmlSerializer ds = new XmlSerializer(typeof(List<Osoba>));
                ListaOsob = (List<Osoba>)ds.Deserialize(textReader);
                textReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // wypełniamy combobox wczytanymi danymi, więc czyścimy comboboxy, żeby się nie powtarzały
            comboBox1.Items.Clear();

            foreach (Osoba n in ListaOsob)
            {
                comboBox1.Items.Add(n.Nazwisko_osoby);
            }
        }


        private void DodajPozycje()
        {
            if (comboBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                //dodajemy pozycje do listy książek wykorzystując konstruktor
                ListaKsiazek.Add(new Ksiazki(comboBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text));
                //dane z uslug zapisujemy w jednym stringu i dodajemy do listview
                string pozycja = String.Format("{0} | {1} | {2} | {3}", comboBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                listBox1.Items.Add(pozycja);
            }
            else
            {
                MessageBox.Show("Uzupełnij puste pola");
            }
        }
        private void button3_Click(object sender, EventArgs e) // dodanie nowej pozycji do listview
        {
            if(SprawdzPoprawnosc())
                DodajPozycje();
            
        }

        private void button4_Click(object sender, EventArgs e) // usuwanie pozycji
        {
            try
            {
                //usuwamy pozycje, ktora jest zaznaczona(selected index)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            catch (ArgumentOutOfRangeException)
            {
                //jeśli nie zaznaczymy żadnej pozycji wyświetlamy komunikat błędu
                MessageBox.Show("Nie zaznaczono żadnego elementu.");
            }
        }

        void Wyczysc() // czyści pola
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox9.Text = null;
            textBox10.Text = null;
            textBox11.Text = null;
            textBox12.Text = null;
            textBox13.Text = null;
            comboBox1.Text = null;
            comboBox2.Text = null;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Wyczysc(); //metoda czyszczaca wszystkie pola
        }

        private bool SprawdzPoprawnosc() // sprawdzenie poprawności
        {
            string WzorData = "^[0-9]{4}-[0-9]{2}-[0-9]{2}$";
            string WzorRok = "^[0-9][0-9][0-9][0-9]$";
            string WzorISBN = "^[0-9]{3}-[0-9]{2}-[0-9]{5}-[0-9]-[0-9]$";
            string WzorKod_pocztowy = "^[0-9]{2}-[0-9]{3}$";

            Regex data = new Regex(WzorData);
            Regex rok = new Regex(WzorRok);
            Regex isbn = new Regex(WzorISBN);
            Regex kod_pocztowy = new Regex(WzorKod_pocztowy);
            
            if(data.IsMatch(textBox7.Text) && rok.IsMatch(textBox4.Text) && isbn.IsMatch(textBox5.Text) && kod_pocztowy.IsMatch(textBox12.Text))
                return true;
            else
            {
                MessageBox.Show("Sprawdź poprawność danych.\n\nWzory danych:\nData: YYYY-MM-DD, np. 2015-11-10\nRok: YYYY, np. 2015\nNumer ISBN: NNN-NN-NNNNN-N-N, np. 123-12-12345-1-1\nKod pocztowy: NN-NNN, np. 34-125");
                return false;
            }
        }


        void Drukuj() //metoda drukowania z użyciem iTextSharp
        {
            Document pdfDoc = new Document();
            FileStream fs = new FileStream("C:\\baza_ksiazek\\" + @textBox6.Text + ".pdf", FileMode.OpenOrCreate);

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, fs);
            BaseFont arial = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\arial.ttf", "iso-8859-2", BaseFont.EMBEDDED);

            Font font = new iTextSharp.text.Font(arial, 9);
            Font fontBold = new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD);

            pdfDoc.Open();

            PdfPTable tabela = new PdfPTable(4);
            Paragraph tytul = new Paragraph(String.Format("Lista nr. {0}\nData sporządzenia: {1}\n", textBox6.Text, textBox7.Text), fontBold);
            tytul.Alignment = 0;
            tytul.ExtraParagraphSpace = 10;
            pdfDoc.Add(tytul);

            Paragraph czytelnik = new Paragraph(String.Format("CZYTELNIK\n Imie: {0}\nNazwisko: {1}\nUlica: {2}\nKod pocztowy: {3}\nMiasto: {4}", textBox1.Text, textBox2.Text, textBox9.Text, textBox12.Text, textBox11.Text));
            czytelnik.Alignment = 2;
            pdfDoc.Add(czytelnik);


            pdfDoc.Add(new Paragraph(" "));
            tabela.AddCell(new Paragraph("AUTOR", fontBold));
            tabela.AddCell(new Paragraph("TYTUŁ", fontBold));
            tabela.AddCell(new Paragraph("ROK", fontBold));
            tabela.AddCell(new Paragraph("NUMER ISBN", fontBold));

            foreach(Ksiazki u in ListaKsiazek)
            {
                tabela.AddCell(new Paragraph(u.Nazwiskoautora, font));
                tabela.AddCell(new Paragraph(u.Tytul,font));
                tabela.AddCell(new Paragraph(u.Rok_wydania,font));
                tabela.AddCell(new Paragraph(u.Nr_ISBN, font));
            }

            pdfDoc.Add(tabela);

            pdfDoc.Close();
            fs.Close();

        }

        private void button7_Click(object sender, EventArgs e) // drukowanie do PDF'a
        {
                Drukuj();
                Wyczysc();
        }

        private void button8_Click(object sender, EventArgs e) // wyjście z aplikacji
        {
            Application.Exit();
        }





    }
}
