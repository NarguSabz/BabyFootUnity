using UnityEngine;

/*Cette classe gère les deplacements de la barre de l equipe rouge.
 * Date: 25 fevrier 2022
 * Auteur: Sabbag Ziarani, Narges
 */
public class DeplacementBarreRouge : MonoBehaviour
{
    private Collider colliderBalle;  // Le collider de la balle
    private GameObject _butRouge; // le but rouge
    private bool _balleDetecte;//boolean permetant de savoir si la balle a ete detectee 

    // Start is called before the first frame update
    void Start()
    {
        //on determine le collider de la balle 
        colliderBalle = GameObject.Find("balle").GetComponent<Collider>();
        _butRouge = GameObject.Find("butRouge");//on ajoute le game object nomee but rouge
        //ceci n est pas activee mais ca permettra de ignorer tous les colliders entre la balle et le but rouge
        /*Physics.IgnoreCollision(colliderBalle, _butRouge.gameObject.GetComponent<Collider>(), true);*/
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {

        //array de tous les objets qui entrent en contact avec celui du gradien rouge
        RaycastHit[] hits;
        hits = Physics.RaycastAll(_butRouge.transform.position, _butRouge.transform.right, 40.0F);

        for (int i = 0; i < hits.Length && !_balleDetecte; i++)
        {
            RaycastHit hit = hits[i];

            // Vérifier si l'objet touché est la balle
            if (hit.collider == colliderBalle)
            {
                //si il s agit de la balle on detecte sa direction et on deplace la barre du gardien rouge en utilisant un lerp vers la direction ou se dirige la balle
                //et on fait un break;

                Debug.Log(hit.collider.gameObject);
                _balleDetecte = true;
                //une fois que la balle a ete detectee, on sort cette boucle

            }

        }
        if (!_balleDetecte)
        {
            //si la balle n a pas ete detecte du tout, on revient vers le centre si on y est pas deja
        }
    }
}
