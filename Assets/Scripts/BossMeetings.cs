using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMeetings : MonoBehaviour
{
    [SerializeField] NavMeshAgent meshAgentBoss;
    void Start()
    {
        meshAgentBoss.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BossMeeting"))
        {
            meshAgentBoss.enabled = true;
            Debug.Log("Se misca BOSS-ul");
        }
    }
}