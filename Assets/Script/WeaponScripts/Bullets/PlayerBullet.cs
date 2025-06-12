using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBase
{
    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
