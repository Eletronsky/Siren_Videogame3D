using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Librer�a para referenciar clases de NavMesh en los que el enemigo, patrullar� y atacar� al jugador cuando lo detecte.

public class EnemyAIBase : MonoBehaviour
{
    [Header("AI Configuration")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;

    [Header ("AI Health")]
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth;

    private void Awake()
    {
        target = GameObject.Find("Player").transform; //Nada m�s comenzar, se referencia al "Player" para poder perseguirlo en cuanto el "Player" entre en el campo de visi�n del enemigo. 
        agent = GetComponent<NavMeshAgent>(); //Autoreferencia a el componente de navmesh para la IA.
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth > 0)
        {
            agent.SetDestination(target.position);
            transform.LookAt(target);
        }
    }

    private void Die()
    {
        agent.enabled = false;
        //Aqui podemos a�adir si queremos animaciones, part�culas, etc a la hora que muera el enemigo.
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }


}
