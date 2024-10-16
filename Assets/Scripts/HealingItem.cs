using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    [SerializeField] PlayerLife playerLife;

    public int healAmount = 20;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            playerLife.Heal(healAmount);

        }
    }
}
