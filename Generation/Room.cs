using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Generation
{
	class Room
	{
		// these values hold grid coordinates for each corner of the room
		public int x1;
		public int x2;
		public int y1;
		public int y2;

		// width and height of room in terms of grid
		public int w;
		public int h;

		// center point of the room
		public Point center;


		// constructor for creating new rooms
		public Room(int x, int y, int w, int h) {

			x1 = x;
			x2 = x + w;
			y1 = y;
			y2 = y + h;
			/*x = x * Main.TILE_WIDTH;
			y = y * Main.TILE_HEIGHT;*/
			this.w = w;
			this.h = h;
			center = new Point((int)Math.Floor((decimal)((x1 + x2) / 2)),
				(int)Math.Floor((decimal)((y1 + y2) / 2)));
		}

		// return true if this room intersects provided room
		public bool intersects(Room room) 
		{
			return (x1 <= room.x2 && x2 >= room.x1 &&
				y1 <= room.y2 && y2 >= room.y1);
		}
	}
}
