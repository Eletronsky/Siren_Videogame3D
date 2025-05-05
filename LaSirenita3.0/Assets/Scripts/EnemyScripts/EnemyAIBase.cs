using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Librería para referenciar clases de NavMesh en los que el enemigo, patrullará y atacará al jugador cuando lo detecte.

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
        target = GameObject.Find("Player").transform; //Nada más comenzar, se referencia al "Player" para poder perseguirlo en cuanto el "Player" entre en el campo de visión del enemigo. 
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
        //Aqui podemos añadir si queremos animaciones, partículas, etc a la hora que muera el enemigo.
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }


}
