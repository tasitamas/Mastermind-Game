
using ConsoleTools;

namespace Mastermind
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Window";

            GameClass game = new GameClass();
            Action a = () => game.Game();


            ConsoleMenu menu = new ConsoleMenu()
                .Add("New Game", game.Game)
                .Add("Load older games", game.List)
                .Add("Exit",ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.WriteHeaderAction = () => Console.WriteLine("Main menu");
                });

            menu.Show();
        }
    }
}