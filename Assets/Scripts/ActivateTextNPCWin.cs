using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateTextNPCWin : MonoBehaviour
{
    //public GameObject buttonPrompt;

    //public TextAsset theText;
    //public TextAsset winText;

    //public int startLine;
    //public int endLine;
    //public int winStartLine;
    //public int winEndLine;

    //public string[] textLines;
    //public string[] winTextLines;

    //public TextBoxManager theTextBox;

    //public PickupController pickupController;

    //public bool requireButtonPress;
    //private bool waitForPress;

    //public bool destroyWhenActivated;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    buttonPrompt.SetActive(false);
    //    theTextBox = FindObjectOfType<TextBoxManager>();

    //    if (theText != null)
    //    {
    //        textLines = (theText.text.Split('\n'));
    //    }

    //    if (endLine == 0)
    //    {
    //        endLine = textLines.Length - 1;
    //    }

    //    if (winText != null)
    //    {
    //        winTextLines = (winText.text.Split('\n'));
    //    }

    //    if (winEndLine == 0)
    //    {
    //        winEndLine = winTextLines.Length - 1;
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (waitForPress && Input.GetKeyDown(KeyCode.E))
    //    {
    //        if (!theTextBox.isActive)
    //        {
    //            if (PlayerPrefs.GetInt(pickupController.levelName) >= pickupController.numberOfPickups)
    //            {
    //                theTextBox.PlayTalkSound(this);
    //                theTextBox.isActive = true;
    //                theTextBox.ReloadScript(winText);
    //                theTextBox.currentLine = winStartLine;
    //                theTextBox.endAtLine = winEndLine;
    //                theTextBox.EnableTextBox();
    //            }
    //            else
    //            {
    //                theTextBox.PlayTalkSound(this);
    //                theTextBox.isActive = true;
    //                theTextBox.ReloadScript(theText);
    //                theTextBox.currentLine = startLine;
    //                theTextBox.endAtLine = endLine;
    //                theTextBox.EnableTextBox();
    //            }

    //        }

    //        if (destroyWhenActivated)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.name == "OrboExpo2")
    //    {
    //        buttonPrompt.SetActive(!theTextBox.isActive);
    //        if (requireButtonPress)
    //        {
    //            waitForPress = true;
    //            return;
    //        }
    //        else
    //        {
    //            theTextBox.ReloadScript(theText);
    //            theTextBox.currentLine = startLine;
    //            theTextBox.endAtLine = endLine;
    //            theTextBox.EnableTextBox();
    //        }


    //        if (destroyWhenActivated)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.name == "OrboExpo2")
    //    {
    //        buttonPrompt.SetActive(false);
    //        waitForPress = false;
    //    }
    //}
}

//most of this activate text file is made by following the tutorial by gamesplusjames on youtube https://www.youtube.com/watch?v=7KNQYPcx-uU