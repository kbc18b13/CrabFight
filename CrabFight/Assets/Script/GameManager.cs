using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int nextGrade = 0;
    int[] junihyou = { 0, 0, 0, 0};

    public void Death(int padNum)
    {
        junihyou[nextGrade] = padNum;
        nextGrade++;
        if (nextGrade == PlayerCrab.GetKaniCount()-1)
        {
            
            UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
        }
    }
}
