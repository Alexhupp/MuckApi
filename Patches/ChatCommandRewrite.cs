using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace AlexMuckApi.Patches
{
    [HarmonyPatch(typeof(ChatBox), "ChatCommand")]
    class ChatCommandRewrite
    {
        private static string[] MessageArgsExternal;
        [HarmonyPrefix]
        private static bool CustomCommands(string message)
        {
            ChatBox Chat = ChatBox.Instance;
            var MessageArgs = message.Substring(1).Split(' ');
            Chat.ClearMessage();
            string text = "#" + ColorUtility.ToHtmlStringRGB(Chat.console);
            foreach (var item in Main.CustomCommands)
            {
                if (item.Key == MessageArgs[0])
                {
                    MessageArgsExternal = MessageArgs;
                    Debug.Log("Check Happened?????????????????");
                    item.Value.DynamicInvoke(message);
                }
            }
            switch (MessageArgs[0])
            {

                case "seed":
                    int seed = GameManager.gameSettings.Seed;
                    Chat.AppendMessage(-1, string.Concat(new object[] { "<color=", text, ">Seed: ", seed, " (copied to clipboard)<color=white>" }), "");
                    UnityEngine.GUIUtility.systemCopyBuffer = string.Concat(seed);
                    break;
                case "ping":
                    Chat.AppendMessage(-1, "<color=" + text + ">pong<color=white>", "");
                    break;
                case "debug":
                    DebugNet.Instance.ToggleConsole();
                    break;
                case "kill":
                    PlayerStatus.Instance.Damage(0);
                    break;
            }
            return false;
        }
    }
}
