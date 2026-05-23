using UnityEngine;
using System.Collections;
public class RoboAtirador : MonoBehaviour
{
    private int contador = 1;
    [SerializeField]private GameObject bala;

    [SerializeField]private Transform saida;

    [SerializeField]private float tempoDeRecarga;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(contador == 1)
        {
            if(collider.gameObject.CompareTag("Player"))
            {
                contador = 0;
                StartCoroutine(Disparar(saida));
                //Instantiate(bala, saida.position, saida.rotation);
            }
        }
        
    }
    IEnumerator Disparar(Transform saida)
    {
        Instantiate(bala, saida.position, saida.rotation);
        yield return new WaitForSeconds(tempoDeRecarga);
        contador = 1;
    }
}
