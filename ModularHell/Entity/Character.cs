using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Net.Mail;

namespace ModularHell 
{
    [XmlInclude(typeof(Character))]
    public class Character : Entity
    {

        private int _moveSpeed = 200;

        [XmlIgnore]
        public Torso NeckSlot;
        public string NeckSlotXml;

        public override void LoadContent()
        {
            base.LoadContent();
            
            xmlAttachmentManager.Type = typeof(Torso);
            NeckSlot = (Torso)xmlAttachmentManager.Load($"Entity/Load/{NeckSlotXml}.xml");
            NeckSlot.Host = this;
            NeckSlot.LoadContent();
        }

        public override void UnloadContent()
        {
            if (!String.IsNullOrEmpty(Name))
                xmlAttachmentManager.Type = typeof(Torso);
                xmlAttachmentManager.Save($"Entity/Load/{NeckSlotXml}.xml", NeckSlot);

            NeckSlot.UnloadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

           // foreach (EntityAttachment attachment in _attachmentSlots)
           //     attachment.Update(gameTime);
            
            doMovement();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            
            base.Draw(spriteBatch);
            NeckSlot.Draw(spriteBatch, new Vector2(45, 80));
            Rectangle headRect = new Rectangle(10,10, 1000, 1000);
            spriteBatch.Draw(_entityTexture, this._position, headRect, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 1.0f);
        }

        private void doMovement() 
        {
            this.isMoving = false;

            if (InputHandler.HoldingKey(Keys.Right)) {
                this._velocity.X = _moveSpeed;  
                isMoving = true;
            }
            
            if (InputHandler.HoldingKey(Keys.Left)) {
                this._velocity.X = -_moveSpeed;
                isMoving = true;
            }
                
            if (InputHandler.HoldingKey(Keys.Up)) {
                this._velocity.Y = -_moveSpeed;
                isMoving = true;
            }
                
            if (InputHandler.HoldingKey(Keys.Down)) {
                this._velocity.Y = _moveSpeed;
                isMoving = true;
            }
                
        }
        public override void Generate() {
            base.LoadContent();

            xmlAttachmentManager.Type = typeof(Torso);
            NeckSlot = (Torso)xmlAttachmentManager.Load($"Entity/Load/Torso.xml");
            NeckSlot.Host = this;
            NeckSlot.Generate();

            if (!string.IsNullOrEmpty(Name)) {
                NeckSlotXml = $"{Name}_NeckSlot";
            }
        }
    };
};