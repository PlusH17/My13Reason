using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace UvodFILM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Film filmO1 = new Film();
            filmO1.Nazev = "Barbie";
            filmO1.JmenoRezisera = "Greta";
            filmO1.RokVzniku = 2023;
            filmO1.PrijmeniRezisera = "Gervig";

            Film film02 = new Film();
            film02.Nazev = "Vykoupení z věznice Shawshank";
            film02.JmenoRezisera = "Frank";
            film02.PrijmeniRezisera = "Darabont";
            film02.RokVzniku = 1994;
            
            Film film03 = new Film();
            film03.Nazev = "Princezna Nevěsta";
            film03.JmenoRezisera = "Rob";
            film03.PrijmeniRezisera = "Reiner";
            film03.RokVzniku = 1987;

            List<Film> film = new List<Film>();
            film.Add(filmO1);
            film.Add(film02);
            film.Add(film03);

            Random random = new Random();
            for (int i = 0; i < film.Count; i++)
            {
                Film HodnocenyFilm = film[i];
                for (int j = 0; j < 16; j++)
                {
                    
                    int PocetHvezd = random.Next(0, 6);
                    HodnocenyFilm.PridatHodnoceni(PocetHvezd);
                }

                HodnocenyFilm.ZjistitHodnoceni();
            }



            Film BESTfilm = film[0];
            Film LONGfilm = film[0];


            foreach (Film i in film)
            {
                if (i.Hodnoceni > BESTfilm.Hodnoceni)
                {
                    BESTfilm = i;
                }

                if (i.Nazev.Length > LONGfilm.Nazev.Length)
                {
                    LONGfilm= i;
                }

                if (i.Hodnoceni  < 3)
                {
                    Console.WriteLine(i.Nazev + " je odpad! Má hodnocení jen  " + i.Hodnoceni);
                }


            }

            Console.WriteLine("Film s nejlepším hodnocením je " + BESTfilm.Nazev);
            Console.WriteLine("Film s nejdelším názvem je " + LONGfilm.Nazev);

            filmO1.Vypis();
            film02.Vypis();
            film03.Vypis();


            Console.ReadLine();
        }
    }

    class Film
    {
        public string Nazev;
        public string JmenoRezisera;
        public string PrijmeniRezisera;
        public int RokVzniku;

        public double Hodnoceni { get; private set; }

        public List<int> TabulkaHodnoceni = new List<int>();
        public void PridatHodnoceni(int NoveHodnoceni)
        {
            TabulkaHodnoceni.Add(NoveHodnoceni);
        }
        public void ZjistitHodnoceni()
        {
            if (TabulkaHodnoceni.Count> 0) {
                int Soucet = 0;
                foreach (int i in TabulkaHodnoceni)
                {

                    Soucet += i;
                }
                Hodnoceni = (double) Soucet / TabulkaHodnoceni.Count;
            }
        }



        string vypis = "";

        public override string ToString()
        {
            vypis += Nazev;
            vypis += " (";
            vypis += RokVzniku.ToString() + "; ";
            vypis += PrijmeniRezisera;
            vypis += " ";
            vypis += JmenoRezisera[0];
            vypis += ".) : ";
            vypis += Hodnoceni;
            vypis += " hvězdiček";
            return vypis;
        }

        public void Vypis()
        {
            Console.WriteLine(ToString());
        }
        

    }
}
