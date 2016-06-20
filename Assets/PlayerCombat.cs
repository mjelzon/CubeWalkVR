using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour {

    public float attack_duration = 0.5f;
    public float attack_cd_time = 1f;
    public AudioClip attackSound;
    public Animation attackAnim;

    private bool attack_on_cd = false;

    public float defend_cd_time = 1f;
    private bool defend_on_cd = false;

    private Collider attackHitbox;
    private AudioSource audioSource;
    private Animator animator;


	// Use this for initialization
	void Start ()
    {
        var colliders = GetComponentsInChildren<Collider>();
        foreach(var collider in colliders)
        {
            if(collider.tag == "DamageHitbox")
            {
                attackHitbox = collider;
                attackHitbox.enabled = false;
            }
        }

        if(GetComponent<AudioSource>())
        {
            audioSource = GetComponent<AudioSource>();
        }

        if(GetComponentInChildren<Animator>())
        {
            animator = GetComponentInChildren<Animator>();
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if (!attack_on_cd)
            {
                StartCoroutine(AttackSequence());
                StartCoroutine(CooldownTimer(attack_cd_time, (x) => { attack_on_cd = x; }));
            }
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            if (!defend_on_cd)
            {
                StartCoroutine(CooldownTimer(defend_cd_time, (x) => { defend_on_cd = x; }));
            }
        }
	}

    IEnumerator AttackSequence()
    {
        attackHitbox.enabled = true;
        audioSource.PlayOneShot(attackSound);
        attackAnim.Play("Attack");

        var timeRemaining = attack_duration;
        while(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        attackHitbox.enabled = false;
    }

    IEnumerator CooldownTimer(float cd_time, System.Action<bool> on_cooldown)
    {
        on_cooldown(true);
        var timeRemaining = cd_time;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        on_cooldown(false);
    }

}
