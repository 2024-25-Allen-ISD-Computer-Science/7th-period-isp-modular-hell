using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ModularHell 
{
    public class Character : Entity
    {

        private int _moveSpeed;

        private EntityAttachment NeckSlot;
        
        public Character() {
            _position = new Vector2(0,0);
            NeckSlot = new EntityAttachment(this, "bruh");
            NeckSlot.LoadMethods();
            _moveSpeed = 200;
        }
        
        public override void LoadContent()
        {
            base.LoadContent();
            _entityTexture = Content.Load<Texture2D>(texturePath);

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
                this._velocity.X = _moveSpeed;
            if (InputHandler.HoldingKey(Keys.Left))
                this._velocity.X = -_moveSpeed;
            if (InputHandler.HoldingKey(Keys.Up))
                this._velocity.Y = -_moveSpeed;
            if (InputHandler.HoldingKey(Keys.Down))
                this._velocity.Y = _moveSpeed;
        }
    };
};