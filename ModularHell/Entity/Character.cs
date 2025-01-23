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

        public Character() : base() {}

        public Character(int slots = 0) : base(slots) {}

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

           // foreach (EntityAttachment attachment in _attachmentSlots)
           //     attachment.Update(gameTime);
            
            doMovement();
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 camPos)
        {
            (EntityAttachment, int, Vector2) Torso = AttachmentSlots[0];
            (EntityAttachment, int, Vector2) lArm = Torso.Item1.AttachmentSlots[0];
            (EntityAttachment, int, Vector2) lLeg = Torso.Item1.AttachmentSlots[2];
            (EntityAttachment, int, Vector2) rArm = Torso.Item1.AttachmentSlots[1];
            (EntityAttachment, int, Vector2) rLeg = Torso.Item1.AttachmentSlots[3];

            Vector2 screenOffset = Vector2.Subtract(_position, camPos);
            Vector2 screenPosition = Vector2.Add(ScreenManager.Instance.MiddleScreen,screenOffset);

            rArm.Item1.Draw(spriteBatch, screenPosition, rArm.Item3, 0.1f * (_velocity.X / _moveSpeed));
            rLeg.Item1.Draw(spriteBatch, screenPosition, rLeg.Item3, -0.1f * (_velocity.X / _moveSpeed));
           
            base.Draw(spriteBatch, camPos);
            Torso.Item1.Draw(spriteBatch, screenPosition, new Vector2(45, 80), 0.1f);
            Rectangle headRect = new Rectangle(10, 10, 1000, 1000);
            spriteBatch.Draw(_entityTexture, screenPosition, headRect, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0.2f);

            lLeg.Item1.Draw(spriteBatch, screenPosition, lLeg.Item3, 0.1f * (_velocity.X / _moveSpeed));
            lArm.Item1.Draw(spriteBatch, screenPosition, lArm.Item3, -0.1f * (_velocity.X / _moveSpeed));

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