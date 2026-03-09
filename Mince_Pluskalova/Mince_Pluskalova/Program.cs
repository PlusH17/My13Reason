using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mince_Pluskalova
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
            Console.WriteLine("Jaké mince máme k dispozici?");
            List<int> typy_minci = new List<int>();
            string[] typy = Console.ReadLine().Split();
            for (int i = 0; i < typy.Length; i++)
            {
                typy_minci.Add(int.Parse(typy[i]));
            }

            Console.WriteLine("Jakou chcete hodnotu");
            int suma = int.Parse(Console.ReadLine());
            #endregion

            #region Backtrecing
            List<List<int>> vysledky = new List<List<int>>();

            Backtrecing(suma, typy_minci, new List<int>(), 0, vysledky);
            #endregion

            #region Výsledky
            if (vysledky.Count == 0)
                Console.WriteLine("Neexistuje žádná kombinace mincí, která by dosáhla požadované hodnoty.");


            foreach (var item in vysledky)
            {
                
                Console.WriteLine(string.Join(" ", item));
            }

            Console.ReadLine();
            #endregion 
        }

        static void Backtrecing(int suma, List<int> typy, List<int> pouzivam, int index_pridane, List<List<int>> vysledky)
        {
            if (suma == 0) 
                return;
            int soucet = pouzivam.Sum();

            if (soucet == suma) //Našli jsme řešení (jupí), přidáme ho do výsledků
            {
                vysledky.Add(new List<int>(pouzivam));
                return;
            }

            for (int i = index_pridane; i < typy.Count; i++) //Procházíme všechny typy mincí, které můžeme přidat (od indexu poslední přidané mince, ať se nevracíme zpátky)
            {
                if (soucet + typy[i] <= suma) //rovnou kontrola, jestli přidáním této mince nepřekročíme požadovanou hodnotu
                {
                    pouzivam.Add(typy[i]);
                    Backtrecing(suma, typy, pouzivam, i, vysledky);
                    pouzivam.RemoveAt(pouzivam.Count - 1); 
                }
            }
        }
    }
}
