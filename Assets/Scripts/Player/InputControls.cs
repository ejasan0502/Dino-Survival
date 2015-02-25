using UnityEngine;
using System.Collections;

public class InputControls : MonoBehaviour {
    public Weapon weap;

    private bool lockMouse = true;
    private bool reload = false;

    void Awake(){
        Screen.lockCursor = lockMouse;
    }

    void Update(){
        if ( Input.GetKeyDown(KeyCode.R) && !reload ){
            weap.Reload();
            reload = true;
        }

        if ( Input.GetMouseButton(0) ){
            RaycastHit hit;
            if ( Physics.Raycast(transform.position,Camera.main.transform.forward, out hit) ){
                if ( hit.collider.gameObject.tag == "Enemy" ){

                } else {
                    weap.Shoot(hit);
                }
            }
        }

        if ( weap.ReturnToIdle() && reload ){
            reload = false;
        }

        if ( Input.GetKeyDown(KeyCode.Escape) ){
            lockMouse = !lockMouse;
            Screen.lockCursor = lockMouse;
        }
    }
}
