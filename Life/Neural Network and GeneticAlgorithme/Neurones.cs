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

namespace NN
{
    internal class Neurones
    {
        /// <summary>
        /// Cette classe represente la modelisation d'un neurone. 
        /// Ce neurones se compose d'un nombre "NombreInput" d'entrée 
        /// a laquel est assiocié un poid avec un seuil quinous donnera une sortie.
        /// /!\Toute les valeurs sont comprise entre -1 et 1./!\
        /// Ces poids seront pondérer avec les entrée - le seuil.|Resultat=(Entree[0]+Poids[0]... Entree[n]+poids[n])- seuil|
        /// Le resultat sera seuillier dans une fonction F, ici sigmoidBiPolaire.
        /// 
        /// </summary>
        
        static Random rand = new Random();

        public List<double> Poids;
        public double sortie { get; internal set; }
        public int NBin { get; internal set; }
        public double seuil { get; set; }
        /// <summary>
        /// Constructeur, Le nombre d'entrée peut varier entre les couche.
        /// Au débutdu programme les poids et le seuil sont aléatoir par avoir le plus large éventaille
        /// de possibiliter, par la suite il seront echanger pour trouver la combinaison parfaite.
        /// </summary>
        public Neurones(int nombreInput)
        {
            NBin = nombreInput;
            Poids = new List<double>();
            for (int i = 0; i < nombreInput; i++)
                Poids.Add(rand.NextDouble()*2-1);
            seuil= rand.NextDouble()*2-1 ;
        }

       /// <summary>
       /// Constructeur de recopie.
       /// </summary>
       
        public Neurones(Neurones neurones)
        {
            Poids=new List<double>();
            for (int i = 0; i < neurones.Poids.Count; i++)
                Poids.Add(neurones.Poids[i]);
            sortie = neurones.sortie;
            NBin = neurones.NBin;
            seuil = neurones.seuil;
            
    }

        
           /// <summary>
           /// Fonction calculant la sortie de la neurones grace au tableau d'entrée.
           /// Je soustrait le seuil.
           /// Pondère les poids avec l'entree conrespondante.
           /// et la borne grace a la fonction Sigmoïd bipolair.
           /// </summary>
           
        public double Calulate(List<double> entree)
        {
            double Resultat=-seuil;
            for (int i = 0; i < Poids.Count; i++)
                Resultat += Poids[i] * entree[i];
            sortie = BiPolarSigmoid(Resultat, 1);
            return sortie;
        }

        /// <summary>
        /// Fonction Sigmoid. borne A a la valeur maximum P.
        /// </summary>

        public static double BiPolarSigmoid (double a, float p){

        float ap =(float) (-a) / p;
		return (2 / (1 + Math.Exp (ap)) -1);
        }

        
    }
}