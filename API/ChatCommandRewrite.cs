using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace AlexMuckApi.API
{
    [HarmonyPatch(typeof(ChatBox), "ChatCommand")]
    class ChatCommandRewrite
    {
        [HarmonyPrefix]
        private static bool CustomCommands(string message)
        {
            foreach (var item in Main.CustomCommands)
            {
                if (item.Key == message.Substring(1).Split(' ')[0].ToLower())
                {
                    Debug.Log("Command Found" + message.Substring(1).Split(' ')[0]);
                    item.Value.DynamicInvoke(message.ToString());
                }
            }
            ChatBox.Instance.ClearMessage();
            return false;
        }
    }
}
