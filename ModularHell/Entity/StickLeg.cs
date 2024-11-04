using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ModularHell 
{
    public class StickLeg : EntityAttachment
    {

        //public HealthComponent Health;  
        //public Vector2 PosOnEntity;
        public StickLeg(Entity host)
        {
            _host = host;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _attachmentTexture = Content.Load<Texture2D>("CharacterHead1");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            doMovement();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_attachmentTexture, _host._position + new Vector2(0,10), Color.White);
        }

        private void doMovement() 
        {
            if (InputHandler.HoldingKey(Keys.Right))
                _host.MoveX(10);
            if (InputHandler.HoldingKey(Keys.Left))
                _host.MoveX(-10);
            if (InputHandler.HoldingKey(Keys.Up))
                _host.MoveY(-10);
            if (InputHandler.HoldingKey(Keys.Down))
                _host.MoveY(10);
        }
    }
}