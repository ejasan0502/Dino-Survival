using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour {
    void OnTriggerEnter(Collider other){
        if ( other.gameObject.tag == "Player" ){
            GetComponent<AudioSource>().Play();

            if ( name.Contains("Bullets") ){
                if ( other.GetComponent<InputControls>().weap != null ) 
                    other.GetComponent<InputControls>().weap.AddBullets(30);
            } else {
                GameObject o = null;

                foreach (Transform t in Camera.main.transform){
                    if ( t.name.Contains(name) ){
                        o = t.gameObject;
                    }
                }

                o.SetActive(true);
                other.GetComponent<InputControls>().weap = o.GetComponent<Weapon>();
            }

            StartCoroutine("DelayDestroy");
        }
    }

    IEnumerator DelayDestroy(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
