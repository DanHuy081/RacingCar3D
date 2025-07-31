using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class FinishLine : MonoBehaviour
{
    public bool isOpponent = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Finish"))
        {
            Time.timeScale = 0f;
            if (isOpponent)
            {
                Instantiate(Resources.Load("Lose"));
            }
            else
            {
                Instantiate(Resources.Load("Win"));
            }
        }
    }
   
}
