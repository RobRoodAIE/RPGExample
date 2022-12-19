using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandBase 
{
    private string _commandId;
    private string _commandDescription; //tell us what it is used for
    private string _commandFormat; //how its displayed in the help table

    public string CommandId           {get{return _commandId; } }
    public string CommandDescription  {get{return _commandDescription; } }
    public string CommandFormat       {get{return _commandFormat; } }

    public DebugCommandBase(string id, string description, string format)
    {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action command;

    public DebugCommand (string id, string description, string format, Action command) : base (id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}

//for generics
public class DebugCommand<T> : DebugCommandBase
{
    private Action<T> command;

    public DebugCommand(string id, string description, string format, Action<T> command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke(T value)
    {
        command.Invoke(value);
    }
}
