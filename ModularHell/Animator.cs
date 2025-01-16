using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

namespace ModularHell 
{
    public class Animator
    {

        public static float Swing(float rotation, float tick)
        {
<<<<<<< HEAD:ModularHell/Animator.cs
            //Console.WriteLine(rotation);
=======
>>>>>>> dc2a7f39d55542dfca2158ef76133d23415b360d:ModularHell/Content/Animator.cs
            
            rotation = (float)Math.Sin(tick);
            
            return rotation;
        }

        public static float Idle(float rotation, float tick)
        {
            rotation = 0.5f * (float)Math.Sin(tick);

            return rotation;
        }
    }
}