using UnityEngine;
using System.Collections;
public class Boss : MonoBehaviour
{
    private Transform alvo;
    private Rigidbody2D rb;

    private GameObject roboAtirador;
    [SerializeField]private int VidaMaximaDoBoss;
    [SerializeField]private float speed;
    private BoxCollider2D _collider;

    private Vector2 direcao;

    private int contador = 2;

    private bool perseguir = false;

    private float numeroGerado = 0f;

    private Transform boss;

    private float VidaMaximaOriginal;
    [SerializeField]private float TempoAnimacao;

    [SerializeField]private float TempoAcao;

    private Transform Ponto1;

    private int contadorGerar = 1;

    private int contadorAumento = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        boss = GetComponent<Transform>();
        alvo = GameObject.Find("Seguir").transform;
        VidaMaximaOriginal = VidaMaximaDoBoss;
        roboAtirador = GameObject.Find("roboAtirador");
        Ponto1 = GameObject.Find("Ponto1").transform;
    }

    // Update is called once per frame
    void Update()
    {
        direcao = (alvo.position - boss.position).normalized;
        Debug.Log(numeroGerado);
        Debug.Log(VidaMaximaDoBoss);
    }
    
    void FixedUpdate()
    {
        float VidaMaximaOriginal = VidaMaximaDoBoss;
        
        if((VidaMaximaDoBoss == 750)&&contador == 2)
        {
            StartCoroutine(Mover(boss));
            contador = 1;

        }

        switch(VidaMaximaDoBoss)
        {
            case 500:
            StartCoroutine(Gerar(roboAtirador));
            //Instantiate(roboAtirador, Ponto1.position, Ponto1.rotation);
            break;

            case 250:
            StartCoroutine(Aumento(speed));
            //speed = speed*2f;
            //TempoAnimacao = TempoAnimacao/2f;
            break;
        }

        if(perseguir == true)
        {
            rb.linearVelocity = new Vector2(speed, speed)*direcao;
        }
        else
        {
            rb.linearVelocity = new Vector2(speed, speed)*0;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(contador == 1)
        {
            if(collider.gameObject.CompareTag("boss_seguir"))
            {
                contador = 0;
                StartCoroutine(GerarAcao(boss));
            }
  
        }
        
        if(collider.gameObject.CompareTag("AtaqueDoPlayer"))
        {
            VidaMaximaDoBoss = VidaMaximaDoBoss-50;
        }
        
        
    }

    IEnumerator GerarAcao(Transform boss)
    {
        numeroGerado = Random.Range(1, 11);
        
        yield return new WaitForSeconds(1f);

        if(numeroGerado == 5)
        {
            StartCoroutine(PerseguirPlayer(boss));
        }
        else
        {
            contador = 1;
        }
        
    }

    IEnumerator PerseguirPlayer(Transform boss)
    {
        perseguir = false;
        
        Transform alvoOriginal = alvo;
        
        Vector2 ColliderOriginal = _collider.size;
        
        alvo = GameObject.Find("Player").transform;

        yield return new WaitForSeconds(TempoAnimacao);
        
        perseguir = true;
        
        _collider.size = new Vector2(0, 0);

        yield return new WaitForSeconds(TempoAnimacao);
        perseguir = false;

        yield return new WaitForSeconds(1f);
        
        perseguir = false;
        _collider.size = new Vector2(4f, 1f);
        alvo = alvoOriginal;
        
        //_collider.size = ColliderOriginal;

        yield return new WaitForSeconds(2f);

        _collider.size = ColliderOriginal;

        perseguir = true;

        contador = 1;


    }

    IEnumerator Mover(Transform boss)
    {
        perseguir = true;
        
        yield return new WaitForSeconds(0.1f);
        
        perseguir = false;

        yield return new WaitForSeconds(TempoAnimacao);
        perseguir = true;
    }

    IEnumerator Gerar(GameObject roboAtirador)
    {
        if(contadorGerar == 1)
        {
            contadorGerar = 0;
            Instantiate(roboAtirador, Ponto1.position, Ponto1.rotation);
        }
        
        yield return new WaitForSeconds(0f);
    }

    IEnumerator Aumento(float speed)
    {
        if(contadorAumento == 1)
        {
            contadorAumento = 0;
            TempoAnimacao = TempoAnimacao/2;
        }
        yield return new WaitForSeconds(0f);
       
    }   
}
