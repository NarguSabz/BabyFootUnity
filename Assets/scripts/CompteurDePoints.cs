using UnityEngine;
using UnityEngine.UI;
/**
* Cette classe est un observateur de la classe zonneArivee qui ecoute les deux zones d arrivee et comptabilise les points.
* Date: 25 fevrier 2022
* Auteur: Sabbagh Ziarani, Narges
*/
public class CompteurDePoints : MonoBehaviour
{
    private ZoneArrive zoneBleu; // la zone d'arrivée bleu qu'on observe
    private ZoneArrive zoneRouge; // la zone d'arrivée rouge qu'on observe
    private Text _nbreDePointsTextBleu; // Le champs de text de points de l equipe bleu qu'on doit mettre à jour
    private Text _nbreDePointsTextRouge; // Le champs de text de points de l equipe rouge qu'on doit mettre à jour
    private int _nombreDePointsRouge; // Les points de l equipe rouge qu'on doit mettre à jour
    private int _nombreDePointsBleu;// Les points de l equipe bleu qu'on doit mettre à jour

    // Start is called before the first frame update
    void Start()
    {
        //on cherche le composant ZoneArrive du butbleu
        zoneBleu = GameObject.Find("butBleu").GetComponent<ZoneArrive>();
        //on cherche le composant ZoneArrive du butrouge
        zoneRouge = GameObject.Find("butRouge").GetComponent<ZoneArrive>();

        //au tout debut le nombre de points des deux equipes est 0 
        _nombreDePointsBleu = 0;
        _nombreDePointsRouge = 0;
        //on cherche les composants texts des texts pointsEquipeBleu et pointsEquipeRouge
        _nbreDePointsTextBleu = GameObject.Find("Canvas/pointsEquipeBleu").GetComponent<Text>();
        _nbreDePointsTextRouge = GameObject.Find("Canvas/pointsEquipeRouge").GetComponent<Text>();

        //on inscris le compteur des points rouge a l evenement de arriveralazone de la classe ZoneArrive, une fois l evenement surviennent la methode CompterPointsRouge sera appele
        zoneBleu.ArriverALaZoneBleu += CompterPointsRouge;
        //on inscris le compteur des points bleu a l evenement de arriveralazone de la classe ZoneArrive, une fois l evenement surviennent la methode CompterPointsBleu sera appele
        zoneRouge.ArriverALaZoneRouge += CompterPointsBleu;
    }
    //cette methode permet de compter les points de l equipe bleu et mettre a jour le champs de text des points
    private void CompterPointsBleu()
    {
        _nombreDePointsBleu++;
        _nbreDePointsTextBleu.text = _nombreDePointsBleu.ToString();

    }
    //cette methode permet de compter les points de l equipe rouge et mettre a jour le champs de text des points
    private void CompterPointsRouge()
    {
        _nombreDePointsRouge++;
        _nbreDePointsTextRouge.text = _nombreDePointsRouge.ToString();
    }
}
