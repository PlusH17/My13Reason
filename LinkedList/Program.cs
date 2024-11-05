using System;

namespace Spojovy_seznam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList(); 
            LinkedList linkedList1 = new LinkedList();

            linkedList.Add(1); 
            linkedList.Add(2);
            linkedList.Add(3);
            linkedList.Add(4);


            linkedList1.Add(2);
            linkedList1.Add(4);

            int min = linkedList.FindMinimum();
            if (min == -69)
                Console.WriteLine("Prázdný seznam");
            else
                Console.WriteLine($"Minimum: {min}");

            Console.WriteLine(linkedList.Find(1));
            Console.WriteLine(linkedList1.Find(1));

            LinkedList result = linkedList.TryingToAdd(linkedList, linkedList1);
            Console.WriteLine("Součet:");
            result.PrintLinkedList();

            linkedList.SortLinkedList();

            Console.WriteLine("Seřazený list");
            linkedList.PrintLinkedList();

            LinkedList ListForBreakthrough = new LinkedList();
            LinkedList ListForBreakthrough01 = new LinkedList();

            ListForBreakthrough.Add(1);
            ListForBreakthrough.Add(2);
            ListForBreakthrough.Add(3);
            ListForBreakthrough.Add(4);
            ListForBreakthrough.Add(5);

            ListForBreakthrough01.Add(1);
            ListForBreakthrough01.Add(3);
            ListForBreakthrough01.Add(5);

            ListForBreakthrough.MakingBreakthrough(ListForBreakthrough, ListForBreakthrough01);
            ListForBreakthrough.PrintLinkedList();



            LinkedList ListForUnion = new LinkedList();
            LinkedList ListForUnion01 = new LinkedList();

            ListForUnion.Add(1);
            ListForUnion.Add(2);
            ListForUnion.Add(3);
            ListForUnion.Add(4);
            ListForUnion.Add(5);

            ListForUnion01.Add(1);
            ListForUnion01.Add(3);
            ListForUnion01.Add(5);
            ListForUnion01.Add(7);
            ListForUnion01.Add(9);

            ListForUnion.CreatingUnion(ListForUnion, ListForUnion01);
            ListForUnion.PrintLinkedList();



        }
    }

    class Node // Node = název třídy pro jednotlivé prvky seznamu
    {
        public Node(int value) 
        {
            Value = value;
        }
        public int Value { get; }
        public Node Next { get; set; }
        public Node Previous { get; set; } //odkaz na předchozí uzel
    }

    class LinkedList // LinkedList = název třídy reprezentující samotný spojový seznam
    {
        public Node Head { get; set; }

        
        public void Add(int value) // funkce pro přidání prvku na začátek seznamu
        {
            Node newNode = new Node(value);

            if (Head == null)
            {
                NOde Head = newNode;
            }
            else
            {
                newNode.Next = Head;
                Head.Previous = newNode;
                Head = newNode;
            }
        }

        public bool Find(int value) // funkce pro hledání prvku s hodnotou v parametru
        {
            Node node = Head;
                        
            while (node != null)
            {
                if (node.Value == value)
                    return true;
                node = node.Next;
            }
            return false;
        }

        public int FindMinimum() //Hledá se minimum
        {
            Node node = Head;
            
            if (Head == null) // v případě že je seznam prázdný
            {
                return -69;
            }
            int minValue = node.Value;

            while (node != null && node.Next != null)
            {
                if (node.Next != null && minValue > node.Next.Value)
                    minValue = node.Next.Value;
                node = node.Next;
            }

            return minValue;
        }

        public void PrintLinkedList()
        {
            if (Head == null) // v případě že je seznam prázdný
            {
                Console.WriteLine("Prázdný seznam");
                return;
            }
            else 
            {
                Node node = Head;
                while (node != null)
                {
                    Console.WriteLine(node.Value);
                    node = node.Next;
                }
                return;
            }
        }

        public void SortLinkedList()
        {
            if (Head != null && Head.Next != null) //v případě že list má více jak 1 prvek
            {
                bool swap = true;
                while (swap == true) //booble sort - procházím celý seznam do té doby, dokud tam není co prohodit
                {
                    swap = false;
                    Node node = Head;

                    while (node.Next != null) //procházím celý seznam
                    {
                        if (node.Value > node.Next.Value) //je potřeba výměna
                        {
                            swap = true;
                            Node first = node.Previous;
                            Node second = node;
                            Node third = node.Next;
                            Node fourth = third.Next;

                            if (first != null && fourth != null)
                            {
                                first.Next = third;
                                third.Previous = first;
                                third.Next = second;
                                second.Previous = third;
                                second.Next = fourth;
                                fourth.Previous = second;
                            }
                            else if (first == null && fourth != null)
                            {
                                Head = third;
                                third.Previous = null; 
                                third.Next = second;
                                second.Previous = third;
                                second.Next = fourth;
                                fourth.Previous = second;
                            }
                            else if (first != null && fourth == null)
                            {
                                first.Next = third;
                                third.Previous = first;
                                third.Next = second;
                                second.Previous = third;
                                second.Next = null; 
                            }
                            else if (first == null && fourth == null)
                            {
                                Head = third;
                                third.Previous = null; 
                                third.Next = second;
                                second.Previous = third;
                                second.Next = null; 
                            }

                            node = Head; //jdu zase od začátku
                        }
                        else // není potřeba výměna - jdu dál
                        {
                            node = node.Next;
                        }
                    }
                }
            }

            if (Head != null && Head.Next == null)
                Console.WriteLine("Seznam má jeden prvek aka již je seřazený");
            if (Head == null)
                Console.WriteLine("Prázdný seznam");
            return;
        }

        public void MakingBreakthrough(LinkedList seznam01, LinkedList seznam02)
        {
            Node node01 = seznam01.Head;

            while (node01 != null)
            {
                int hledana = node01.Value;
                bool jetam = seznam02.Find(hledana); // kontrola, jestli bude hodnota součástí průniku
                
                if (jetam == false) // Pokud tam hodnota hodnota není , odstraníme uzel ze seznamu01
                {
                    Node before = node01.Previous;
                    Node after = node01.Next;

                    if (before != null && after != null)
                    {
                        before.Next = after;
                        after.Previous = before;
                    }
                    else if (before == null && after != null) 
                    {
                        seznam01.Head = after;
                        after.Previous = null; 
                    }
                    else if (before != null && after == null) 
                    {
                        before.Next = null;
                    }
                    else // Pokud je to jediný uzel
                    {
                        seznam01.Head = null;
                    }

                    node01 = after; 
                }
                else // Pokud tam je jdeme na další uzel
                {                   
                    node01 = node01.Next;
                }
            }

            Console.WriteLine("Destruktivní průnik dokončen.");
            return;
        }

        public void CreatingUnion(LinkedList seznam01, LinkedList seznam02)
        {
            Node node01 = seznam01.Head;
            Node node02 = seznam02.Head;

            while (node02 != null)
            {
                int hodnota02 = node02.Value;
                bool jetam = seznam01.Find(hodnota02); // Zkontrolujeme, zda hodnota existuje v seznamu02

                if (jetam == false)
                    seznam01.Add(hodnota02);
                node02 = node02.Next;     
            }
            Console.WriteLine("Destruktivní sjednocení dokončeno.");
            return;
        }

        public LinkedList TryingToAdd(LinkedList list01, LinkedList list02)
        {
            Node uzel01 = list01.Head;
            Node uzel02 = list02.Head;

            LinkedList seznam01 = new LinkedList();
            Node node01 = seznam01.Head;

            LinkedList seznam02 = new LinkedList();
            Node node02 = seznam02.Head;


            int PocetV01 = 0;
            int PocetV02 = 0;
            //Zjistím, který seznam je delší
            while (uzel01 != null) 
            {
                PocetV01++;
                uzel01 = uzel01.Next;
            }

            while (uzel02 != null)
            {
                PocetV02++;
                uzel02 = uzel02.Next;
            }

            if (PocetV02 > PocetV01) //prohodím hodnoty tak, aby seznam01 byl vždy ten delší
            {
                seznam01 = list02;
                node01 = seznam01.Head;

                seznam02 = list01;
                node02 = seznam02.Head;
            }
            else
            {
                seznam01 = list01;
                node01 = seznam01.Head;

                seznam02 = list02;
                node02 = seznam02.Head;
            }


            LinkedList Final = new LinkedList(); //konečný seznam s výsledkem

            while (node01.Next != null) //dojedu "nakonec" seznamu, protože pod sebou se sčítá od konce
            {
                node01 = node01.Next;
            }

            while (node02.Next != null)
            {
                node02 = node02.Next;
            }

            int HodnotaHorniho = node01.Value;
            int HodnotaSpodnihp = node02.Value;

            
            int zbytek = 0;
            while (node02 != null)
            {
                int soucet = node01.Value + node02.Value + zbytek;
                if (soucet > 9)
                {
                    soucet -= 10;
                    zbytek = 1;
                }
                else
                {
                    zbytek = 0;
                }

                Final.Add(soucet);
                node01 = node01.Previous; //přesouvám se dopředu
                node02 = node02.Previous;
            }
            while (node01 != null) //zkontroluji co zbývá z většího
            {
                int prebytek = node01.Value + zbytek;
                Final.Add(prebytek);
                node01 = node01.Previous;
            }
            
            return Final;
        }

    }
}