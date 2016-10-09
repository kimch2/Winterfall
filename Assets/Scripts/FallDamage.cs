using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour {

    private CharacterController charController;
    private Stats statsScript;
    private float timeFlying;

    void Start ()
    {
        charController = gameObject.GetComponent<CharacterController>();
        statsScript = gameObject.GetComponent<Stats>();
    }

	void Update () {

        if (!charController.isGrounded)
        {
            timeFlying += Time.deltaTime;
        }

        if(charController.isGrounded && timeFlying != 0)
        {
            if (timeFlying >= 1.5f)
            {
                statsScript.health -= timeFlying * 15;
            }
            timeFlying = 0;
        }
    }
}
