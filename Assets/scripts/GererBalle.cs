using UnityEngine;
/**
 * Cette classe gère les comportements liees a la balle.
 * Date: 25 fevrier 2022
 * Auteur: Sabbag Ziarani, Narges
 */
public class GererBalle : MonoBehaviour
{
    private Rigidbody _rigidBodyBalle;//le rigidbody de la balle
    private Vector3 _velociteMinimum;//la vitesse minimum de la balle, qui va permettre a ajouter une force si la vitesse est plus petit
    private float _forceAppliqueeSurLaBalle;//ceci indique la force a appliquee sur la balle
    // Start is called before the first frame update
    void Start()
    {
        _rigidBodyBalle = gameObject.GetComponent<Rigidbody>();//on cherche le composant rigidbody de la balle a l aide de la methode getComponent
        _velociteMinimum = new Vector3(5, 0, 0);//la vitesse minimum
        _forceAppliqueeSurLaBalle = 500;//ceci est la force a appliquer sur la balle

    }

    // Update is called once per frame
    void Update()
    {
        //si la vitesse en x est inferieur a la vitesse minimum ou superieur a la vitesse minimum fois -1 on va capter le bouton space
        if (_rigidBodyBalle.velocity.x < _velociteMinimum.x && _rigidBodyBalle.velocity.x > -1 * (_velociteMinimum.x))
        {
            if (Input.GetKey("space"))
            {
                //on appelle la methode pousserBalle
                PousserBalle();
            }
        }

    }

    //cette methode permet de aleatoirement appliquer une force dans une direction haut ou bas du jeu
    private void PousserBalle()
    {
        //on genere une valeur aleatoire entre la variable -forceappliquee et la forceAppliquee
        float _aleatoire = Random.Range(-1 * _forceAppliqueeSurLaBalle, _forceAppliqueeSurLaBalle);
        //on met une force aleatoire
        _rigidBodyBalle.AddForce(_aleatoire, 0, 0);
    }
    //dans le cas d une collision, cette methode est appelee
    private void OnCollisionEnter(Collision collision)
    {
        //on definie l objet avec lequel on a fait une collision
        GameObject _objetDeCollision = collision.gameObject;
        //dans le cas d un cube, cela veut dire que l objet avec qui on a fait une collision s agit du joueur
        if (_objetDeCollision.name == "Cube")
        {
            //dans le cas ou le joueur  s ajit du gardien de l equipe rouge on entre dans cette indentation
            if (_objetDeCollision.transform.parent.parent.name == "BarreRouge")
            {
                //on applique une force venant de la methode TrouverForceSelonAngle
                _rigidBodyBalle.AddForce(TrouverForceSelonAngle());
            }
            //si jamais le joueur n etais pas le gardien rouge, on vient ici
            else
            {
                //si jamais le joueur n etais pas le gardien rouge, on vient ici
                //si la touche d a ete enfoncee on ajoute une force positive sur x
                //sinon si la touche A a ete enfoncee on ajoute une force negative sur x

                if (Input.GetKey("d"))
                {
                    _rigidBodyBalle.AddForce(_forceAppliqueeSurLaBalle, 0, 0);

                }
                else if (Input.GetKey("a"))
                {
                    _rigidBodyBalle.AddForce(-_forceAppliqueeSurLaBalle, 0, 0);

                }



            }

        }
    }
    //cette methode permet de generer un angle aleatoire entre -15 et 15, et trouver la direction vers laquelle la force sur la balle va etre appliquee
    private Vector3 TrouverForceSelonAngle()
    {
        //on genere un angle aleatoire entre -15 et 15 degree
        float _angleAleatoire = Random.Range(-15.0f, 15.0f);
        //afin de trouver la direction vers la quelle va etre projeter la balle, il faut trouver un vecteur multiple du vecteur qui forme l hypothenus de l angle
        //afin de trouver la composant x de cette vecteur, il faut faire un cosinus de l angle, qui va donner le x necessaire pour atteindre le point
        //afin de trouver la composant z de cette vecteur, il faut faire un sinus de l angle, qui va donner le z necessaire pour atteindre le point
        //finalement on les fait multiplier par la force et par 5 pour que la force soit 5 fois plus grande que celle d habitude
        float composantXDuPoint = Mathf.Cos(_angleAleatoire) * _forceAppliqueeSurLaBalle * 5;
        float composantZDuPoint = Mathf.Sin(_angleAleatoire) * _forceAppliqueeSurLaBalle * 5;
        //on retorne la force a appliquer
        return new Vector3(composantXDuPoint, 0, composantZDuPoint);
    }
}
