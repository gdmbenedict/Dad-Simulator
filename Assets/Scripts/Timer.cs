using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Serialized
    [SerializeField] float timer = 300;
    [SerializeField] bool active = false;
    [SerializeField] int loseSceneID = 2;

    // Backend
    TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        // Get timer text component from children. Please use prefab if able.
        timerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only do things if timer is supposed to be active.
        if (active)
        {
            timer -= Time.deltaTime;

            // Lose things.
            if (timer < 0)
            {
                OnLose();
            }
        }

        SetTimerText();
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

    private void SetTimerText() // Format time into text, and print to string.
    {
        float localTimerTime = timer;

        // Define various time elements.
        int minutes = 0, seconds = 0;
        while (localTimerTime > 60)
        {
            localTimerTime -= 60;
            minutes++;
        }
        seconds = (int)localTimerTime;

        string txt = $"Timer: {minutes}:{seconds}";

        timerText.text = txt;
    }
}
