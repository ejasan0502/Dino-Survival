using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {
    public float fieldOfViewAngle = 110f;
    public bool playerInSight;

    private GameObject player;

    void Awake(){
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerStay(Collider other){
        if ( other.gameObject == player ){
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction,transform.forward);

            if ( angle > fieldOfViewAngle*0.5f ){
                RaycastHit hit;

                if ( Physics.Raycast(transform.position, direction.normalized, out hit) ){
                    if ( hit.collider.gameObject == player ){
                        playerInSight = true;
                    }
                }
            }
        }   
    }

    void OnTriggerExit(Collider other){
        if ( other.gameObject == player ){
            playerInSight = false;
        }
    }
}
