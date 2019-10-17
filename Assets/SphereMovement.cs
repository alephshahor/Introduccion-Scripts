using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
  private Rigidbody rb;         // Contiene la componente Rigidbody.
  private bool isGrounded;      // Indica si la esfera está tocando 
                                // el suelo.
  public float groundThrust;    // Indica la fuerza a aplicar para
                                // el movimiento en los ejes X y Z.
  public float elevationThrust; // Indica la fuerza a aplicar para
                                // el movimiento en el eje Y.
  
	// Use this for initialization
	void Start () {
		  rb = gameObject.GetComponent<Rigidbody>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Terrain")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            isGrounded = false;
        }
    }

    void Update () {
		if(Input.GetKey(KeyCode.W)){
			 rb.AddForce(new Vector3(1,0,1) * groundThrust);
		}

		if(Input.GetKey(KeyCode.A)){
			rb.AddForce(new Vector3(-1,0,1) * groundThrust);
		}

		if(Input.GetKey(KeyCode.D)){
			rb.AddForce(new Vector3(1,0,-1) * groundThrust);
		}

		if(Input.GetKey(KeyCode.S)){
			rb.AddForce(new Vector3(-1,0,-1) * groundThrust);
		}

        if(Input.GetKeyDown(KeyCode.Space)){
            if (isGrounded){
                rb.AddForce(Vector3.up * elevationThrust);
            }
        }

        if (Input.GetKey(KeyCode.E)){
            rb.transform.Rotate(0,Time.deltaTime * rotationSpeed , 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.R)){
            rb.transform.Rotate(0, (-1) * Time.deltaTime * rotationSpeed,  0 , Space.Self);
        }
    }
}
