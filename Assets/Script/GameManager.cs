using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GameManager : MonoBehaviour
{
    public GameObject Playerprefab;
    public GameObject Enemyprefab;
    public GameObject CameraMain;
    public GameObject canvas;
    public GameObject canvasPrefab;
    public GameObject textPrefab;
    private GameObject player;
    private GameObject TextNiveauPlayer;
    private GameObject NiveauEnCours;
    public static GameManager instance;
    public int Niveau = 1;
    public int EnemyCount = 0;
    public int PlayerNiveau = 1;
    private int EnemySpawn = 3;
    private bool gameover = false;
    private int sizeCam = 11;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        NiveauEnCours = Instantiate(new GameObject("TextNiveauPlayer"));
        NiveauEnCours.AddComponent<TextMeshPro>();
        NiveauEnCours.GetComponent<TextMeshPro>().fontSize = 100;
        NiveauEnCours.transform.SetParent(canvas.transform, false);
        NiveauEnCours.transform.localPosition = new Vector2(-589f, 323f);
        NiveauEnCours.transform.localScale = new Vector3(3.96f, 3.96f, 3.96f);
        NiveauEnCours.GetComponent<RectTransform>().sizeDelta = new Vector2(51, 20);
        NiveauEnCours.GetComponent<TextMeshPro>().text = "Niveau " + Niveau.ToString();
        player = Instantiate(Playerprefab);
        CameraMain.GetComponent<Camera>().orthographicSize = sizeCam;
        TextNiveauPlayer = Instantiate(new GameObject("TextNiveauPlayer"));
        TextNiveauPlayer.AddComponent<TextMeshPro>();
        TextNiveauPlayer.transform.SetParent(player.transform, false);
        TextNiveauPlayer.transform.localPosition = new Vector2(0.39f, 1.39f);
        TextNiveauPlayer.transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);
        player.GetComponent<Player>().Niveau = PlayerNiveau;
        player.GetComponent<Player>().tag = "Player";
        TextNiveauPlayer.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 5);
        TextNiveauPlayer.GetComponent<TextMeshPro>().text = "Niveau " + PlayerNiveau.ToString();
        EnemiesSpawn();

    }

    void Update()
    {
        if (!gameover)
        {
            TextNiveauPlayer.GetComponent<TextMeshPro>().text = "Niveau " + PlayerNiveau.ToString();


            if (0 == EnemyCount)
            {
                NextLevel();
                CameraMain.GetComponent<Camera>().orthographicSize = sizeCam;
            }
        }
        
        
    }

    private int GetRandomLevel(int min, int max)
    {
        return new System.Random().Next(min, max);
    }

    private void EnemiesSpawn()
    {
        bool SpawnOk = false;
        int[] niveauxennemies = new int[EnemySpawn];
        Vector3[] enemiespos = new Vector3[EnemySpawn];
        for (int i = 0; i < EnemySpawn; i++)
        {
            if (i == 0)
            {
                niveauxennemies[i] = GetRandomLevel((PlayerNiveau - (PlayerNiveau / EnemySpawn)), PlayerNiveau);
            }
            else
            {
                niveauxennemies[i] = GetRandomLevel((PlayerNiveau-(PlayerNiveau/EnemySpawn)), PlayerNiveau + niveauxennemies[i - 1]);
            }


        }

        for (int i = 0; i < EnemySpawn; i++)
        {
            SpawnOk = false;

            while (!SpawnOk)
            {
                enemiespos[i] = new Vector3(UnityEngine.Random.Range(-(sizeCam-2) * 2, (sizeCam - 2)*2), UnityEngine.Random.Range(-(sizeCam-2), (sizeCam-2)), 0);
                if (Vector3.Distance(player.transform.position, enemiespos[i]) > 3)
                {
                    SpawnOk = true;
                }
            }
   
            

        }


        for (int i = 0; i < EnemySpawn; i++)
        {
            var enemy = Instantiate(Enemyprefab, enemiespos[i], Quaternion.identity);
            int enemyLevel = niveauxennemies[i];
            enemy.GetComponent<Ennemi>().Niveau = enemyLevel;
            enemy.GetComponent<Ennemi>().tag = "Enemy";
            var text = Instantiate(new GameObject("niveau"));
            text.AddComponent<TextMeshPro>();
            text.transform.SetParent(enemy.transform,false);
            text.transform.localPosition = new Vector2(0.45f, 2.19f);
            text.transform.localScale = new Vector3(0.21f, 0.21f, 0.21f);
            text.GetComponent<TextMeshPro>().text = "Niveau " + enemyLevel.ToString();
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 5);
           
            EnemyCount++;
            SpawnOk = false;
        }
    }

    private void NextLevel()
    {
        var temp = (EnemySpawn+1) * 1.2f;
        EnemySpawn = (int)temp;
        Niveau += 1;
        sizeCam++;
        NiveauEnCours.GetComponent<TextMeshPro>().text = "Niveau " + Niveau.ToString();
        EnemiesSpawn();
    }

    public void UpdateNiveau(int newniveau)
    {
        if(PlayerNiveau < newniveau)
        {
            GameOver();
        }
        PlayerNiveau += newniveau;
        
        
    }
    
    public void EnemyDead()
    {
        EnemyCount--;
    }

    private void GameOver()
    {
        (this).gameover = true;
        Destroy(player);
        GameObject gameover = Instantiate(new GameObject("TextNiveauPlayer"));
        gameover.AddComponent<TextMeshPro>();
        gameover.GetComponent<TextMeshPro>().fontSize = 30;
        gameover.GetComponent<TextMeshPro>().color = UnityEngine.Color.red;
        gameover.GetComponent<TextMeshPro>().text = "GAMEOVER";
    }
}
