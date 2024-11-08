using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ModularHell;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        //Sets screen size to .Dimensions set in ScreenManager
        _graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
        _graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        ScreenManager.Instance.LoadContent(Content);
    }

    protected override void UnloadContent()
    {
        ScreenManager.Instance.UnloadContent();
    }
    protected override void Update(GameTime gameTime)
    {
        //exits game
        if (InputHandler.HoldingKey(Keys.Escape))
            this.Exit();


        ScreenManager.Instance.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();
        ScreenManager.Instance.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

}
