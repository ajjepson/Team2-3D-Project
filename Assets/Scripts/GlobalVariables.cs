using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables globalVariables;
    public static bool playerAlive;

    private void Awake()
    {
        globalVariables = this;
    }
}
