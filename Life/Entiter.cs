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
    class Entiter:Sensor
    {
        /// <summary>
        /// Cette classe represente le point blanc, c'est le joueur.
        /// Elle possède :
        /// ►Un rond.
        /// ►Les nourritures.
        /// ►Si il est en vie.
        /// ►La position de la frame précédente.
        /// ►Un compteur
        /// ►Un cerveau modeliser par un cerveau de neurones.
        /// ►La distnce parcourue depuis la derniere nouriture manger.
        /// </summary>

        public CircleShape thesprite
        { get;set;}
        private food[] food;
        public bool isalive
        { get; internal set; }
        Vector2f anciennepos;
        int counter = 0;
        NN.ReseauDeNeurones theBrain;
        private float disparcour = 0;

        /// <summary>
        /// COnstructeur qui met a jour les varaibles
        /// </summary>
        /// <param name="theentiter"></param>
        /// <param name="ia"></param>
        public Entiter(food[] theentiter,NN.ReseauDeNeurones ia):base()
        {
            theBrain = ia;
            isalive = true;
            //thread = new Thread(actiondelentiter);
            food = theentiter;
            theBrain.score = 0;
            thesprite = new CircleShape(1);
            thesprite.Rotation = (float)Program.rand.NextDouble() * 360;

            anciennepos = new  Vector2f(IHM.size.X/2, IHM.size.Y/2);
            thesprite.Position = anciennepos;
            thesprite.Origin = new Vector2f(thesprite.Radius, thesprite.Radius);
           // thread.Start();

        }
        //Appelle les fonction gerant l'entiter.
        public void actiondelentiter()
        {
              gestionDesEntree();
                mouvement();
            testdevie();
        }
        //Retourne l'indice de la nourriture la plus proche.
        int quelestlefoodleplusproche()
        {
            int retour = 0;
            float distmin = 9999999;
            for (int i = 0; i < food.Length; i++)
            {
                float dx = (food[i].thesprite.Position.X - thesprite.Position.X);
                float dy = (food[i].thesprite.Position.Y - thesprite.Position.Y) ;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);
                if (distance<distmin)
                {
                    distmin = distance;
                    retour = i;
                }
            }

            return retour;
        }
        /// <summary>
        /// Calcule les entrée pour avoir les sortie du réseau a jour.
        /// </summary>
        private void gestionDesEntree()
        {
            
            List<double> entree = new List<double>();
            setPosition(thesprite.Position, thesprite.Rotation, thesprite.Radius);
            int prochef = quelestlefoodleplusproche();
            float dx = food[prochef].thesprite.Position.X - thesprite.Position.X;
            float dy = food[prochef].thesprite.Position.Y - thesprite.Position.Y;
            
            float angle = thesprite.Rotation - (float)Math.Atan2(dy, dx);
            if (angle > Math.PI) angle -= (float)(2 * Math.PI);
            double distance = Math.Sqrt(dx * dx + dy * dy)/ Math.Sqrt(IHM.size.X * IHM.size.X + IHM.size.Y* IHM.size.Y);
            List<float> sensorvalue = getValue(food[prochef]);
            angle/= 360;
            for (int i = 0; i < sensorvalue.Count; i++)
                entree.Add(sensorvalue[i]);
       //     entree.Add(distance);
            entree.Add(angle);
            entree.Add(distance);
            entree.Add(1);
            theBrain.CalculateCouches(entree);
        }
        //Test la colision et la distance parcourue.

        void testdevie()
        {
            for (int i = 0; i < food.Length; i++)
                if (thesprite.GetGlobalBounds().Intersects(food[i].thesprite.GetGlobalBounds()))
                {
                    food[i].repop();
                    disparcour = 0;
                    theBrain.score += 100;
                    if(thesprite.Radius<5)
                    thesprite.Radius+=.5f;
                    thesprite.FillColor=new Color(138,0,0);

                }

            if (disparcour > 1000)
            {
                isalive = false;
                
            }
            if (!new FloatRect(0, 0, IHM.size.X, IHM.size.Y).Intersects(thesprite.GetGlobalBounds())||counter>50)
            {
                isalive = false;
                theBrain.score /=3;
            }
            }
        
        //Bouge en fonction des sorties.
        private void mouvement()
        {
            float sdx =  (float)(theBrain.getNeuronesSortieNumeroI(0).sortie)*3;
            float sdy = (float)(theBrain.getNeuronesSortieNumeroI(1).sortie)*3;
            float factor= (float)theBrain.getNeuronesSortieNumeroI(2).sortie * 10;
            if (sdx < -1)
                sdx = -1;
            else if (sdx > 1)
                sdx = 1;
            else
                sdx = 0;


            if (sdy < -1)
                sdy = -1;
            else if (sdy > 1)
                sdy = 1;
            else
                sdy = 0;

             thesprite.Position += new Vector2f((float)(sdx),(float)(sdy)) *factor;
            if (thesprite.Position == anciennepos)
                counter++;
            disparcour += (float)Math.Sqrt(sdx * sdx + sdy * sdy) +1;
            anciennepos = thesprite.Position;
            setPosition(thesprite.Position, thesprite.Rotation,thesprite.Radius);
        }



    }

    }

