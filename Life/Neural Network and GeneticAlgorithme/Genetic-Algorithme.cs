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
    class Genetic_Algorithme
    {
        /// <summary>
        /// L'algorithme genetique lui permet d'interchanger les poids present dans les neurones.
        /// Il est composée: 
        /// -D'un nombre d'entrée = a celui du réseau des neurones.
        /// -Des réseau de neurones a tester.
        /// -D'un réseau de neurones best, c'est une copie du meilleur score, il est réutilisée tant qu'il n'a pas été batu.
        /// - 2 Pourcentage, Change to mutate, percentOfPopulationSave.
        ///                 ►Qui correspond au pourcentage qu'un individue mute
        ///                 ►Qui correspond au pourcentage de personne qui sont sauvegarder a la fin de la generation.
        /// -IndiceSauvegarder est une list d'entier representant les indice des joueurs sauvegarder avec un pourcentage 
        /// en fonction du score du réseau de neurones.
        /// -Un entier ncc qui est egale au nombre de couche cachée present dans les réseau.
        /// -une auutre entier qui correspond au nombre de neurones par couche cachée.
        /// </summary>
        private int nbEntree, nbSortie;
        private List<ReseauDeNeurones> lesreseau;
        static Random rand = new Random();
        ReseauDeNeurones Best;
        private float chancetoMutate, percentOfpopulationceserved;
        List<int> indiceSauvrder = new List<int>();
        List<ReseauDeNeurones> Mutatedindividue;
        int ncc,nbneurperCC;
        static public string best {get; internal set;}

        /// <summary>
        /// COnstructeur initialisant le nombre d'entrée et de sortie.
        /// et initialise aussi la liste de réseau de neurones a traiter.
        /// 
        /// </summary>
        /// <param name="nbE"></param>
        /// <param name="nbS"></param>
        /// <param name="neur"></param>
        public Genetic_Algorithme(int nbE,int nbS, List<ReseauDeNeurones> neur)
        {
            nbEntree = nbE;
            nbSortie = nbS;
            lesreseau = neur;
            Best = new ReseauDeNeurones(lesreseau[0]);
            chancetoMutate = .30f;
            percentOfpopulationceserved = 0.15f;
            ncc = lesreseau[0].nCC;
            nbneurperCC = lesreseau[0].nombreDeneuronesParCoucheCache;
        }
 
        /// <summary>
        /// Cette méthode evalue les individue apres les avoir tester.
        /// NbPopSave corresponds a la population sauvegarder par rapport a la taille de la populutaion total.
        /// Je remet a zero les liste d'indice sauvegarder et la liste des individue mutée.
        /// J'apelle la methode qui me trie les réseau en fonction de leurs score.
        /// Je met a jour le best reseau. si il a ete battue je le met a jour sinon, je le réimplante dans la list des réseau.
        /// Je met a jour les indice sauvegarder et les individue mutée.
        /// </summary>
        public void EvaluationDesIndividus()
        {
            int nbpopsave = (int)(lesreseau.Count * percentOfpopulationceserved);
            indiceSauvrder = new List<int>();
            Mutatedindividue = new List<ReseauDeNeurones>();
            TriDesRéseauEnFonctionDeLeurScore();

            if (lesreseau[lesreseau.Count-1].score > Best.score)
            {
                Best = new ReseauDeNeurones(lesreseau[lesreseau.Count-1]);
                System.Console.WriteLine("|Last Best score =" + Best.score.ToString() + " A la generation : " + Life.IHM.generation.ToString());
            }
            else
                lesreseau[lesreseau.Count - 2] =new ReseauDeNeurones(Best);
            for (int i = lesreseau.Count - 1; i >= nbpopsave; i--)
                    for (int j = 0; j < lesreseau[i].score/100; j++)
                {
                    indiceSauvrder.Add(i);
                }
            for (int i = 0; i < Mutatedindividue.Count; i++)
            {
                lesreseau[i] = Mutatedindividue[i];
                indiceSauvrder.Add(i);
            }
            
        }
        private void TriDesRéseauEnFonctionDeLeurScore()
        {
            double[] scoreRepresent = new double[lesreseau.Count];
            for (int i = 0; i < lesreseau.Count; i++)
            {
                scoreRepresent[i] = lesreseau[i].score;
                if ((rand.NextDouble() < chancetoMutate))
                {
                    Mutatedindividue.Add(mutatedindividue(nbEntree, nbSortie));
                    
                }
            }
            tri_selection(scoreRepresent,scoreRepresent.Length);
            

        }
        /// <summary>
        /// Ici les individue sont crée a partire des meilleur sauvegarder.
        /// </summary>
        public void CreatioDesnouveauInddividues()
        {
            int nbIndividuACree =(int) ((1 - percentOfpopulationceserved)* lesreseau.Count);
            Couches[,] Couchecacher = new Couches[nbIndividuACree,lesreseau[0].getnCC()];
            Couches[] Couchesortie = new Couches[nbIndividuACree];

            //lesreseau[0] = best;
            //indiceSauvrder.Add(0);

            for (int i = 0; i < nbIndividuACree; i++)
            {

                int mother = indiceSauvrder[rand.Next(indiceSauvrder.Count - 1)];
                int father;
                while((father=indiceSauvrder[rand.Next(indiceSauvrder.Count - 1)])==mother);
                for (int j = 0; j < lesreseau[i].getnCC(); j++)
                {
                    Couchecacher[i, j] = new Couches(lesreseau[i].nombreDeneuronesParCoucheCache, nbEntree);
                    if (lesreseau[mother].getnCC() >= 1)
                        for (int k = 0; k < Couchecacher[i, j].NumOfNeurones; k++)
                            Couchecacher[i, j].someNeurones[k] = creationdunenfant(lesreseau[mother].Couchecachee[j].someNeurones[k], lesreseau[father].Couchecachee[j].someNeurones[k]);

                }
            
            
                Couchesortie[i] = new Couches(nbSortie, nbEntree);
                
                for (int j = 0; j < Couchesortie[i].NumOfNeurones; j++)
                    Couchesortie[i].someNeurones[j] = creationdunenfant(lesreseau[mother].CoucheSortie.someNeurones[j], lesreseau[father].CoucheSortie.someNeurones[j]);


            }

            InsertionDesNouveauIndividue(Couchecacher, Couchesortie);

        }
        /// <summary>
        /// Crée un enfant en fonction de deux neurones r1 et r2.
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        Neurones creationdunenfant(Neurones r1, Neurones r2)
        {
            Neurones n=new Neurones(r1.NBin);
            double[] Poids;
            n.Poids.Clear();
           

            if (rand.NextDouble() < .5f)
            {
                Poids = new double[r1.Poids.Count];
                r1.Poids.CopyTo(Poids);
            }
            else
            {
                Poids = new double[r2.Poids.Count];
                r2.Poids.CopyTo(Poids);
            }
            for (int i = 0; i < Poids.Length; i++)
                n.Poids.Add(Poids[i]);

            return n;

        }
        public void InsertionDesNouveauIndividue( Couches[,] couchecacher, Couches[] couchesortie)
        {
           
            for (int i = 0; i < couchesortie.Length; i++)
            {
                lesreseau[i].CoucheSortie = couchesortie[i];
                for (int j = 0; j < lesreseau[i].getnCC(); j++)
                    lesreseau[i].Couchecachee[j]=couchecacher[i,j];
            }

        }
        /**
        *   Trie le tableau donné selon l'algorithme de tri par sélection
        *
        *   int tab[] :: tableau à trier
        *   int taille :: taille du tableau
        *
        *   return void
        **/
        void tri_selection(double[] tab, int taille)
        {
            int indice_max;

            // à chaque tour de boucle, on va déplacer le plus grand élément
            // vers la fin du tableau, on diminue donc à chaque fois sa taille
            // car le dernier élément est obligatoirement correctement
            // placé (et n'a donc plus besoin d'être parcouru/déplacé)

            for (; taille > 1; taille--) // tant qu'il reste des éléments non triés
            {
                indice_max = max(tab, taille);

                echanger(tab, taille - 1, indice_max); // on échange le dernier élément avec le plus grand
            }
        }


        void echanger(double[] tab, int x, int y)
        {
            double tmp;

            tmp = tab[x];
            tab[x] = tab[y];
            tab[y] = tmp;
            ReseauDeNeurones tempo = lesreseau[x];
            lesreseau[x] = lesreseau[y];
            lesreseau[y] = tempo;
        }
      
        private ReseauDeNeurones mutatedindividue(int nbe,int nbs)
        {
            ReseauDeNeurones retour=new ReseauDeNeurones(nbEntree, nbSortie, ncc,nbneurperCC);
            return retour;
        }
        /**
*   Renvoie l'indice du plus grand élément du tableau
*
*   int tab[] :: tableau dans lequel on effectue la recherche
*   int taille :: taille du tableau
*
*   return int l'indice du plus grand élément
**/
        int max(double[] tab, int taille)
        {
            // on considère que le plus grand élément est le premier
            int i = 0, indice_max = 0;

            while (i < taille)
            {
                if (tab[i] > tab[indice_max])
                    indice_max = i;
                i++;
            }

            return indice_max;
        }

    }
}
