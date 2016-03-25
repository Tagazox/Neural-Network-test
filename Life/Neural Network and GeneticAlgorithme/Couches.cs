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
using System.Collections.Generic;

namespace NN
{
    internal class Couches
    {
        /// <summary>
        /// Une couche est composée de pluesieur neurones elle permet de 
        /// les rasambler de facon logique les Neurones 
        /// Representation d'une couche:
        /// </summary>

            //Les neurones de la couche.
        public Neurones[] someNeurones { get; set; }
        //Nombre de neurones present dans la couche.
        public int NumOfNeurones { get; internal set; }
        //Nombre d'entree que peut recevoir chaque neurones
        int numberofInputparNeurones;
        //COnstructeur, premet l'initialisation de toure les variables.
        public Couches(int nbNeurones,int numberOfInputperNeurones)
        {
            NumOfNeurones = nbNeurones;
            numberofInputparNeurones = numberOfInputperNeurones;
            someNeurones = new Neurones[NumOfNeurones];
            for (int i = 0; i < NumOfNeurones; i++)
                someNeurones[i] = new Neurones(numberOfInputperNeurones);
        }
        //Constructeur de recopie.
        public Couches(Couches couches)
        {
            NumOfNeurones = couches.NumOfNeurones;
            numberofInputparNeurones = couches.numberofInputparNeurones;
            someNeurones = new Neurones[NumOfNeurones];
            for (int i = 0; i < NumOfNeurones; i++)
                someNeurones[i] = new Neurones(couches.someNeurones[i]);
        }
        //Calcule la sortie pour chaque Neurones.
        public void CalculateNeurones(List<double> entree)
        {
            
            for (int i = 0; i < NumOfNeurones; i++)
            someNeurones[i].Calulate(entree);
            
        }
        //Retourne un tableau representant les sortie des neurones.
        public List<double> getSortieNeurones()
        {
            List<double> sortie =new List<double>();
            for (int i = 0; i < NumOfNeurones; i++)
                sortie.Add( someNeurones[i].sortie);
            return sortie;
        }
    }
}