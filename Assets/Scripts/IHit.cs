using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit
{
    public int MyDamage { get;  }

    public abstract void OnTriggerEnter2D(Collider2D other);
    
}
