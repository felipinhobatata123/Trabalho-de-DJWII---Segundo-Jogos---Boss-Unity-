using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PlayerAcoes : MonoBehaviour
{
    [SerializeField]private GameObject bala;
    private Transform x_negativo;
    private Transform x_positivo;

    [SerializeField]private int NumeroDeVidas = 100;
    [SerializeField]private float recarga;
    private Rigidbody2D rb;
    private bool andar = true;
    private bool atirar = true;

    private Vector2 direcaoAndar;

    private Vector2 direcaoMira;

    private BoxCollider2D _collider;

    private bool podeAndar = true;
    private bool invencivel = false;

    [SerializeField]private float velocidade;
    [SerializeField]private float atrito;

    private float contadorParalisar = 1;
    [SerializeField]private float tempoInvencivel;

    [SerializeField]private float tempoParalisado;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        x_negativo = GameObject.Find("x_negativo").transform;
        x_positivo = GameObject.Find("x_positivo").transform;
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(andar == true)
        {
            rb.linearVelocity = direcaoAndar * velocidade;
        }
        else
        {
            rb.linearVelocity = (rb.linearVelocity)*atrito;
        }

        if(bala == GameObject.Find("bala_1"))
        {
            recarga = 1;
        }

        if(bala == GameObject.Find("bala_2"))
        {
            recarga = 3;
        }

        if(bala == GameObject.Find("bala_3"))
        {
            recarga = 5;
        }
    }
    void Update()
    {
        Debug.Log(NumeroDeVidas);
    }
    public void Atirar(InputAction.CallbackContext context)
    {
        if((atirar == true)&& context.performed)
        {
            StartCoroutine(Disparar(bala));
        }
        
    }

    public void Mira(InputAction.CallbackContext context)
    {
        direcaoMira = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
    }

    public void Caminhar(InputAction.CallbackContext context)
    {
        if(podeAndar == true)
        {
            direcaoAndar = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
            andar = true;
        
            if(context.canceled)
            {
                andar = false;
            }
        }
        else
        {
            andar = false;
        }

    }
    IEnumerator Disparar(GameObject bala)
    {
        atirar = false;
        switch(direcaoMira.x, direcaoMira.y)
        {
            case (-1f, 0f):
            Instantiate(bala, x_negativo.position, x_negativo.rotation);
            break;

            case (1f, 0f):
            Instantiate(bala, x_positivo.position, x_positivo.rotation);
            break;
        }

        yield return new WaitForSeconds(recarga);

        atirar = true;
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(invencivel == false)
        {
            switch(collider.gameObject.tag)
            {
            case"AtaqueBoss_fase1":
            NumeroDeVidas = NumeroDeVidas - 2;
            StartCoroutine(Invencibilidade(_collider));
            break;

            case"Boss":
            NumeroDeVidas = NumeroDeVidas - 1;
            StartCoroutine(Invencibilidade(_collider));
            break;
            
            case "AtaqueDoRoboAtirador_":
            if((invencivel == false)&& contadorParalisar == 1)
            {
                StartCoroutine(Paralisar(rb));
            }
            break;
            }
        }
    }

    IEnumerator Invencibilidade(BoxCollider2D _collider)
    {
        Vector2 _colliderOriginal = _collider.size;
        invencivel = true;

        yield return new WaitForSeconds(tempoInvencivel);
        invencivel = false;
    }

    IEnumerator Paralisar( Rigidbody2D rb )
    {
        podeAndar = false;
        contadorParalisar = 0;
        yield return new WaitForSeconds(tempoParalisado);
        podeAndar = true;
        contadorParalisar = 1;
    }
}
