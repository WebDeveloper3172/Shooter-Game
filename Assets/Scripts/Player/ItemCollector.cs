using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;


public class ItemCollector : MonoBehaviour
{
    public int coins = 0;

    [SerializeField] TextMeshProUGUI coinsUIText;

    [SerializeField] AudioSource collectionSound;
    [SerializeField] TextMeshProUGUI gameOverActualScore;
    [SerializeField] TextMeshProUGUI finishLevelActualScore;

    private const string CoinTag = "Coin";
 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(CoinTag))
        {
            Destroy(other.gameObject);
            coins++;
            UpdateCoinsUIText();
            collectionSound.Play();
            Debug.Log("Unsaved coin score" + coins);
        }
    }

    private void UpdateCoinsUIText()
    {
        coinsUIText.text = "COINS SCORE : " + coins;
        gameOverActualScore.text = "COINS SCORE : " + coins;
        finishLevelActualScore.text = "COINS SCORE : " + coins;
    }

    public void OnClick()
    {
        ScoreManager.instance.Save(coins);
    }

}

