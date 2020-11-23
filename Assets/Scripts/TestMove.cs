using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private SpriteRenderer quadRenderer;

    public float scrollSpeed = 0.01f;
    public float x;
    public FloatReference FallSpeed;
    // Start is called before the first frame update

    private void Awake()
    {
    }

    void Start()
    {
        quadRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        if (GameStage.stage == Stages.Play)
        {
            x += FallSpeed.Variable.Value * (-1) * (0.02f);
            scrollSpeed = x;
            Vector2 textureOffset = new Vector2(scrollSpeed, 0);
            quadRenderer.material.SetTextureOffset("_MainTex", textureOffset);
        }
    }
}
