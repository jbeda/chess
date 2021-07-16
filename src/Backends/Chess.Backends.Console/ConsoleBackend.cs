namespace Chess.Backends.Console
{
    public sealed class ConsoleBackend : IBackend
    {
        public ConsoleBackend(Game game)
        {
            mGame = game;
            Renderer = new Renderer(this);
            InputManager = new InputManager(this);
        }
        public void Update()
        {
            if (InputManager is InputManager inputManager)
            {
                inputManager.Update();
            }
        }
        public IRenderer Renderer { get; private set; }
        public IInputManager InputManager { get; private set; }
        public string Name { get { return "Console"; } }
        public Game Game { get { return mGame; } }
        private readonly Game mGame;
    }
}
