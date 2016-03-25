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
using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Life
{
    class Sensor
    {
        /// <summary>
        /// Les capteurs de l'entiter.
        /// </summary>
        public RectangleShape[] line { get; internal set; }
        private float startlenght;
        public Sensor()
        {
            startlenght = 500;

            line = new RectangleShape[8];
            for (int i = 0; i < line.Length; i++)
            {
                line[i] = new RectangleShape(new Vector2f(startlenght, 1));
                line[i].FillColor = new Color((byte)(i * (255 / line.Length)), (byte)(i * (255 / line.Length)), 255);
                line[i].Rotation = i * (360 / line.Length);
            }

        }
        public void setPosition(Vector2f origine, float rot, float radius)
        {

            for (int i = 0; i < line.Length; i++)
            {

                line[i].Position = new Vector2f(origine.X+(float)Math.Cos((line[i].Rotation)* 0.0174533) *radius, origine.Y + (float)Math.Sin((line[i].Rotation)* 0.0174533) *radius);
            }



        }
        public List<float> getValue(food theFood)
        {
            List<float> value = new List<float>();


            for (int i = 0; i < line.Length; i++)
            {
                line[i].Size = new Vector2f(5, 1);
                if (line[i].GetGlobalBounds().Left < 0 || line[i].GetGlobalBounds().Top < 0 || line[i].GetGlobalBounds().Left > 1280 || line[i].GetGlobalBounds().Top > 720)
                    value.Add(1);
                else
                    value.Add(0);


            }
            

        
            for (int i = 0; i < line.Length; i++)
            {
                line[i].Size = new Vector2f(startlenght, 1);
                if (line[i].GetGlobalBounds().Intersects(theFood.thesprite.GetGlobalBounds()))
                    value.Add(1);
                else
                    value.Add(0);
                /*while (line[i].GetGlobalBounds().Intersects(theFood.thesprite.GetGlobalBounds()) && line[i].Size.X >= 1)
                {
                    line[i].Size -= new Vector2f(1, 0);


                }
                value.Add(((startlenght - line[i].Size.X) / startlenght) * cst);*/
            }
            return value;
        }
    }
}
