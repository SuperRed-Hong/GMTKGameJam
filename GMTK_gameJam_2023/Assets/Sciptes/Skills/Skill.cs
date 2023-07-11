using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill //: MonoBehaviour
{
    protected PlayerController player;
    public abstract int UseSkill();
}
