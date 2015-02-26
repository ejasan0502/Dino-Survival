using UnityEngine;
using System.Collections;

public class InputControls : MonoBehaviour {
    public Weapon weap;

    private bool lockMouse = true;
    private bool reload = false;

    void Awake(){
        //Screen.lockCursor = lockMouse;
    }

    void Update(){
        if ( weap != null ){
            if ( Input.GetKeyDown(KeyCode.R) ){
                if ( weap.clip < 30 ){
                    weap.Reload();
                    reload = true;
                }
            }

            if ( Input.GetMouseButton(0) && !reload ){
                RaycastHit hit;
                Physics.Raycast(weap.muzzle.transform.position,Camera.main.transform.forward, out hit);
                weap.Shoot(hit);

                if ( weap.clip < 1 ){
                    weap.Reload();
                    reload = true;
                }
            }

            if ( Input.GetMouseButton(1) ){
                weap.Aim();
            }

            if ( weap.ReturnToIdle() && reload ){
                reload = false;
            }
        }

        if ( Input.GetKeyDown(KeyCode.Escape) ){
            lockMouse = !lockMouse;
            Screen.lockCursor = lockMouse;
        }
    }
}
