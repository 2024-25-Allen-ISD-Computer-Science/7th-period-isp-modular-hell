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
    public class Character : Entity
    {

        private int _moveSpeed = 200;


        [XmlIgnore]
        public Torso NeckSlot;
        public string NeckSlotXml;


        
        public Character() {
            
        }
        
        public override void LoadContent()
        {

            base.LoadContent();
            _entityTexture = Content.Load<Texture2D>(texturePath);
            
            NeckSlot = new Torso();
            xmlAttachmentManager.Type = NeckSlot.Type;
            NeckSlot = (Torso)xmlAttachmentManager.Load("Entity/Load/" + NeckSlotXml + ".xml");
            NeckSlot.host = this;
            NeckSlot.LoadContent();
            
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

           // foreach (EntityAttachment attachment in _attachmentSlots)
           //     attachment.Update(gameTime);
            
            doMovement();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            NeckSlot.lArm.Draw(spriteBatch, new Vector2(50,80));

            NeckSlot.rArm.Draw(spriteBatch, new Vector2(20, 80));
            
            base.Draw(spriteBatch);
            NeckSlot.Draw(spriteBatch, new Vector2(45, 80));
            Rectangle headRect = new Rectangle(10,10, 1000, 1000);
            spriteBatch.Draw(_entityTexture, this._position, headRect, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0.2f);

            
            NeckSlot.rArm.Draw(spriteBatch, new Vector2(20, 80));

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
    };
};