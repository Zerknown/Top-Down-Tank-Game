using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemySprite : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    void Start()
    {
        ChangeSprite();    
    }

    void ChangeSprite()
    {
        spriteRenderer.sprite = spriteArray[Random.Range(0, spriteArray.Length)];
    }

}
