using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Burucki.Utils
{
    public static class InputManager
    {
        private static KeyboardState _currentKeyboardState;
        private static KeyboardState _previousKeyboardState;
        private static MouseState _currentMouseState;
        private static MouseState _previousMouseState;

        public static void Update()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();

            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }

        // Check if a key is currently being held down
        public static bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

        // Check if a key was just pressed (not held)
        public static bool IsKeyPressed(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }

        // Check if a key was just released
        public static bool IsKeyReleased(Keys key)
        {
            return _currentKeyboardState.IsKeyUp(key) && _previousKeyboardState.IsKeyDown(key);
        }

        // Get current mouse position
        public static Vector2 GetMousePosition()
        {
            return new Vector2(_currentMouseState.X, _currentMouseState.Y);
        }

        // Check if a mouse button is pressed
        public static bool IsMouseButtonPressed(ButtonState current, ButtonState previous)
        {
            return current == ButtonState.Pressed && previous == ButtonState.Released;
        }

        // Check if left mouse button was just pressed
        public static bool IsLeftMousePressed()
        {
            return IsMouseButtonPressed(_currentMouseState.LeftButton, _previousMouseState.LeftButton);
        }

        // Check if right mouse button was just pressed
        public static bool IsRightMousePressed()
        {
            return IsMouseButtonPressed(_currentMouseState.RightButton, _previousMouseState.RightButton);
        }
    }
}
