using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    // Start is called before the first frame update
    public int Niveau;
    public GameObject Player;

    private void Awake()
    {
        
    }
    void Start()
    {
        Niveau= Random.Range(Player.GetComponent<Player>().Niveau/2, Player.GetComponent<Player>().Niveau);
    }

  
}
