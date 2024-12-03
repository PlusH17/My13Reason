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
            if(cislo <= 0)
            {
                Console.WriteLine("Přijde ti to jako kladné číslo? Mně ne... ");
                return false;
            }

            if (cislo == 1)
            {
                Console.WriteLine("1");
                return false;
            }

            List<string> UzVytiskle = new List<string>();
            Queue<int> Original = new Queue<int>();

            for (int i = 0; i < cislo; i++)
            {
                Original.Enqueue(1);
            }

            Console.WriteLine(string.Join("+", Original)); 

            for (int KolikOdeberu = 2; KolikOdeberu < Original.Count; KolikOdeberu++)
            {
                Queue<int> Fronta = new Queue<int>(Original);

                while (Fronta.Count > 1)
                { 
                    int pomocnik = 0;
                    int DoCyklu = 0;
                    while (DoCyklu < KolikOdeberu && Fronta.Count > 0)
                    {
                        int horni = Fronta.Peek();
                        pomocnik += horni;
                        Fronta.Dequeue();
                        DoCyklu++;
                    }

                    Fronta.Enqueue(pomocnik);

                    string frontaString = string.Join("+", Fronta);
                    if (!UzVytiskle.Contains(frontaString))
                    {
                        UzVytiskle.Add(frontaString);
                        Console.WriteLine(frontaString); 
                    }
                }
            }
            return true;
        }
    }
}