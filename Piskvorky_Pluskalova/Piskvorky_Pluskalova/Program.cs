using System;

namespace Piskvorky_Pluskalova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Jakou chcete výšku pole?");
            int vyska = Int32.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Jakou chcete šířku pole?");
            int sirka = Int32.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Na kolik žetonů chcete hrát?");
            int zetony = Int32.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Kolik hráčů bude hrát?");
            int hrac = Int32.Parse(Console.ReadLine());
            Console.Clear();

            Hra hra = new Hra(zetony, sirka, vyska, hrac);
            Hrac vitez = hra.Play();

            Console.WriteLine($"Vítězem je: {vitez.Jmeno} s symbolem {vitez.Symbol}");
            Console.ReadLine();
        }
    }

    class Hra
    {
        private int pocetVyhernichZetonu;
        private int pocetSloupcu;
        private int pocetRadku;
        private int pocetHracu;

        private char[,] hraciPole;
        private Hrac[] hraci;

        public Hra(int pocetVyhernichZetonu, int pocetSloupcu, int pocetRadku, int pocetHracu)
        {
            this.pocetVyhernichZetonu = pocetVyhernichZetonu;
            this.pocetSloupcu = pocetSloupcu;
            this.pocetRadku = pocetRadku;
            this.pocetHracu = pocetHracu;
            hraciPole = new char[pocetSloupcu, pocetRadku];

            for (int x = 0; x < pocetSloupcu; x++)
                for (int y = 0; y < pocetRadku; y++)
                    hraciPole[x, y] = ' ';

            hraci = new Hrac[pocetHracu];
            VytvorHrace();
        }

        private void VytvorHrace()
        {
            for (int i = 0; i < hraci.Length; i++)
            {
                Console.WriteLine($"Napiš jméno hráče {i + 1}:");
                string jmeno = Console.ReadLine();
                Console.WriteLine($"Jaký symbol pro něj budeš chtít použít:");
                char symbol = Console.ReadKey().KeyChar;
                Console.ReadLine(); 
                hraci[i] = new Hrac(jmeno, symbol);
                Console.Clear();
            }
        }

        public Hrac Play()
        {
            int tah = 0;
            while (true)
            {
                int indexHrace = tah % pocetHracu;
                Hrac aktualniHrac = hraci[indexHrace];

                Console.WriteLine($"Nyní hraje: {aktualniHrac.Jmeno} ({aktualniHrac.Symbol})");
                VypisPole();

                int sloupec = -1;
                bool validniTah = false;

                while (!validniTah)
                {
                    Console.WriteLine("Do kterého sloupce chceš hodit žeton?");
                    if (int.TryParse(Console.ReadLine(), out sloupec) && sloupec >= 1 && sloupec <= pocetSloupcu)  // Pokusí se převést vstup z klávesnice na celé číslo a zároveň zkontroluje, že číslo je v platném rozsahu sloupců

                    {
                        if (Zeton(sloupec - 1, aktualniHrac.Symbol, out Position pozice))
                        {
                            if (Check(pozice, aktualniHrac))
                                return aktualniHrac;
                            validniTah = true;
                        }
                        else
                        {
                            Console.WriteLine("Sloupec je plný! Zkus jiný.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Neplatný vstup. Zadej číslo sloupce.");
                    }
                }

                tah++;
                Console.Clear();
            }
        }

        private void VypisPole()
        {
            Console.Write("");
            for (int i = 0; i < pocetSloupcu; i++) //pro hezká čísla nahoře
                Console.Write($" {i + 1}");
            Console.WriteLine();

            for (int y = 0; y < pocetRadku; y++)
            {
                Console.Write("|"); //pro hezky vypadající pole
                for (int x = 0; x < pocetSloupcu; x++)
                {
                    Console.Write($"{hraciPole[x, y]}|");
                }
                Console.WriteLine();
            }
        }

        private bool Zeton(int sloupec, char symbol, out Position pozice)
        {
            for (int y = pocetRadku - 1; y >= 0; y--) //od spodu, protože gravitace
            {
                if (hraciPole[sloupec, y] == ' ')
                {
                    hraciPole[sloupec, y] = symbol;
                    pozice = new Position { Column = sloupec, Row = y };
                    return true;
                }
            }

            pozice = default;
            return false;
        }

        private bool Check(Position pos, Hrac hrac)
        {
            return CheckDirection(pos, hrac, 1, 0) || // řádek
                   CheckDirection(pos, hrac, 0, 1) || // sloupec
                   CheckDirection(pos, hrac, 1, 1) || // diagonála ↘
                   CheckDirection(pos, hrac, 1, -1);  // diagonála ↙
        }

        private bool CheckDirection(Position pos, Hrac hrac, int dx, int dy)
        {
            int count = 1;

            count += PocetVDirection(pos, hrac, dx, dy);
            count += PocetVDirection(pos, hrac, -dx, -dy);

            return count >= pocetVyhernichZetonu;
        }

        private int PocetVDirection(Position pos, Hrac hrac, int dx, int dy)
        {
            int count = 0;
            int x = pos.Column + dx;
            int y = pos.Row + dy;

            while (x >= 0 && x < pocetSloupcu && y >= 0 && y < pocetRadku && hraciPole[x, y] == hrac.Symbol)
            {
                count++;
                x += dx;
                y += dy;
            }

            return count;
        }
    }

    class Hrac
    {
        public string Jmeno { get; }
        public char Symbol { get; }

        public Hrac(string jmeno, char symbol)
        {
            Jmeno = jmeno;
            Symbol = symbol;
        }
    }

    struct Position
    {
        public int Row;
        public int Column;
    }
}
