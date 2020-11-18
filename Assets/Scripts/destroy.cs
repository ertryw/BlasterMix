using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public float destroyTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndPrint(destroyTime));
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }
    }

}
