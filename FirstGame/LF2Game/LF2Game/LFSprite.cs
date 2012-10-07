using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LF2Game
{
   public class LFSprite
    {
        public Texture2D Texture { get; set; }
        public Rectangle sourceRectangle {get; set;}
        public Rectangle destinationRectangle {get; set;}
        public Boolean is_moving { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private int timer = 0;
        private int [] frames = new int[] {0,1,2,3,4,5,6,7,8};
        private Boolean up { get; set; }
        


    public LFSprite(Texture2D texture, int rows, int columns)
    {
        Texture = texture;
        Rows = rows;
        Columns = columns;
        currentFrame = 0;
        totalFrames = Rows * Columns;
        int width = Texture.Width / Columns;
        int height = Texture.Height / Rows;
        int row = (int)((float)currentFrame / (float)Columns);
        int column = currentFrame % Columns;
        System.Console.WriteLine(texture.Width);
    }

    public void Update()
    {
        
        timer++;
        if (timer % 8 == 0)
        {
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
           
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        int width = Texture.Width / Columns;
        int height = Texture.Height / Rows;
        int row = (int)((float)currentFrame / (float)Columns);
        int column = currentFrame % Columns;
        sourceRectangle = new Rectangle(width * column, height * row, width, height);
        destinationRectangle = new Rectangle((int)location.X + timer, (int)location.Y, width, height);
        spriteBatch.Begin();
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        spriteBatch.End();
    }
    }
}
