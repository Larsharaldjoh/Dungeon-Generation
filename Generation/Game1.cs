using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Generation
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;
		Texture2D texture;
		int maxRooms = 2000;//minRoomSizemaxRoomSizeMAP_WIDTHMAP_HEIGHT
		int minRoomSize = 10;
		int maxRoomSize = 30;
		int MAP_WIDTH = GraphicsDeviceManager.DefaultBackBufferWidth;
		int MAP_HEIGHT = GraphicsDeviceManager.DefaultBackBufferHeight;
		int Step = 0;
		int Max = 0;
		int NextStep = 1;
		List<Room> rooms;
		List<Room> toRemove;
		Random randxw;
		Random randyh;
		Rectangle infoBox = new Rectangle(10, 10, 40, 80);
		private const float _delay = 10; // milliseconds
		private float _remainingDelay = _delay;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
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
			IsMouseVisible = true;
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			randyh = new Random();
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			texture = Content.Load<Texture2D>("Block");
			font = Content.Load<SpriteFont>("Roboto");
			rooms  = new List<Room>();
			//rand = new Random((2152 + maxRooms * minRoomSize / maxRoomSize + MAP_HEIGHT - MAP_WIDTH / 3));
			randxw = new Random();
			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
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
			var timer = (float)gameTime.ElapsedGameTime.Milliseconds;

			_remainingDelay -= timer;

			
			
			
			if (_remainingDelay <= 0)
			{	
				// Removes intersecting rooms
				if (toRemove != null && toRemove.Count > 0)
				{
					foreach (Room room in toRemove)
					{
						rooms.Remove(room);
					}
					drawRooms();
				}
				if (rooms.Count <= maxRooms) //Step < maxRooms || true
				{
					NextStep++;
					if (rooms.Count > Max)
						Max = rooms.Count;
				} 
				placeRooms();

				_remainingDelay = _delay;
			}
			// TODO: Add your update logic here

			
			base.Update(gameTime);
		}

		private void placeRooms()
		{
			toRemove = new List<Room>();
			// randomize values for each room
			for (; Step < NextStep; Step++) 
			{
				int w = randxw.Next(minRoomSize, maxRoomSize);
				int h = randyh.Next(minRoomSize, maxRoomSize);
				int x = randxw.Next(MAP_WIDTH - w);
				int y = randyh.Next(MAP_HEIGHT - h);

				// create room with randomized values
				Room newRoom = new Room(x, y, w, h);


				// Checks if newRoom intersects with other rooms in rooms list, and adds them to cleanup list
				foreach (Room otherRoom in rooms) {
					if (newRoom.intersects(otherRoom)) {
						toRemove.Add(otherRoom);
						System.Diagnostics.Debug.WriteLine("newRoom at: " + newRoom.x1.ToString() + "," + newRoom.x2.ToString() + "," + newRoom.y1.ToString() + "," + newRoom.y2.ToString() +
							"\n othRoom at: " + otherRoom.x1.ToString() + "," + otherRoom.x2.ToString() + "," + otherRoom.y1.ToString() + "," + otherRoom.y2.ToString());
					}
					
				}

				// push new room into rooms array
				rooms.Add(newRoom);
			}
		}


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			drawRooms();
			cursorCoordinates();
			spriteBatch.Begin();
			spriteBatch.DrawString(font, Max.ToString(), new Vector2(20, 20), Color.Red);
			spriteBatch.DrawString(font, rooms.Count.ToString(), new Vector2(20, 40), Color.Red);
			spriteBatch.End();

			base.Draw(gameTime);
		}

		private void drawRooms()
		{
			foreach (Room room in rooms)
			{
				spriteBatch.Begin();
				Rectangle temprect;
				temprect = new Rectangle(room.x1, room.y1, room.w, room.h);
				spriteBatch.Draw(texture, temprect, Color.Black);
				spriteBatch.End();
			}
		}

		private void cursorCoordinates()
		{
			Point mousePosition = Mouse.GetState().Position;
			spriteBatch.Begin();
			spriteBatch.DrawString(font, "X:" + mousePosition.X.ToString(), new Vector2(20, 60), Color.Red);
			spriteBatch.DrawString(font, "Y:" + mousePosition.Y.ToString(), new Vector2(20, 75), Color.Red);
			spriteBatch.End();
		}
	}
}
