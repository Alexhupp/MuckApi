using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            VERSION = "1.0.1";

        internal static Dictionary<string, Func<string,bool>> CustomCommands = new Dictionary<string, Func<string, bool>> { };
        internal static Dictionary<string, string> CommandInfo = new Dictionary<string, string> { };
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
            AddChatCommand("commands", "Shows a list of commands and their descriptions", new Func<string, bool>(API.ChatCommands.help));
            AddChatCommand("kill","Instantly kills the executing player", new Func<string, bool>(API.ChatCommands.kill));
            AddChatCommand("ping","Returns Pong", new Func<string, bool>(API.ChatCommands.ping));
            AddChatCommand("debug","Shows Debug information such as fps, ping, packets, etc...", new Func<string, bool>(API.ChatCommands.debug));
            AddChatCommand("seed","Shows the current run's Seed", new Func<string, bool>(API.ChatCommands.seed));
        }
        public static bool AddChatCommand(string name, Func<string,bool> function)
        {
            CustomCommands.Add(name, function);
            CommandInfo.Add(name, "Description Was Not Set By Mod");
            return true;
        }
        public static bool AddChatCommand(string name,string description, Func<string, bool> function)
        {
            CustomCommands.Add(name, function);
            CommandInfo.Add(name, description);
            return true;
        }
    }
}
