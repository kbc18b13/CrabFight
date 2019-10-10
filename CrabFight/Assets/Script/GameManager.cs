﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int nextGrade = 0;
    int[] junihyou = { 0, 0, 0};
    int winnerNum = 0;

    public void Awake()
    {
        nextGrade = PlayerCrab.GetKaniCount() - 2;
        winnerNum = (PlayerCrab.GetKaniCount() - 1) * PlayerCrab.GetKaniCount() / 2;
    }

    public void Death(int padNum)
    {
        //Debug.Log("落ちた" + padNum);
        junihyou[nextGrade] = padNum;
        nextGrade--;
        winnerNum -= padNum;
        if (nextGrade == -1)
        {
            Result.First = winnerNum;
            Result.Second = junihyou[0];
            Result.Third = junihyou[1];
            Result.Fourth = junihyou[2];
            UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
            //Debug.Log(Result.First + "," + Result.Second + "," + Result.Third + "," + Result.Fourth);
        }
    }
}
