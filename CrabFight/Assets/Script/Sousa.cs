﻿using UnityEngine;
using UnityEngine.SceneManagement;
public class Sousa : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Bボタンが押されるとタイトルに戻る。
        if(Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("ka");
        }
    }
}