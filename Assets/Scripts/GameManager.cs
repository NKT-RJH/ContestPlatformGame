using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isClear;
    public bool isDead;

    public int SceneID;
    public int NextSceneID;

    public PlayerData player;

    public PlayerController playeController;

    public Text startText;

    public GameObject clearParticle;

    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerData>();
        playeController = FindObjectOfType<PlayerController>();
        player.StageStart();
    }

    private void Start()
    {
        playeController.movement.inputLock = true;
        StartCoroutine(StartTextView());
    }



    void Update()
    {
        
    }

    public void Dead()
    {
        if (!isDead && !isClear)
        {

            isDead = true;
            player.addScore = 0;
            player.deadCount++;
            player.Damage();
            
            
            if (player.playerHP <= 0)
            {
                GameOver();
            }
            else
            {
                SceneManager.LoadScene(SceneID);
            }
        }
        
    }

    public void Clear()
    {

        StartCoroutine(ClearStage());
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator StartTextView()
    {
        
        startText.gameObject.SetActive(true);
        startText.text = "Ready";

        yield return new WaitForSeconds(1.5f);

        startText.color = Color.yellow;
        startText.text = "GO!";

        yield return new WaitForSeconds(0.5f);
        startText.gameObject.SetActive(false);

        playeController.movement.inputLock = false;
    }

    private IEnumerator ClearStage()
    {
        if (player.deadCount == 0)
        {
            player.AddScore(10);

        }
        else
        {
            player.deadCount = 0;
        }

        player.StageEnd();
        isClear = true;
        playeController.movement.inputLock = true;



        //startText.color = Color.yellow;
        startText.text = "Stage " + (SceneID + 1).ToString() + " Clear!";

        startText.gameObject.SetActive(true);
        clearParticle.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        startText.gameObject.SetActive(false);
        clearParticle.SetActive(false);

        SceneManager.LoadScene(NextSceneID);
    }
}
