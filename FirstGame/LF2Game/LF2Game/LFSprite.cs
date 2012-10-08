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
        private int row = 0;
        //private int column = 0;
        private int [] frames = new int[] {0,1,2,3,4,5,6,7,8};
        private Boolean up { get; set; }
        public enum PlayerFace { right, left };
        public PlayerFace facing = PlayerFace.right;
        public PlayerFace oldState = PlayerFace.right;
        public enum PlayerState { walk, run, jump, hurt, attack, item, stand }
        public PlayerState current_state = PlayerState.stand;
        public Vector2 location = new Vector2(400, 200);
        


    public LFSprite(Texture2D texture, int rows, int columns)
    {
        Texture = texture;
        Rows = rows;
        Columns = columns;
        currentFrame = 0;
        totalFrames = 8;
        int width = Texture.Width / Columns;
        int height = Texture.Height / Rows;
        int row = (int)((float)currentFrame / (float)Columns);
        int column = currentFrame % Columns;
        System.Console.WriteLine(texture.Width);
        System.Console.WriteLine(texture.Height);
    }

    public void Update()
    {
        timer++;
        if (timer % 8 == 0)
        {
            if (current_state == PlayerState.run)
            {
                if (facing == PlayerFace.right)
                {
                    if (oldState == PlayerFace.right)
                    {

                        currentFrame++;
                        if (currentFrame == totalFrames)
                            currentFrame = 0;
                    }
                    else
                    {
                        oldState = PlayerFace.right;
                        currentFrame = 1;
                    }
                    row = 2;
                }
                else
                {
                    if (oldState == PlayerFace.left)
                    {

                        currentFrame++;
                        if (currentFrame == totalFrames)
                            currentFrame = 0;
                    }
                    else
                    {
                        currentFrame = 1;
                        oldState = PlayerFace.left;
                    }
                    row = 3;
                    
                }
            }
            else
            {
                if (current_state == PlayerState.stand)
                {
                    if (facing == PlayerFace.right)
                    {
                        currentFrame = 0;
                        row = 0;
                    }
                    else
                    {
                        row = 1;
                        currentFrame = 7;
                    }
                }
                else
                {
                    if (facing == PlayerFace.right)
                    {
                        row = 0;
                        if (oldState == PlayerFace.right)
                        {

                            currentFrame++;
                            if (currentFrame == totalFrames)
                                currentFrame = 0;
                        }
                        else
                        {
                            currentFrame = 1;
                        }
                        oldState = PlayerFace.right;
                    }
                    else if (facing == PlayerFace.left)
                    {
                        row = 1;
                        if (oldState == PlayerFace.left)
                        {

                            currentFrame++;
                            if (currentFrame == totalFrames)
                                currentFrame = 0;
                        }
                        else
                        {
                            currentFrame = 1;
                        }
                        oldState = PlayerFace.left;

                    }
                }
            }

           
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        int width = Texture.Width / Columns;
        int height = Texture.Height / Rows;
        int column = currentFrame;
        sourceRectangle = new Rectangle(width * column, height * row, width, height);
        destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
        spriteBatch.Begin();
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        spriteBatch.End();
    }
    }
}
