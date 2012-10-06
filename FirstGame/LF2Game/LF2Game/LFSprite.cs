using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tutorial
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
        private Boolean loop = false;


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
        if (timer % 8 == 0 && is_moving)
        {
            System.Console.WriteLine("Current Frame is:" + currentFrame);
            System.Console.WriteLine(currentFrame + " of " + totalFrames);
            if (loop == false)
                currentFrame++;
            else
                currentFrame--;
            if (currentFrame == totalFrames - 1)
            {
                loop = true;
            }
            if (currentFrame == 0)
                loop = false;
        }
        else
            if (!is_moving)
                currentFrame = 0;
            return;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        int width = Texture.Width / Columns;
        int height = Texture.Height / Rows;
        int row = (int)((float)currentFrame / (float)Columns);
        int column = currentFrame % Columns;
        sourceRectangle = new Rectangle(width * column, height * row, width, height);
        destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
        spriteBatch.Begin();
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        spriteBatch.End();
    }
    }
}
