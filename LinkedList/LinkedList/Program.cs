using System;

namespace Spojovy_seznam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList(); // vytvoření nového instance typu LinkedList (to je už náš vlastní datový typ)
            linkedList.SortLinkedList();
            linkedList.Add(4); // můžeme na něm volat námi implmentovanou metodu Add
            linkedList.Add(5);
            linkedList.Add(6);
            linkedList.Find(5); // nebo funkci Find
            int minimum = linkedList.FindMinimum();
            Console.WriteLine("Minimum je: " + minimum);
            linkedList.PrintLinkedList();
  
        }
    }

    class Node // Node je náš název pro třídu reprezentující jeden prvek spojového seznamu
    {
        public Node(int value) // konstruktor třídy Node - volá se při vytváření nové instance
        {
            Value = value;
        }
        public int Value { get; }
        public Node Next { get; set; }
    }

    class LinkedList // LinkedList je náš název pro třídu reprezentující samotný spojový seznam
    {
        public Node Head { get; set; }

        public void Add(int value) // metoda pro přidání prvku na začátek seznamu
        {
            if (Head == null)
                Head = new Node(value);
            else
            {
                Node newNode = new Node(value);
                newNode.Next = Head;
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

        public int FindMinimum()
        {
            if (Head == null) // 
                throw new InvalidOperationException("Seznam je prázdný"); // InvalidOperationException= třída už od Microsoftu

            Node node = Head;
            int minValue = node.Value;

            while (node != null)
            {
                if (node.Next != null && minValue > node.Next.Value)
                    minValue = node.Next.Value;
                node = node.Next;
            }

            return minValue;
        }

        public void PrintLinkedList()
        {
            if (Head == null) // 
                throw new InvalidOperationException("Seznam je prázdný"); // InvalidOperationException= třída už od Microsoftu
            Node node = Head;
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }

            return;
                
        }

        public void SortLinkedList()
        {
            if (Head != null)
            {

                Node node = Head;

                while (node.Next != null)
                {
                    if (node.Next.Value > node.Value)
                    {
                        node = node.Next;
                    }
                    else
                    {
                        Node pomoc = node.Next;
                        node.Next = node.Next.Next;
                        node.Next.Next = pomoc;

                    }
                }
            }


        }

    }
}