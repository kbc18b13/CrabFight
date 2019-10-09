using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//ＵＩコンポーネント使用。
public class R_Button : MonoBehaviour
{
    Button button1;
    Button button2;

    // Start is called before the first frame update
    void Start()
    {
        //ボタンコンポーネントの取得。
        button1 = GameObject.Find("Canvas/Button1").GetComponent<Button>();
        button2 = GameObject.Find("Canvas/Button2").GetComponent<Button>();

        //最初に選択状態にしたいボタンの設定。
        Selectable sel = GetComponent<Selectable>();
        button1.Select();
   
        
    }

    // Update is called once per frame
    void Update()
    {
        



    }
}
