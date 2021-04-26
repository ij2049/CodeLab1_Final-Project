using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; private set; }
    
    [SerializeField] private TMPro.TextMeshProUGUI messageText;
    [SerializeField] private GameObject panel;

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

    public void ShowMessages(List<string> messages)
    {
        currentMessages = messages;

        panel.SetActive(true);

        StartCoroutine(ShowMultipleMessages());
    }

    IEnumerator ShowMultipleMessages()
    {
        //change our text property to our current messages and index of our messages
        // which is 0. Press the space or mouse button -> show new message
        messageText.text = currentMessages[msgId];
        
        //while our message ID is less than currentMessages amounts check this
        //it's going to keep looping
        while (msgId < currentMessages.Count)
        {
            //press space or mouse button, show another messages
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                msgId++;

                if (msgId < currentMessages.Count)
                    messageText.text = currentMessages[msgId];

            }

            yield return null;

        }
        panel.SetActive(false);
        msgId = 0;
    }
}
