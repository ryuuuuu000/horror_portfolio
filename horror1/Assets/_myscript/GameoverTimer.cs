using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverTimer : MonoBehaviour
{
    public float gameovertime = 120;

    float nowtime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nowtime += Time.deltaTime;

        if (nowtime > gameovertime)
        {
            SceneManager.LoadScene("Gameover3");

        }
        Debug.Log(nowtime);
    }
}
