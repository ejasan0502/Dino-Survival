using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill : MonoBehaviour {
    public string description;

    public virtual void Apply(Character target){}
}
