///This programme is for learn, represent a neural network.
///   Copyright(C) 2016 Hippolyte Nioche
///
///
///This program is free software; you can redistribute it and/or modify
///it under the terms of the GNU General Public License as published by
///the Free Software Foundation;

///This program is distributed in the hope that it will be useful,
///but WITHOUT ANY WARRANTY; without even the implied warranty of
///MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
///GNU General Public License for more details.

///You should have received a copy of the GNU General Public License.
using SFML.Graphics;
using SFML.System;

namespace Life
{
    class food
    {
        /// <summary>
        /// La nourriture repop quand elle a ete manger, c'est tout.
        /// </summary>
        public RectangleShape thesprite
        { get; set; }
  
 
        public food()
        {
 
            thesprite = new RectangleShape(new Vector2f(10, 10));
            thesprite.FillColor = new Color(Color.Green);
            thesprite.Scale = new Vector2f(.75f, .75f);
            thesprite.Position = new Vector2f((float)(Program.rand.NextDouble() * IHM.size.X), (float)(Program.rand.NextDouble() * IHM.size.Y));
        }
        public void repop()
        {
            thesprite.Position = new Vector2f((float)(Program.rand.NextDouble() * (IHM.size.X-50)+50), (float)(Program.rand.NextDouble() * (IHM.size.Y-50)+50));
        }

    }
}
