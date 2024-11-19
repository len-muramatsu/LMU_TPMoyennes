using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HNI_TPmoyennes
{

    public class Eleve
    {
        public string nom { get; set; }
        public string prenom { get; set; }
        public Dictionary<string, List<float>> Notes { get; set; }
        public List<float> MoyennesdelEleve { get; set; }
        public List<string> Matieres { get; set; }
     
        public Eleve(string nomDelEleve, string prenomDelEleve, List<string> matieres)
        {
            nom = nomDelEleve;
            prenom = prenomDelEleve;
            Notes = new Dictionary<string, List<float>>();
            Matieres = matieres; 
            MoyennesdelEleve = new List<float>();
        }

        public void ajouterNote(Note note)            
        {
            if (note.matiere < 0 || note.matiere >= Matieres.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(note.matiere), "L'index de la matière est hors limites.");
            }

            string nomMatiere = Matieres[note.matiere];
            if (!Notes.ContainsKey(nomMatiere))
            {
                Notes[nomMatiere] = new List<float>();
            }
            if (Notes.Count <200)
            {                
                Notes[nomMatiere].Add(note.note);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(Notes), "L'élève a déjà 200 notes.");
            }
            
        }

        public float moyenneMatiere(int matiere)
        {
            if (matiere < 0 || matiere >= 11)
            {
                throw new ArgumentOutOfRangeException(nameof(matiere), "L'index de la matière est hors limites {11}.");
            }
            string nomMatiere = Matieres[matiere];

            if (Notes.ContainsKey(nomMatiere) && Notes[nomMatiere].Count > 0)
            {
                float moyennearrondie = (float)Math.Round(Notes[nomMatiere].Average(), 2);
                                
                return moyennearrondie;
            }
            return 0;
        }

        public float moyenneGeneral()
        {
            for (int i = 0; i < Matieres.Count; i++)
            {
                MoyennesdelEleve.Add(moyenneMatiere(i));               
            }
            float moyennefinale = MoyennesdelEleve.Average();

            return (float)Math.Round(moyennefinale, 2);            
        }
    }

    public class Classe
    {
        public string nomClasse { get; set; }
        public List<Eleve> eleves { get; set; }
        public List<string> matieres { get; set; }
        public Dictionary<string, float> Moyennes { get; set; }

        public List<float> MoyennesdelaClasse = []; 
        public List<float> MoyennesdelaMatiere = [];
        public float MoyennedelaMatiere = new(); 
        public float MoyennedelaClasse = new(); 

        public Classe(string nomdelaclasse)
        {
            nomClasse = nomdelaclasse;
            eleves = new List<Eleve>();
            matieres = new List<string>();
            Moyennes = new Dictionary<string, float>();

        }

        public void ajouterEleve(string prenom, string nom)
        {
            if (eleves.Count < 30)
            {
                eleves.Add(new Eleve(nom, prenom, matieres));
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(eleves), "Nombre d'élèves maximum atteint {30}.");
            }
            
        }

        public void ajouterMatiere(string matiere)
        {
            if (matieres.Count < 10)
            {
                matieres.Add(matiere);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(matieres), "Nombre de matières maximum atteint {10}.");
            }            
        }

        public float moyenneMatiere(int matiere) 
        {
            if (matiere < 0 || matiere >= 11)
            {
                throw new ArgumentOutOfRangeException(nameof(matiere), "L'index de la matière est hors limites {11}.");
            }
            string nomMatiere = matieres[matiere];

            //Initialisation de la liste des moyennes d'une matière.
            for (int i = 0; i < eleves.Count; i++)
            {
                eleves[i].moyenneGeneral();
                MoyennesdelaMatiere.Add(eleves[i].MoyennesdelEleve[matiere]);
            }           
            //Calcul de la moyenne.
            float MoyennedelaMatiere = (float)Math.Round(MoyennesdelaMatiere.Average(), 2);

            if (!Moyennes.ContainsKey(nomMatiere))
            {
                Moyennes[nomMatiere] = new float();
            }
            Moyennes[nomMatiere] = (MoyennedelaMatiere);

            return MoyennedelaMatiere;            
        }
        public float moyenneGeneral()
        {
            //Calcul de toutes les moyennes.
            for (int i =0; i < matieres.Count;i++)
            {
                MoyennesdelaClasse.Add(moyenneMatiere(i));               
            }
            //Calcul de la moyenne.
            float MoyennedelaClasse = (float)Math.Round(MoyennesdelaClasse.Average(), 2);
            
            Moyennes["Moyenne Générale"] = new float();
            Moyennes["Moyenne Générale"] = MoyennedelaClasse;

            return MoyennedelaClasse;
        }

    }
}
