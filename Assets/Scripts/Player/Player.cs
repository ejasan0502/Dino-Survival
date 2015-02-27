using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public Text hpText;
    public Stats stats;
    public List<Skill> skills;

    private Stats currentStats;

    void Awake(){
        currentStats = stats;
        hpText.text = currentStats.hp + "/" + stats.hp;
    }

    public void Inflict(float rawDmg){
        float dmg = rawDmg - currentStats.def;
        if ( dmg < 1 ) dmg = 1f;
        currentStats.hp -= dmg;
        hpText.text = currentStats.hp + "/" + stats.hp;
        if ( currentStats.hp < 1 ){
            Console.Log("Death");
        }
    }
}
