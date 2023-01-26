using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Ennemi : MonoBehaviour
{
    // Start is called before the first frame update
    public int niveau;
    public AudioClip SoundToPlay;


    public Ennemi(int Niveau)
    {
        niveau = Niveau;
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            GameManager.instance.UpdateNiveau(niveau);
            GameManager.instance.EnemyDead();
            AudioSource.PlayClipAtPoint(SoundToPlay, Camera.main.transform.position);
            Destroy(gameObject);

        }
    }
    

}
