using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace CwiczMatme.Gameplay
{
    public class Dzialania
    {
        protected Random rnd = new Random();
        public bool[] Opcje = new bool[4];
        public int Punkty=0;
        protected int LiczbaMax = 100, LiczbaMin = 1;
        protected int LiczbaDzielMin = 2, LiczbaDzielMax = 10;
        protected int IloscElementowDzialania = 2;
        public double? UserInput;
        //zmienia zakres losowych liczb w zależności od wykonywanych działań
        private void SprawdzZakres(ref string rownanie)
        {
            if (rownanie[^1] == '/' || rownanie[^1] == '*')
                rownanie += rnd.Next(LiczbaDzielMin, LiczbaDzielMax);
            else
                rownanie += rnd.Next(LiczbaMin, LiczbaMax);
        }
        public double PodajWynik(string wygenerowaneRownanie)
        {
            double wartosc;
            wartosc = Math.Round(Convert.ToDouble(new DataTable().Compute(wygenerowaneRownanie,null)),1);
            if (UserInput == wartosc)
            {
                Punkty += 2;
            }
            else
            {
                Punkty--;
            }
            ZwiekszPoziomTrudnosci(Punkty);
            return wartosc;
        }

        public string StworzDzialanie()
        {
            string rownanie = " ";
            for (int i = 0; i < IloscElementowDzialania; i++)
            {
                if (i !=IloscElementowDzialania-1)
                {
                    SprawdzZakres(ref rownanie);
                    rownanie += $"{JakiZnakMiedzyLiczbami()}";
                }
                else
                {
                    SprawdzZakres(ref rownanie);
                }
            }
            return rownanie;
        }
        //tutaj będę musiał prawdopowodbnie dodać tablicę z boolami
        private char JakiZnakMiedzyLiczbami()
        {
            int wybor;
            List<char> symbole = new List<char>();

            if(Opcje[0])
            symbole.Add('+');
            if(Opcje[1])
            symbole.Add('-');
            if(Opcje[2])
            symbole.Add('*');
            if(Opcje[3])
            symbole.Add('/');
            wybor = rnd.Next(0,symbole.Count());
            if (wybor== symbole.IndexOf('*')|| wybor==symbole.IndexOf('/'))
            {
                wybor = rnd.Next(0,symbole.Count());
            }
            //teraz ta funkcja będzie musiała zwrócić losową wartośc z listy charów, myślę o losowej wartości z przedziału od 0 do symbole.Length i jeśli wyjdzie
            //* lub / to wykona losowanie ponownie i dopiero wtedy zwróci wartość
            //możliwe, że jeśli wybiorę mniej opcji to będzie out of range exception
            switch (wybor)
            {
                case 0: return symbole[0];
                case 1: return symbole[1];
                case 2: return symbole[2];
                case 3: return symbole[3];
                default:
                    return '0';
            }
        }
        private void ZwiekszPoziomTrudnosci(int punkty)
        {
            if (punkty%10==0)
                IloscElementowDzialania++;
        }

    }
}