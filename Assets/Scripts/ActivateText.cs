using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateText : MonoBehaviour
{
    //public GameObject buttonPrompt;

    public TextAsset text;
    [SerializeField] private TextAsset theText;
    public TextAsset winText;

    public int startLine;
    public int endLine;
    //public int winStartLine;
    //public int winEndLine;
    public int theTextStartLine;
    public int theTextEndLine;

    public TextAsset[] teenTextFiles;
    public TextAsset[] dadTextFiles;

    private string[] theTextLines;
    public string[] textLines;
    public string[] winTextLines;

    public string fileName = "teenDialog";

    public TextBoxManager theTextBox;

    //public PickupController pickupController;

    public bool requireButtonPress;
    private bool waitForPress;

    public bool destroyWhenActivated;

    // Start is called before the first frame update
    void Start()
    {
        //buttonPrompt.SetActive(false);
        theTextBox = FindObjectOfType<TextBoxManager>();

        if (text != null)
        {
            textLines = (text.text.Split('\n'));
        }

        if (endLine == 0)
        {
            endLine = textLines.Length - 1;
        }

        if (winText != null)
        {
            winTextLines = (winText.text.Split('\n'));
        }

        //if (winEndLine == 0)
        //{
        //    winEndLine = winTextLines.Length - 1;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForPress && Input.GetKeyDown(KeyCode.E))
        {
            if (!theTextBox.isActive)
            {
                //theTextBox.PlayTalkSound(this);
                theTextBox.isActive = true;
                theTextBox.ReloadScript(text);
                theTextBox.currentLine = startLine;
                theTextBox.endAtLine = endLine;
                theTextBox.EnableTextBox();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "OrboExpo2")
        {
            //buttonPrompt.SetActive(!theTextBox.isActive);
            if (requireButtonPress)
            {
                waitForPress = true;
                return;
            }
            else
            {
                theTextBox.ReloadScript(theText);
                theTextBox.currentLine = startLine;
                theTextBox.endAtLine = endLine;
                theTextBox.EnableTextBox();
            }


            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "OrboExpo2")
        {
            //buttonPrompt.SetActive(false);
            waitForPress = false;
        }
    }

    public void GetTextFile(TextAsset textFile)
    {
        theText = textFile;
        if (theText != null)
        {
            theTextLines = (theText.text.Split('\n'));
            //Debug.Log(theTextLines);
        }
        theTextEndLine = theTextLines.Length - 1;
        //Debug.Log(theTextLines.Length - 1);
        theTextBox.isTyping = false;
        theTextBox.ReloadScript(theText);
        theTextBox.currentLine = theTextStartLine;
        theTextBox.endAtLine = theTextEndLine;
        theTextBox.choice1.gameObject.SetActive(false);
        theTextBox.choice2.gameObject.SetActive(false);
        theTextBox.choice1.GetComponent<Button>().onClick.AddListener(delegate { GetTextFile(teenTextFiles[0]); });
        theTextBox.choice2.GetComponent<Button>().onClick.AddListener(delegate { GetTextFile(teenTextFiles[1]); });
        theTextBox.EnableTextBox();
    }
}

//most of this activate text file is made by following the tutorial by gamesplusjames on youtube https://www.youtube.com/watch?v=7KNQYPcx-uU