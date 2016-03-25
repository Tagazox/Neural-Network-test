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
    internal class ReseauDeNeurones
    {
        /// <summary>
        /// Le reseau de neurones permet d'interconnecter et de gerer les neurones. 
        /// Un réseau de neurones est composée de plusieur couche de plusieurs Neurones.
        /// Une couche de sortie permettant de recuperrer la sortie des neurones.
        /// Une neurones par sortie est nessécaire.
        /// Un nombre N de couche caché N[0;+Infini] Elle permette de rajouter de la précision au sortie,
        /// mais plus il y a de couche caché plus l'algorithme d'apprentissage va etre long...
        /// plus de poid = plus de possibiliter.
        /// /!\/!\/!\nCC= Nombre de couche cahée/!\/!\/!\
        /// Le score permet de savoir si le réseau de neurones s'est bien comporter auquel le score sera élever
        /// ou si il a été faible auquel cas on peut avoir un score négatif.
        /// </summary>


        public Couches CoucheSortie { get; internal set; }
        public Couches[] Couchecachee { get; internal set; }
        public int nCC, Nentree, Nsortie;

        public float score { get; set; }
        public int nombreDeneuronesParCoucheCache { get; set; }
        public int getnCC()
        { return nCC; }
        /// <summary>
        /// Ici le contructeur définie les variable Nombre d'entrée de l'enssamble.NbEntrée>=1
        /// Le nombre de sortie>=1;
        /// Le nombre de couche cachée voulu.
        /// Le nombre de neurones par couche cachée il doit etre >= au nombre d'entrée.
        /// </summary>
        /// <param name="NbEntree"></param>
        /// <param name="NbSortie"></param>
        /// <param name="NbCoucheCachee"></param>
        /// <param name="nombreDeneuronesParCoucheCachée"></param>
        public ReseauDeNeurones(int NbEntree, int NbSortie, int NbCoucheCachee,int nombreDeneuronesParCoucheCachée= 5)
        {
            score = 0;
            nCC = NbCoucheCachee;
            Nentree = NbEntree;
            Nsortie = NbSortie;
            nombreDeneuronesParCoucheCache = nombreDeneuronesParCoucheCachée;
            Couchecachee = new Couches[NbCoucheCachee];
            if (NbCoucheCachee >= 1)
            {
                for (int i = 0; i < NbCoucheCachee; i++)
                    Couchecachee[i] = new Couches(nombreDeneuronesParCoucheCachée, NbEntree);
                CoucheSortie = new Couches(NbSortie, nombreDeneuronesParCoucheCachée);
            }
            else
                CoucheSortie = new Couches(NbSortie, nombreDeneuronesParCoucheCachée);
        }
        //Constructeur de recpie.
        public ReseauDeNeurones(ReseauDeNeurones reseauDeNeurones)
        {

            score = reseauDeNeurones.score;
            nCC = reseauDeNeurones.getnCC() ;
            Nentree = reseauDeNeurones.Nentree;
            Nsortie = reseauDeNeurones.Nsortie;
            nombreDeneuronesParCoucheCache = reseauDeNeurones.nombreDeneuronesParCoucheCache;
            Couchecachee = new Couches[nCC];

            for (int i = 0; i < nCC; i++)
                Couchecachee[i] = new Couches(reseauDeNeurones.Couchecachee[i]);
            CoucheSortie = new Couches(reseauDeNeurones.CoucheSortie);
        }
        //Cette fonction calcule toute les couche a partire du tableau d'entre passer en parametre.
        public void CalculateCouches(List<double> entree)
        {
            //I il n'y a pas de couche cachée la couche de sortie recoit les entrée en parametre.
            if (nCC == 0)
            {
                CoucheSortie.CalculateNeurones(entree);
            }
            else if (nCC >= 1)// La couche cachée 0 recoit toujours Toute les entrée et la couche de sortie recoit elle le dernier tableau de sortie des couche cachée.
            {
                Couchecachee[0].CalculateNeurones(entree);
                for (int i = 1; i < nCC; i++)
                {
                    Couchecachee[i].CalculateNeurones(Couchecachee[i - 1].getSortieNeurones());
                }
                CoucheSortie.CalculateNeurones(Couchecachee[nCC - 1].getSortieNeurones());

            }
        }
        /// <summary>
        /// Retourne le Neurones de sortie numero i.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Neurones getNeuronesSortieNumeroI(int i)
        {
            return CoucheSortie.someNeurones[i];
        }
        
    }
}