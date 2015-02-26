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
}
