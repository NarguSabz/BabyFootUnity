using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeplacementBarres : MonoBehaviour
{
    //ceci est la couleur de la barre qui est selectionne
    private Color _couleurBarreSelectionne;
    //ceci est la couleur original de la barre
    private Color _couleurBarreOriginal;
    //ce variable va stocker le meshrenderer de game object
    private MeshRenderer m_renderer;
    private bool _barreSelectionne;
    private bool CoroutineExecutee;

    private Vector3 _positionInitiaBarre;
    private Quaternion _rotationInitiaBarre;
    private Vector3 _positionInitiaBalle;

    private Coroutine _compteARebours; // On conserve une référence de la coroutine pour pouvoir l'arêter.
    TextMeshProUGUI _compteAReboursText;

    private ZoneArrive zoneBleu; // la zone d'arrivée bleu qu'on observe
    private ZoneArrive zoneRouge; // la zone d'arrivée rouge qu'on observe


    // Start is called before the first frame update
    void Start()
    {
        //on cherche le composant ZoneArrive du butbleu
        zoneBleu = GameObject.Find("butBleu").GetComponent<ZoneArrive>();
        //on cherche le composant ZoneArrive du butrouge
        zoneRouge = GameObject.Find("butRouge").GetComponent<ZoneArrive>();
        //on inscris le compteur des points bleu a l evenement de arriveralazone de la classe ZoneArrive, une fois l evenement surviennent la methode CompterPointsRouge sera appele

        zoneBleu.ArriverALaZoneBleu += GererBut;
        //on inscris le compteur des points rouge a l evenement de arriveralazone de la classe ZoneArrive, une fois l evenement surviennent la methode CompterPointsBleu sera appele
        zoneRouge.ArriverALaZoneRouge += GererBut;
        //la couleur de la barre qui est selectionne est rouge
        _couleurBarreSelectionne = Color.red;
        //ceci permet de chercher le meshRenderer du ce game object et a le stocker dans le variable ci dessous
        m_renderer = GetComponent<MeshRenderer>();
        //ceci permet de chercher la couleur du ce game object et a le stocker dans le variable _couleurOriginal
        _couleurBarreOriginal = m_renderer.material.color;
        _compteAReboursText = GameObject.Find("Canvas/compteARebours").GetComponent<TextMeshProUGUI>();
        _positionInitiaBarre = transform.position;
        _rotationInitiaBarre = transform.rotation;
        _positionInitiaBalle = GameObject.Find("balle").transform.position;

        _compteARebours = StartCoroutine(CompterARebours());
        CoroutineExecutee = true;
        _barreSelectionne = false;


    }
    // Update is called once per frame
    void Update()
    {

        if (_barreSelectionne && !CoroutineExecutee)
        {
            // _horizontal = Input.GetAxis("Horizontal");

            if (Input.GetKey("d"))
            {
                Quaternion rotationInitiale = transform.rotation;
                Quaternion rotationCible = transform.rotation * Quaternion.Euler(0, 90, 0);
                float pourcentageRotation = Time.deltaTime * 5;
                transform.rotation = Quaternion.Slerp(rotationInitiale, rotationCible, pourcentageRotation);
            }
            else if (Input.GetKey("a"))
            {
                Quaternion rotationInitiale = transform.rotation;
                Quaternion rotationCible = transform.rotation * Quaternion.Euler(0, -90, 0);
                float pourcentageRotation = Time.deltaTime * 5;
                transform.rotation = Quaternion.Slerp(rotationInitiale, rotationCible, pourcentageRotation);
            }
            if (Input.GetMouseButton(0))
            {
                //monter
                Vector3 positionDepart = transform.position;
                Vector3 positionCible = new Vector3(positionDepart.x, positionDepart.y, 2.5f);
                /*Vector3 positionCible = new Vector3(positionDepart.x, positionDepart.y, positionDepart.z + 10);
                if (positionCible.z >= 2)
                {
                    positionCible = new Vector3(positionDepart.x, positionDepart.y, 2);
                }*/
                float distance = Vector3.Distance(positionCible, positionDepart);
                float pourcentageTranslation = Time.deltaTime * 5 / distance;
                transform.position = Vector3.Lerp(positionDepart, positionCible, pourcentageTranslation);

            }
            else if (Input.GetMouseButton(1))
            {
                //decendre
                Vector3 positionDepart = transform.position;
                Vector3 positionCible = new Vector3(positionDepart.x, positionDepart.y, -1f);

                /*Vector3 positionCible = new Vector3(positionDepart.x, positionDepart.y, positionDepart.z - 10);
                if (positionCible.z <= -2)
                {
                    positionCible = new Vector3(positionDepart.x, positionDepart.y, -2);
                }*/
                float distance = Vector3.Distance(positionCible, positionDepart);
                float pourcentageTranslation = Time.deltaTime * 5 / distance;
                transform.position = Vector3.Lerp(positionDepart, positionCible, pourcentageTranslation);
            }
        }
        /*Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, 0.5f);*/
    }

    void FixedUpdate()
    {


    }

    /**
 * Coroutine qui compte 3 seconds
 */
    private IEnumerator CompterARebours()
    {
        PlacerBarrePositionInitial();
        for (int duree = 3; duree >= 0; duree--)
        {
            _compteAReboursText.text = duree.ToString();
            // On attend une secondes
            yield return new WaitForSeconds(1.0f);

        }
        CoroutineExecutee = false;
        PlacerBallePositionInitial();


    }
    private void PlacerBarrePositionInitial()
    {
        transform.position = _positionInitiaBarre;
        transform.rotation = _rotationInitiaBarre;

    }
    private void PlacerBallePositionInitial()
    {
        GameObject.Find("balle").transform.position = _positionInitiaBalle;
    }
    private void GererBut()
    {
        _compteARebours = StartCoroutine(CompterARebours());
        CoroutineExecutee = true;
    }

    //ce methode permet de changer la couleur de la barre en rouge tant que il y a un souris la dessus
    private void OnMouseOver()
    {
        if (!CoroutineExecutee)
        {
            m_renderer.material.color = _couleurBarreSelectionne;
        }
        //on dit que la barre est selectionne si la couleur change a rouge
        _barreSelectionne = true;
    }
    //ce methode permet de changer la couleur de la barre en sa couleur original quand le souris sort
    private void OnMouseExit()
    {
        m_renderer.material.color = _couleurBarreOriginal;
        //on dit que la barre  nest pas selectionne si la couleur change a la couleur originale
        _barreSelectionne = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = collision.GetContact(0).thisCollider;
        Debug.Log("hey");
        // Now do whatever you need with myCollider.
        // (If multiple colliders were involved in the collision, 
        // you can find them all by iterating through the contacts)
    }
}
