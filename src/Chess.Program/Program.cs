using Chess.Backends.Console;

namespace Chess.Program
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.SetBackend(new ConsoleBackend(game));
            game.Run();
        }
    }
}
