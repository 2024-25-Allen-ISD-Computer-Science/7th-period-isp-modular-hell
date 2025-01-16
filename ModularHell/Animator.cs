using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ModularHell 
{
    public class Animator
    {

        public static float Swing(float rotation, float tick)
        {
            //Console.WriteLine(rotation);
            
            rotation = (float)Math.Sin(tick);
            
            return rotation;
        }
    }
}