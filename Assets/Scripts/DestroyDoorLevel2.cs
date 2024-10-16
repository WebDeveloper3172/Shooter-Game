using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestroyDoorLevel2 : MonoBehaviour
{
    [SerializeField] PlayerLife PlayerLife;
    [SerializeField] GameObject door;
    private bool disappeared = false;
    [SerializeField] GameObject notificationText;

    private void Start()
    {
        notificationText.SetActive(false);
    }

    void Update()
    {

        if (PlayerLife.NumberEnemy == 7 && !disappeared)
        {
            Destroy(door);
            disappeared = true;
            notificationText.SetActive(true);
            Debug.Log("Din acest moment se dezactiveaza textul");
            Invoke(nameof(DeactivateText) , 3f);
        }
    }
    void DeactivateText()
    {
        Debug.Log("Deactivating Text");
        notificationText.SetActive(false);
    }
}
