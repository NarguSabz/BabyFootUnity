using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GererBalle : MonoBehaviour
{
    Vector3 positionInitial;
    private DeplacementBarres _instanceDeplacementBaree;

    // Start is called before the first frame update
    void Start()
    {
        positionInitial = gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("g"))
        {
            transform.position = new Vector3(-22, 0, 0);
            /*Rigidbody _r;
            _r = gameObject.GetComponent<Rigidbody>();
            _r.AddForce(new Vector3(Input.GetAxis("Horizontal") * 2, 0, 0));*/

        }
        else if (Input.GetKey("f"))
        {
            transform.position = new Vector3(24, 0, 0);

            /*Rigidbody _r;
            _r = gameObject.GetComponent<Rigidbody>();
            _r.AddForce(new Vector3(Input.GetAxis("Horizontal") * 2, 0, 0));*/

        }
        else if (Input.GetKey("l"))
        {
            transform.position = positionInitial;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
    }
}
