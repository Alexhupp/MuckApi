using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AlexMuckApi.API
{
    class ChatCommands
    {
        public static bool kill(string message)
        {
            PlayerStatus.Instance.Damage(0);
            return true;
        }
        public static bool debug(string message)
        {
            DebugNet.Instance.ToggleConsole();
            return true;
        }
        public static bool ping(string message)
        {
            ChatBox Chat = ChatBox.Instance;
            string text = "#" + ColorUtility.ToHtmlStringRGB(Chat.console);
            Chat.AppendMessage(-1, "<color=" + text + ">pong<color=white>", "");
            return true;
        }
        public static bool seed(string message)
        {
            ChatBox Chat = ChatBox.Instance;
            string text = "#" + ColorUtility.ToHtmlStringRGB(Chat.console);
            int seed = GameManager.gameSettings.Seed;
            Chat.AppendMessage(-1, string.Concat(new object[] { "<color=", text, ">Seed: ", seed, " (copied to clipboard)<color=white>" }), "");
            UnityEngine.GUIUtility.systemCopyBuffer = string.Concat(seed);
            return true;
        }
        public static bool help(string message)
        {
            ChatBox Chat = ChatBox.Instance;
            Debug.Log("[Command List]");
            IOrderedEnumerable<KeyValuePair<string, string>> sortedDict =
                Main.CommandInfo.OrderBy(i => i.Key.Length).ThenBy(i => i.Key);
            foreach (var item in sortedDict)
            {
                Debug.Log("[ " + char.ToUpper(item.Key[0]).ToString() + item.Key.Substring(1) + " ]: " + item.Value);
            }
            return true;
        }
    }
}
