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

        public override void Draw(ref Camera cam, SpriteBatch spriteBatch, ref Vector2 AnimationOffset, ref float rotation)
        {
            base.Draw(ref cam, spriteBatch, ref AnimationOffset, ref rotation);
        }

        public void AddAttachment(string attachment, EntityAttachment slot){
            var NewAttachment = new EntityAttachment();
            Host.xmlAttachmentManager.Type = typeof(EntityAttachment);
            slot = Host.xmlAttachmentManager.Load("Entity/Load/" + attachment + ".xml");
            slot.LoadContent();
        }

    }
}