using UnityEngine;
using UnityEngine.SceneManagement;
public class Sousa : MonoBehaviour
{
    bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("B_0") && flag == true)
        {
            SceneManager.LoadScene("ka");
            flag = false;
        }

    }
    public void backClick()
    {
    }

}
