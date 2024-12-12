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
    [XmlInclude(typeof(Torso))]
    public class Torso : EntityAttachment
    {
        [XmlIgnore]
        public EntityAttachment lArm {get; private set;}
        public string lArmXML;
        public Vector2 lArmOffset;

        [XmlIgnore]
        public EntityAttachment rArm {get; private set;}
        public string rArmXML;
        public Vector2 rArmOffset;
        [XmlIgnore]
        public EntityAttachment lLeg {get; private set;}
        public string lLegXML;
        public Vector2 lLegOffset;
        [XmlIgnore]
        public EntityAttachment rLeg {get; private set;}
        public string rLegXML;
        public Vector2 rLegOffset;
        [XmlIgnore]
        public XmlManager<EntityAttachment> xmlAttachmentManager;


        //public HealthComponent Health;  
        //public Vector2 PosOnEntity;

        public Torso() {
            xmlAttachmentManager = new XmlManager<EntityAttachment>();
            lArmOffset = new Vector2();
            lArmOffset = new Vector2();
            lArmOffset = new Vector2();
            lArmOffset = new Vector2();
        }
        
        public override void LoadContent()
        {
            base.LoadContent();
            _attachmentTexture = Content.Load<Texture2D>(texturePath);
            xmlAttachmentManager.Type = typeof(EntityAttachment);
            

            lArm = xmlAttachmentManager.Load("Entity/Load/" + lArmXML + ".xml");
            lArm.Host = this.Host;
            lArm.LoadContent();
            lArm.LoadAnimation("Swing");

            rArm = xmlAttachmentManager.Load("Entity/Load/" + rArmXML + ".xml");
            rArm.Host = this.Host;
            rArm.LoadContent();
            rArm.LoadAnimation("Swing");
        }

        public override void UnloadContent()
        {
            if (!String.IsNullOrEmpty(Host.Name))
                xmlAttachmentManager.Type = typeof(EntityAttachment);
                xmlAttachmentManager.Save($"Entity/Load/{lArmXML}.xml", lArm);
                xmlAttachmentManager.Save($"Entity/Load/{rArmXML}.xml", rArm);

            
        }
        

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset, float interval)
        {

            lArm.Draw(spriteBatch, new Vector2(50,80));   
            
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
                0.0f // layerdepth
                );

            rArm.Draw(spriteBatch, new Vector2(20, 80));
        }

        public void AddAttachment(string attachment, EntityAttachment slot){

            Host.xmlAttachmentManager.Type = typeof(EntityAttachment);
            slot = Host.xmlAttachmentManager.Load("Entity/Load/" + attachment + ".xml");
            slot.LoadContent();
        }

        public override void Generate()
        {
            base.Generate();

            xmlAttachmentManager.Type = typeof(EntityAttachment);
            lArm = (EntityAttachment)xmlAttachmentManager.Load($"Entity/Load/DefaultArm.xml");
            lArm.Host = this.Host;
            lArm.Generate();
            rArm = (EntityAttachment)xmlAttachmentManager.Load($"Entity/Load/DefaultArm.xml");
            rArm.Host = this.Host;
            rArm.Generate();
        

            if (!string.IsNullOrEmpty(Host.Name)) {
                lArmXML = $"{Host.Name}_NeckSlot_lArm";
                rArmXML = $"{Host.Name}_NeckSlot_rArm";
            }
        }

    }
}