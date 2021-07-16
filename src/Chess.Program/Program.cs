using Chess.Frontends.Console;

namespace Chess.Program
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.SetFrontend(new ConsoleFrontend(game));
            game.Run();
        }
    }
}
