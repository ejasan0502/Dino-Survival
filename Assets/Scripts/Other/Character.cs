using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	public string description;
    
	public Stats stats;
	public Stats currentStats;

    public virtual bool isPlayer {
        get {
            return false;
        }
    }
}
