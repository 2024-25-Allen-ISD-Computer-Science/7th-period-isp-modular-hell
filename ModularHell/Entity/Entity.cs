using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Data;


namespace ModularHell 
{
    [XmlInclude(typeof(Character))]
    public class Entity
    {
        protected Texture2D _entityTexture;
        public (EntityAttachment, int, Vector2)[] AttachmentSlots;
        //      (Attachment, Slot Tier, Position)
        public string texturePath;
        public string Name {get; set;}

        [XmlIgnore]
        public Vector2 _velocity;
        public Vector2 _position;
        protected static ContentManager Content;

        [XmlIgnore]
        public XmlManager<EntityAttachment> xmlAttachmentManager;

        [XmlIgnore]
        public bool isMoving = false;
        [XmlIgnore]
        public string animation;
        [XmlIgnore]
        public string characterState = "Idle";
        public string previousState;

        public int frame = 0;
        public int transitionFrame = 50;
        public int frameRate = 1;

        public Entity()
        {
            xmlAttachmentManager = new XmlManager<EntityAttachment>();
            AttachmentSlots = new (EntityAttachment, int, Vector2)[0];
        }

        public Entity(int slots = 0){
            xmlAttachmentManager = new XmlManager<EntityAttachment>();
            AttachmentSlots = new (EntityAttachment, int, Vector2)[slots];
        }

        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            _entityTexture = Content.Load<Texture2D>(texturePath);

            if (AttachmentSlots.Length > 0) {
                foreach (var (attachment, tier, position) in AttachmentSlots){
                    attachment.Host = this;
                    attachment.LoadContent();
                    //temp
                    attachment.LoadAnimation("Idle");
                }
            }
        }

        public virtual void UnloadContent()
        {
            if (AttachmentSlots.Length > 0) {
                foreach (var (attachment, tier, position) in AttachmentSlots){
                    attachment.UnloadContent();
                }
            }
        }

        public virtual void Update(GameTime gameTime, ref int[,] collisionMap)
        {
            DoPhysics(gameTime, ref collisionMap);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 camPos)
        {
            Vector2 camPlayerOffset = Vector2.Subtract(camPos, _position);
            Vector2 screenPosition = Vector2.Add(ScreenManager.Instance.MiddleScreen,camPlayerOffset);
            Rectangle headRect = new Rectangle(0, 0, _entityTexture.Width, _entityTexture.Height);
            screenPosition.X -= headRect.Width / 10 / 2;
            screenPosition.Y -= headRect.Height / 10;

            doAnimation(spriteBatch, screenPosition, gameTime);

            spriteBatch.Draw(_entityTexture, screenPosition, headRect, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0.2f);
        }

        public virtual void Generate() {
            //LoadContent();

            xmlAttachmentManager.Type = typeof(Torso);

            /*
            if (AttachmentSlots.Length > 0) {
                AttachmentSlots[1].Item1 = new Torso();
                AttachmentSlots[1].Item1.Generate();
            }
            */
            AttachmentSlots[0].Item1 = (Torso)xmlAttachmentManager.Load($"Entity/Load/Torso.xml");
            AttachmentSlots[0].Item1.Host = this;
            AttachmentSlots[0].Item1.Generate();

            /*
            if (!string.IsNullOrEmpty(Name)) {
                NeckSlotXml = $"{Name}_NeckSlot";
            }
            */
        }

        protected void DoPhysics(GameTime gameTime, ref int[,] collisionMap){
            if (_velocity != Vector2.Zero) {
                Vector2 StepMove = Vector2.Multiply(_velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);
                Vector2 nextPosition = _position + StepMove;
                int nextXTile = (int)(nextPosition.X / 100);
                int nextYTile = (int)(nextPosition.Y / 100);
                
                Console.Write(collisionMap[nextYTile,nextXTile]);
                if (collisionMap[nextYTile,nextXTile] == 1) {
                    if (nextPosition.X < _position.X) {
                        StepMove.X = -(_position.X % 100) + 1;
                        _velocity.X = 0;
                    } else if (nextPosition.X > _position.X){
                        StepMove.X = 100 - (_position.X % 100) - 1;
                        _velocity.X = 0;
                    }
                    if (nextPosition.Y < _position.Y) {
                        StepMove.Y = -(_position.Y % 100) + 1;
                        _velocity.Y = 0;
                    } else if (nextPosition.Y < _position.Y){
                        StepMove.Y = 100 - (_position.Y % 100) - 1;
                        _velocity.Y = 0;
                    }
                }
                
                _position += StepMove;

                //System.Console.WriteLine("character is at " + _position);
                //use world friction once put into xml
                _velocity.X *= .5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _velocity.Y *= .5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_velocity.X * _velocity.X < 50) 
                {
                    _velocity.X = 0;
                }
                if (_velocity.Y * _velocity.Y < 50)
                {
                    _velocity.Y = 0;
                }
            }

        }


        private void doAnimation(SpriteBatch spriteBatch, Vector2 screenPosition, GameTime gameTime) {
            Console.WriteLine(this.characterState);
            if (this.characterState != this.previousState) {
                this.frame = 0;
                this.transitionFrame = 0;
            }

            if (this.isMoving) {
                this.LoadAnimation("Walk", spriteBatch, screenPosition, gameTime);
            } else {
                this.LoadAnimation("Idle", spriteBatch, screenPosition, gameTime);
            }
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

    }
}