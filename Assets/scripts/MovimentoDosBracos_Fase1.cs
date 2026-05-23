using UnityEngine;
using System.Collections;
public class MovimentoDosBracos_Fase1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float numeroGiros;
    private Transform bracos;

    private Transform _boss;

    
    void Start()
    {
        bracos = GameObject.Find("OsBracos").transform;
        _boss = GameObject.Find("boss").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bracos.position == _boss.position)
        {
            StartCoroutine(Girar(bracos));
            //bracos.rotation = Quaternion.Euler(0, 0, numeroGiros);
        }
        else
        {
            Destroy(GameObject.Find("OsBracos"));
        }
        
    }

    IEnumerator Girar(Transform bracos)
    {
        numeroGiros = numeroGiros+1;

        yield return new WaitForSeconds(0.01f);

        bracos.rotation = Quaternion.Euler(0, 0, numeroGiros);
        
    }
}
