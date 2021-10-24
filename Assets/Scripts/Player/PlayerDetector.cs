using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public GameOverManager gameOverManager;
 	public bool isWarning = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !other.isTrigger)
        {
        	if (!isWarning)
        	{
        		float enemyDistance = Vector3.Distance(transform.position,other.transform.position);
            	gameOverManager.ShowWarning(enemyDistance);
            	Debug.Log("warning");
        	}
        	
        }
    }
}
