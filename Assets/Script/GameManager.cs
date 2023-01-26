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
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject cameraMain;
    public GameObject canvas;
    public GameObject canvasPrefab;
    public GameObject textPrefab;
    private GameObject _player;
    private GameObject _textNiveauPlayer;
    private GameObject _niveauEnCours;
    public static GameManager instance;
    public int niveau = 1;
    public int enemyCount = 0;
    public int playerNiveau = 1;
    private int _enemySpawn = 3;
    private bool _gameOver = false;
    private int _sizeCam = 11;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        _niveauEnCours = Instantiate(new GameObject("TextNiveauPlayer"));
        _niveauEnCours.AddComponent<TextMeshPro>();
        _niveauEnCours.GetComponent<TextMeshPro>().fontSize = 100;
        _niveauEnCours.transform.SetParent(canvas.transform, false);
        _niveauEnCours.transform.localPosition = new Vector2(-589f, 323f);
        _niveauEnCours.transform.localScale = new Vector3(3.96f, 3.96f, 3.96f);
        _niveauEnCours.GetComponent<RectTransform>().sizeDelta = new Vector2(51, 20);
        _niveauEnCours.GetComponent<TextMeshPro>().text = "Niveau " + niveau.ToString();
        _player = Instantiate(playerPrefab);
        cameraMain.GetComponent<Camera>().orthographicSize = _sizeCam;
        _textNiveauPlayer = Instantiate(new GameObject("TextNiveauPlayer"));
        _textNiveauPlayer.AddComponent<TextMeshPro>();
        _textNiveauPlayer.transform.SetParent(_player.transform, false);
        _textNiveauPlayer.transform.localPosition = new Vector2(0.39f, 1.39f);
        _textNiveauPlayer.transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);
        _player.GetComponent<Player>().niveau = playerNiveau;
        _player.GetComponent<Player>().tag = "Player";
        _textNiveauPlayer.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 5);
        _textNiveauPlayer.GetComponent<TextMeshPro>().text = "Niveau " + playerNiveau.ToString();
        EnemiesSpawn();

    }

    void Update()
    {
        if (!_gameOver)
        {
            _textNiveauPlayer.GetComponent<TextMeshPro>().text = "Niveau " + playerNiveau.ToString();


            if (0 == enemyCount)
            {
                NextLevel();
                cameraMain.GetComponent<Camera>().orthographicSize = _sizeCam;
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
        int[] niveauxennemies = new int[_enemySpawn];
        Vector3[] enemiespos = new Vector3[_enemySpawn];
        for (int i = 0; i < _enemySpawn; i++)
        {
            if (i == 0)
            {
                niveauxennemies[i] = GetRandomLevel((playerNiveau - (playerNiveau / _enemySpawn)), playerNiveau);
            }
            else
            {
                niveauxennemies[i] = GetRandomLevel((playerNiveau-(playerNiveau/_enemySpawn)), playerNiveau + niveauxennemies[i - 1]);
            }


        }

        for (int i = 0; i < _enemySpawn; i++)
        {
            SpawnOk = false;

            while (!SpawnOk)
            {
                enemiespos[i] = new Vector3(UnityEngine.Random.Range(-(_sizeCam-2) * 2, (_sizeCam - 2)*2), UnityEngine.Random.Range(-(_sizeCam-2), (_sizeCam-2)), 0);
                if (Vector3.Distance(_player.transform.position, enemiespos[i]) > 3)
                {
                    SpawnOk = true;
                }
            }
   
 

        }


        for (int i = 0; i < _enemySpawn; i++)
        {
            var enemy = Instantiate(enemyPrefab, enemiespos[i], Quaternion.identity);
            int enemyLevel = niveauxennemies[i];
            enemy.GetComponent<Ennemi>().niveau = enemyLevel;
            enemy.GetComponent<Ennemi>().tag = "Enemy";
            var text = Instantiate(new GameObject("niveau"));
            text.AddComponent<TextMeshPro>();
            text.transform.SetParent(enemy.transform,false);
            text.transform.localPosition = new Vector2(0.45f, 2.19f);
            text.transform.localScale = new Vector3(0.21f, 0.21f, 0.21f);
            text.GetComponent<TextMeshPro>().text = "Niveau " + enemyLevel.ToString();
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 5);           
            enemyCount++;
            SpawnOk = false;
        }
    }

    private void NextLevel()
    {
        var temp = (_enemySpawn+1) * 1.2f;
        _enemySpawn = (int)temp;
        niveau += 1;
        _sizeCam++;
        _niveauEnCours.GetComponent<TextMeshPro>().text = "Niveau " + niveau.ToString();
        EnemiesSpawn();
    }

    public void UpdateNiveau(int newniveau)
    {
        if(playerNiveau < newniveau)
        {
            GameOver();
        }
        playerNiveau += newniveau;
        
        
    }

    public void EnemyDead()
    {
        enemyCount--;


    }

    private void GameOver()
    {
        (this)._gameOver = true;
        Destroy(_player);
        GameObject gameover = Instantiate(new GameObject("TextNiveauPlayer"));
        gameover.AddComponent<TextMeshPro>();
        gameover.GetComponent<TextMeshPro>().fontSize = 30;
        gameover.GetComponent<TextMeshPro>().color = UnityEngine.Color.red;
        gameover.GetComponent<TextMeshPro>().text = "GAMEOVER";
    }
}
