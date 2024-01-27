using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] bool active;
    [SerializeField] int loseSceneID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            timer -= Time.deltaTime;

            // Lose things.
            if (timer < 0)
            {
                OnLose();
            }
        }
    }

    public void SetTimer(float time) // set timer to whatever time we tell it, if for some reason we need to change it.
    {
        timer = time;
    }

    public void SetActive(bool active) // Use this to set the timer active or not.
    {
        this.active = active;
    }

    private void OnLose() // if we lose, load the lose scene. We can change this to cause player to lose however we want.
    {
        SceneManager.LoadScene(loseSceneID);
    }
}
