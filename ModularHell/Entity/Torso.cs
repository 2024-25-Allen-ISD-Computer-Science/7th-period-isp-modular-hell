using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices.Marshalling;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ModularHell 
{
    public class Torso : EntityAttachment
    {
        //public HealthComponent Health;  
        //public Vector2 PosOnEntity;

        public Torso() : base(4) {
        } //creates the 4 slots 
        public override void LoadContent()
        {
            base.LoadContent();
            _attachmentTexture = Content.Load<Texture2D>(texturePath);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset, float interval)
        {
            Texture2D torsoTexture = Content.Load<Texture2D>("Torso");
            Rectangle torsoRect = new Rectangle(0,0, 1000, 1000);
            var origin = new Vector2(torsoTexture.Width / 2f, torsoTexture.Height / 4f);
            var rotation = 0.0f;

            spriteBatch.Draw(
                torsoTexture, // texture
                Vector2.Add(Vector2.Zero, offset), // position
                torsoRect, // rect
                Color.White, // color (useful for recolors of the same attachment sprites)
                rotation, // rotation
                origin, // origin
                0.1f, // scale
                SpriteEffects.None, // sprite effects
                0.3f // layerdepth
                );
        }

        public void AddAttachment(string attachment, EntityAttachment slot){
            var NewAttachment = new EntityAttachment();
            Host.xmlAttachmentManager.Type = typeof(EntityAttachment);
            slot = Host.xmlAttachmentManager.Load("Entity/Load/" + attachment + ".xml");
            slot.LoadContent();
        }

    }
}