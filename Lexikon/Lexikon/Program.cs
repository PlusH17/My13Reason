using System;
using System.Collections.Generic;
using System.Linq;

namespace Lexikon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadejte vstup:");
            string vstup = Console.ReadLine();

            List<string> slova = new List<string>(vstup.Split(' '));
            List<string> porovnani = new List<string>();
            List<string> vsechny = new List<string>();


            for (int i = 0; i < slova.Count - 1; i++)
            {
                string prvni = slova[i];
                string druhy = slova[i + 1];

                int minDelka = Math.Min(prvni.Length, druhy.Length);

                for (int j = 0; j < minDelka; j++)
                {
                    if (prvni[j] != druhy[j])
                    {
                        porovnani.Add($"{prvni[j]} < {druhy[j]}");
                        if (vsechny.Contains(prvni[j].ToString()) == false)
                            vsechny.Add(prvni[j].ToString());
                        if (vsechny.Contains(druhy[j].ToString()) == false)
                            vsechny.Add(druhy[j].ToString());
                        break;
                    }
                }


            }

            for (int i = 0; i < porovnani.Count; i++)
            {
                string pomer = porovnani[i];
                string pred = pomer[0].ToString();
                string za = pomer[pomer.Length - 1].ToString();

                if (porovnani.Contains($"{za} < {pred}"))
                {
                    Console.WriteLine("NELZE - obsahuje cyklus");
                    Console.ReadLine();
                    break;
                }
            }


            List<string> vzadu = new List<string>();
            List<string> konecny = new List<string>();

            while (vsechny.Count > 0)
            {
                vzadu.Clear(); //
                foreach (string pomer in porovnani)
                {
                    string posledni = pomer[pomer.Length - 1].ToString();
                    if (vzadu.Contains(posledni) == false)
                        vzadu.Add(posledni);
                }

                for (int j = vsechny.Count - 1; j >= 0; j--) // Pozpátku, protože Index out of range je podpásovka
                {
                    string pismenko = vsechny[j];
                    if (vzadu.Contains(pismenko) == false)
                    {
                        konecny.Add(pismenko);
                        vsechny.RemoveAt(j); 
                    }
                }

                for (int i = porovnani.Count - 1; i >= 0; i--) // Opět pozpátku... aspoň mám kontent do tabulky bolesti
                {
                    string dvojice = porovnani[i]; 
                    bool obsahujePismenko = false;

                    foreach (string pismenko in konecny)
                    {
                        if (dvojice.Contains(pismenko))
                        {
                            obsahujePismenko = true;
                            break; 
                        }
                    }
                    if (obsahujePismenko == true)
                    {
                        porovnani.RemoveAt(i);
                    }
                }
            }

            Console.WriteLine("Správné pořádí znaků v tajemném slovníku (pokud se u něktérého písmenka neví, tak ho nevypíše - nechceme šířit hoaxy):");
            Console.WriteLine(string.Join("  ->  ", konecny));
            Console.ReadLine();

            //Bude uspořádání abecedy vždy jednoznačné? Odpověď napište do komentáře v programu. NE - viz příklad 5 (xyz xz), protože není dán žádný poměr mezi x a zbylými písmeny
            //Uveďte příklad vstupu, pro který takové uspořádání ani nexistuje. Ty co obsahují cykly, viz příklad 6 (aa ab ac ca ba), taky by šlo "abc acd bdc cbc cad cac"
        }
    }
}