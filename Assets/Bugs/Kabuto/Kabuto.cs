using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kabuto : Bug {

    private Animator anim;
    // Use this for initialization
    new void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public new void TriggerInteractable()
    {
        //idk how this is rly gonna work yet
        base.TriggerInteractable();
        anim.SetTrigger("trigger");
        Debug.Log("KABUTO TRIGGERED");
    }
}
