using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public Health health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay(Collision collision)
    {
        IEnemyTouchDamage enemyTouchDamage = (IEnemyTouchDamage)collision.transform.GetComponent(typeof(IEnemyTouchDamage));
        if(enemyTouchDamage != null && enemyTouchDamage.CanDamage)
        {
            health.TakeDamage();
        }


        //if(collision.transform.GetComponent<DamageTrigger>())
        //{
        //    health.TakeDamage();
        //}
    }
}
