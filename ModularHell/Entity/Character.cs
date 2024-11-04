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

        public Character() {
            _position = new Vector2(0,0);
            _attachmentSlots = new List<EntityAttachment>();
            _MaxAttachments = 2;
            //temp
            if (_attachmentSlots.Count <= _MaxAttachments)
                _attachmentSlots.Add(new StickLeg(this));
        }
        
        public override void LoadContent()
        {
            base.LoadContent();
            _entityTexture = Content.Load<Texture2D>("CharacterHead1");

            foreach (EntityAttachment attachment in _attachmentSlots)
                attachment.LoadContent();
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            foreach (EntityAttachment attachment in _attachmentSlots)
                attachment.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (EntityAttachment attachment in _attachmentSlots)
                attachment.Draw(spriteBatch);

            spriteBatch.Draw(_entityTexture, this._position, Color.White);
        }
    };
};