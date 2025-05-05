using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Librer�a para usar clases de NavMesh.

public class EnemyAIBasic : MonoBehaviour
{
    [Header("AI Configuration")]
    [SerializeField] NavMeshAgent agent; //Ref al componente que permite que el objeto tenga IA.
    [SerializeField] Transform target; //Ref al transform del objeto a perseguir.
    [SerializeField] LayerMask targetLayer; //Determina cual es la capa de detecci�n del target.
    [SerializeField] LayerMask groundLayer; //Determina cual es la capa de detecci�n del suelo.

    [Header("Patroling Stats")]
    public Vector3 walkPoint; //Direcci�n hacia la que se mover� la IA si no detecta al target.
    [SerializeField] float walkPointRange; //Rango m�ximo de direcci�n a generar.
    bool walkPointSet; //Determina si la IA ha llegado al objetivo y entonces genera un nuevo objetivo.

    [Header("Attack Configuration")]
    public float timeBetweenAttacks; //Tiempo de espera entre ataque y ataque (Se puede igualar a la duraci�n de la animaci�n de ataque).
    bool alreadyAttacked; //Determina si ya se ha atacado (evita ataques infinitos seguidos).
    //Variables que se usan si el ataque es a distancia.
    [SerializeField] GameObject projectile; //Ref al prefab de la bala f�sica. 
    [SerializeField] Transform shootPoint; //Ref a la posici�n desde donde se genera la bala.
    [SerializeField] float shootSpeedZ; //Velocidad de la bala hacia delante.
    [SerializeField] float shootSpeedY; //Velocidad de la bala hacia arriba (Solo si es catapulta con gravedad).


    [Header("States & Detection")]
    [SerializeField] float sightRange; //Distancia de detecci�n del target de la IA.
    [SerializeField] float attackRange; //Distancia a partir de la cual la IA ataca. 
    [SerializeField] bool targetInSightRange; //Determina si el target est� a distancia de detecci�n.
    [SerializeField] bool targetInAttackRange; //Determina si el target est� a distancia de ataque.


    private void Awake()
    {
        target = GameObject.Find("Player").transform; //Al inicio del juego el target es el Player.
        agent = GetComponent<NavMeshAgent>(); //Al inicio del juego se autoreferencia el componente Agente.
    }

    // Update is called once per frame
    void Update()
    {
        EnemyStateUpdater();
    }



    void EnemyStateUpdater()
    {
        //Chequear si el target est� en los rangos de detecci�n y/o ataque.
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, targetLayer);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetLayer);

            //Cambios din�micos de estado de la IA.
            //Si no detecta al target ni est� en rango de ataque: Patrulla.
            if (!targetInSightRange && !targetInAttackRange) Patroling();
            //Si detecta al target pero no est� en rango de ataque: Persigue. 
            if (targetInSightRange && !targetInAttackRange) ChaseTarget();
            //Si detecta al target y est� en rango de ataque: Ataca.
            if (targetInSightRange && targetInAttackRange) AttackTarget();
    }

    void Patroling()
    {
        //Sistema de patrullaje

        if (!walkPointSet)
        {
            //Si no hay punto al que dirigirse, se genera un nuevo punto. 
            SearchWalkPoint();
        }
        else
        {
            //Si el punto generado es caminable, el agente lo perseguir�.
            agent.SetDestination(walkPoint);
        }

        //Sistema para que el agente busque un nuevo destino en caso de que ya haya llegado al punto actual.
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1) walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        //Este m�todo es un sistema de generaci�n de puntos a perseguir por el agente.

        //Sistema de generaci�n de puntos a patrullar Random.
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //Determinamos el nuevo punto random a perseguir. 
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Detecci�n: Si no hay suelo debajo, para evitar bucles infinitos.
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true; //Confirmamos que el punto es caminable, por lo que empezar� el movimento.
        }
    }

    void ChaseTarget()
    {
        //Una vez detecta al target, el agente lo persigue. 
        agent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        //Cuando se comienza a atacar, el agente se queda quieto (se persigue a s� mismo)
        agent.SetDestination(transform.position);
        //El agente siempre observa directamente al target.
        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            //Si no estamos atacando, se comienza a atacar.
            //AQU� IR�A EL C�DIGO DE ATAQUE A PERSONALIZAR. 

            //En este ejemplo, vamos a generar una bala, referenciar su rigidbody y empujarla por fuerzas.
            Rigidbody rb = Instantiate(projectile, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * shootSpeedZ, ForceMode.Impulse);
            //Si es modo catapulta, se a�ade la siguiente l�nea.
            //rb.AddForce(transform.up * shootSpeedY, ForceMode.Impulse);

            //Se termina el ataque, empieza el cooldown de intervalo de ataque. 
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); //Vuelve a atacar en el intervalo de tiempo indicado, se suele timear con la animaci�n de ataque. 

        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //Funci�n para que los Gizmos de detecci�n (perseguir/ataque) se dibujan en escena al seleccionar el objeto.

    private void OnDrawGizmosSelected()
    {
        //Dibuja una esfera de color rojo que define el rango de ataque.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //Dibuja una esfera de color amarillo que deifne el rango de persecuci�n.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
