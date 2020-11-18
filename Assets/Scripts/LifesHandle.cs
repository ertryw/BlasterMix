using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifesHandle : MonoBehaviour
{

    public FloatReference LifesObj;
    public Text LifesTxt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ShowLifes()
    {
        LifesTxt.text = "";
        for (int i = 0; i < LifesObj.Variable.Value; i++)
        {
            LifesTxt.text += "O ";

        }    
    }

    // Update is called once per frame
    void Update()
    {
        ShowLifes();
    }
}
