using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    public int score;

    public GameObject itemAcceptParticle;

    public Animator animator;
    public PlayerData playerData;

    public AudioSource audioSource;
    public AudioClip acceptAudio;

    private void OnEnable()
    {
        
        animator = GetComponent<Animator>();

        audioSource = FindObjectOfType<AudioSource>();
        playerData = FindObjectOfType<PlayerData>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(acceptAudio, 0.5f);
            playerData.AddScore(score);
            Instantiate(itemAcceptParticle, gameObject.transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
