using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mastermind
{
    class GameClass
    {
        static string[] colors = { "red", "green", "blue", "yellow", "purple", "brown" };
        Random rnd = new Random();
        string fileName;
        ConsoleMenu subMenu = new ConsoleMenu();

        private string[] ColorGenerate(string[] colors)
        {
            string[] gameColors = new string[4];

            for (int i = 0; i < gameColors.Length; i++)
            {
                gameColors[i] = colors[rnd.Next(0, 6)];
            }

            return gameColors;
        }
        private string[] Input()
        {
            string[] inputs = new string[4];

            for (int i = 0; i < inputs.Length; i++)
            {
                Console.Write("Write your guess: ");
                inputs[i] = Console.ReadLine().ToLower();
            }

            return inputs;
        }
        private bool Forfeit()
        {
            Console.Write("Do you want to give up? Y / N: ");
            string input = Console.ReadLine();
            Console.WriteLine();
            if (input.ToLower() == "y")
            {
                Console.Clear();
                Console.WriteLine("Thanks for playing!");
                return true;
            }
            return false;
        }
        private bool GuessCheck(string[] gameColors, string[] inputs)
        {
            string[] inputsCopy = SupportMethods<string>.Copy(inputs);
            string[] gameColorsCopy = SupportMethods<string>.Copy(gameColors);

            int blackSpike = 0;
            int whiteSpike = 0;

            for (int i = 0; i < inputsCopy.Length; i++)
            {
                if (inputsCopy[i] == gameColorsCopy[i])
                {
                    inputsCopy[i] = "X";
                    gameColorsCopy[i] = "X";
                    blackSpike++;
                }
            }

            int j = 0;
            while (j < inputsCopy.Length)
            {
                if (inputsCopy[j] != "X")
                {
                    int index = SupportMethods<int>.FindIndex(gameColorsCopy, inputsCopy[j]);
                    if (index != -1)
                    {
                        gameColorsCopy[index] = "X";
                        inputsCopy[j] = "X";
                        whiteSpike++;
                    }
                }
                j++;
            }


            Console.WriteLine($"Black spikes: {blackSpike}");
            Console.WriteLine($"White spikes: {whiteSpike}\n");

            if (blackSpike == 4)
            {
                Console.WriteLine("You won!");
                System.Threading.Thread.Sleep(1500);
                return true;
            }
            return false;
        }
        private void Load()
        {
            Console.Clear();
            Console.WriteLine(SupportMethods<string>.GetFile(subMenu.CurrentItem.Index));
            Console.ReadLine();
        }

        public void Game()
        {
            fileName = SupportMethods<string>.GetFileLocation();

            string[] gameColors = ColorGenerate(colors);
            int i = 0;
            bool correct = false;
            bool forfeit = false;

            while (i < 20 && correct == false && forfeit == false)
            {
                Console.WriteLine($"{i + 1}. Guess");
                string[] inputs = Input();
                correct = GuessCheck(gameColors, inputs);
                if (!correct)
                {
                    forfeit = Forfeit();
                    Console.WriteLine("You lost!");
                    SupportMethods<object>.Save(fileName, gameColors, i, false);
                }
                else
                {
                    SupportMethods<object>.Save(fileName,gameColors, i, true);
                }
                i++;
            }
            if (i >= 20)
            {
                Console.WriteLine("You lost!");
                SupportMethods<object>.Save(fileName, gameColors, i, false);
            }
        }
        public void List()
        {
            Console.Clear();
            

            int dirLength = SupportMethods<int>.DirectoryLength() - 1;
            
            subMenu.Configure(config =>
            {
                config.WriteHeaderAction = () => Console.WriteLine($"Choose from 1-{SupportMethods<int>.DirectoryLength()-1}:");
            });

            for (int i = 0; i < dirLength; i++)
            {
                subMenu.Add($"{i + 1}. Game", Load);
            }
            subMenu.Add("Exit", ConsoleMenu.Close);

            subMenu.Show();
        }

    }
}
