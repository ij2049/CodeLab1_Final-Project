using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageAction : Actions
{
    [Multiline(5)]
    [SerializeField] List<string> message;

    [SerializeField] private bool enableDialog;
    [SerializeField] private string yesText, noText;
    [SerializeField] private List<Actions> yesActions, noActions;

    public override void Act()
    {
        //Debug.Log(message);
        DialogSystem.Instance.ShowMessages(message, enableDialog, yesActions, noActions, yesText, noText);
    }
}