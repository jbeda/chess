namespace Chess
{
    public struct KeyState
    {
        public bool Up, Down, Held;
        public static KeyState Default()
        {
            var state = new KeyState();
            state.Up = state.Down = state.Held = false;
            return state;
        }
    }
    public enum Key
    {
        Q, W, E, R, T, Y, U, I, O, P,
        A, S, D, F, G, H, J, K, L,
        Z, X, C, V, B, N, M
    }
    public interface IInputManager
    {
        IFrontend Frontend { get; }
        KeyState this[Key key] { get; }
        KeyState GetKey(Key key);
    }
}