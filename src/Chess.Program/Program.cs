using Chess.Frontends.Console;

namespace Chess.Program
{
    public class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.SetFrontend(new ConsoleFrontend(game));

            game.Board[new Vec2(0)].Piece = new Piece(new Vec2(0), PieceColor.White, PieceType.Pawn, game.Board);

            game.Run();
        }
    }
}
