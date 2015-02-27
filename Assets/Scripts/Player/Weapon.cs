using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour {
    public float dmg;
    public float fireRate;
    public int clipSize;
    public int maxBulletSize;
    public GameObject muzzle;

    public AudioClip atkSound;
    public AudioClip reloadSound;

    public int clip = 30;
    private int bullets = 90;

    private AudioSource audioSource;
    private Animation weapAnim;
    private GameObject bulletDecal;
    private Text bulletsText;
    private GameObject bloodHit;

    private float shootTime = 0f;
    private bool aiming = false;

    void Awake(){
        weapAnim = GetComponent<Animation>();
        bulletDecal = Resources.Load("Prefabs/Bullet Decal") as GameObject;
        bloodHit = Resources.Load("Particle Effects/Blood Hit") as GameObject;
        audioSource = GetComponent<AudioSource>();
        bulletsText = GameObject.FindWithTag("Bullets Text").GetComponent<Text>();
        bulletsText.text = clip + "/" + bullets;
    }

    public void Aim(){
        aiming = !aiming;

        if ( aiming ){
            
        } else {
            
        }
    }

    public void Reload(){
        if ( bullets < 1 ) return;

        audioSource.clip = reloadSound;
        audioSource.Stop();
        audioSource.Play();

        weapAnim.wrapMode = WrapMode.Once;
        weapAnim.Play("metarig|reload_full",PlayMode.StopAll);

        if ( clipSize > bullets ){
            clip = bullets;
            bullets = 0;
        } else {
            bullets -= clipSize - clip;
            clip = clipSize;
        }

        bulletsText.text = clip + "/" + bullets;
    }

    public void Shoot(RaycastHit hit){
        if ( clip < 1 ) {
            return;
        }

        weapAnim.wrapMode = WrapMode.Once;
        weapAnim.Play("metarig|shoot",PlayMode.StopAll);

        if ( Time.time - shootTime >= fireRate ){
            audioSource.clip = atkSound;
            audioSource.Stop();
            audioSource.Play();

            if ( hit.collider != null ){
                var hitRotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);

                if ( hit.collider.gameObject.tag == "Enemy" ){
                    hit.collider.GetComponent<Enemy>().Inflict(dmg);
                    Instantiate(bloodHit, hit.point, hitRotation);
                } else if ( hit.collider.gameObject.tag != "Invisible" ){
                    Instantiate(bulletDecal, hit.point, hitRotation);
                }
            }

            clip--;
            shootTime = Time.time;
            bulletsText.text = clip + "/" + bullets;
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

    public void AddBullets(int amt){
        bullets += amt;
        bulletsText.text = clip + "/" + bullets;
    }
}
