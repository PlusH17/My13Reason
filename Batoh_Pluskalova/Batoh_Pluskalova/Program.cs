//Není načítání ze souboru, pardon (dodělám po plese), ale s natáčením reakce + přípravou na ni, ale hlavně a ÚKLIDEM PO NÍ, nebyl čas ani energie... Jako omluvu za polohotový úkol, malý spoiler: Jahn není Jahoda, ale Borovice je Michaela. 


using System;
using System.Collections.Generic;
using System.Linq;

namespace Batoh_Pluskalova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Nacteni();
        }

        static void Nacteni()
        {
            #region Načítání dat
            Console.WriteLine("Jakou cenu mají věci?");
            List<int> cena_veci = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            Console.WriteLine("Jakou váhu mají věci?");
            List<int> vaha_veci = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            Console.WriteLine("Jakou maximální váhu?");
            int maxVaha = int.Parse(Console.ReadLine());
            #endregion

            #region Backtracking
            List<int> bestKombo = new List<int>();

            // Voláme bez maxCena
            Backtrecing(maxVaha, vaha_veci, new List<int>(), 0, cena_veci, bestKombo);
            #endregion

            #region Výsledky
            if (bestKombo.Count == 0)
                Console.WriteLine("Neexistuje žádná kombinace věcí.");
            else
            {
                // Výpočet celkové ceny pro výpis
                int celkovaCena = 0;
                foreach (int i in bestKombo) celkovaCena += cena_veci[i];

                Console.WriteLine(celkovaCena);
                foreach (var index in bestKombo)
                {
                    Console.Write((index + 1) + " "); // Výpis indexu + 1 (1-based)
                }
            }
            Console.ReadLine();
            #endregion 
        }

        static void Backtrecing(int maxVaha, List<int> vahyVeci, List<int> IndexyPouzivanych, int index_pridane, List<int> uzitekveci, List<int> BestIndexy)
        {
            int soucetUzitku = 0;
            int soucetVahy = 0;
            foreach (var index in IndexyPouzivanych)
            {
                soucetUzitku += uzitekveci[index];
                soucetVahy += vahyVeci[index];
            }

            int nejlepsiUzitek = 0;
            foreach (var index in BestIndexy)
            {
                nejlepsiUzitek += uzitekveci[index];
            }

            // Pokud jsme našli lepší, uložíme ji
            if (soucetUzitku > nejlepsiUzitek)
            {
                BestIndexy.Clear();
                BestIndexy.AddRange(IndexyPouzivanych);
            }

            for (int i = index_pridane; i < vahyVeci.Count; i++)
            {
                if (soucetVahy + vahyVeci[i] <= maxVaha)
                {
                    IndexyPouzivanych.Add(i);
                    // Rekurze (i + 1 zajistí 0-1 výběr)
                    Backtrecing(maxVaha, vahyVeci, IndexyPouzivanych, i + 1, uzitekveci, BestIndexy);
                    IndexyPouzivanych.RemoveAt(IndexyPouzivanych.Count - 1);
                }
            }
        }
    }
}