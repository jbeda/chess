namespace Chess.Frontends.Console
{
    public sealed class ConsoleFrontend : IFrontend
    {
        public ConsoleFrontend(Game game)
        {
            mGame = game;
            Renderer = new Renderer(this);
            mInputManager = new InputManager(this);
        }
        public void Update()
        {
            mInputManager.Update();
        }
        public IRenderer Renderer { get; private set; }
        public IInputManager InputManager { get { return mInputManager; } }
        public string Name { get { return "Console"; } }
        public Game Game { get { return mGame; } }
        private readonly Game mGame;
        private readonly InputManager mInputManager;
    }
}
