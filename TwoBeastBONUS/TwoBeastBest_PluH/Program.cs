using System;
using System.Collections.Generic;

namespace BeastBest_PluH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Maze maze = new Maze();

            // Šířka
            while (true)
            {
                try
                {
                    Console.WriteLine("Zadejte šířku");
                    maze.Width = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Prosím číslo");
                }
            }

            // Výška
            while (true)
            {
                try
                {
                    Console.WriteLine("Zadejte výšku:");
                    maze.Height = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Prosím číslo");
                }
            }

            // Bludiště
            Console.WriteLine("Zadejte bludiště - ukončete prázdným řádkem");
            maze.lines = new List<string>();
            maze.Bludiste = maze.CreateMaze();

            // ✔️ NAJÍT HRÁČE AŽ TEĎ
            List<Player> players = NajitHrace(maze);

            for (int kolo = 0; kolo < 20; kolo++)
            {
                foreach (Player player in players)
                {
                    Hra(player, maze);
                }

                maze.WriteMaze();
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        class Maze
        {
            public int Width;
            public int Height;
            public List<string> lines;
            public char[,] Bludiste;

            public char[,] CreateMaze()
            {
                char[,] arr = new char[Height, Width];

                while (true)
                {
                    string line = Console.ReadLine();
                    if (string.IsNullOrEmpty(line)) break;
                    lines.Add(line);
                }

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                        arr[i, j] = lines[i][j];
                }
                return arr;
            }

            public void WriteMaze()
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                        Console.Write(Bludiste[i, j]);
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

        static List<Player> NajitHrace(Maze maze)
        {
            List<Player> players = new List<Player>();
            char[] znaky = { '^', 'v', '>', '<' };

            for (int r = 0; r < maze.Height; r++)
            {
                for (int c = 0; c < maze.Width; c++)
                {
                    if (Array.IndexOf(znaky, maze.Bludiste[r, c]) != -1)
                    {
                        players.Add(new Player
                        {
                            Radek = r,
                            Sloupec = c,
                            natoceni = maze.Bludiste[r, c]
                        });
                    }
                }
            }
            return players;
        }

        static bool JeObsazeno(int r, int c, Maze maze)
        {
            char ch = maze.Bludiste[r, c];
            return ch == 'X' || ch == '^' || ch == 'v' || ch == '<' || ch == '>';
        }

        static bool WallInFront(Player p, Maze maze)
        {
            int r = p.Radek, c = p.Sloupec;
            switch (p.natoceni)
            {
                case '^': r--; break;
                case '>': c++; break;
                case 'v': r++; break;
                case '<': c--; break;
            }
            return JeObsazeno(r, c, maze);
        }

        static bool WallIinRight(Player p, Maze maze)
        {
            int r = p.Radek, c = p.Sloupec;
            switch (p.natoceni)
            {
                case '^': c++; break;
                case '>': r++; break;
                case 'v': c--; break;
                case '<': r--; break;
            }
            return JeObsazeno(r, c, maze);
        }

        

        static bool WallInSikmo(Player p, Maze maze)
        {
            int r = p.Radek, c = p.Sloupec;
            switch (p.natoceni)
            {
                case '^': r--; c++; break;
                case '>': r++; c++; break;
                case 'v': r++; c--; break;
                case '<': r--; c--; break;
            }
            return JeObsazeno(r, c, maze);
        }

        static void OtoceniVlevo(Player p, Maze maze)
        {
            char[] o = { '^', '>', 'v', '<' };
            p.natoceni = o[(Array.IndexOf(o, p.natoceni) + 3) % 4];
            maze.Bludiste[p.Radek, p.Sloupec] = p.natoceni;
        }

        static void OtoceniVpravo(Player p, Maze maze)
        {
            char[] o = { '^', '>', 'v', '<' };
            p.natoceni = o[(Array.IndexOf(o, p.natoceni) + 1) % 4];
            maze.Bludiste[p.Radek, p.Sloupec] = p.natoceni;
        }

        static void Go(Player p, Maze maze)
        {
            int r = p.Radek, c = p.Sloupec;
            switch (p.natoceni)
            {
                case '^': r--; break;
                case '>': c++; break;
                case 'v': r++; break;
                case '<': c--; break;
            }

            maze.Bludiste[p.Radek, p.Sloupec] = '.';
            p.Radek = r;
            p.Sloupec = c;
            maze.Bludiste[r, c] = p.natoceni;
        }

        static void Hra(Player p, Maze maze)
        {
            if (WallIinRight(p, maze) && !WallInFront(p, maze))
                Go(p, maze);
            else if (WallIinRight(p, maze) && WallInFront(p, maze))
                OtoceniVlevo(p, maze);
            else if (!WallIinRight(p, maze) && !WallInSikmo(p, maze))
                OtoceniVpravo(p, maze);
            else if (WallInSikmo(p, maze) && !WallInFront(p, maze))
                Go(p, maze);
            else if (WallInFront(p, maze) && !WallIinRight(p, maze))
                OtoceniVpravo(p, maze);
        }
    }
}
