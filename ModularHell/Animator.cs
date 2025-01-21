using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;

namespace ModularHell 
{
    public class Animator
    {

        public static float Swing(float rotation, float tick)
        {
            
            rotation = (float)Math.Sin(tick);
            
            return rotation;
        }

        public static float Idle(Character entity, SpriteBatch spriteBatch)
        {
            var tick = 0;
            

            return tick;
        }

        public static void Walk(Character entity, SpriteBatch spriteBatch, int ms)
        {
            var keyframes = new List<Dictionary<string, object>>();

            var attachments = entity.AttachmentSlots;
            var torso = attachments[0].Item1.AttachmentSlots[0].Item1;
            var rArm = attachments[0].Item1.AttachmentSlots[1].Item1;
            var lArm = attachments[0].Item1.AttachmentSlots[0].Item1;
            var rLeg = attachments[0].Item1.AttachmentSlots[3].Item1;
            var lLeg = attachments[0].Item1.AttachmentSlots[2].Item1;

            rArm.rotation = 44.5f;


            //while (entity.characterState == "Walking") {
            //    tick += 1;
            //    rArm.Draw(spriteBatch, new Vector2(20, 80), -0.1f);
                
            //}
            
            Console.WriteLine(attachments);
        }
    }
}