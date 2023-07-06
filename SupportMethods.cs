using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    public class SupportMethods<T>
    {
        public static T[] Copy(T[] array)
        {
            T[] copied = new T[array.Length];

            for (int i = 0; i < copied.Length; i++)
            {
                copied[i] = array[i];
            }

            return copied;
        }
        public static int FindIndex(string[] array, string value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void Save(string location, string[] array, int idx, bool win)
        {
            if (win)
            {
                File.AppendAllText(location, "You won!\n");
                File.AppendAllText(location, $"{idx + 1}. guess was the correct one!\n");
                File.AppendAllText(location, "The colors in this game: ");
                ArrayToFile(location, array);
            }
            else
            {
                File.AppendAllText(location, "You lost!\n");
                File.AppendAllText(location, "The colors in this game: ");
                ArrayToFile(location, array);
            }
        }

        private static void ArrayToFile(string location,string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                File.AppendAllText(location, array[i] + " ");
            }
            File.AppendAllText(location, "\n");
        }
        public static string GetFileName()
        {
            string dir = Directory.GetCurrentDirectory();
            int fileCount = Directory.GetFiles(dir, "*.txt").Length + 1;

            return $"{fileCount}.txt";
        }
        public static string GetFileLocation()
        {
            return Path.GetFullPath(GetFileName());
        }
        public static int DirectoryLength()
        {
            string dir = Directory.GetCurrentDirectory();
            return Directory.GetFiles(dir, "*.txt").Length + 1;
        }
        public static string GetFile(int index)
        {
            return File.ReadAllText($"{index+1}.txt");
        }
    }
}
