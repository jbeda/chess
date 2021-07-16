namespace Chess.Backends.Console
{
    public sealed class ConsoleBackend : IBackend
    {
        public ConsoleBackend(Game game) {
            this.mGame = game;
        }
        public IRenderer CreateRenderer() {
            return new Renderer(this);
        }
        public string Name { get { return "Console"; } }
        public Game Game { get { return this.mGame; } }
        private Game mGame;
    }
}
