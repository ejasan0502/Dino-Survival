using UnityEngine;
using System.Collections;

public class Factor {
    public float hunger;
    public float thirst;
    public float sleep;

    public Factor(){
        hunger = 0;
        thirst = 0;
        sleep = 0;
    }

    public static Factor operator+(Factor a, Factor b){
        Factor f = new Factor();

        f.hunger = a.hunger + b.hunger;
        f.thirst = a.thirst + b.thirst;
        f.sleep = a.sleep + b.sleep;

        return f;
    }
}
