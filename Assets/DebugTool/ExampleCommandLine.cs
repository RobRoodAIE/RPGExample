using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExampleCommandLine : MonoBehaviour
{
    string input;

    public static ExampleDebugCommand ADD_XP;
    public static ExampleDebugCommand ADD_GOLD;
    public static ExampleDebugCommand<int> SET_GOLD;
    public static ExampleDebugCommand KILL;
    public static ExampleDebugCommand SPAWN_ITEM;
    public static ExampleDebugCommand HELP;

    public List<object> commandList;


    bool showConsole;
    bool showHelp;


    public void OnToggleCommandLine(InputValue value)
    {
        showConsole = !showConsole;
        showHelp = false;
    }

    public void OnReturn(InputValue value)
    {
        if (showConsole)
        {
            HandleInput();
        }
    }


    public void Awake()
    {
        ADD_XP = new ExampleDebugCommand("Add_Xp", "Add Expeience", "Add_Xp", () => RPGController.Instance.AddXP());

        ADD_GOLD = new ExampleDebugCommand("Add_Gold", "Add 1000 Gold", "Add_Gold", () => RPGController.Instance.AddGold(1000));

        SET_GOLD = new ExampleDebugCommand<int>("Set_Gold", "Add 1000 Gold", "Set_Gold", (x) => RPGController.Instance.SetGold(x));


        //KILL = new ExampleDebugCommand("Kill", "KILL <thing>", "Kill", () => RPGController.Instance.Kill(1000));

        //SPAWN_ITEM = new ExampleDebugCommand("Spawn", "Spawn <thing>", "Spawn", () => RPGController.Instance.SpawnItem(1000));

        HELP = new ExampleDebugCommand("help", "Show commands", "help", ()=> showHelp = true);

        commandList = new List<object>() { ADD_XP, ADD_GOLD, SET_GOLD, HELP };

    }

    Vector2 scroll;

    private void OnGUI()
    {
        if (!showConsole)
        {
            return;
        }

        float y = 0f;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, y, Screen.width - 20, 20 * commandList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y,Screen.width, 90 ), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                ExampleDebugCommandBase command = commandList[i] as ExampleDebugCommandBase;
                string label = $"{command.CommandFormat} - {command.CommandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }
            GUI.EndScrollView();
            y += 100;
        }


        GUI.Box(new Rect(0, y, Screen.width, 30), "");    //Start x, start y, width, height
        GUI.backgroundColor = Color.cyan;
        input = GUI.TextField(new Rect(10f, y +5, Screen.width - 15, 20), input);

    }


    void HandleInput()
    {
        string[] properties = input.Split(' '); 

        for (int i = 0;i < commandList.Count; i++)
        {
            ExampleDebugCommandBase commandBase = commandList[i] as ExampleDebugCommandBase;
            if (input.Contains(commandBase.CommandId))
            {
                if(commandList[i] as ExampleDebugCommand != null)
                {
                    (commandList[i] as ExampleDebugCommand).Invoke();
                }
                else if (commandList[i] as ExampleDebugCommand<int> != null)
                {
                    (commandList[i] as ExampleDebugCommand<int>).Invoke(int.Parse(properties[1]));
                }

                //jack's idea
                input = "";
            }
        }


    }


}
