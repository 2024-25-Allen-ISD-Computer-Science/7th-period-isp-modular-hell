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
        [XmlIgnore]
        public EntityAttachment lArm;
        public string lArmXML;
        [XmlIgnore]
        public EntityAttachment rArm;
        public string rArmXML;
        [XmlIgnore]
        public EntityAttachment lLeg;
        public string lLegXML;
        [XmlIgnore]
        public EntityAttachment rLeg;
        public string rLegXML;
        //public HealthComponent Health;  
        //public Vector2 PosOnEntity;

        public Torso() : base(4) {
        } //creates the 4 slots 
        public override void LoadContent()
        {
            base.LoadContent();
            _attachmentTexture = Content.Load<Texture2D>(texturePath);

            lLeg = new EntityAttachment();
            Host.xmlAttachmentManager.Type = typeof(EntityAttachment);
            lLeg = Host.xmlAttachmentManager.Load("Entity/Load/" + lLegXML + ".xml");
            lLeg.Host = this.Host;
            lLeg.LoadContent();
            lLeg.LoadAnimation("Swing");
            
            lArm = new EntityAttachment();
            Host.xmlAttachmentManager.Type = typeof(EntityAttachment);
            //Console.WriteLine(lArmXML);
            lArm = Host.xmlAttachmentManager.Load("Entity/Load/" + lArmXML + ".xml");
            lArm.Host = this.Host;
            lArm.LoadContent();
            lArm.LoadAnimation("Swing");

            rLeg = new EntityAttachment();
            Host.xmlAttachmentManager.Type = typeof(EntityAttachment);
            rLeg = Host.xmlAttachmentManager.Load("Entity/Load/" + rLegXML + ".xml");
            rLeg.Host = this.Host;
            rLeg.LoadContent();
            rLeg.LoadAnimation("Swing");

            rArm = new EntityAttachment();
            Host.xmlAttachmentManager.Type = typeof(EntityAttachment);
            rArm = Host.xmlAttachmentManager.Load("Entity/Load/" + rArmXML + ".xml");
            rArm.Host = this.Host;
            rArm.LoadContent();
            rArm.LoadAnimation("Swing");
        }

        public override void UnloadContent()
        {
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
                new Vector2(this.Host._position.X + offset.X, this.Host._position.Y + offset.Y), // position
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