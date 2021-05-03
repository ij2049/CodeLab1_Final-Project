﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; private set; }
    
    [SerializeField] private TMPro.TextMeshProUGUI messageText, yesText, noText;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button yesButton, noButton;

    private List<string> currentMessages = new List<string>();
    private int msgId = 0;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        panel.SetActive(false);
    }

    public void ShowMessages(List<string> messages, bool dialog, List<Actions> yesActions = null, List<Actions> noActions = null, string yes = "Yes", string no = "No")
    {
        msgId = 0;
        
        yesButton.transform.parent.gameObject.SetActive(false);
        
        currentMessages = messages;

        panel.SetActive(true);

        if (dialog)
        {
            yesText.text = yes;
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(delegate
            {
                panel.SetActive(false);
                
                if (yesActions != null)
                AssignActionsToButtons(yesActions);
                
            });
            noText.text = no;
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(delegate
            {
                panel.SetActive(false);
                
                if (noActions != null)
                AssignActionsToButtons(noActions);
            
            });
        }
        
        StartCoroutine(ShowMultipleMessages(dialog));
    }

    IEnumerator ShowMultipleMessages(bool useDialog)
    {
        //change our text property to our current messages and index of our messages
        // which is 0. Press the space or mouse button -> show new message
        messageText.text = currentMessages[msgId];
        
        //while our message ID is less than currentMessages amounts check this
        //it's going to keep looping
        while (msgId < currentMessages.Count)
        {
            //press space or mouse button, show another messages
            if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0) && Extensions.IsMouseOverUI()))
            {
                msgId++;

                if (msgId < currentMessages.Count)
                    messageText.text = currentMessages[msgId];
                
                if(useDialog && msgId == currentMessages.Count - 1)
                    yesButton.transform.parent.gameObject.SetActive(true);

            }

            yield return null;

        }

        if (!useDialog)
        {
            panel.SetActive(false);
        }
        

    }

    void AssignActionsToButtons(List<Actions> actions)
    {
        List<Actions> localActions = actions;

        for (int i = 0; i < localActions.Count; i++)
        {
            localActions[i].Act();
        }
            
    }

    public void HideDialog()
    {
        panel.SetActive(false);
    }
}
