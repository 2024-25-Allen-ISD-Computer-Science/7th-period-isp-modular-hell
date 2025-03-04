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

        [XmlIgnore]
        public Keyframe previousKeyframe = null;


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

            Vector2 camPlayerOffset = Vector2.Subtract(camPos, _position);
            Vector2 screenPosition = Vector2.Add(ScreenManager.Instance.MiddleScreen,camPlayerOffset);
            Rectangle headRect = new Rectangle(0, 0, _entityTexture.Width, _entityTexture.Height);
            screenPosition.X -= headRect.Width / 10 / 2;
            screenPosition.Y -= headRect.Height / 10;

            doAnimation(spriteBatch, screenPosition, gameTime);

            spriteBatch.Draw(_entityTexture, screenPosition, headRect, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0.2f);
        }

        private void doMovement() 
        {
            this.isMoving = false;
            Vector2 AccelerationVector = Vector2.Zero;
            //this.frameRate = (int)(1 * Math.Sqrt(this._velocity.X * this._velocity.X + this._velocity.Y * this._velocity.Y));

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
                this.previousState = this.characterState;
                this.characterState = "Walking";
            } else {
                this.previousState = this.characterState;
                this.characterState = "Idle";
            }
                
        }

    };
};