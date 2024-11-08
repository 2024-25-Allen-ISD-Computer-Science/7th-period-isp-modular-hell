using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ModularHell 
{
    public class Character : Entity
    {

        private EntityAttachment NeckSlot;
        
        public Character() {
            _position = new Vector2(0,0);
            NeckSlot = new EntityAttachment(this, "bruh");
            NeckSlot.LoadMethods();
        }
        
        public override void LoadContent()
        {
            base.LoadContent();
            _entityTexture = Content.Load<Texture2D>("CharacterHead1");

            //foreach (EntityAttachment attachment in _attachmentSlots)
              //  attachment.LoadContent();
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
            base.Draw(spriteBatch);

           // foreach (EntityAttachment attachment in _attachmentSlots)
            //    attachment.Draw(spriteBatch);

            spriteBatch.Draw(_entityTexture, this._position, Color.White);
        }

        private void doMovement() 
        {
            if (InputHandler.HoldingKey(Keys.Right))
                this.MoveX(10);
            if (InputHandler.HoldingKey(Keys.Left))
                this.MoveX(-10);
            if (InputHandler.HoldingKey(Keys.Up))
                this.MoveY(-10);
            if (InputHandler.HoldingKey(Keys.Down))
                this.MoveY(10);
        }
    };
};