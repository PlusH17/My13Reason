using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace zasobnik
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Závorky ke kontrole");
            String kontrola = Console.ReadLine();
            bool Check = CheckingZavorky(kontrola);
            Console.WriteLine(Check);

            Console.WriteLine("KLADNÉ CELÉ číslo na rozklad");
            int rozklad = int.Parse(Console.ReadLine());
            DecompositingNumber(rozklad);

            Console.ReadLine();
        }

        public static bool CheckingZavorky(string zkoumani)
        {
            Stack<char> Zasobnik = new Stack<char>();

            foreach (char znak in zkoumani)
            {
                if (znak == '{' || znak == '[' || znak == '(')
                {
                    Zasobnik.Push(znak);
                }
                else if (znak == '}' || znak == ']' || znak == ')')
                {
                    if (Zasobnik.Count == 0)
                        return false;
                    char horni = Zasobnik.Peek();

                    if ((horni == '{' && znak == '}') ||
                        (horni == '[' && znak == ']') ||
                        (horni == '(' && znak == ')'))
                    {
                        Zasobnik.Pop();
                    }
                }
            }

            if (Zasobnik.Count > 0)
                return false;

            return true;
        }

        public static bool DecompositingNumber(int cislo)
        {
            if (cislo <= 0)
            {
                Console.WriteLine("Přijde ti to jako kladné číslo? Mně ne...");
                return false;
            }

            if (cislo == 1)
            {
                Console.WriteLine("1");
                return false;
            }

            List<string> UzVytiskle = new List<string>();
            Queue<int> Original = new Queue<int>();

            // Naplnění fronty jedničkami
            for (int i = 0; i < cislo; i++)
            {
                Original.Enqueue(1);
            }

            Console.WriteLine(string.Join("+", Original)); // základní rozklad (1+1+...+1)

            // Zavolání rekurzivní metody
            GenerateCombinations(Original, UzVytiskle);

            return true;
        }

        private static void GenerateCombinations(Queue<int> currentQueue, List<string> UzVytiskle)
        {
            int count = currentQueue.Count;

            for (int KolikOdeberu = 2; KolikOdeberu < count; KolikOdeberu++)
            {
                Queue<int> Fronta = new Queue<int>(currentQueue);

                while (Fronta.Count > 1)
                {
                    int pomocnik = 0;
                    int DoCyklu = 0;

                    // Odebírání hodnot pro sloučení
                    while (DoCyklu < KolikOdeberu && Fronta.Count > 0)
                    {
                        int horni = Fronta.Peek();
                        pomocnik += horni;
                        Fronta.Dequeue();
                        DoCyklu++;
                    }

                    Fronta.Enqueue(pomocnik); // Přidání součtu zpět do fronty

                    // Převod fronty na řetězec pro kontrolu a výpis
                    string frontaString = string.Join("+", Fronta);
                    string frontaReverseString = string.Join("+", Fronta.Reverse()); // Reverzní verze

                    if (!UzVytiskle.Contains(frontaString) && !UzVytiskle.Contains(frontaReverseString))
                    {
                        UzVytiskle.Add(frontaString);
                        Console.WriteLine(frontaString);

                        
                        GenerateCombinations(new Queue<int>(Fronta), UzVytiskle);
                    }
                }
            }
        }
    }
}
