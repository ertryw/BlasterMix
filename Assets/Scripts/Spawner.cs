using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject PointShield;
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public FloatReference FallSpeed;

    public GameObject ImageLeft;
    public GameObject ImageRight;

    public float offset = 0.1f;
    public float polynomial = 30;
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine("Spawn");
        StartCoroutine("PremiumON");
    }

    public bool Right = false;

    public int PremiumShield = 11;


    IEnumerator PremiumON()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10,20));
            Right = true;
            PremiumShield = Random.Range(5, 15);
        }
    }

    IEnumerator Spawn()
    {
        float i = 0; ;
        bool one = false;
        int shieldpop = 0;
        while (true)
        {

            if (GameStage.stage == Stages.Play)
            {
                polynomial = 0.3490808f * (Mathf.Pow(FallSpeed.Variable.Value, -1.934455f));

                Vector3 rp1 = new Vector3(SpawnPoint1.transform.position.x + (Random.Range(-0.5f, 0.5f)), SpawnPoint1.transform.position.y, 0);
                Vector3 rp2 = new Vector3(SpawnPoint2.transform.position.x + (Random.Range(-0.5f, 0.5f)), SpawnPoint1.transform.position.y, 0);

                yield return new WaitForSeconds(FallSpeed.Variable.Value * polynomial);


                if (Right == false)
                {
                    i = Random.Range(0f, 1f);
                    ImageLeft.SetActive(false);
                    ImageRight.SetActive(false);
                }

                if (Right == true && one == false)
                {
                    i = Random.Range(0f, 1f);
                    one = true;
                }




                if (i > 0.5f)
                {
                    Instantiate(PointShield, rp1, Quaternion.identity);
                    if (Right)
                    {
                        ImageLeft.SetActive(false);
                        ImageRight.SetActive(true);
                    }
                }



                if (i < 0.5f)
                {
                    Instantiate(PointShield, rp2, Quaternion.identity);
                    if (Right)
                    {
                        ImageLeft.SetActive(true);
                        ImageRight.SetActive(false);
                    }
                }

                if (Right == true)
                    shieldpop++;

                if (one && Right && shieldpop >= PremiumShield)
                {
                    one = false;
                    Right = false;
                    shieldpop = 0;
                }
            }


            yield return 0;
        }
    


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
