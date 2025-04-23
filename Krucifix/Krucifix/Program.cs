using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krucifix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose\r\n(Choose?)\r\nSomeone's gotta die today\r\nAnd you have got the final say\r\n PREFIX (1) \r\n POSTFIX (2) ");
            string fix = Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Vložte na rozklad");
            string vyraz = Console.ReadLine();

            List<string> oper = vyraz.Split(' ').ToList();

            Stack<float> stack = new Stack<float>();
            switch (fix)
            {
                case "1":
                    while (oper.Count > 0)
                    {
                        string horni = oper[0];
                        switch(horni)
                        {
                            case "+":
                                float cislovyze = stack.Pop();
                                float cislonize = stack.Pop();
                                float novy = cislonize + cislovyze;

                                stack.Push(novy);
                                oper.Remove(horni);

                                break;

                            case "-":
                                float cislovyze01 = stack.Pop();
                                float cislonize01 = stack.Pop();
                                float novy01 = cislonize01 - cislovyze01;

                                stack.Push(novy01);
                                oper.Remove(horni);
                                break;

                            case "*":
                                float cislovyze02 = stack.Pop();
                                float cislonize02 = stack.Pop();
                                float novy02 = cislonize02 * cislovyze02;

                                stack.Push(novy02);
                                oper.Remove(horni);
                                break;

                            case "/":
                                try 
                                {
                                    float cislovyze03 = stack.Pop();
                                    float cislonize03 = stack.Pop();
                                    float novy03 = cislonize03 / cislovyze03;

                                    stack.Push(novy03);
                                    oper.Remove(horni);
                                    
                                }
                                catch (DivideByZeroException)
                                {
                                    Console.WriteLine("Dude, 0 vole");
                                    Console.ReadLine();
                                    
                                }
                                break;
                                    
                            default:
                                float cislo = float.Parse(oper[0]);
                                stack.Push(cislo);
                                oper.Remove(horni);
                                break;

                        }
                    }
                    break;
                case "2":
                    break;
            }
            Console.WriteLine(stack.Peek());
            Console.ReadLine ();

        }


    }
}
