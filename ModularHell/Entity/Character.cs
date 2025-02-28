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

        private int _movementAcceleration = 200;

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

        public override void Update(GameTime gameTime, ref int[,] collisionMap) {
            base.Update(gameTime, ref collisionMap);

           // foreach (EntityAttachment attachment in _attachmentSlots)
           //     attachment.Update(gameTime);
            
            doMovement();
            
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 camPos)
        {
            base.Draw(spriteBatch, gameTime, camPos);
        }

        private void doMovement() 
        {
            this.isMoving = false;
            Vector2 AccelerationVector = Vector2.Zero;

            if (InputHandler.HoldingKey(Keys.Right)) {
                AccelerationVector.X = 1;
            }
            
            if (InputHandler.HoldingKey(Keys.Left)) {
                AccelerationVector.X = -1;
            }
                
            if (InputHandler.HoldingKey(Keys.Up)) {
                AccelerationVector.Y = -1;
            }
                
            if (InputHandler.HoldingKey(Keys.Down)) {
                AccelerationVector.Y = 1;
            }

            if (AccelerationVector != Vector2.Zero) {
                AccelerationVector.Normalize();
                this.isMoving = true;
                _velocity += AccelerationVector * _movementAcceleration;
            }

            if (isMoving) {
                this.characterState = "Walk";
            } else {
                this.characterState = "Idle"; 
            }
        }

    };
};