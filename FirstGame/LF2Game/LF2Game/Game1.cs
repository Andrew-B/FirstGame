using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LF2Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D background,background_top,background_bottom,feet,ground;
        private Rectangle groundRectangle = new Rectangle(0, 300, 800, 100);
        private int distance_from_object = 74;
        private SpriteFont font;
        private int score = 0;
        private LFSprite Player1;
        private int packet,previous_packet = 0;
        private GamePadState old_Game_Pad_State;
        private GamePadState even_Older_Game_Pad_State;
        
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("g1");
            background_top = Content.Load<Texture2D>("back1");
            background_bottom = Content.Load<Texture2D>("w1");
            feet = Content.Load <Texture2D>("Ground");
            ground = Content.Load<Texture2D>("Ground");
            Texture2D texture = Content.Load<Texture2D>("Davis4");
            Player1 = new LFSprite(texture, 6, 8);
            font = Content.Load<SpriteFont>("Score");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Get Control Input

            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            packet = gamePadState.PacketNumber;
            if (Player1.current_state == LFSprite.PlayerState.falling)
            {
                //code here to make fall happen until collision
                if (Player1.location.Y + 1 <= (groundRectangle.Y - distance_from_object))
                {
                    Player1.location.Y += 2;
                }
                else
                {
                    Player1.current_state = LFSprite.PlayerState.stand;
                }
            }
            else
            {
                if ((Player1.current_state == LFSprite.PlayerState.jump || Player1.current_state == LFSprite.PlayerState.run_jump) && Player1.velocity_y < 10)
                {
                    Player1.current_state = LFSprite.PlayerState.jump;
                    Player1.velocity_y += 1;
                    Player1.location.Y -= Player1.velocity_y;
                }
                else if ((Player1.current_state == LFSprite.PlayerState.jump || Player1.current_state == LFSprite.PlayerState.run_jump) && Player1.velocity_y >= 10)
                {
                    //code here
                    //make player state to fall then create another if statement outside of this set
                    Player1.current_state = LFSprite.PlayerState.falling;
                }
                else
                {
                    if (gamePadState.DPad.Left == ButtonState.Pressed)
                    {
                        if (gamePadState.Buttons.A == ButtonState.Pressed)
                        {
                            Player1.current_state = LFSprite.PlayerState.jump;
                            if (gamePadState.Triggers.Right > 0)
                            {
                                Player1.current_state = LFSprite.PlayerState.run_jump;
                            }
                        }
                        if (gamePadState.Buttons.B == ButtonState.Pressed)
                        {
                            Player1.current_state = LFSprite.PlayerState.defend;
                        }
                        else
                        {
                            if (gamePadState.Triggers.Right > 0)
                            {
                                Player1.current_state = LFSprite.PlayerState.run;
                                Player1.location.X -= 3;

                            }
                            else
                            {
                                Player1.current_state = LFSprite.PlayerState.walk;

                                Player1.location.X -= 1.5F;
                            }
                        }
                        Player1.facing = LFSprite.PlayerFace.left;
                    }
                    else if (gamePadState.DPad.Right == ButtonState.Pressed)
                    {
                        if (gamePadState.Buttons.A == ButtonState.Pressed)
                        {
                            Player1.current_state = LFSprite.PlayerState.jump;
                            if (gamePadState.Triggers.Right > 0)
                            {
                                Player1.current_state = LFSprite.PlayerState.run_jump;
                            }
                        }
                        if (gamePadState.Buttons.B == ButtonState.Pressed)
                        {
                            Player1.current_state = LFSprite.PlayerState.defend;
                        }
                        else
                        {
                            if (gamePadState.Triggers.Right > 0)
                            {
                                Player1.current_state = LFSprite.PlayerState.run;
                                Player1.location.X += 3;

                            }
                            else
                            {

                                Player1.current_state = LFSprite.PlayerState.walk;

                                Player1.location.X += 1.5F;
                            }
                        }
                        Player1.facing = LFSprite.PlayerFace.right;
                    }
                    else if (gamePadState.Buttons.B == ButtonState.Pressed)
                    {
                        Player1.current_state = LFSprite.PlayerState.defend;
                    }
                    else if (gamePadState.Buttons.A == ButtonState.Pressed)
                    {
                        Player1.current_state = LFSprite.PlayerState.jump;
                        Player1.velocity_y += 2;
                        Player1.location.Y -= Player1.velocity_y;
                    }
                    else
                    {
                        Player1.current_state = LFSprite.PlayerState.stand;
                    }
                }//end of if state = jump
            }//end of if falling
                Player1.feetRectangle = new Rectangle((int)Player1.location.X + 25,(int)Player1.location.Y + 50, 31, 26);
                even_Older_Game_Pad_State = old_Game_Pad_State;
                old_Game_Pad_State = gamePadState;
                System.Console.WriteLine("The old state is Left is:" + old_Game_Pad_State.DPad.Right + "even older state is:" + even_Older_Game_Pad_State.DPad.Right);
                previous_packet = packet;
            
            
                Player1.Update();
            
                score += 1;
                // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(background_top, new Rectangle(10, 100, 800, 100), Color.White);
            spriteBatch.Draw(background_bottom, new Rectangle(0, 300, 800, 200), Color.Red);
            //spriteBatch.Draw(ground, groundRectangle, Color.Red);
            spriteBatch.Draw(feet, Player1.feetRectangle, Color.Red);
            spriteBatch.DrawString(font, "Score:" + score, new Vector2(100, 100), Color.White);
            spriteBatch.End();
            Player1.Draw(spriteBatch, Player1.location);

            base.Draw(gameTime);
        }
    }
}
