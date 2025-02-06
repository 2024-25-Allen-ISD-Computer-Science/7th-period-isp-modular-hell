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

        [XmlIgnore]
        public string animation;
        [XmlIgnore]
        public string characterState = "Idle";
        public string previousState;

        public int frame = 0;
        public int frameRate = 1;


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

        public void LoadAnimation(string name, SpriteBatch spriteBatch, Vector2 screenPosition, GameTime gameTime)
        {
            foreach (var method in typeof(Animator).GetMethods())
            {
                //Console.WriteLine(method.Name);
                if (method.Name == name)
                {
                   this.animation = name;
                   method.Invoke(this, [this, spriteBatch, screenPosition, Convert.ToInt32((float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond)]);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 camPos)
        {
            Vector2 camPlayerOffset = Vector2.Subtract(camPos, _position);
            Vector2 screenPosition = Vector2.Add(ScreenManager.Instance.MiddleScreen,camPlayerOffset);
            Rectangle headRect = new Rectangle(0, 0, 1000, 1000);
            screenPosition.X -= headRect.Width / 10 / 2;
            screenPosition.Y -= headRect.Height / 10;

            doAnimation(spriteBatch, screenPosition, gameTime);
            //AttachmentSlots[0].Item1.AttachmentSlots[0].Item1.Draw(spriteBatch, new Vector2(50,80), 0.1f * (_velocity.X / _moveSpeed));
            //AttachmentSlots[0].Item1.AttachmentSlots[2].Item1.Draw(spriteBatch, new Vector2(60,120), -0.1f * (_velocity.X / _moveSpeed));
           
            base.Draw(spriteBatch, gameTime, camPos);
            //AttachmentSlots[0].Item1.Draw(spriteBatch, new Vector2(45, 80), 0.1f);

            spriteBatch.Draw(_entityTexture, screenPosition, headRect, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0.2f);
            //AttachmentSlots[0].Item1.AttachmentSlots[3].Item1.Draw(spriteBatch, new Vector2(35, 120), 0.1f * (_velocity.X / _moveSpeed));
            //AttachmentSlots[0].Item1.AttachmentSlots[1].Item1.Draw(spriteBatch, new Vector2(20, 80), -0.1f * (_velocity.X / _moveSpeed));
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

            if (isMoving) {
                if (this.characterState == "Idle") {
                    this.previousState = this.characterState;
                    this.characterState = "Walking";
                } else {
                    this.characterState = "Walking";
                }
            } else {
                if (this.characterState == "Walking") {
                    this.previousState = this.characterState;
                    this.characterState = "Idle";
                } else {
                    this.characterState = "Idle";
                }
            }
                
        }

        private void doAnimation(SpriteBatch spriteBatch, Vector2 screenPosition, GameTime gameTime) {
            Console.WriteLine(this.previousState);
            Console.WriteLine(this.characterState);
            if (this.characterState != this.previousState) {
                //this.frame = 0;
            }

            if (this.isMoving) {
                this.LoadAnimation("Walk", spriteBatch, screenPosition, gameTime);
            } else {
                this.LoadAnimation("Idle", spriteBatch, screenPosition, gameTime);
            }
        }

    };
};