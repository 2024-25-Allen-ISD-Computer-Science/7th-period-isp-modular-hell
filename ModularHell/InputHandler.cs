using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ModularHell 
{
    public static class InputHandler
    {

        public static KeyboardState KeyboardState{get => keyboardState;}
        private static KeyboardState keyboardState;
        public static Keys[] PressedKeys{get => KeyboardState.GetPressedKeys();}
        public static MouseState MouseState {get => mouseState;}
        private static MouseState mouseState;
        
        public static Vector2 MousePosition {get {return new Vector2(mouseState.X, mouseState.Y);}}
        public static bool HoldingKey(Keys key) 
        {
            return KeyboardState.IsKeyDown(key);
        }

        public static void Update() {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }
    }
}
