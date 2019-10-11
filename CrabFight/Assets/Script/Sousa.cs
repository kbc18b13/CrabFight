using UnityEngine;
using UnityEngine.SceneManagement;
public class Sousa : MonoBehaviour
{
    bool flag = true;
    public AudioClip modor;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("B_0") && flag == true)
        {
            audioSource.PlayOneShot(modor);
            flag = false;
        }
        else if(flag == false)
        {
                SceneManager.LoadScene("ka");
        }
    }
    public void SousaClick()
    {
        //タイトル画面に切り替え
        audioSource.PlayOneShot(modor);
        SceneManager.LoadScene("ka");
    }

}
