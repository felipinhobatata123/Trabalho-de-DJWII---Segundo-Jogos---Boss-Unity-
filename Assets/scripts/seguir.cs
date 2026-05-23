using UnityEngine;

public class seguir : MonoBehaviour
{
    [SerializeField]private Transform alvo;
    private Vector2 direcao;

    private Transform _Seguir;
    [SerializeField]private float speed_x;
    [SerializeField]private float speed_y;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Seguir = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direcao = (alvo.position - _Seguir.position).normalized;
        rb.linearVelocity = new Vector2(speed_x, speed_y)*direcao;

    }
}
