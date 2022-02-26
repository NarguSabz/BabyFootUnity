using UnityEngine;
/**
 * Cette classe gère les Zone d arrivees. Elle déclenche un événement
 * quand la balle entre sur la zone bleu ou rouge
 * Date: 25 fevrier 2022
 * Auteur: Sabbag Ziarani, Narges
 */

//on definie une delegate afin de pouvoir utiliser les events
public delegate void ZoneArrivee();
public class ZoneArrive : MonoBehaviour
{
    //on instantanise deux evenements, ArriverALaZoneBleu et ArriverALaZoneRouge
    public event ZoneArrivee ArriverALaZoneBleu;
    public event ZoneArrivee ArriverALaZoneRouge;

    // la propriete pour chercher la balle
    private GameObject Balle
    {
        get
        {
            return GameObject.Find("balle");
        }
    }

    //cette methode permet d effectuer une tache dans le cas d un trigger avec la zone d arrive
    private void OnTriggerEnter(Collider other)
    {

        //on s assure que l objet qui a active le trigger s agit de la balle
        if (other.gameObject == Balle)
        {

            //si la l objet liee a ce script est le but bleu, on entre dans cette indentation
            if (gameObject.name == "butBleu")
            {

                // on s assure qu il y a des observateurs a cette evenement sinon ca ne sert a rien a declenche cette evenement
                if (ArriverALaZoneBleu != null)
                {
                    //on appel l evenement
                    ArriverALaZoneBleu();
                }

            }
            //si la l objet liee a ce script est le but rouge, on entre dans cette indentation

            else if (gameObject.name == "butRouge")
            {
                // on s assure qu il y a des observateurs a cette evenement sinon ca ne sert a rien a declenche cette evenement
                if (ArriverALaZoneRouge != null)
                {
                    //on appel l evenement

                    ArriverALaZoneRouge();
                }
            }
        }
    }
}
