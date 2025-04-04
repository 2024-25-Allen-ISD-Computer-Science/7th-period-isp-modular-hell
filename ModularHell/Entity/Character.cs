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
        public List<Object> Summons;



        public override void LoadContent()
        {
            base.LoadContent();
            Summons = new List<Object>();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, ref int[,] collisionMap) {
            doMovement();
            base.Update(gameTime, ref collisionMap);
            foreach (Object summon in Summons){
                summon.Update(gameTime);
            }

           // foreach (EntityAttachment attachment in _attachmentSlots)
           //     attachment.Update(gameTime);
            
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, ref Camera cam)
        {
            base.Draw(spriteBatch, gameTime, ref cam);
            foreach (Object summon in Summons){
                summon.Draw(ref cam, spriteBatch);
            }

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
                if (AccelerationVector.X > 1) {
                    facing = "right";
                } else if (AccelerationVector.X < 1) {
                    facing = "left";
                }
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

        
        public override void CheckForTrigger(ref Camera cam) {
            if (InputHandler.HoldingKey(Keys.F)) {
                Vector2 shootVec = cam.ScreenToWorldPosition(InputHandler.MousePosition) - _position;
                shootVec.Normalize();
                Object fireball = new Object(_position, shootVec * 30);
                fireball.LoadContent();
                Summons.Add(fireball);
            }
        }
    };
};