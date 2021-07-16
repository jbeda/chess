namespace Chess
{
    public interface IFrontend
    {
        IRenderer Renderer { get; }
        IInputManager InputManager { get; }
        string Name { get; }
        Game Game { get; }
        void Update();
    }
}