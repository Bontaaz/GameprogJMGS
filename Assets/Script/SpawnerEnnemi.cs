using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnnemi : MonoBehaviour
{
    public GameObject Ennemi;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Instantiate(Ennemi, new Vector3(Random.Range(-10.66f, 10.66f), Random.Range(-4.12f, 4.12f),0), Quaternion.identity);

        }

    }
}
