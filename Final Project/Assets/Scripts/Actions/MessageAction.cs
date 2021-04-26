﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageAction : Actions
{
    [Multiline(5)]
    [SerializeField] List<string> message;

    public override void Act()
    {
        //Debug.Log(message);
        DialogSystem.Instance.ShowMessages(message);
    }
}