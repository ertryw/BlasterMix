using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailHandle : MonoBehaviour
{
    public FloatReference FallSpeed;
    public float multiplier;
    Rigidbody2D rb;
    public GameObject Boom;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, (1 - FallSpeed.Variable.Value) * multiplier);
        rb.AddTorque(Random.Range(-1f,1f));
    }

    public void SnailBoom()
    {

        GameObject boom = Instantiate(Boom, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //boom.transform.parent = transform;
    }

}
