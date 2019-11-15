using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameConsole
{
    class Program
    {
        static List<char[]> maze = new List<char[]>();
        static int LineWidth = 60;
        static char hero = '@';
        public static int score = 0;
        public static int lives = 3;
        public static char death = '9';
        public static char lava = '▒';
        public static char zero = '0';
        public static int sum;
        static void Main(string[] args)
        {

            bool status = true;
            reading();
            fix();
            show();
            while (status)
            {
                var keyInfo = Console.ReadKey();
                move(keyInfo);
                show();
            }
            Console.ReadKey();
        }
        static void reading()
        {
            string[] file = File.ReadAllLines("lab.txt");
            foreach (string el in file)
            {
                maze.Add(el.ToCharArray());
            }
            for (int i = 0; i < maze.Count; i++)
                for (int j = 0; j < maze[i].Length; j++)
                {
                    if (maze[i][j] == '+') sum += 1;
                }
        }
        static void show()
        {
            if (lives <= 0) you_lose();
            Console.SetCursorPosition(0, 0);
            HeaderLine("┌─┐");
            HeaderLine("│ │");
            HeaderLine("││", "Типо лабиринт");
            HeaderLine("││", "");
            HeaderLine("│ │");
            HeaderLine("└─┘");
            for (int i = 0; i < maze.Count; i++) {
                for (int j = 0; j < maze[i].Length; j++)
                {
                        switch(maze[i][j])
                      {
                        case '9':
                            maze[i][j] = lava;
                            death = maze[i][j];
                            break;
                        case '0':
                            maze[i][j] = '│';
                            zero = maze[i][j];
                            break;
                        case '@':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case '▒':
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case '=':
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                      }
                        Console.Write(maze[i][j]);
                    Console.ForegroundColor = ConsoleColor.White;

                }
                    Console.WriteLine();
                }

            
            string str = "";
            if (score == 0)
            {
                str = "Ты собери хоть что-то в этой игре?";
            }
            else if (score == 1)
            {
                str = "Ну что поздравляю ты получил ключ";
            }
            else if (score <= 4)
            {
                str = "Вау ты далеко продвинулся";
            }
            else if (score <= 17)
            {
                str = "Ого ты добился хорошего результата, почти всё собрал";
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Score: {score}/{sum}\tLive: {lives}/3\tСдаться: ESQ\t{str}");
            Console.ForegroundColor = ConsoleColor.White;

        }
        static void you_win()
        {
                Console.Clear();
                HeaderLine("┌─┐");
                HeaderLine("│ │");
                HeaderLine("│ │", "Ура,победа,можешь немного порадоваться ибо ты победил");
                HeaderLine("│ │", "Мои тебе поздравления");
                HeaderLine("│ │");
                HeaderLine("└─┘");
                Console.WriteLine("\n\t\t\t\t\t\t До встречи в следующий раз");
                Console.WriteLine("\n\n\n");
                Console.ReadKey();
                System.Environment.Exit(0);
        }
        static void you_lose()
        {
            Console.Clear();
            HeaderLine("┌─┐");
            HeaderLine("│ │");
            HeaderLine("││", "Ты проиграл :(");
            HeaderLine("││", "До встречи в следующий раз!");
            HeaderLine("│ │");
            HeaderLine("└─┘");
            Console.ReadKey();
            System.Environment.Exit(0);
        }
        static void HeaderLine(string charect)
        {
            Console.Write(charect[0]);
            Console.Write(new String(charect[1], LineWidth - 2));
            Console.WriteLine(charect[2]);
        }
        static void HeaderLine(string charect, string text)
        {
            Console.Write(charect[0]);
            var textheader = (LineWidth - text.Length - 2) / 2;
            Console.Write(new string(' ', textheader));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(new String(' ', (LineWidth - textheader - text.Length) - 2));

            Console.WriteLine(charect[1]);
        }
        static void fix()
        {
            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && maze[i + 1][j] == '0' && maze[i - 1][j] == '0'
                        && maze[i][j - 1] == '0' && maze[i][j + 1] == '0') maze[i][j] = '┼';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ') && (maze[i + 1][j] == '0'
                        || maze[i + 1][j] != ' ') && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '├';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ') && (maze[i + 1][j] == '0'
                        || maze[i + 1][j] != ' ') && (maze[i][j - 1] == '0' || maze[i][j - 1] != ' ')) maze[i][j] = '┤';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i + 1][j] != ' ') && (maze[i][j - 1] == '0'
                        || maze[i][j - 1] != ' ') && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '┬';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ') && (maze[i][j - 1] == '0'
                        || maze[i][j - 1] != ' ') && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '┴';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ')
                        && (maze[i][j - 1] == '0' || maze[i][j - 1] != ' ')) maze[i][j] = '┘';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ')
                        && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '└';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i + 1][j] != ' ')
                        && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '┌';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i + 1][j] != ' ')
                        && (maze[i][j - 1] == '0' || maze[i][j - 1] != ' ')) maze[i][j] = '┐';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i][j + 1] == '0' || maze[i][j - 1] == '0'
                        || maze[i][j + 1] != ' ' || maze[i][j - 1] != ' ')) maze[i][j] = '─';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i - 1][j] == '0'
                        || maze[i + 1][j] != ' ' || maze[i - 1][j] != ' ')) maze[i][j] = '│';
        }
        static void down(int i, int j)
        {
            maze[i][j] = ' ';
            maze[i + 1][j] = hero;
            show();
        }
        static void up(int i, int j)
        {

            maze[i][j] = ' ';
            maze[i - 1][j] = hero;
            show();

        }
        static void left(int i, int j)
        {
            maze[i][j] = ' ';
            maze[i][j - 1] = hero;
            show();
        }
        static void right(int i, int j)
        {
            maze[i][j] = ' ';
            maze[i][j + 1] = hero;
            show();
        }
        static void upback(int i, int j)
        {
            lives--;
            maze[i][j] = ' ';
            maze[i - 1][j] = hero;
            show();
            if (maze[i + 1][j] != zero && maze[i + 1][j] != death && maze[i + 1][j] != '┼' && maze[i + 1][j] != '├' && maze[i + 1][j] != '┤' && maze[i + 1][j] != '┬' && maze[i + 1][j] != '┴' && maze[i + 1][j] != '┘'
                && maze[i + 1][j] != '└' && maze[i + 1][j] != '┌' && maze[i + 1][j] != '┐' && maze[i + 1][j] != '─' && maze[i + 1][j] != '│')
            {
                maze[i - 1][j] = death;
                maze[i + 1][j] = hero;
            }
            else
            {
                maze[i - 1][j] = death;
                maze[i][j] = hero;
            }
            show();
        }
        static void downback(int i, int j)
        {
            lives--;
            maze[i][j] = ' ';
            maze[i + 1][j] = hero;
            show();
            if (maze[i + 1][j] != zero && maze[i + 1][j] != death && maze[i + 1][j] != '┼' && maze[i + 1][j] != '├' && maze[i + 1][j] != '┤' && maze[i + 1][j] != '┬' && maze[i + 1][j] != '┴' && maze[i + 1][j] != '┘'
                && maze[i + 1][j] != '└' && maze[i + 1][j] != '┌' && maze[i + 1][j] != '┐' && maze[i + 1][j] != '─' && maze[i + 1][j] != '│')
            {
                maze[i + 1][j] = death;
                maze[i - 1][j] = hero;
            }
            else
            {
                maze[i + 1][j] = death;
                maze[i][j] = hero;
            }
            show();
        }
        static void leftback(int i, int j)
        {
            lives--;
            maze[i][j] = ' ';
            maze[i][j - 1] = hero;
            show();
            if (maze[i + 1][j] != zero && maze[i + 1][j] != death && maze[i + 1][j] != '┼' && maze[i + 1][j] != '├' && maze[i + 1][j] != '┤' && maze[i + 1][j] != '┬' && maze[i + 1][j] != '┴' && maze[i + 1][j] != '┘'
                && maze[i + 1][j] != '└' && maze[i + 1][j] != '┌' && maze[i + 1][j] != '┐' && maze[i + 1][j] != '─' && maze[i + 1][j] != '│')
            {
                maze[i][j - 1] = death;
                maze[i][j + 1] = hero;
            }
            else
            {
                maze[i][j - 1] = death;
                maze[i][j] = hero;
            }
            show();
        }
        static void rightback(int i, int j)
        {
            lives--;
            maze[i][j] = ' ';
            maze[i][j + 1] = hero;
            show();
            if (maze[i + 1][j] != zero && maze[i + 1][j] != death && maze[i + 1][j] != '┼' && maze[i + 1][j] != '├' && maze[i + 1][j] != '┤' && maze[i + 1][j] != '┬' && maze[i + 1][j] != '┴' && maze[i + 1][j] != '┘'
                && maze[i + 1][j] != '└' && maze[i + 1][j] != '┌' && maze[i + 1][j] != '┐' && maze[i + 1][j] != '─' && maze[i + 1][j] != '│')
            {
                maze[i][j + 1] = death;
                maze[i][j - 1] = hero;
            }
            else
            {
                maze[i][j + 1] = death;
                maze[i][j] = hero;
            }
            show();
        }
        static void move(ConsoleKeyInfo key)
        {
            int x = 0, y = 0;
            for (int i = 0; i < maze.Count; i++)
                for (int j = 0; j < maze[i].Length; j++)
                {
                    if (maze[i][j] == hero)
                    {
                        x = i;
                        y = j;
                    }
                }
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    you_lose();
                    break;
                case ConsoleKey.UpArrow:
                    if (maze[x - 1][y] == ' ')
                    {
                        up(x, y);
                        show();
                    }
                    else if (maze[x - 1][y] == death)
                    {
                        upback(x, y);
                        show();
                    }
                    else if (maze[x - 1][y] == '+')
                    {
                        score++;
                        Console.Clear();
                        up(x, y);
                        show();
                    }
                    else if (maze[x - 1][y] == '-')
                    {
                        if (score <= 0)
                        {
                            score = 0;
                            show();
                        }
                        else
                        {
                            up(x, y);
                            score--;
                            show();
                        }
                        Console.Clear();
                        show();
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (maze[x + 1][y] == ' ')
                    {
                        down(x, y);

                        show();
                    }
                    else if (maze[x + 1][y] == death)
                    {
                        downback(x, y);
                        show();
                    }
                    else if (maze[x + 1][y] == '+')
                    {
                        score++;
                        down(x, y);
                        Console.Clear();
                        show();
                    }
                    else if (maze[x + 1][y] == '-')
                    {
                        if (score <= 0)
                        {
                            score = 0;

                            show();
                        }
                        else
                        {
                            down(x, y);
                            score--;
                            show();
                        }
                        Console.Clear();
                        show();
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (maze[x][y - 1] == ' ')
                    {
                        left(x, y);
                        show();
                    }
                    else if (maze[x][y - 1] == death)
                    {
                        leftback(x, y);
                        show();
                    }
                    else if (maze[x][y - 1] == '+')
                    {
                        score++;
                        left(x, y);
                        Console.Clear();
                        show();
                    }
                    else if (maze[x][y - 1] == '-')
                    {
                        if (score <= 0)
                        {
                            score = 0;
                            show();
                        }
                        else
                        {
                            left(x, y);
                            score--;
                            show();
                        }
                        Console.Clear();
                        show();
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (maze[x][y + 1] == ' ')
                    {
                        right(x, y);
                        show();
                    }
                    else if (maze[x][y + 1] == death)
                    {
                        rightback(x, y);
                        show();
                    }
                    else if (maze[x][y + 1] == '+')
                    {
                        score++;
                        right(x, y);
                        Console.Clear();
                        show();
                    }
                    else if (maze[x][y + 1] == '-')
                    {
                        if (score <= 0)
                        {
                            score = 0;
                            show();
                        }
                        else
                        {
                            right(x, y);
                            score--;
                            show();
                        }
                        Console.Clear();
                        show();
                    }
                    else if (maze[x][y + 1] == '=')
                    {
                        you_win();
                        show();
                    }
                    break;
            }

        }
    }
}


