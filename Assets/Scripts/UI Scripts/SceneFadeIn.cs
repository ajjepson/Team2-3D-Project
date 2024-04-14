using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeIn : MonoBehaviour
{
    UIController fade;
   
    
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<UIController>();
        fade.FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
