using System;
using System.Collections.Generic;

namespace StabilniManzelstvi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool test = false;

            List<Female> female = new List<Female>();
            List<Male> male = new List<Male>();
            int[,] Woman = null;
            int[,] Man = null;
            int count = 0;

            while (!test)
            {
                try
                {
                    Console.WriteLine("Zadejte počet párů:");
                    count = int.Parse(Console.ReadLine());
                    test = true;

                    for (int i = 0; i < count; i++) //vytvoříme ženy a muže na vdávání
                    {
                        female.Add(new Female());
                        male.Add(new Male());
                    }

                    Woman = new int[count, count]; //preference žen, prázdné
                    Man = new int[count, count]; //preference mužů, prázdné

                    LoadArray(count, Woman, Man);

                    for (int i = 0; i < count; i++) //každému přidáme preference, jméno ale hlavně, že jsou single
                    {
                        female[i].numbername = i;
                        female[i].AddPreferenceW(Woman);
                        female[i].Husband = -1;

                        male[i].numbername = i;
                        male[i].AddPreferenceM(Man);
                        male[i].Wife = -1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ošklivý vstup, zkus znovu ({ex.Message})");
                }
            }

            bool allMarried = false;

            while (!allMarried)
            {
                // 1) single ženy udělají nabídku svému nejlepšímu muži
                for (int i = 0; i < female.Count; i++)
                {
                    if (female[i].Husband == -1 && female[i].preference.Count > 0)
                    {
                        MakeOffer(female[i], male);
                    }
                }

                // 2) muži si vyberou nejlepší nabídku podle svých preferencí, nehledě na to, jestli jsou zadaný
                for (int i = 0; i < male.Count; i++)
                {
                    ChooseWife(male[i], female);
                }

                // 3) zkontroluje, jestli jsou všichni v manželství
                allMarried = true;
                for (int i = 0; i < female.Count; i++)
                {
                    if (female[i].Husband == -1)
                    {
                        allMarried = false;
                        break; //aspoň jedna žena nemá manžela
                    }
                }
            }

            
            Console.WriteLine("Výstup:");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(female[i].Husband + 1);
            }

            Console.ReadLine();
        }


        //Načte preference a rozdělí je pro ženy a muže
        static void LoadArray(int size, int[,] Woman, int[,] Man)
        {
            Console.WriteLine($"Zadejte {size * 2} řádků (nejdřív ženy, pak muži):");

            for (int i = 0; i < size * 2; i++)
            {
                string[] line = Console.ReadLine().Split();

                for (int j = 0; j < line.Length; j++)
                {
                    int info = int.Parse(line[j]) - 1;
                    if (i < size)
                        Woman[i, j] = info;
                    else
                        Man[i - size, j] = info;
                }
            }
        }


        //Vezme preferenci s nejnižším indexem aka největší preferenci -> po odmítnutí/manželství ji odstraní
        static void MakeOffer(Female female, List<Male> list)
        {
            int wanted = female.preference[0];
            list[wanted].offers.Add(female.numbername);
            female.preference.RemoveAt(0);
        }

        static void ChooseWife(Male male, List<Female> list)
        {
            if (male.offers.Count == 0) return; //nikdo ho nechce

            int best = male.Wife;

            for (int i = 0; i < male.offers.Count; i++)
            {
                int offer = male.offers[i];
                // porovnání podle preferencí (nižší index = lepší)
                if (best == -1 || male.preference.IndexOf(offer) < male.preference.IndexOf(best))
                {
                    // pokud měl manželku, rozvede ji -> smutná žena
                    if (male.Wife != -1)
                    {
                        list[male.Wife].Husband = -1;
                    }

                    // nové manželství
                    male.Wife = offer;
                    list[offer].Husband = male.numbername;
                    best = offer;
                }
            }

            // vyčistit nabídky po vyhodnocení
            male.offers.Clear();
        }
    }

    class Female
    {
        public int numbername; //int aby se nikde nemuselo přepínat z str na int, až na začátek
        public int Husband;
        public List<int> preference = new List<int>();

        public void AddPreferenceW(int[,] Woman)
        {
            for (int i = 0; i < Woman.GetLength(1); i++)
            {
                preference.Add(Woman[numbername, i]);
            }
        }
    }

    class Male
    {
        public int numbername;
        public int Wife;
        public List<int> preference = new List<int>();
        public List<int> offers = new List<int>();

        public void AddPreferenceM(int[,] Man)
        {
            for (int i = 0; i < Man.GetLength(1); i++)
            {
                preference.Add(Man[numbername, i]);
            }
        }
    }
}
