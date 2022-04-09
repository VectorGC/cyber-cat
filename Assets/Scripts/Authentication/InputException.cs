using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputException : Exception
{
    public InputException(string message) : base(message)
    {
    }
}
