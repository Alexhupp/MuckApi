using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AlexMuckApi
{   
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        #region[Declarations]
        public const string
            MODNAME = "MuckApi",
            AUTHOR = "YaBoiAlex",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "1.0";
        internal static Dictionary<string, Func<string,bool>> CustomCommands = new Dictionary<string, Func<string, bool>> { };
        internal readonly ManualLogSource log;
        internal readonly Harmony harmony;
        internal readonly Assembly assembly;
        public readonly string modFolder;

        #endregion

        // Custom classes
        
        public Main()
        {
            log = Logger;
            harmony = new Harmony(GUID);
            assembly = Assembly.GetExecutingAssembly();
            modFolder = Path.GetDirectoryName(assembly.Location);
        }

        public void Start()
        {
            harmony.PatchAll(assembly);
            AddChatCommand("kill", new Func<string, bool>(API.ChatCommands.kill));
            AddChatCommand("ping", new Func<string, bool>(API.ChatCommands.ping));
            AddChatCommand("debug", new Func<string, bool>(API.ChatCommands.debug));
            AddChatCommand("seed", new Func<string, bool>(API.ChatCommands.seed));
        }
        public static bool AddChatCommand(string name, Func<string, bool> function)
        {
            CustomCommands.Add(name, function);
            return true;
        }
    }
}
