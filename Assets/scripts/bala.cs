using UnityEngine;

public class bala : MonoBehaviour
{
    private Transform x_negativo;
    private Transform x_positivo;

    [SerializeField]private float velocidadeBala;

    private Rigidbody2D rb;

    private Transform _bala;
    void Start()
    {
        x_negativo = GameObject.Find("x_negativo").transform;
        x_positivo = GameObject.Find("x_positivo").transform;
        _bala = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_bala.position == x_negativo.position)
        {
            rb.linearVelocity = new Vector2(-1, 0)*velocidadeBala;
        }
        if(_bala.position == x_positivo.position)
        {
            rb.linearVelocity = new Vector2(1, 0)*velocidadeBala;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        switch(collider.gameObject.tag)
        {
            case "Boss":
            Destroy(this.gameObject);
            break;

            case "parede":
            Destroy(this.gameObject);
            break;
        }
    }
}
