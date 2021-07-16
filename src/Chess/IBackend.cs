namespace Chess
{
    public interface IBackend
    {
        IRenderer Renderer { get; }
        IInputManager InputManager { get; }
        string Name { get; }
        Game Game { get; }
        void Update();
    }
}