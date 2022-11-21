using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : GameBehaviour
{
    Animator anim;
    int health = 5;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Hit()
    {
        health--;
        //if(health >0)
        //    anim.SetTrigger("Hit");
        //else
        //    anim.SetTrigger("Die");

        //does same thing as lines above but just in one line
        anim.SetTrigger(health > 0 ? "Hit" : "Die");
    }


}
