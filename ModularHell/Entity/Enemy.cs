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
    public class Enemy : Entity
    {

        private int _movementAcceleration = 200;

        [XmlIgnore]
        public Torso NeckSlot;
        public string NeckSlotXml;

        public Enemy() : base() {}

        public Enemy(int slots = 0) : base(slots) {}



        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, ref int[,] collisionMap) {
            doMovement();
            base.Update(gameTime, ref collisionMap);

           // foreach (EntityAttachment attachment in _attachmentSlots)
           //     attachment.Update(gameTime);
            
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, ref Camera cam)
        {
            base.Draw(spriteBatch, gameTime, ref cam);
            
        }

        private void doMovement() 
        {
            this.isMoving = false;
            Vector2 AccelerationVector = Vector2.Zero;
            

            if (isMoving) {
                this.previousState = this.characterState;
                this.characterState = "Walking";
            } else {
                this.previousState = this.characterState;
                this.characterState = "Idle";
            }
                
        }

        public override void Generate() {
            //LoadContent();

            xmlAttachmentManager.Type = typeof(Torso);

            /*
            if (AttachmentSlots.Length > 0) {
                AttachmentSlots[1].Item1 = new Torso();
                AttachmentSlots[1].Item1.Generate();
            }
            */
            AttachmentSlots[0].Item1 = (Torso)xmlAttachmentManager.Load($"Entity/Load/old/Torso.xml");
            AttachmentSlots[0].Item1.Host = this;
            AttachmentSlots[0].Item1.Generate();
            
            characterState = "Idle";

            frame = 0;
            transitionFrame = 50;
            frameRate = 1;

            /*
            if (!string.IsNullOrEmpty(Name)) {
                NeckSlotXml = $"{Name}_NeckSlot";
            }
            */
        }

    };
};