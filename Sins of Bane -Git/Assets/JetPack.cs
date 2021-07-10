using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPack : MonoBehaviour
{
    public float jumpHeight = 5f;

    public float maxFuel = 100f;

    public static float fuel = 100f;
    public float timeUsed;

    public float burnRate = 0.01f;
    public float refillRate = 0.1f;

    public int timesPressed;

    public Text fuelText;

    public FuelBarScript fuelBarScript;

    // Start is called before the first frame update
    void Start()
    {
        fuelBarScript.SetFuel(fuel);
    }

    // Update is called once per frame
    void Update()
    {
        if(fuel >= maxFuel)
        {
            fuel = maxFuel;
        }
        Fly();
    }

    void Fly()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && JetPackPickUp.equipped == true )
        {
            timesPressed++;
            InvokeRepeating("Fuel", 0f, burnRate);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
            CancelInvoke("Fuel");
            timeUsed = 0f;
        }

        if (fuel < maxFuel )
        {
            //StartCoroutine(RefillFuel());
            if (!Input.GetKeyDown(KeyCode.LeftShift) && timeUsed == 0 )
            {
                InvokeRepeating("Refill", 2f, refillRate);
            }
        }
        if (fuel == maxFuel)
        {
            CancelInvoke("Refill");
        }
        else if(timeUsed > 0)
        {
            CancelInvoke("Refill");
        }
    }

    void Fuel()
    {
        if(fuel > 0f)
        {
            timeUsed += Time.timeScale / 100;
            fuel = fuel - 1f;
            fuelBarScript.SetFuel(fuel);
            fuelText.text = (int)fuel + "%";
        }
    }

    void Refill()
    {
        if(fuel <= maxFuel)
        {
            fuel = fuel + refillRate;
            fuelBarScript.SetFuel(fuel);
            fuelText.text = (int)fuel + "%";
        }

    }

    IEnumerator RefillFuel()
    {
        yield return new WaitForSeconds(timeUsed);
        fuel++;
        fuelBarScript.SetFuel(fuel);
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(5);
    }
}
