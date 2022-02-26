using System.Collections;
using TMPro;
using UnityEngine;

/*Cette classe gère les deplacements des barres bleus.
 * Date: 25 fevrier 2022
 * Auteur: Sabbag Ziarani, Narges
 */
public class DeplacementBarres : MonoBehaviour
{
    //ceci est la couleur de la barre qui est selectionne
    private Color _couleurBarreSelectionne;
    //ceci est la couleur original de la barre
    private Color _couleurBarreOriginal;
    private MeshRenderer m_renderer;    //ce variable va stocker le meshrenderer de game object

    private bool _barreSelectionne;//boolean qui indique si la barre liee a ce script est selectionnee
    private bool CoroutineExecutee;//boolean qui indique si le coroutine est entrain d etre executee

    private Vector3 _positionInitiaBarre;//ce variable stoke la vecteur de la position initial de la barre
    private Quaternion _rotationInitiaBarre;//ce variable stoke la rotation de la position initial de la barre
    private Vector3 _positionInitiaBalle;//ce variable stoke la vecteur de la position initial de la balle

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
        //on inscris la methode qui gere les but aux evenements ArriverALaZoneBleu et ArriverALaZoneRouge
        zoneBleu.ArriverALaZoneBleu += GererBut;
        zoneRouge.ArriverALaZoneRouge += GererBut;
        //la couleur de la barre qui est selectionne est rouge
        _couleurBarreSelectionne = Color.red;
        //ceci permet de chercher le meshRenderer du ce game object et a le stocker dans le variable ci dessous
        m_renderer = GetComponent<MeshRenderer>();
        //ceci permet de chercher la couleur du ce game object et a le stocker dans le variable _couleurOriginal
        _couleurBarreOriginal = m_renderer.material.color;
        _compteAReboursText = GameObject.Find("Canvas/compteARebours").GetComponent<TextMeshProUGUI>();//on cherche le text qui contient le compte a rebours
        //on stoke la position et la rotation initiale de la barre et de la balle ici
        _positionInitiaBarre = transform.position;
        _rotationInitiaBarre = transform.rotation;
        _positionInitiaBalle = GameObject.Find("balle").transform.position;
        //on part le coroutine du compte a rebours
        StartCoroutine(CompterARebours());
        //on dit que le coroutine est entrain d etre execute et que aucun barre est selectionne
        CoroutineExecutee = true;
        _barreSelectionne = false;


    }
    // Update is called once per frame
    void Update()
    {
        //si la barre est selectionne et qu il n ya aucun coroutin de compte a rebours qui roule on entre dans cette indentation
        if (_barreSelectionne && !CoroutineExecutee)
        {
            //si la touche d est enfoncee on va ici
            if (Input.GetKey("d"))
            {
                //on identifie la rotation initial
                Quaternion rotationInitiale = transform.rotation;
                // on dit que la rotation cible est 90 degree autour de l axe des y, puisque touche d est au sens inverse des aiguilles d une montre
                Quaternion rotationCible = transform.rotation * Quaternion.Euler(0, 90, 0);
                //on definie la pourcentage du rotation et on le multiplie par time.deltat.time
                float pourcentageRotation = Time.deltaTime * 5;
                //on fait une rotation du type slerp, du rotation initial a la rotation cible selon le pourcentage qu on a definie
                transform.rotation = Quaternion.Slerp(rotationInitiale, rotationCible, pourcentageRotation);
            }
            //si la touche a est enfoncee on va ici
            else if (Input.GetKey("a"))
            {
                //on identifie la rotation initial
                Quaternion rotationInitiale = transform.rotation;
                // on dit que la rotation cible est -90 degree autour de l axe des y, puisque touche d est au sens des aiguilles d une montre
                Quaternion rotationCible = transform.rotation * Quaternion.Euler(0, -90, 0);
                //on definie la pourcentage du rotation et on le multiplie par time.delta.time
                float pourcentageRotation = Time.deltaTime * 5;
                //on fait une rotation du type slerp, du rotation initial a la rotation cible selon le pourcentage qu on a definie
                transform.rotation = Quaternion.Slerp(rotationInitiale, rotationCible, pourcentageRotation);
            }
            //si la bouton gauche est appuye on vient ici
            if (Input.GetMouseButton(0))
            {
                //on identifie la position initial
                Vector3 positionDepart = transform.position;
                //on identifie la position cible
                Vector3 positionCible = new Vector3(positionDepart.x, positionDepart.y, 2.5f);
                //on determine la distance entre le pointinitial et le pointcible
                float distance = Vector3.Distance(positionCible, positionDepart);
                //on  determine une pourcentage de translation et on le multiplie par time.delta.time
                float pourcentageTranslation = Time.deltaTime * 5 / distance;
                //on fait une rotation du type lerp, du position initial a la position cible selon le pourcentage qu on a definie
                transform.position = Vector3.Lerp(positionDepart, positionCible, pourcentageTranslation);

            }
            //si la bouton droite est appuye on vient ici
            else if (Input.GetMouseButton(1))
            {
                //on identifie la position initial
                Vector3 positionDepart = transform.position;
                //on identifie la position cible
                Vector3 positionCible = new Vector3(positionDepart.x, positionDepart.y, -1f);
                //on determine la distance entre le pointinitial et le pointcible
                float distance = Vector3.Distance(positionCible, positionDepart);
                //on  determine une pourcentage de translation et on le multiplie par time.delta.time
                float pourcentageTranslation = Time.deltaTime * 5 / distance;
                //on fait une rotation du type lerp, du position initial a la position cible selon le pourcentage qu on a definie
                transform.position = Vector3.Lerp(positionDepart, positionCible, pourcentageTranslation);
            }
        }
    }




    //cette methode de type ienumerator est un compte a rebours qui permet de compter de 3 a zero
    private IEnumerator CompterARebours()
    {
        //on place les barres a leurs places initiales
        PlacerBarrePositionInitial();
        //on freez la position de la balle pour que ca ne bouge pas par la force pendant le compte a rebours
        GameObject.Find("balle").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        for (int duree = 3; duree >= 0; duree--)
        {
            _compteAReboursText.text = duree.ToString();
            // On attend une secondes
            yield return new WaitForSeconds(1.0f);

        }
        //on remet le text du compte a rebours a champs vide
        _compteAReboursText.text = "";
        //on freez la position y de la balle pour que ca ne tombe pas au dehors du jeu
        GameObject.Find("balle").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        //on place la balle a ca place initiale
        PlacerBallePositionInitial();
        //on dit que l execution du coroutine est terminee
        CoroutineExecutee = false;


    }
    //cette methode permet de placer la barre a  sa position et rotation initial
    private void PlacerBarrePositionInitial()
    {
        transform.position = _positionInitiaBarre;
        transform.rotation = _rotationInitiaBarre;

    }
    //cette methode permet de placer la balle a sa position initial
    private void PlacerBallePositionInitial()
    {
        GameObject balle = GameObject.Find("balle");
        balle.transform.position = _positionInitiaBalle;
    }
    //cette methode permet de gerer le but dans le cas d un but ca va partir le coroutine du compte a rebours
    private void GererBut()
    {
        StartCoroutine(CompterARebours());
        CoroutineExecutee = true;
    }

    //ce methode permet de changer la couleur de la barre en rouge tant que il y a un souris la dessus
    private void OnMouseOver()
    {
        //si jamais le compte a rebours est entrain d etre executee, on ne change pas la couleur
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
}
