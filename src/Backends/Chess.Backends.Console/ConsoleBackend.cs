namespace Chess.Backends.Console
{
    public sealed class ConsoleBackend : IBackend
    {
        public ConsoleBackend(Game game)
        {
            this.mGame = game;
            this.Renderer = new Renderer(this);
            this.InputManager = new InputManager(this);
        }
        public void Update()
        {
            if (this.InputManager is InputManager inputManager)
            {
                inputManager.Update();
            }
        }
        public IRenderer Renderer { get; private set; }
        public IInputManager InputManager { get; private set; }
        public string Name { get { return "Console"; } }
        public Game Game { get { return this.mGame; } }
        private Game mGame;
    }
}
