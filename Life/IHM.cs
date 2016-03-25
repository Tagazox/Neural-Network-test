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
using System.Threading;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Life
{
    class IHM
    {
        /// <summary>
        /// IHM
        /// </summary>
        public static Vector2f size = new Vector2f(1280, 720);
        RenderWindow thewindow;
        NN.LiaisonNN_Jeux liaison;
        Entiter[] lentiter;
        Mutex[] mtx;
        food[] thefood;
        public static int generation = 0;
        bool drawLines = false;
        int nbrDeFood=50;
        Thread t ;
        Thread[] t1;
        public IHM()
        {
            
            mtx = new Mutex[4];
             t = new Thread(isalldead);
             t1 = new Thread[4];
            for (int i = 0; i < 4; i++)
            {
                t1[i] = new Thread(manager);
                mtx[i] = new Mutex();
            } 
            liaison = new NN.LiaisonNN_Jeux();
            thefood = new food[nbrDeFood];
            for (int i = 0; i < nbrDeFood; i++)
                thefood[i] = new food();
            lentiter = new Entiter[liaison.theIa.Count];
            for (int i = 0; i < lentiter.Length; i++)
                lentiter[i] = new Entiter(thefood,liaison.theIa[i]);
            thewindow = new RenderWindow(new VideoMode((uint)size.X, (uint)size.Y), "Fish");
             thewindow.SetFramerateLimit(60);
            thewindow.Closed += OnClose;
            thewindow.Resized += Onresize;
            
            t.Start();
            for (int i = 0; i < 4; i++)
                t1[i].Start();
            Loopgame();
        }

        private void manager()
        {
            
            while (thewindow.IsOpen)
            {
                mtx[0].WaitOne(10000,false);
                for (int i = 0; i < lentiter.Length/4; i++)
                    if(lentiter[i].isalive)
                    lentiter[i].actiondelentiter();
                mtx[0].ReleaseMutex();
                mtx[1].WaitOne(10000, false);
                for (int i = lentiter.Length / 4 ; i < 2*lentiter.Length/4 ; i++)
                    if (lentiter[i].isalive)
                        lentiter[i].actiondelentiter();
                mtx[1].ReleaseMutex();
                mtx[2].WaitOne(10000, false);
                for (int i = 2 * lentiter.Length / 4; i <  3*lentiter.Length/4 ; i++)
                    if (lentiter[i].isalive)
                        lentiter[i].actiondelentiter();
                mtx[2].ReleaseMutex();
                mtx[3].WaitOne(10000, false);
                for (int i = 3 * lentiter.Length / 4; i < lentiter.Length; i++)
                    if (lentiter[i].isalive)
                        lentiter[i].actiondelentiter();
                mtx[3].ReleaseMutex();
                Thread.Sleep(Program.sleep);
            }
            Thread.CurrentThread.Abort();
        }

        private void Loopgame()
        {
           
            while (thewindow.IsOpen)
            {
                // Process events

                if(Keyboard.IsKeyPressed(Keyboard.Key.A))
                    Program.sleep++;
                else if(Keyboard.IsKeyPressed(Keyboard.Key.D))
                    if (Program.sleep >= 1)
                    Program.sleep--;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                    drawLines = !drawLines;
                thewindow.DispatchEvents();
                


                thewindow.SetTitle("| Generation= " + generation.ToString()+NN.Genetic_Algorithme.best);
                 
               
                // Clear screen
                thewindow.Clear(new Color(Color.Black));

                // Draw the sprite
                for (int i = 0; i < lentiter.Length; i++)
                    if (lentiter[i].isalive)
                    {
                        thewindow.Draw(lentiter[i].thesprite);
                        if (drawLines)
                            for (int j = 0; j < lentiter[i].line.Length; j++)
                                thewindow.Draw(lentiter[i].line[j]);
                    }
                

                for (int i = 0; i < thefood.Length; i++)
                      thewindow.Draw(thefood[i].thesprite);
                
                // Update the window
                thewindow.Display();
            }
            
        }
        private void Onresize(object sender, EventArgs e)
        {
               //RenderWindow window = (RenderWindow)sender;
              // size = new Vector2f(window.Size.X,window.Size.Y); 
        }
        private void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }
        private void isalldead()
        { 
            while (thewindow.IsOpen)
            {
                bool isallalive = true;
                for (int i = 0; i < lentiter.Length; i++)
                    if (lentiter[i].isalive)
                        isallalive = false;

                if (isallalive)
                {
                    liaison.iterate();
                    generation++;
                    for (int i = 0; i < lentiter.Length; i++)
                        lentiter[i] = new Entiter(thefood, liaison.theIa[i]);
                }
                Thread.Sleep(20);
            }
            Thread.CurrentThread.Abort();
        }
    }

}
    