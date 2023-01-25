using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ennemi : MonoBehaviour
{
    // Start is called before the first frame update
    public int Niveau;
    


    public Ennemi(int niveau)
    {
        Niveau = niveau;
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            GameManager.instance.UpdateNiveau(Niveau);
            GameManager.instance.EnemyDead();
            Destroy(gameObject);
        }
    }
    

}
