using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MuckApi
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        #region[Declarations]
        public const string
            MODNAME = "MuckApi",
            AUTHOR = "MuckApiGithub",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "1.0.8";

        internal static Dictionary<string, Func<string, bool>> CustomCommands = new Dictionary<string, Func<string, bool>> { };
        internal static Dictionary<string, string> CommandInfo = new Dictionary<string, string> { };
        internal static bool itemsInitialized = false;
        internal static bool workBenchTabsUpdated = false;
        internal static bool anvilTabsUpdated = false;
        internal static Dictionary<int, List<InventoryItem>> craftableItems = new Dictionary<int, List<InventoryItem>>();
        internal static Dictionary<string, int> ItemTab = new Dictionary<string, int>();
        internal static int itemOffset;
        internal static int currentTab;
        internal static OtherInput.CraftingState currentCraftingState;
        //internal static InventoryItem[] items;
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
            
            itemOffset = ItemManager.Instance.allItems.Count - 1;
            craftableItems.Add(0, new List<InventoryItem>());
            craftableItems.Add(1, new List<InventoryItem>());
            craftableItems.Add(2, new List<InventoryItem>());
            craftableItems.Add(3, new List<InventoryItem>());
            harmony.PatchAll(assembly);
            AddChatCommand("commands", "Shows a list of commands and their descriptions", new Func<string, bool>(API.ChatCommands.help));
            AddChatCommand("kill", "Instantly kills the executing player", new Func<string, bool>(API.ChatCommands.kill));
            AddChatCommand("ping", "Returns Pong", new Func<string, bool>(API.ChatCommands.ping));
            AddChatCommand("debug", "Shows Debug information such as fps, ping, packets, etc...", new Func<string, bool>(API.ChatCommands.debug));
            AddChatCommand("seed", "Shows the current run's Seed", new Func<string, bool>(API.ChatCommands.seed));


        }
        public static bool AddChatCommand(string name, Func<string, bool> function)
        {
            CustomCommands.Add(name, function);
            CommandInfo.Add(name, "Description Was Not Set By Mod");
            return true;
        }
        public static bool AddChatCommand(string name, string description, Func<string, bool> function)
        {
            CustomCommands.Add(name, function);
            CommandInfo.Add(name, description);
            return true;
        }
        public static InventoryItem[] LoadAllItemsFromResoruce(string fileName, Assembly loader)
        {
            Debug.Log("[MuckAPI] Init CustomItems");

            var asset = GetAssetBundleFromResources(fileName, loader);
            var items = asset.LoadAllAssets<InventoryItem>();

            return items;
        }


        public static void setCraftingByName(InventoryItem[] items, string name,int tab)
        {

            InventoryItem item = null;
            for (int i = 0; i < items.Length; i++)
            {
                Debug.Log(items[i].name + " | " + name);
                if (items[i].name == name)
                {
                    item = items[i];
                }
            }
            if (item != null)
            {
                ItemTab.Add(item.name, tab);
                item.id = itemOffset += 1;
                Debug.Log($"Custom Item: {item.id} {item.name} ID: {item.id}");

                if (item.craftable)
                {
                    Debug.Log("Requirement Name: " + item.stationRequirement.name);
                    switch (item.stationRequirement.name)
                    {
                        case ("Workbench"):
                            Debug.Log("Added to Workbench Craftables");
                            craftableItems[0].Add(item);
                            break;
                        case ("Anvil"):
                            Debug.Log("Added to Anvil Craftables");
                            craftableItems[1].Add(item);
                            break;
                        case ("Cauldron"):
                            craftableItems[2].Add(item);
                            break;
                        case ("Fletching Table"):
                            craftableItems[3].Add(item);
                            break;
                    }
                }
            }
        }

        public static void InitItems(InventoryItem[] items) {                

                

            ItemManager.Instance.allScriptableItems = ItemManager.Instance.allScriptableItems.Concat(items).ToArray();

            for (int i = 0; i < items.Length; i++) {

                Debug.Log(items[i].id);
                ItemManager.Instance.allItems.Add(items[i].id, items[i]);
            }
        }

        public static AssetBundle GetAssetBundleFromResources(string fileName, Assembly loader)
        {
            var execAssembly = loader;

            var resourceName = execAssembly.GetManifestResourceNames().Single(str => str.EndsWith(fileName));

            using (var stream = execAssembly.GetManifestResourceStream(resourceName))
            {
                return AssetBundle.LoadFromStream(stream);
            }
        }



    }

}
