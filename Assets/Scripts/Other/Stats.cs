using UnityEngine;
using System.Collections;

public class Stats {
    public float hp;
    public float sta;
    public float dmg;
    public float def;
    public float spd;

    public Stats(){
        hp = 0f;
        sta = 0f;
        dmg = 0f;
        def = 0f;
        spd = 0f;
    }

    public static Stats operator+(Stats a, Stats b){
        Stats s = new Stats();

        s.hp = a.hp + b.hp;
        s.sta = a.sta + b.sta;
        s.dmg = a.dmg + b.dmg;
        s.def = a.def + b.def;
        s.spd = a.spd + b.spd;

        return s;
    }
    public static Stats operator-(Stats a, Stats b){
        Stats s = new Stats();

        s.hp = a.hp - b.hp;
        s.sta = a.sta - b.sta;
        s.dmg = a.dmg - b.dmg;
        s.def = a.def - b.def;
        s.spd = a.spd - b.spd;

        return s;
    }
    public static Stats operator*(Stats a, Stats b){
        Stats s = new Stats();

        s.hp = a.hp * b.hp;
        s.sta = a.sta * b.sta;
        s.dmg = a.dmg * b.dmg;
        s.def = a.def * b.def;
        s.spd = a.spd * b.spd;

        return s;
    }
    public static Stats operator/(Stats a, Stats b){
        Stats s = new Stats();

        s.hp = a.hp / b.hp;
        s.sta = a.sta / b.sta;
        s.dmg = a.dmg / b.dmg;
        s.def = a.def / b.def;
        s.spd = a.spd / b.spd;

        return s;
    }
}
