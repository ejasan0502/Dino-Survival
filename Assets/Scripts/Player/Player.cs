using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public Stats stats;
    public List<Skill> skills;

    private Stats currentStats;

    void Awake(){
        currentStats = stats;
    }

    public void Inflict(float rawDmg){
        float dmg = rawDmg - currentStats.def;
        if ( dmg < 1 ) dmg = 1f;
        currentStats.hp -= dmg;
        if ( currentStats.hp < 1 ){
            Console.Log("Death");
        }
    }
}
