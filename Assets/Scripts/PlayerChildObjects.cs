using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChildObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMove.playerCurrentHealth <= 0)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
