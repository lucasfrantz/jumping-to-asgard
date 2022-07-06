using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSprite : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player") && (this.gameObject.GetComponent<SpriteRenderer>().sprite != null))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
