using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private NavMeshAgent navAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        this.navAgent = this.GetComponent<NavMeshAgent>();//Get the Game Object's nav agent
    }

    // Update is called once per frame
    void Update()
    {
        if (this.navAgent != null)
        {
            this.navAgent.SetDestination(this.player.position);//Follow the player
        }
    }
}
