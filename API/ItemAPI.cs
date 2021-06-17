using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MuckApi.API
{
    [HarmonyPatch(typeof(CraftingUI), "UpdateCraftables")]
    class ItemAPI1
    {

        static void Prefix(CraftingUI __instance)
        {
            Debug.Log("Craftables Updated");
            ItemsFunctions.manageCraftables(__instance);
        }
    }

    [HarmonyPatch(typeof(OtherInput), "FindCurrentCraftingState")]
    class ItemAPI2
    {

        static void Prefix(OtherInput __instance)
        {
            Main.currentCraftingState = __instance.craftingState;
        }
    }

    [HarmonyPatch(typeof(CraftingUI), "OpenTab")]
    class ItemAPI4
    {
        public static void Prefix(int i)
        {
            Main.currentTab = i;
        }
    }

    class ItemsFunctions {
        internal static void manageCraftables(CraftingUI instance)
        {

            List<InventoryItem> addItems = new List<InventoryItem>();

            switch (Main.currentCraftingState)
            {
                case OtherInput.CraftingState.Workbench:
                    if (!Main.workBenchTabsUpdated)
                    {
                        for (int i = 0; i < Main.craftableItems[0].Count; i++)
                        {
                            //instance.tabs[currentTab].items[instance.tabs[currentTab].items.Length - 1] != craftableItems[0][craftableItems[0].Length - 1]
                            Debug.Log("Workbench Items Updated! " + i);
                            if (Main.ItemTab[Main.craftableItems[0][i].name] == Main.currentTab)
                            {
                                addItems.Add(Main.craftableItems[0][i]);
                                Main.workBenchTabsUpdated = true;
                            }
                        }
                        instance.tabs[Main.currentTab].items = instance.tabs[Main.currentTab].items.Concat(addItems).ToArray();
                    }
                    break;
                case OtherInput.CraftingState.Anvil:
                    if (!Main.anvilTabsUpdated)
                    {
                        for (int i = 0; i < Main.craftableItems[1].Count; i++)
                        {
                            Debug.Log("Anvil Item Last Char: " + Main.craftableItems[1][i].description[Main.craftableItems[1][i].description.Length - 1].ToString());
                            Debug.Log("Current Tab: " + Main.currentTab);
                            if (Main.ItemTab[Main.craftableItems[1][i].name] == Main.currentTab)
                            {
                                Debug.Log("Anvil Item Added");
                                addItems.Add(Main.craftableItems[1][i]);
                                Main.anvilTabsUpdated = true;
                            }
                            instance.tabs[Main.currentTab].items = instance.tabs[Main.currentTab].items.Concat(addItems).ToArray();
                        }
                    }
                    break;
                case OtherInput.CraftingState.Cauldron:
                    break;
                case OtherInput.CraftingState.Fletch:
                    break;
            }
        }
    }
}
