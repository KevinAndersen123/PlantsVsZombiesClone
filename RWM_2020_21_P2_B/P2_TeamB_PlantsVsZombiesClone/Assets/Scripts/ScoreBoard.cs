using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ScoreBoardInfo
{
    public string name;
    public int enemiesKilled;
    public double time;
}

public class ScoreBoard : MonoBehaviour
{
    //Transform Container
    public Transform transformContainer;
    //Entry
    public Transform entry;

    //Max Scores showing
    public int maxEntrys = 5;

    //Height of Entry
    float heightOfEntry;

    //List of ScoreboardInfo
    private List<ScoreBoardInfo> scoreboardListInfo;
    private List<Transform> scoreboardListTransform;

    private void Awake()
    {
        //Dont show template score entry
        entry.gameObject.SetActive(false);

        //Get Height of score entrys
        heightOfEntry = entry.transform.GetComponent<RectTransform>().sizeDelta.y;

        //Fill list with default values
        scoreboardListInfo = new List<ScoreBoardInfo>()
        {
            new ScoreBoardInfo { name = "Tom", time = Random.Range(0,60), enemiesKilled = Random.Range(0,30) },
            new ScoreBoardInfo { name = "Kevin", time = Random.Range(0,60), enemiesKilled = Random.Range(0,30) },
            new ScoreBoardInfo { name = "Ash", time = Random.Range(0,60), enemiesKilled = Random.Range(0,30) },
            new ScoreBoardInfo { name = "Pete", time = Random.Range(0,60), enemiesKilled = Random.Range(0,30) },
            new ScoreBoardInfo { name = "Josh", time = Random.Range(0,60), enemiesKilled = Random.Range(0,30) },
        };


        //sortListByTime();
        scoreboardListTransform = new List<Transform>();

        ScoreBoardInfo newScore;
        newScore.name = "You";
        newScore.time = Data.s_levelTime;
        newScore.enemiesKilled = Data.s_zombiesKilled;

        playerEntry(newScore);

        

        /*
        foreach (ScoreBoardInfo scoreboardEntry in scoreboardListInfo)
        {
            highScoreEntry(scoreboardEntry, transformContainer, scoreboardListTransform);
        }
        */


    }

    private void highScoreEntry(ScoreBoardInfo scoreBoardInfo, Transform container, List<Transform> transforms )
    {
        Transform entryTransform = Instantiate(entry, container);
        RectTransform entryrect = entryTransform.GetComponent<RectTransform>();
        entryrect.anchoredPosition = new Vector2(0, -(transforms.Count) * heightOfEntry);
        entryTransform.gameObject.SetActive(true);

        //Fill with Test Values
        entryTransform.Find("Name").GetComponent<Text>().text = scoreBoardInfo.name;
        entryTransform.Find("Time").GetComponent<Text>().text = scoreBoardInfo.time.ToString();
        entryTransform.Find("Kills").GetComponent<Text>().text = scoreBoardInfo.enemiesKilled.ToString();

        transforms.Add(entryTransform);
    }

    private void sortListByTime()
    {
        //Sort the times highest to Time
        for (int i = 0; i < scoreboardListInfo.Count; i++)
        {
            for (int j = i + 1; j < scoreboardListInfo.Count; j++)
            {
                if (scoreboardListInfo[j].time > scoreboardListInfo[i].time)
                {
                    //Swap the placements
                    ScoreBoardInfo tempHolder = scoreboardListInfo[i];
                    scoreboardListInfo[i] = scoreboardListInfo[j];
                    scoreboardListInfo[j] = tempHolder;
                }
            }
        }
    }

    public void playerEntry(ScoreBoardInfo playersInfo)
    {
        scoreboardListInfo.Add(playersInfo);

        sortListByTime();
        
        foreach (ScoreBoardInfo scoreboardEntry in scoreboardListInfo)
        {
            highScoreEntry(scoreboardEntry, transformContainer, scoreboardListTransform);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //scoreBoardList = new List<ScoreBoardInfo>();
    }


    // Update is called once per frame
    void Update()
    {
        

    }
}
