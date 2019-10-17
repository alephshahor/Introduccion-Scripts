# Introduccion a los Scripts en Unity

# Ejercicio 1

>>
* Crear un script para el personaje que lo desplace por la pantalla, sin aplicar simulación física.
* Agregar un campo público que permita graduar la velocidad del movimiento desde el inspector de objetos.
* Estar a la escucha de si el usuario ha utilizado los ejes virtuales. Elegir cuáles se va a permitir utilizar: flechas, awsd.
* El recorrido virtual realizado con los contraladores (teclas) debe ser proporcional a lo que se desplaza el jugador:
  * Si sólo pulsa una vez, corresponderá a una unidad, Unity asigna +1 o -1 según la dirección del movimiento
  * Si se mantiene pulsado, el jugador debe avanzar en un movimiento continuo, así que Unity asigna un valor entre 0 y 1 ó 0 y -1
Una vez que tenemos la proporción del desplazamiento, este también debe ser proporcional a la velocidad que hemos establecido para el objeto. El objeto debe cumplir los siguientes requisitos en el movimiento:
Se debe mover exclusivamente en el plano XZ de su sistema de referencia (su suelo)
El avance se debe producir hacia adelante.
* Elegir otros ejes virtuales para el giro y girar al jugador sobre el eje OY (up).


En primer lugar he creado un objeto 3D esférico y le he aplicado una componente *RigidBody* .  
A continuación le he añadido el siguiente script:

* Declaración de las variables:
```Java
private Rigidbody rb;         // Contiene la componente Rigidbody.
private bool isGrounded;      // Indica si la esfera está tocando
                              // el suelo.
public float groundThrust;    // Indica la fuerza a aplicar para
                              // el movimiento en los ejes X y Z.
public float elevationThrust; // Indica la fuerza a aplicar para
                              // el movimiento en el eje Y.
public float rotationSpeed;   // Velocidad de rotación del objeto.        
```
* **Inicialización de las variables**. En este caso consiste únicamente en obtener la componente RigidBody del objeto:

```Java
void Start () {
    rb = gameObject.GetComponent<Rigidbody>();
}
```

* **Implementación de las funciones que comprueban si el objeto está en contacto con el suelo o no**.   
Simplemente chequea las colisiones e interpreta que el objeto está en el suelo si el nombre del objeto con el que colisiona es *Terrain*.

```Java
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
```
* **Implementación de la función update**.
En esta función se pone al programa a la escucha del Input por parte del usuario, dependiendo de la tecla que se pulse se le aplica al objeto una fuerza en una dirección determinada y proporcional al empuje que se haya especificado.

```Java
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
```

Como se puede ver, aunque no se pide que se haga en el ejercicio, el objeto también puede moverse en el *eje Y*, es decir, saltar. Es por ello que es necesario una función ( *isGrounded* ) que indica si el objeto está en el aire o no, evitando así que pueda elevarse sin caer. El objeto también gira alrededor del eye Y.

## Capturas

#### Movimiento hacia delante y detrás.
[![Movimiento hacia delante y detrás](https://i.gyazo.com/ba37083437a70df8dc1aa69117dddeda.gif)](https://gyazo.com/ba37083437a70df8dc1aa69117dddeda)

#### Movimiento hacia izquierda y derecha.
[![Movimiento hacia izquierda y derecha](https://i.gyazo.com/d3176ed33c1da2704620c24083ad1366.gif)](https://gyazo.com/d3176ed33c1da2704620c24083ad1366)

#### Movimiento de salto.
[![Movimiento de salto](https://i.gyazo.com/5cb38cacdc2f4e3109b4697eb3f0d66e.gif)](https://gyazo.com/5cb38cacdc2f4e3109b4697eb3f0d66e)

#### Movimiento de rotación.
[![Movimiento de rotación](https://i.gyazo.com/4ed3e30e789fb32b63c84c1dbfc4836d.gif)](https://gyazo.com/4ed3e30e789fb32b63c84c1dbfc4836d)
