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

        public static KeyboardState KeyboardState{get => Keyboard.GetState();}
        public static Keys[] PressedKeys{get => KeyboardState.GetPressedKeys();}

        public static bool HoldingKey(Keys key) 
        {
            return KeyboardState.IsKeyDown(key);
        }
    }
}
