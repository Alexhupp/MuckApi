using System;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MuckApi.API
{

    [HarmonyPatch(typeof(GameManager), "Awake")]
    class ItemAPI : MonoBehaviour
    {
        static void Prefix()
        {
            Debug.Log("[MuckAPI] Init CustomItems");
            List<InventoryItem> items = Main.instance.items;

            ItemManager.Instance.allScriptableItems = ItemManager.Instance.allScriptableItems.Concat(items).ToArray();

            for (int i = 0; i < items.Count; i++)
            {
                ItemManager.Instance.allItems.Add(items[i].id, items[i]);
            }
        }
    }
}
