using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int MyMaxHealth { get;}
    public int MyCurrentHealth { get; set; }
    public abstract void OnHit(int amount);
    public void Death();

}
