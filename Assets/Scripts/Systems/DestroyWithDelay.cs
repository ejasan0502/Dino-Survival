using UnityEngine;
using System.Collections;

public class DestroyWithDelay : MonoBehaviour {
    public float wait = 1f;

    IEnumerator Start(){
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }
}
