using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public Animation animObj;
    public Stats stats;
    
    private Stats currentStats = new Stats();
    public int maxAttentionSpan = 10;
    public Transform player;
    public float atkD = 3f;
    public float atkRate = 2f;
    public AudioClip idle;
    public AudioClip atk;
    public Transform[] nodes;

    private int nodeIndex = 0;
	private GameObject target = null;
    private EnemySight enemySight;
    private NavMeshAgent agent;
    private AudioSource audioSource;

    private int attentionSpan = 0;
    private float atkTime = 0f;

    void Awake(){
        currentStats = stats;
        enemySight = transform.GetChild(0).GetComponent<EnemySight>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animObj.wrapMode = WrapMode.Once;
    }

    void Update(){
        if ( currentStats.hp > 0 ){
            if ( target == null ){
                if ( nodes.Length > 0 ){
                    if ( Vector3.Distance(transform.position,nodes[nodeIndex].position) <= 0.5f ){
                        if ( nodeIndex < nodes.Length-1 ){
                            nodeIndex++;
                        } else {
                            nodeIndex = 0;
                        }
                    } else {
                        audioSource.clip = idle;
                        animObj.Play("Allosaurus_Run");
                        agent.SetDestination(nodes[nodeIndex].position);
                    }
                }

                if ( enemySight.playerInSight ){
                    target = GameObject.FindWithTag("Player");
                    attentionSpan = maxAttentionSpan;
                    StartCoroutine("Attention");
                }
            } else {
                if ( Vector3.Distance(transform.position,target.transform.position) <= atkD ){
                    Attack();
                } else {
                    audioSource.clip = idle;
                    animObj.Play("Allosaurus_Run");
                    agent.SetDestination(target.transform.position);
                }

                if ( enemySight.playerInSight ){
                    attentionSpan = maxAttentionSpan;
                }
            }
        }
    }

    private IEnumerator Attention(){
        while(true){
            yield return new WaitForSeconds(1f);
            attentionSpan--;
            if ( attentionSpan < 1 ){
                target = null;
                StopCoroutine("Attention");
            }
        }
    }

    private void Attack(){
        if ( Time.time - atkTime >= atkRate ){
            audioSource.clip = atk;
            audioSource.Play();
            animObj.Play("Allosaurus_Attack01");
            target.GetComponent<Player>().Inflict(currentStats.dmg);
            atkTime = Time.time;
        }
    }

    private IEnumerator Death(){
        foreach (Collider c in transform.GetComponents<Collider>()){
            c.enabled = false;
        }

        audioSource.Stop();
        agent.Stop();
        StopCoroutine("Attention");
        animObj.Play("Allosaurus_Die");

        while (animObj.isPlaying){
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }

    public void Inflict(float rawDmg){
        float dmg = rawDmg - currentStats.def;
        if ( dmg < 1 ) dmg = 1f;
        currentStats.hp -= dmg;
        if ( currentStats.hp < 1 ){
            StartCoroutine("Death");
        }
    }
}
