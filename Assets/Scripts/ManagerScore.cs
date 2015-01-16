using UnityEngine;
using System.Collections;

public class ManagerScore : MonoBehaviour
{

    GarbageCollector player1;
    GarbageCollector player2;
    int scoreJ1;
    int scoreJ2;
    public TextMesh textScoreJ1;
    public TextMesh textScoreJ2;
    public TextMesh time;
    public float timer = 120;


    void Start()
    {
        GameObject go1 = GameObject.Find("Player_1");
        GameObject go2 = GameObject.Find("Player_2");
        player1 = go1.GetComponent<GarbageCollector>();
        player2 = go2.GetComponent<GarbageCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        time.text = "Time : " + (Mathf.Floor(timer)).ToString();
        if (timer < 0)
        {
            time.text = "GAME OVER - PRESS ENTER";
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1;
                Application.LoadLevel("mainScene");
            }
        }
        textScoreJ1.text = "Score Orange : " + player1.getScore().ToString();
        textScoreJ2.text = "Score Pink : " + player2.getScore().ToString();
    }
}
