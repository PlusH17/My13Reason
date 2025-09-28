using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BeastBest_PluH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Zakládání proměnných
            Maze maze = new Maze();
            Player player = new Player();

            // Kontrola vstupu od uživatele 
            bool oksirka = false;
            while (!oksirka)
            {
                try
                {
                    Console.WriteLine("Zadejte šířku");
                    string strWidth = Console.ReadLine();
                    maze.Width = int.Parse(strWidth);
                    int Width = maze.Width;
                    oksirka = true;
                }
                catch
                {
                    Console.WriteLine("Prosím číslo");
                }
            }

            bool okvyska = false;
            while (!okvyska)
            {
                try
                {
                    Console.WriteLine("Zadejte výšku:");
                    string strHeight = Console.ReadLine();
                    maze.Height = int.Parse(strHeight);
                    int Height = maze.Height;
                    okvyska = true;
                }
                catch
                {
                    Console.WriteLine("Prosím číslo");
                }
            }

            bool okbludiste = false;
            while (!okbludiste)
            {
                try
                {
                    Console.WriteLine("Zadejte bludiště - ukončete prázdným řádkem");
                    maze.lines = new List<string>();
                    List<string> lines = maze.lines;

                    maze.Bludiste = maze.CreateMaze();
                    okbludiste = true;
                }
                catch
                {
                    Console.WriteLine("Špatně zadané bludiště, chcete ho vložit znovu (ENTER) nebo začít od začatku (a)"); //Kdyby nám až moc pozdě došlo, že jsme zadali špatné hodnoty pro výšku a šířku
                    string odpoved = Console.ReadLine();
                    if (odpoved == "a")
                        return;
                }
            }


            NajitHrace(player, maze); //nevíme jak je Beast natočen nebo kde se nachází -> hledá se


            for (int kolo = 0; kolo < 20; kolo++)
            {
                Console.WriteLine($"{kolo + 1}. krok");
                Hra(player, maze);
                
                Console.WriteLine();

            }

        }

        class Maze
        {
            public int Width;
            public int Height;
            public List<string> lines; //pro přepis
            public char[,] Bludiste; //2D pole

            public char[,] CreateMaze() //vrací 2D pole
            {
                char[,] arr = new char[Height, Width];

                while (true)
                {
                    string line = Console.ReadLine();
                    if (string.IsNullOrEmpty(line)) break; //čeká na prázdný řádek, který značí, že zadávání bludiště je u konce
                    lines.Add(line);
                }

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        arr[i, j] = lines[i][j];
                    }
                }
                return arr;
            }

            public void WriteMaze() //vypisuje bludiště
            {

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        Console.Write(Bludiste[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }
        class Player
        {
            public char natoceni;
            public int Radek;
            public int Sloupec;
        }

        static void NajitHrace(Player player, Maze maze) //hledá natočení hráče a jeho souřadnice
        {
            char[] vyznaceniHrace = { '^', 'v', '>', '<' };
            for (int znak = 0; znak < vyznaceniHrace.Length; znak++)
            {
                for (int row = 0; row < maze.Height; row++)
                {
                    char[] radek = new char[maze.Width];
                    for (int col = 0; col < maze.Width; col++)
                    {
                        radek[col] = maze.Bludiste[row, col];
                    }

                    int Index = Array.IndexOf(radek, vyznaceniHrace[znak]);
                    if (Index != -1)
                    {
                        player.natoceni = vyznaceniHrace[znak];
                        player.Radek = row;
                        player.Sloupec = Index;

                    }
                }
            }
        }

        static void OtoceniVlevo(Player player, Maze maze)
        {
            char[] otoceni = { '^', '>', 'v', '<' };
            char novysmer = ' ';
            int noveotoceni = Array.IndexOf(otoceni, player.natoceni) - 1;
            if (noveotoceni == -1)
                novysmer = otoceni[3];
            else
                novysmer = otoceni[noveotoceni];

            player.natoceni = novysmer;

            maze.Bludiste[player.Radek, player.Sloupec] = player.natoceni;
        }

        static void OtoceniVpravo(Player player, Maze maze)
        {
            char[] otoceni = { '^', '>', 'v', '<' };
            char novysmer = ' ';
            int noveotoceni = Array.IndexOf(otoceni, player.natoceni) + 1;
            if (noveotoceni == 4)
                novysmer = otoceni[0];
            else
                novysmer = otoceni[noveotoceni];

            player.natoceni = novysmer;


            maze.Bludiste[player.Radek, player.Sloupec] = player.natoceni;
        }

        static bool WallInFront(Player player, Maze maze)
        {
            char koukam = player.natoceni;
            int kontrolaSloupce = player.Sloupec;
            int kontrolaradku = player.Radek;

            switch (koukam)
            {
                case '^':
                    kontrolaradku -= 1;
                    break;
                case '>':
                    kontrolaSloupce += 1;
                    break;
                case 'v':
                    kontrolaradku += 1;
                    break;
                case '<':
                    kontrolaSloupce -= 1;
                    break;

            }

            if (maze.Bludiste[kontrolaradku, kontrolaSloupce] == 'X')
                return true;
            else return false;

        }

        static bool WallIinRight(Player player, Maze maze)
        {
            char koukam = player.natoceni;
            int kontrolaSloupce = player.Sloupec;
            int kontrolaradku = player.Radek;




            switch (koukam)
            {
                case '^':
                    kontrolaSloupce += 1;
                    break;
                case '>':
                    kontrolaradku += 1;
                    break;
                case 'v':
                    kontrolaSloupce -= 1;
                    break;
                case '<':
                    kontrolaradku -= 1;
                    break;

            }

            if (maze.Bludiste[kontrolaradku, kontrolaSloupce] == 'X')
                return true;
            else return false;

        }

        static bool WallInleft(Player player, Maze maze)
        {
            char koukam = player.natoceni;
            int kontrolaSloupce = player.Sloupec;
            int kontrolaradku = player.Radek;

            switch (koukam)
            {
                case '^':
                    kontrolaSloupce -= 1;
                    break;
                case '>':
                    kontrolaradku -= 1;
                    break;
                case 'v':
                    kontrolaSloupce += 1;
                    break;
                case '<':
                    kontrolaradku += 1;
                    break;

            }

            if (maze.Bludiste[kontrolaradku, kontrolaSloupce] == 'X')
                return true;
            else return false;

        }

        static bool WallInSikmo(Player player, Maze maze)
        {
            char koukam = player.natoceni;
            int kontrolaSloupce = player.Sloupec;
            int kontrolaradku = player.Radek;

            switch (koukam)
            {
                case '^':
                    kontrolaSloupce += 1;
                    kontrolaradku -= 1;
                    break;
                case '>':
                    kontrolaSloupce += 1;
                    kontrolaradku += 1;
                    break;
                case 'v':
                    kontrolaSloupce -= 1;
                    kontrolaradku += 1;
                    break;
                case '<':
                    kontrolaSloupce -= 1;
                    kontrolaradku -= 1;
                    break;

            }

            if (maze.Bludiste[kontrolaradku, kontrolaSloupce] == 'X')
                return true;
            else return false;
        }

        static void Go(Player player, Maze maze)
        {
            char koukam = player.natoceni;
            int kontrolaSloupce = player.Sloupec;
            int kontrolaradku = player.Radek;

            switch (koukam)
            {
                case '^':
                    kontrolaradku -= 1;
                    break;
                case '>':
                    kontrolaSloupce += 1;
                    break;
                case 'v':
                    kontrolaradku += 1;
                    break;
                case '<':
                    kontrolaSloupce -= 1;
                    break;

            }

            maze.Bludiste[player.Radek, player.Sloupec] = '.';
            player.Radek = kontrolaradku;
            player.Sloupec = kontrolaSloupce;

            maze.Bludiste[kontrolaradku, kontrolaSloupce] = player.natoceni;


        }

        static void Hra(Player player, Maze maze)
        {
            if (WallIinRight(player, maze) == true && WallInFront(player, maze) == false) //nejlepší možnost, jdeme podél pravé stěny
                Go(player, maze);
            else if (WallIinRight(player, maze) == true && WallInFront(player, maze) == true) //roh, dá se jít podle stejné pravé stěny
                OtoceniVlevo(player, maze);
            else if (WallIinRight(player, maze) == false && WallInSikmo(player, maze) == false) //nacházíme se v prostoru a zmizela nám naše stěna (viz krok 5) -> hledáme naši pravou stěnu
                OtoceniVpravo(player, maze);
            else if (WallInSikmo(player, maze) == true && WallInFront(player, maze) == false) //našli jsme naší pravou stěnu, jdeme za ní
                Go(player, maze);
            else if (WallInFront(player, maze) == true && WallIinRight(player, maze) == false) //slepá ulička
                OtoceniVpravo(player, maze);
            maze.WriteMaze();
        }
    }

}
