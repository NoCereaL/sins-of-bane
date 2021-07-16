using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackHolder : MonoBehaviour
{
    public AstroMovement movement;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(movement.transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
