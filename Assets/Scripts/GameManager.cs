using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private float createTime;
    [SerializeField] private float speedGame;
    public float SpeedGame { get { return speedGame; } set { speedGame = value; } }
    public static GameManager instanse;
    [SerializeField] private Text score;
    [SerializeField] private Text bulletCount;
    [SerializeField] private GameObject panel;
    private int scoreCount;
    [SerializeField] private Text endScore;
    [SerializeField] private Text bestScore;
    [SerializeField] private Text startbestScore;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pauseMenu;
    private bool isStartGame = false;
    private RewardedAd rewardedAd;
    AdRequest request;
    [SerializeField] private Button bonusRewardPause;
    [SerializeField] private Button bonusRewardEnd;
    int time;
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 0;
        if (isStartGame)
        {
            StartGame();
            startPanel.SetActive(false);
        }
        instanse = this;
        speedGame /= 10;
    }

    void Start()
    {


        MobileAds.Initialize(initStatus => { });

        /*List<string> deviceIds = new List<string>();
        deviceIds.Add("2077ef9a63d2b398840261c8221a0c9b");
        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);*/
        //this.rewardedAd = new RewardedAd(adUnitId);
        //this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        //request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        //this.rewardedAd.LoadAd(request);
        CreateAndLoadRewardedAd();




        if (PlayerPrefs.HasKey("score"))
        {
            int bestScoreCount = PlayerPrefs.GetInt("score");
            bestScore.text = bestScoreCount.ToString();
            startbestScore.text = bestScore.text;
        }
       
        
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        player.GetComponent<Player>().AddBullet(5);
        player.GetComponent<Player>().AddGoldArmor();
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            speedGame /= 2f;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            speedGame *= 2f;
        }

        if(pauseMenu.active == true)
        {
            Time.timeScale = 0;
        }
        else if(pauseMenu.active == false)
        {
            Time.timeScale = 1;
        }
        
    }

    IEnumerator CreateObject()
    {
        yield return new WaitForSeconds (0.7f);
        while (true)
        {
           
            int index = UnityEngine.Random.Range(0, 11);
            if (index == 3 || index == 9)
            {
                Instantiate(gameObjects[index], spawnPoints[0].position, Quaternion.identity);

            }
            else if (index == 4 || index == 10)
            {
                int point = UnityEngine.Random.Range(0, 2);
                Instantiate(gameObjects[index], spawnPoints[point].position, Quaternion.identity);
            }

            else
            {
                int point = UnityEngine.Random.Range(0, 3);
                Instantiate(gameObjects[index], spawnPoints[point].position, Quaternion.identity);
               

            }
            yield return new WaitForSeconds(createTime);

        }
    }

    public void AddScore(int addScore)
    {
        scoreCount += addScore;
        score.text = scoreCount.ToString();
    }

    public void EndGame()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
        if (int.Parse(bestScore.text) < (int.Parse(score.text)))
            {
            bestScore.text = score.text;
            PlayerPrefs.SetInt("score", (int.Parse(score.text)));
        }
        endScore.text = score.text;
        panel.SetActive(true);
    }

  
   public void StartGame()
    {
        isStartGame = true;
        Time.timeScale = 1;
        StartCoroutine(CreateObject());
    }

    public void RestartGame()
    {
        if (player.GetComponent<Player>().GoldArmor == 3)
        {
            bonusRewardPause.gameObject.SetActive(false);
        }
        else if (player.GetComponent<Player>().GoldArmor == 0)
        {
            bonusRewardPause.gameObject.SetActive(true);
        }
        bonusRewardEnd.gameObject.SetActive(true);
        scoreCount = 0;
        score.text = scoreCount.ToString();
        Time.timeScale = 1;
        player.transform.position = new Vector3(1.3f, -6.08f, -1);
        player.SetActive(true);
        StartCoroutine(CreateObject());

       
    }

    public bool DestroyPlayer()
    {
        if (player.active == false)
        {
            return true;
        }
        return false;
    }

    public void OpenPauseMenu()
    {
        if (startPanel.active == false && panel.active == false)
        pauseMenu.SetActive(true);
    }

    private void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9652455907097724/7195867771";

#endif

        this.rewardedAd = new RewardedAd(adUnitId);


        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

   

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
     
        this.CreateAndLoadRewardedAd();
       
    }

    public void ShowAdAndReward()
    {
        UserChoseToWatchAd();
    }


   
    
}
