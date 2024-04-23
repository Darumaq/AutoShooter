using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Transform player;
    public GameObject basherPrefab;
    public float spawnInterval = 1;
    float timeSinceSpawn;
    float spawnDistance = 30;
    int points = 0;
    public GameObject pointsCounter;
    public GameObject timeCounter;
    public GameObject gameOverScreen;
    public float levelTime = 60f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        timeSinceSpawn = 0;
    }

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        // Sprawdzenie, czy czas jest mniejszy lub równy zero
        if (levelTime <= 0)
        {
            GameOver();
        }
        else
        {
            // Odjêcie czasu
            levelTime -= Time.deltaTime;
            // Ustawienie czasu na zero, jeœli jego wartoœæ spadnie poni¿ej zera
            if (levelTime < 0)
            {
                levelTime = 0;
            }
            UpdateUI();
        }

        if (timeSinceSpawn > spawnInterval)
        {
            Vector2 random = Random.insideUnitCircle.normalized;
            Vector3 randomPosition = new Vector3(random.x, 0, random.y);
            randomPosition *= spawnDistance;
            randomPosition += player.position;

            if (!Physics.CheckSphere(new Vector3(randomPosition.x, 1, randomPosition.z), 0.5f))
            {
                Instantiate(basherPrefab, randomPosition, Quaternion.identity);
                timeSinceSpawn = 0;
            }
        }
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }

    private void UpdateUI()
    {
        pointsCounter.GetComponent<TextMeshProUGUI>().text = "Punkty: " + points.ToString();
        timeCounter.GetComponent<TextMeshProUGUI>().text = Mathf.Floor(levelTime).ToString();
    }

    public void GameOver()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.transform.Find("MainTurret").GetComponent<WeaponController>().enabled = false;

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject basher in enemyList)
        {
            basher.GetComponent<BasherController>().enabled = false;
        }

        gameOverScreen.transform.Find("FinalScoreText").GetComponent<TextMeshProUGUI>().text = "Wynik koñcowy: " + points.ToString();

        gameOverScreen.SetActive(true);
    }
}
