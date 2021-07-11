using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public static int TeamOneScore;
    public static int TeamTwoScore;

    public Text TeamOneText;
    public Text TeamTwoText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TeamOneText.text = TeamOneScore + "";
        TeamTwoText.text = TeamTwoScore + "";
    }
}
