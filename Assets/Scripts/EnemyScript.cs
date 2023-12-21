using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    private Animator ani;
    public Quaternion angulo;
    public float grado;

    private GameObject target;
    public bool atacando;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento_Enemigo();
    }

    public void Comportamiento_Enemigo()
    {

        if(Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;


            }
        }
        else 
        {

            if(Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                ani.SetBool("walk", false);

                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                ani.SetBool("attack", false);
            }
            else 
            {
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

                ani.SetBool("attack", true);
                atacando = true;
                print("ATACO");
            }
            
        }
    }

    public void FinalAni()
    {
        ani.SetBool("attack", false);
        atacando = false;

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach(ContactPoint contactPoint in other.contacts) 
            {
                if(contactPoint.normal.y < 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

}
