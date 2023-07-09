using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterData : MonoBehaviour
{
    [SerializeField] public abstract float MySpeed { get; set; }
    [SerializeField] public abstract float MyDashSpeed { get; set; }
    [SerializeField] public abstract RuntimeAnimatorController MyController { get; }
    [SerializeField] public abstract float MyDashTime { get; set; }


    public abstract void SpecialAttack();
    public abstract void EndSpecialAttack();
    public virtual void Start()
    {

    }
}
