using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health = 3;
    public float immuneTime = 1f;
    private bool isImmune = false;

    private Animation anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponentInChildren<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DamageHitbox")
        {
            if (!isDead && !isImmune)
            {
                health--;
                anim.Play("Damage");

                //StartCoroutine(ImmuneTimer());

                if (health == 0)
                {
                    StartCoroutine(DeathSequence());
                }
            }
        }
    }


    private bool isDead = false;
    IEnumerator DeathSequence()
    {
        isDead = true;
        anim.Play("Dead");
        while (anim.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(gameObject);
    }

    IEnumerator ImmuneTimer()
    {
        isImmune = true;

        var timeRemaining = immuneTime;
        while(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        isImmune = false;
    }

}
