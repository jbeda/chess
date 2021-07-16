using System;

namespace Chess.Backends.Console
{
    internal class InputManager : IInputManager
    {
        public InputManager(ConsoleBackend backend)
        {
            this.mBackend = backend;
        }
        public KeyState this[Key key]
        {
            get
            {
                return this.GetKey(key);
            }
        }
        public KeyState GetKey(Key key)
        {
            throw new NotImplementedException();
        }
        public void Update()
        {
            // todo: check for key updates
        }
        public IBackend Backend { get { return this.mBackend; } }
        private ConsoleBackend mBackend;
    }
}