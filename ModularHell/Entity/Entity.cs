using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Data;
using System.Net;


namespace ModularHell
{
    [XmlInclude(typeof(Character)), XmlInclude(typeof(Enemy))]
    public class Entity
    {
        protected Texture2D _entityTexture;
        public Vector2 Dimensions;

        public (EntityAttachment, int, Vector2)[] AttachmentSlots;
        //      (Attachment, Slot Tier, Position)
        public string texturePath;
        public string Name { get; set; }
        [XmlIgnore]
        public Rectangle TextureRect;
        [XmlIgnore]
        public Rectangle PhysicsRect;

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
        public string previousState = "Idle";

        public int frame = 0;
        public int transitionFrame = 50;
        public int frameRate = 20;
        [XmlIgnore]
        public Keyframe previousKeyframe = null;

        public Entity()
        {
            xmlAttachmentManager = new XmlManager<EntityAttachment>();
            AttachmentSlots = new (EntityAttachment, int, Vector2)[0];
        }

        public Entity(int slots = 0)
        {
            xmlAttachmentManager = new XmlManager<EntityAttachment>();
            AttachmentSlots = new (EntityAttachment, int, Vector2)[slots];
        }

        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            _entityTexture = Content.Load<Texture2D>(texturePath);
            Dimensions.X = (int)(_entityTexture.Width / 10);
            Dimensions.Y = (int)(_entityTexture.Height / 10);


            if (AttachmentSlots.Length > 0)
            {
                foreach (var (attachment, tier, position) in AttachmentSlots)
                {
                    attachment.Host = this;
                    attachment.PositionOnHost = position;
                    attachment.LoadContent();
                    //temp
                    attachment.LoadAnimation("Idle");
                }
            }
        }

        public virtual void UnloadContent()
        {
            if (AttachmentSlots.Length > 0)
            {
                foreach (var (attachment, tier, position) in AttachmentSlots)
                {
                    attachment.UnloadContent();
                }
            }
        }

        public virtual void Update(GameTime gameTime, ref int[,] collisionMap)
        {
            PhysicsRect = new Rectangle((int)_position.X, (int)_position.Y, (int)Dimensions.X, (int)Dimensions.Y);
            DoPhysics(gameTime, ref collisionMap);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, ref Camera cam)
        {
            Vector2 screenPosition = cam.WorldToScreenPosition(_position);

            TextureRect = new Rectangle((int)screenPosition.X, (int)screenPosition.Y, (int)(Dimensions.X * cam.Scale), (int)(Dimensions.Y * cam.Scale));

            this.doAnimation(ref cam, spriteBatch, gameTime);

            spriteBatch.Draw(_entityTexture, TextureRect, Color.White);
            /*
            if (ScreenManager.Instance.Debug)
            {
                spriteBatch.Draw(_entityTexture, screenPosition, TextureRect, Color.White, 0f, Vector2.Zero, cam.Scale, SpriteEffects.None, 0.2f);
            }
            */
        }

        public virtual void Generate()
        {
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

        protected void DoPhysics(GameTime gameTime, ref int[,] collisionMap)
        {
            //makes 4 corners of hitbox
            Vector2[] Corners = { _position, new Vector2(_position.X + PhysicsRect.Width, _position.Y), new Vector2(_position.X, _position.Y + PhysicsRect.Height), new Vector2(_position.X + PhysicsRect.Width, _position.Y + PhysicsRect.Height) };
            Vector2 middlePoint = new Vector2(PhysicsRect.Center.X, PhysicsRect.Center.Y);

            Vector2 PushBack = Vector2.Zero;

            Vector2 StepMove = Vector2.Multiply(_velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);

            for (int corner = 0; corner < Corners.Length; corner++)
            {
                Vector2 CornerNextPos = Corners[corner] + StepMove;
                int nextXTile = (int)(CornerNextPos.X / 100);
                int nextYTile = (int)(CornerNextPos.Y / 100);
                
                if (nextXTile < 0 || nextYTile < 0 || nextYTile > collisionMap.Length || nextXTile > collisionMap.Length)
                {
                    _velocity = Vector2.Zero;
                    this._position = new Vector2(200, 200);
                    break;
                }
                
                if (collisionMap[nextYTile, nextXTile] == 1)
                {
                    PushBack += middlePoint - Corners[corner];
                }
            }

            if (PushBack != Vector2.Zero)
            {
                PushBack.Normalize();
                StepMove = PushBack;
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


        public void doAnimation(ref Camera cam, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Console.WriteLine(this.characterState);
            if (this.characterState != this.previousState)
            {
                this.frame = 0;
                this.transitionFrame = 0;
            }
            previousState = characterState;
            if (this.isMoving)
            {
                ch
                this.LoadAnimation("Walk", ref cam, spriteBatch, gameTime);
            }
            else
            {
                this.LoadAnimation("Idle", ref cam, spriteBatch, gameTime);
            }
        }
        public void LoadAnimation(string name, ref Camera cam, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var method in typeof(Animator).GetMethods())
            {
                //Console.WriteLine(method.Name);
                if (method.Name == name)
                {
                    this.animation = name;
                    method.Invoke(this, [this, cam, spriteBatch, Convert.ToInt32((float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond)]);
                }
            }
        }

    }
}