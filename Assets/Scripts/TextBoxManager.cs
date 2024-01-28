using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBox;

    public TextMeshProUGUI theText;

    public int currentLine;
    public int endAtLine;

    //public GameObject choice1;
    //public GameObject choice2;

    //public GameObject winTextObject;
    //public GameObject continueButton;

    public bool canMove;

    //public ActivateTextNPCWin NPC;

    public FirstPersonController player;
    //public PickupController pickupController;

    //public AudioSource audioSource;
    //public AudioClip[] captainTalkSounds;
    //public AudioClip[] oldManTalkSounds;

    //public Rigidbody rb;

    public TextAsset textFile;
    public string[] textLines;

    public bool isActive;

    public bool isTyping = false;
    private bool cancelTyping = false;

    public float typeSpeed;

    public bool stopPlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        //choice1.SetActive(false);
        //choice2.SetActive(false);

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if (isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }

    }

    private void Update()
    {
        if (canMove == false)
        {
            //rb.isKinematic = true;
            player.MoveSpeed = 0;
            player.SprintSpeed = 0;
        }
        else if (canMove == true)
        {
            //rb.isKinematic = false;
            player.MoveSpeed = 4;
            player.SprintSpeed = 6;
        }

        if (Input.GetKeyDown(KeyCode.Return) && textBox.activeSelf)
        {
            if (!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                        DisableTextBox();
                }
                else
                {
                    //PlayTalkSound(NPC);
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }
        theText.text = lineOfText;
            //choice1.SetActive(true);
            //choice2.SetActive(true);
        isTyping = false;
        cancelTyping = false;
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;

        if (stopPlayerMovement)
        {
            canMove = false;
        }

        StartCoroutine(TextScroll(textLines[currentLine]));
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;
        canMove = true;
    }

    //public void PlayTalkSound(ActivateTextNPCWin NPC)
    //{
    //    if (NPC.gameObject.CompareTag("Captain"))
    //    {
    //        audioSource.PlayOneShot(captainTalkSounds[Random.Range(0, captainTalkSounds.Length)]);
    //    }
    //    else if (NPC.gameObject.CompareTag("OldMan"))
    //    {
    //        audioSource.PlayOneShot(oldManTalkSounds[Random.Range(0, oldManTalkSounds.Length)]);
    //    }
    //}

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
        }
    }
}
