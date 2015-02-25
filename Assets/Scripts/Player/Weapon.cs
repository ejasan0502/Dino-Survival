using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    public float fireRate;
    public int clipSize;
    public int magSize;
    public int maxBulletSize;

    public AudioClip atkSound;

    private int clip;
    private int mag;
    private int bullets;

    private AudioSource audioSource;
    private Animation weapAnim;
    private GameObject bulletDecal;

    private float shootTime = 0f;
    private bool shooting = false;

    void Awake(){
        weapAnim = GetComponent<Animation>();
        bulletDecal = Resources.Load("Prefabs/Bullet Decal") as GameObject;
        audioSource = GetComponent<AudioSource>();
    }

    public void Reload(){
        weapAnim.wrapMode = WrapMode.Once;
        weapAnim.Play("metarig|reload_full",PlayMode.StopAll);
    }

    public void Shoot(RaycastHit hit){
        weapAnim.wrapMode = WrapMode.Once;
        weapAnim.Play("metarig|shoot",PlayMode.StopAll);

        if ( shooting ){
            if ( Time.time - shootTime >= fireRate ){
                audioSource.Stop();
                audioSource.Play();

                var hitRotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
                Instantiate(bulletDecal, hit.point, hitRotation);

                shootTime = Time.time;
            }
        } else {
            audioSource.clip = atkSound;
            audioSource.Play();

            var hitRotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
            Instantiate(bulletDecal, hit.point, hitRotation);

            shootTime = Time.time;
            shooting = true;
        }
    }

    public bool ReturnToIdle(){
        if ( !weapAnim.isPlaying ){
            weapAnim.wrapMode = WrapMode.Loop;
            weapAnim.Play("metarig|idle");
            return true;
        }
        return false;
    }
}
