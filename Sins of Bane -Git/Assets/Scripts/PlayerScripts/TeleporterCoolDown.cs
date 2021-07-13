using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TeleporterCoolDown : MonoBehaviour
{

    public float coolDownTime;
    public Collider2D collider;
    public Light2D light;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player") {
            StartCoroutine(CoolDown());
        }

    }

    IEnumerator CoolDown() {

        collider.isTrigger = true;
        light.color = new Color32(244, 255, 0, 0);
        yield return new WaitForSeconds(coolDownTime);
        collider.isTrigger = false;
        light.color = new Color32(37,255,0, 0);
    }
}
