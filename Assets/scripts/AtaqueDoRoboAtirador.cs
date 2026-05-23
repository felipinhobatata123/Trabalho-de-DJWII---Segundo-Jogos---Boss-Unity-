using UnityEngine;
using System.Collections;
public class AtaqueDoRoboAtirador : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField]private float speed_x;

    private float NumeroGerado = 0f;
    [SerializeField]private float speed_y;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NumeroGerado = Random.Range(5, 25);
        speed_x = NumeroGerado;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Aumentar(rb.linearVelocity.y));
        rb.linearVelocity = new Vector2(speed_x, rb.linearVelocity.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
    }
    
    IEnumerator Aumentar(float float_y)
    {
        rb.gravityScale = 1f;
        
        yield return new WaitForSeconds(1f);
        rb.gravityScale = -2f;

        yield return new WaitForSeconds(2f);
        rb.gravityScale = 4f;
        
    }
   
    
}
