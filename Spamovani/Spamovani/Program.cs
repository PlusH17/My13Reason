using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        
        Console.WriteLine("Zadejte matici počtu zpráv (hodnota -1 pro neexistující kontakt):");
        List<int[]> matice = new List<int[]>(); // "tabulka" 
        string radek;
        while (!string.IsNullOrEmpty(radek = Console.ReadLine())) // Čte vstupní řádky, dokud je co číst
        {
            matice.Add(radek.Split(' ').Select(int.Parse).ToArray()); // Parsuje řádek do pole čísel a přidává do "tabulky"
        }

        int PocetStudentu = matice.Count; // Počet studentů (velikost matice)
                
        Console.WriteLine("Zadejte jména kontaktů oddělená středníkem:");
        string[] jmena = Console.ReadLine().Split(';'); // seznam jmen

        Console.WriteLine("Zadejte jméno zoufalého studenta hledajíího řešení práce:");
        string startovniJmeno = Console.ReadLine();

        int startovniIndex = Array.IndexOf(jmena, startovniJmeno); //pozice studenta v "tabulce"
        if (startovniIndex == -1) // Pokud jméno není v seznamu studentu, tak konec
        {
            Console.WriteLine("Chyba: Zoufalý student není v tabulce kontaktů");
            return;
        }

        // Pole potřebná pro Dijkstrův algoritmus: 
        int[] vzdalenosti = Enumerable.Repeat(int.MaxValue, PocetStudentu).ToArray(); // Pole vzdáleností, všechny hodnoty v něm na max
        bool[] navstiveno = new bool[PocetStudentu]; 
        int[] predchudci = new int[PocetStudentu]; // Pole "předchůdců" pro každého studenta, například pro Matouše je předchůdce Anikin viz příklad, který btw vymyslel ZIKMUND ne já, jak některé hoaxy tvrdily
        for (int i = 0; i < PocetStudentu; i++) predchudci[i] = -1; // Zatím neznáme žédné předchůdce, proto všichni na -1 (žádný předchůdce)

        vzdalenosti[startovniIndex] = 0; // sami sobě nemusíme psát

        // Dijkstrův algoritmus (cca) :
        for (int i = 0; i < PocetStudentu; i++)
        {
            int aktualni = -1; // aktuální pozice, -1 znamená, že nikde nejsme

            
            for (int j = 0; j < PocetStudentu; j++) // hledání nejbližšího A neprozkoumaného uzelu
            {
                if (!navstiveno[j] && (aktualni == -1 || vzdalenosti[j] < vzdalenosti[aktualni]))
                {
                    aktualni = j; // Aktualizace aktuálního uzlu
                }
            }

            navstiveno[aktualni] = true; // Označ uzel jako navštívený
                                         
            if (vzdalenosti[aktualni] == int.MaxValue) // Pokud všechny zbývající uzly jsou nedosažitelné, ukonči smyčku, třeba když má někdo jenom jednoho kamaráda
                break;

            // Aktualizuj vzdálenosti sousedních uzlů
            for (int soused = 0; soused < PocetStudentu; soused++)
            {
                if (matice[aktualni][soused] != -1 && vzdalenosti[aktualni] + matice[aktualni][soused] < vzdalenosti[soused]) // Pokud existuje kontakt a vede k lepšímu výsledku
                {
                    vzdalenosti[soused] = vzdalenosti[aktualni] + matice[aktualni][soused]; // Aktualizace vzdálenosti na tu lepší
                    predchudci[soused] = aktualni; // Aktualizace předchůdce na toho lepšího
                }
            }
        }

        // Vytvoření stromu nejkratších cest
        int[,] strom = new int[PocetStudentu, PocetStudentu]; // 2D matice = "tabulka výsledků", reprezentuje strom

        for (int soused = 0; soused < PocetStudentu; soused++) // Pro každý uzel
        {
            if (predchudci[soused] != -1) // Pokud má uzel předchůdce
            {
                strom[predchudci[soused], soused] = 1; // Zapíše, že tento kontakt je v nejkratším stromě
            }
        }
                
        Console.WriteLine("Matice stromu nejkratších cest:");
        for (int i = 0; i < PocetStudentu; i++)
        {
            for (int j = 0; j < PocetStudentu; j++)
            {
                Console.Write(strom[i, j] + " ");
            }
            Console.WriteLine(); // přidává nový řádek
        }

        
        Console.WriteLine("Až si dočtete  a překreslíte strom, zmáčkněte cokoliv...");
        Console.ReadKey();
    }
}
