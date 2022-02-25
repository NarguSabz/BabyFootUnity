using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Cette classe gère les Zone d arrivees. Elle déclenche un événement
 * quand la balle entre sur la zone
 * 
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


            if (gameObject.name == "butBleu")
            {

                // on s assure qu il y a des observateurs a cette evenement sinon ca ne sert a rien a declenche cette evenement
                if (ArriverALaZoneBleu != null)
                {

                    ArriverALaZoneBleu();
                }

            }
            else if (gameObject.name == "butRouge")
            {
                // on s assure qu il y a des observateurs a cette evenement sinon ca ne sert a rien a declenche cette evenement
                if (ArriverALaZoneRouge != null)
                {
                    ArriverALaZoneRouge();
                }
            }
        }
    }
}
