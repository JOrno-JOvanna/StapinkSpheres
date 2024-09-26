using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayControls : MonoBehaviour
{
    public DelegateHolder delegateHolder;
    public Instructions instructions;
    public Transform sphereSpawn;
    public GameObject pausePanel, winTab, boosterPanel, prizePanel;
    public SoundEffects soundEffects;
    public float spheresSpeed;
    public Image targetImage;
    public TextMeshProUGUI score, goalScoreText, levelNumberText, finalScore, finalTime;

    private bool gameStarted;
    private int levelNumber, goalScore, targetImageID;
    private float timefloat = 0f;
    List<float> sphereSpeeds = new List<float>();

    public void Start()
    {
        delegateHolder.OnGameStart += GameStart;
        delegateHolder.DestroySpheresByMistake += RegisterResults;
    }

    private void GameStart(int levelNum, int goal, float speedVar)
    {
        levelNumber = levelNum;
        goalScore = goal;
        spheresSpeed = speedVar;

        for (int i = 0; i < sphereSpawn.childCount; i++)
        {
            sphereSpeeds.Add(speedVar + UnityEngine.Random.Range(0f, 4.1f));
        }

        goalScoreText.text = "/" + goal.ToString();
        levelNumberText.text = levelNum.ToString();

        targetImageID = UnityEngine.Random.Range(0, sphereSpawn.childCount);

        gameStarted = true;
    }

    public virtual void Update()
    {
        if (sphereSpawn.childCount != 0 && gameStarted)
        {
            targetImage.sprite = sphereSpawn.GetChild(targetImageID).GetComponent<SpriteRenderer>().sprite;
        }

        for (int i = 0; i < sphereSpawn.childCount; i++)
        {
            float changeYAxis = sphereSpawn.GetChild(i).position.y;
            float changeXAxis = sphereSpawn.GetChild(i).position.x;

            switch (levelNumber)
            {
                case < 10:
                    changeYAxis -= 0.1f;
                    break;

                case >= 10:
                    changeXAxis -= 0.1f;
                    break;
            }

            if (gameStarted && int.Parse(score.text) <= goalScore)
            {
                Vector3 startPosition = sphereSpawn.GetChild(i).position;
                Vector3 endPosition = new Vector3(changeXAxis, changeYAxis);
                SmoothTransform(sphereSpawn.GetChild(i), startPosition, sphereSpeeds[i], endPosition);
            }
        }

        TapOnSphere();
    }

    public void FixedUpdate()
    {
        if (gameStarted && int.Parse(score.text) <= goalScore)
        {
            timefloat += Time.deltaTime;
        }
    }

    private void SmoothTransform(Transform sphere, Vector3 startPosition, float spheresSpeed, Vector3 endPosition)
    {
        sphere.position = Vector3.Lerp(startPosition, endPosition, spheresSpeed * Time.deltaTime);
    }

    private IEnumerator RespawnSpheres()
    {
        yield return new WaitForSeconds(1f);

        delegateHolder.InvokeOnMakePositions(0.1f, 0.9f, 0.4f, 0.8f, true);

        yield return null;

        delegateHolder.InvokeOnGameStart(levelNumber, goalScore, spheresSpeed);
    }

    private void TapOnSphere()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Sphere"))
            {
                if(gameStarted)
                {
                    if(hit.collider.GetComponent<SpriteRenderer>().sprite == targetImage.sprite)
                    {
                        soundEffects.PlaySphereAudio();
                        hit.collider.transform.GetChild(0).gameObject.SetActive(true);
                        RegisterResults(5);
                    }
                    else
                    {
                        RegisterResults(-15);
                    }
                }
                else if(instructions.interact)
                {
                    if(hit.collider.GetComponent<SpriteRenderer>().sprite == instructions.internTargetImage.sprite)
                    {
                        soundEffects.PlaySphereAudio();
                        hit.collider.transform.GetChild(0).gameObject.SetActive(true);
                        instructions.interact = false;
                        instructions.instructionsText[0].SetActive(false);
                        instructions.instructionsText[1].SetActive(true);
                        StartCoroutine(instructions.SwitchInstructionTexts());
                    }
                }
            }
        }
    }

    private IEnumerator BoosterActivation()
    {
        List<float> originalSpeed = new List<float>();

        for(int i = 0; i < sphereSpeeds.Count; i++)
        {
            originalSpeed.Add(sphereSpeeds[i]);

            sphereSpeeds[i] = sphereSpeeds[i] / 3f;
        }

        yield return new WaitForSeconds(6.5f);

        for (int i = 0; i < originalSpeed.Count; i++)
        {
            sphereSpeeds[i] = originalSpeed[i];
        }
    }

    private void RegisterResults(int newScore)
    {
        gameStarted = false;
        StartCoroutine(RespawnSpheres());
        score.text = (int.Parse(score.text) + newScore).ToString();

        if (int.Parse(score.text) <= 0)
        {
            score.text = 0.ToString();
        }
        else if(int.Parse(score.text) >= goalScore)
        {
            if (new[] {3, 6, 9, 12}.Contains(levelNumber))
            {
                prizePanel.SetActive(true);
                Invoke("WinGameStatement", 1.5f);
            }
            else
            {
                WinGameStatement();
            }
        }
        else if(int.Parse(score.text) % 100 == 0)
        {
            boosterPanel.SetActive(true);

            StartCoroutine(BoosterActivation());
        }
    }

    public void PauseTheGame(bool pauseGame)
    {
        if(gameStarted)
        {
            gameStarted = !gameStarted;

            pausePanel.SetActive(!gameStarted);

            delegateHolder.InvokeOnPauseGame();
        }
        else if(pauseGame)
        {
            gameStarted = !gameStarted;

            pausePanel.SetActive(!gameStarted);
        }
    }

    public void RestartGame()
    {
        score.text = 0.ToString();
        timefloat = 0;

        pausePanel.SetActive(false);

        StartCoroutine(RespawnSpheres());
    }

    public void ExitGame()
    {
        StopAllCoroutines();

        for (int i = 0; i < sphereSpawn.childCount; i++)
        {
            Destroy(sphereSpawn.GetChild(i).gameObject);
        }

        gameStarted = false;

        score.text = 0.ToString();
        timefloat = 0;

        delegateHolder.InvokeOnPauseGame();
        delegateHolder.InvokeOnExitGame();
        
        pausePanel.SetActive(false);
    }

    private void WinGameStatement()
    {
        delegateHolder.InvokeWinLevel(levelNumber, int.Parse(score.text));

        string finaltime;

        if(timefloat > 60)
        {
            int minutes = Mathf.RoundToInt(timefloat / 60);
            int seconds = Mathf.RoundToInt(timefloat) - minutes * 60;

            finaltime = $"{minutes}" + ":" + seconds.ToString("D2");
        }
        else
        {
            finaltime = timefloat.ToString();
        }

        finalScore.text = score.text;
        finalTime.text = finaltime;

        delegateHolder.InvokeSetPatternGame();

        winTab.SetActive(true);

        ExitGame();
    }

    public void NextLevel()
    {
        winTab.SetActive(false);

        delegateHolder.InvokeNextLevel(levelNumber + 1);
    }

    private void OnApplicationQuit()
    {
        delegateHolder.OnGameStart -= GameStart;
        delegateHolder.DestroySpheresByMistake -= RegisterResults;
    }
}
