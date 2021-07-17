namespace Chess
{
    public struct ControlScheme
    {
        public Key Up { get; set; }
        public Key Down { get; set; }
        public Key Left { get; set; }
        public Key Right { get; set; }
        public Key Select { get; set; }
    }
    public abstract class Player
    {
        public Player(ControlScheme controlScheme)
        {
            mControlScheme = controlScheme;
        }
        protected abstract void Update(Game game);
        protected virtual void Render(IRenderer renderer) { }
        protected ControlScheme mControlScheme;
    }
    public sealed class HumanPlayer : Player
    {
        public HumanPlayer(ControlScheme? controlScheme = null) : base(controlScheme ?? DefaultControls()) { }
        protected override void Update(Game game)
        {
            // todo: take input from game.Frontend.InputManager
        }
        private static ControlScheme DefaultControls()
        {
            return new ControlScheme
            {
                Up = Key.W,
                Down = Key.S,
                Left = Key.A,
                Right = Key.D,
                Select = Key.E
            };
        }
    }
}