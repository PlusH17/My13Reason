using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<Student> tree = new BinarySearchTree<Student>();

            // Čteme data z CSV souboru
            using (StreamReader streamReader = new StreamReader("studenti_shuffled.csv"))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]), // Id
                        studentData[1],                  // Jméno
                        studentData[2],                  // Příjmení
                        Convert.ToInt16(studentData[3]), // Věk
                        studentData[4]);                 // Třída

                    tree.Insert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }

            Console.WriteLine(tree.Find(20)?.Value);

            Console.WriteLine(tree.Min()?.Value);

            Student newStudent = new Student(142, "Ody", "king of Ithaca", 42, "9.E");
            tree.Insert(newStudent.Id, newStudent);


            Console.WriteLine(tree.Find(142)?.Value);
            

            Console.WriteLine(tree.Show());

            tree.Delete(142);
            Console.WriteLine(tree.Show());






        }
    }

    class Node<T>
    {
        public int Key { get; set; }
        public T Value { get; set; }
        public Node<T> LeftSon { get; set; }
        public Node<T> RightSon { get; set; }

        public Node(int key, T value)
        {
            Key = key;
            Value = value;
            LeftSon = null;
            RightSon = null;
        }
    }

    class BinarySearchTree<T>
    {
        public Node<T> Root { get; private set; }


        public void Delete(int fuckey) ///NEZKOUŠET na velkém BST, počítač RIP, vím, ALE (na mou skromnou obranu), nestrávila jsem na tom 3 hodiny abych neodevzdala nic, takže, se musím naučit žít s stack overflow
        {
            Node<T> NajdiRodice(int fuckey)
            {
                Node<T> parent = null;
                Node<T> node = Root;

                while (node != null && node.Key != fuckey)
                {
                    parent = node;
                    if (fuckey < node.Key)
                    {
                        node = node.LeftSon;
                    }
                    else
                    {
                        node = node.RightSon;
                    }
                }

                return parent;
            }

            Node<T> rodic = NajdiRodice(fuckey);
            Node<T> NashleNode = Find(fuckey);

            if (NashleNode == null) return; 

            bool pomocnik_pro_nahradu = false;
            int pomocnyklic = -1;
            T pomocnyvalue = default(T);

            if (NashleNode.LeftSon != null && NashleNode.RightSon != null)
            {
                Node<T> nahrada = NashleNode.RightSon;
                while (nahrada.LeftSon != null)
                {
                    nahrada = nahrada.LeftSon;
                }

                if (nahrada.RightSon != null)
                {
                    pomocnik_pro_nahradu = true;
                    pomocnyklic = nahrada.RightSon.Key;
                    pomocnyvalue = nahrada.RightSon.Value;
                }

                int novykey = nahrada.Key;
                Node<T> rodicnahrady = NajdiRodice(novykey);

                if (rodic == null)
                {
                    Root = nahrada;
                }
                else if (rodic.LeftSon == NashleNode)
                {
                    rodic.LeftSon = nahrada;
                }
                else
                {
                    rodic.RightSon = nahrada;
                }

                nahrada.LeftSon = NashleNode.LeftSon;
                nahrada.RightSon = NashleNode.RightSon;

                if (pomocnik_pro_nahradu == true)
                {
                    rodicnahrady.LeftSon = new Node<T>(pomocnyklic, pomocnyvalue);
                }
            }
            else if (NashleNode.LeftSon == null && NashleNode.RightSon != null)
            {
                // Pokud má pouze pravého syna
                if (rodic == null)
                {
                    Root = NashleNode.RightSon;
                }
                else if (rodic.LeftSon == NashleNode)
                {
                    rodic.LeftSon = NashleNode.RightSon;
                }
                else
                {
                    rodic.RightSon = NashleNode.RightSon;
                }
            }
            else if (NashleNode.RightSon == null && NashleNode.LeftSon != null)
            {
                // Pokud má pouze levého syna
                if (rodic == null)
                {
                    Root = NashleNode.LeftSon;
                }
                else if (rodic.LeftSon == NashleNode)
                {
                    rodic.LeftSon = NashleNode.LeftSon;
                }
                else
                {
                    rodic.RightSon = NashleNode.LeftSon;
                }
            }
            else
            {
                // Pokud nemá žádné děti
                if (rodic == null)
                {
                    Root = null;
                }
                else if (rodic.LeftSon == NashleNode)
                {
                    rodic.LeftSon = null;
                }
                else
                {
                    rodic.RightSon = null;
                }
            }
        }




        /// <summary>
        /// Vloží nový uzel do BST.
        /// </summary>
        public void Insert(int newKey, T newValue)
        {
            if (Root == null)
            {
                Root = new Node<T>(newKey, newValue);
                return;
            }

            Node<T> parent = null;
            Node<T> current = Root;

            while (current != null)
            {
                parent = current;
                if (newKey < current.Key)
                    current = current.LeftSon;
                else if (newKey > current.Key)
                    current = current.RightSon;
                else
                    return;
            }

            if (newKey < parent.Key)
                parent.LeftSon = new Node<T>(newKey, newValue);
            else
                parent.RightSon = new Node<T>(newKey, newValue);
        }

        /// <summary>
        /// Najde uzel s daným klíčem.
        /// </summary>
        public Node<T> Find(int key)
        {
            Node<T> current = Root;

            while (current != null)
            {
                if (key == current.Key)
                    return current;
                current = key < current.Key ? current.LeftSon : current.RightSon;
            }
            Console.WriteLine("Není");
            return null;
        }

        /// <summary>
        /// Vrátí výpis BST.
        /// </summary>
        public string Show()
        {
            StringBuilder sb = new StringBuilder();
            void _show(Node<T> node)
            {
                if (node != null)
                {
                    _show(node.LeftSon);
                    sb.Append(node.Key + " ");
                    _show(node.RightSon);
                }
            }
            _show(Root);
            return sb.ToString();
        }

        /// <summary>
        /// Vrátí nejmenší uzel ve stromu.
        /// </summary>
        public Node<T> Min()
        {
            Node<T> current = Root;
            while (current?.LeftSon != null)
                current = current.LeftSon;
            return current;
        }
    }

    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }
        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string className)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            ClassName = className;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} (ID: {Id}) ze třídy {ClassName}";
        }
    }
}
