using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Xml.Linq;
using System.Data;
using System.Xml.Serialization;

namespace ModularHell
{
    public class Map {

        protected ContentManager Content;

        public string mapTexturePath;
        private Texture2D mapTexture;

        [XmlIgnore]
        public int[,] collisionMap;
        private Texture2D ballTexture;
        public string texturePath;

        public Map() {
            mapTexturePath = "Maps/cave";
            texturePath = "ball";

            collisionMap = new int[,]
            {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            };
        }


        public virtual void LoadContent() {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            mapTexture = Content.Load<Texture2D>(mapTexturePath);
            //ballTexture = Content.Load<Texture2D>(texturePath);


        }
        
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 camPos)
        {       
            spriteBatch.Draw(mapTexture, -camPos + ScreenManager.Instance.MiddleScreen, Color.White);

            for (int row = 0; row < collisionMap.GetLength(0); row ++) {
                for (int column = 0; column < collisionMap.GetLength(1); column ++)
                {
                    if (collisionMap[row,column] == 1) {
                        float y = (100 * row) - camPos.Y + ScreenManager.Instance.MiddleScreen.Y;
                        float x = (100 * column) - camPos.X + ScreenManager.Instance.MiddleScreen.Y;
 

                        //Rectangle rect = new Rectangle((int)x,(int)y,100,100);

                        //spriteBatch.Draw(ballTexture, rect, Color.White);
                    }
                }
            }
         
        }

        public static void SaveMapAsXml(int[,] tileMap, string fileName = "tile_map.xml")
        {
            // Get map dimensions
            int width = tileMap.GetLength(1);
            int height = tileMap.GetLength(0);

            // Create the root element
            XElement mapElement = new XElement("map");
            mapElement.SetAttributeValue("width", width);
            mapElement.SetAttributeValue("height", height);

            // Create the 'tiles' element to hold all the rows
            XElement tilesElement = new XElement("tiles");

            // Iterate through each row and add it to the XML
            for (int y = 0; y < height; y++)
            {
                XElement rowElement = new XElement("row");
                
                for (int x = 0; x < width; x++)
                {
                    XElement tileElement = new XElement("tile");
                    tileElement.Value = tileMap[y, x].ToString(); // Set the tile ID as text
                    rowElement.Add(tileElement);
                }

                tilesElement.Add(rowElement);
            }

            // Add the 'tiles' element to the root
            mapElement.Add(tilesElement);

            // Save the XML to a file
            mapElement.Save(fileName);
        }
    }
}