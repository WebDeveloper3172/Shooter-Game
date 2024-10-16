using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource loseLife;
    [SerializeField] private AudioSource addLife;

    [SerializeField] private GameObject gameOverPopUp;
    [SerializeField] private GameObject finishPopup;


    private bool dead = false;
    private bool levelFinished = false;

    public int Damage = 10;
    public int MaxHealth = 100;
    public int currentHealth;
    private int reserveHealth;

    [SerializeField] HealthBar HealthBar;
    [SerializeField] ScreenScript gameOverPopUpScript;
    [SerializeField] ScreenScript finishPopUpScript;

    [SerializeField] TextMeshProUGUI enemyScore;
    [SerializeField] int numberEnemiesForEachLevel;
    public static int NumberEnemy = 0;

   void Start()
    {
        currentHealth = MaxHealth;
        reserveHealth = currentHealth;
        gameOverPopUp.SetActive(false);
        finishPopup.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y < -10f && !dead)
        {
            Die();
        }

        if (NumberEnemy == numberEnemiesForEachLevel && !levelFinished)
        {
            FinishLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(Damage);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0)
        { 
            Die();
        }
        currentHealth -= amount;
        HealthBar.UpdateHealthBar(MaxHealth,currentHealth);
        loseLife.Play();
    }
    public void Heal(int amount)
    {
        if (currentHealth >= reserveHealth)
        { currentHealth = MaxHealth; }
        else { currentHealth += amount; }
        HealthBar.UpdateHealthBar(MaxHealth, currentHealth);
        addLife.Play();

    }
    public void PopUpAfterDie()
    {
        gameOverPopUpScript.Initial();
        gameOverPopUp.SetActive(true);
    }

    private void Die()
    {
        dead = true;
        deathSound.Play();
        Invoke(nameof(PopUpAfterDie) , 0.3f);
        Debug.Log("I have died");
    }

    private void FinishLevel()
    {
        UnlockedNewLevel();
        levelFinished = true;
        winSound.Play();
        finishPopUpScript.Initial();
        finishPopup.SetActive(true);
        Debug.Log("Popup activated when the number of enemies is: " + NumberEnemy);

    }

    public void CalculateScoreEnemy()
    {
        enemyScore.text = $"ENEMY : {NumberEnemy} / {numberEnemiesForEachLevel}"; 
    }
    void UnlockedNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
