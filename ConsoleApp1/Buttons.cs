using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Buttons
    {
        public static void Load()
        {
            ConsoleKeyInfo button = Console.ReadKey();
            if (button.Key == ConsoleKey.Escape) Program.Close();
            else if (button.Key == ConsoleKey.F1) Filear.Save();
            else
            {
                Console.Clear();
                Program.PathToFile(Filear.dir);
            }
        }
    }
}
