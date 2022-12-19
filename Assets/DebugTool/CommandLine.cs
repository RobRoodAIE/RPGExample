using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommandLine : MonoBehaviour
{
    bool showConsole;
    bool showHelp;
    string input;


    public static DebugCommand ADD_XP;
    public static DebugCommand ADD_GOLD;
    public static DebugCommand<int> SET_GOLD;
    public static DebugCommand HELP;

    public List<object> commandList;

    //using new input system
    public void OnToggleCommandLine(InputValue value)
    {
        showConsole = !showConsole;
        //reset showing commands
        showHelp = false;
    }

    public void OnReturn(InputValue value)
    {
        if (showConsole)
        {
            HandleInput();
        }
    }

    private void Awake()
    {

        ADD_XP = new DebugCommand("Add_Xp", "Add Experince!", "Add_Xp", () =>
        {
            RPGController.Instance.AddXP();
        });


        ADD_GOLD = new DebugCommand("Add_Gold", "Add gold!", "Add_Gold", () =>
        {
            RPGController.Instance.AddGold(1000);
        });

        SET_GOLD = new DebugCommand<int>("Set_Gold", "Sets ammount of gold!", "Set_Gold to <gold_ammount>", (x) =>
        {
            RPGController.Instance.SetGold(x);
        });

        HELP = new DebugCommand("help", "Show Commands", "help", () =>
        {
            showHelp = true; 
        });

        commandList = new List<object>
        {
            ADD_XP, ADD_GOLD, SET_GOLD, HELP
        };
    }


    //for GUI scrolling
    Vector2 scroll;

    private void OnGUI()
    {
        if (!showConsole)
        {
            return;
        }

        float y = 0f; //starting UI Y position

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y, Screen.width, 90), scroll, viewport);

            for (int i = 0; i<commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.CommandFormat} - {command.CommandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }
            GUI.EndScrollView();
            y += 100;  //offset UI U 
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");    //Start x, start y, width, height
        GUI.backgroundColor = Color.cyan;
        input = GUI.TextField(new Rect(10f, y + 5, Screen.width - 15, 20), input);
    }

    /*
    void HandleInput()
    {
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.CommandId))
            {
                //cast and invoke
                (commandList[i] as DebugCommand).Invoke();
            }
        }
    }
    */


    //for spliting strings
    void HandleInput()
    {
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.CommandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    //cast and invoke
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    //cast and invoke
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }


                //jack's idea
                input = "";
            }
        }
    }

}