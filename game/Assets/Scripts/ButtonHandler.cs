using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    public void playGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void gosettings()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void gomenu()
    {
        SceneManager.LoadSceneAsync(2);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
