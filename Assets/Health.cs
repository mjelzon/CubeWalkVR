using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    private static Health instance;
    public static Health Instance
    {
        get { return instance; }
    }

    private Heart[] hearts = new Heart[3];

    private int maxHealth = 3;
    private int currentHealth = 3;
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start ()
    {
        var heartComponents = GetComponentsInChildren<Heart>();
        for(int i = 0; i < 3; i++)
        {
            hearts[heartComponents[i].index] = heartComponents[i];
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void TakeDamage()
    {
        if (immune) return;

        StartCoroutine(ImmuneTimer());

        hearts[currentHealth - 1].EmptyHeart();

        currentHealth--;

        if(currentHealth == 0)
        {
            //player dies
        }
    }

    public void AddHealth()
    {
        if (currentHealth < maxHealth)
        {
            hearts[currentHealth++].FillHeart();
        }
    }

    public float immuneTime = 3f;
    private bool immune = false;
    IEnumerator ImmuneTimer()
    {
        immune = true;
        var timer = immuneTime;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        immune = false;
    }
}
