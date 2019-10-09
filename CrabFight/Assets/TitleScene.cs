using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    Button start;
    Button sousa;
    // Start is called before the first frame update
    void Start()
    {
        //startbuttunのボタンオブジェクトの取得。
        start=GameObject.Find("/Canvas/start").GetComponent<Button>();
        //sousabuttunのボタンオブジェクトの取得。
        sousa = GameObject.Find("/Canvas/sousa").GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            //Aが押されたら非表示
            start.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //Bが押されたら表示
            start.enabled = true;
        }
            // SceneManager.LoadScene("Number");

}
}
