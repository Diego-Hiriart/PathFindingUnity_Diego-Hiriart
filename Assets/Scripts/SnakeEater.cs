using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEater : MonoBehaviour
{
    private bool dead;
    private int eatenFruits;
    [SerializeField]
    private GameObject jointPrefab;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        eatenFruits = 0;
    }

    private void OnCollisionEnter(Collision collision)//Lose game if colliding with wall or self
    {      

        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("SnakeJoint"))
        {
            this.dead = true;           
        }
    }

    private void OnTriggerEnter(Collider trigger)//Grow when colliding with a fruit
{
        if (trigger.gameObject.CompareTag("Fruit"))
        {
            this.eatenFruits++;
            Destroy(trigger.gameObject);//Destroy the eaten fruit
            GrowJoint();//Grow a new joint
        }
    }

    public int GetEatenFruits()
    {
        return this.eatenFruits;
    }

    public bool IsDead()
    {
        return this.dead;
    }

    private void GrowJoint()//Make a new joint behind the last existing joint
    {
        this.gameObject.GetComponentInParent<SnakeMovement>().AddJoint(jointPrefab);//Add joint to the part list of movement controller
    }
}
