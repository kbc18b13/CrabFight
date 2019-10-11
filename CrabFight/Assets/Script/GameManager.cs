using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public AudioSource _audio;

    int nextGrade = 0;
    int[] junihyou = { 0, 0, 0};
    int winnerNum = 0;

    bool end = false;
    float endTime = 2.0f;
    float _audioMax = 0.32f;

    const float c_startTime = 0.5f;
    float startTime = c_startTime;

    public void Awake()
    {
        nextGrade = KaniGenerator.GetKaniKazu() - 2;
        winnerNum = (KaniGenerator.GetKaniKazu() - 1) * KaniGenerator.GetKaniKazu() / 2;
    }

    public void Update()
    {
        if(startTime > 0)
        {
            image.color = new Vector4(1, 1, 1, (startTime) / c_startTime);
            _audio.volume = _audioMax * ((c_startTime - startTime) / c_startTime);
            startTime -= Time.deltaTime;
            if(startTime <= 0)
            {
                startTime = 0;
            }
        }

        if (end)
        {
            endTime -= Time.deltaTime;
            image.color = new Vector4(1, 1, 1, (2.0f - endTime) / 2);
            _audio.volume = _audioMax * (endTime/ 2);
            if (endTime <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
            }
        }
    }

    public void Death(int padNum)
    {
        if (nextGrade != -1)
        {
            junihyou[nextGrade] = padNum;
            nextGrade--;
            winnerNum -= padNum;
            if (nextGrade == -1)
            {
                Result.First = winnerNum;
                Result.Second = junihyou[0];
                Result.Third = junihyou[1];
                Result.Fourth = junihyou[2];
                end = true;
                //Debug.Log(Result.First + "," + Result.Second + "," + Result.Third + "," + Result.Fourth);
            }
        }
    }
}
