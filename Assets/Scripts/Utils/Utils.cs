using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace daifuDemo
{
    public interface IUtils : IUtility
    {
        void AdjustContentHeight(RectTransform transform);
    }
    
    public class Utils : IUtils
    {
        public Dictionary<string, IUnRegister> UnRegisterList { get; }

        public void AdjustContentHeight(RectTransform transform)
        {
            float totalHeight = 0f;
            int activeChildAmount = 0;

            VerticalLayoutGroup verticalLayoutGroup = transform.GetComponent<VerticalLayoutGroup>();

            float topPadding = verticalLayoutGroup.padding.top;
            float bottomPadding = verticalLayoutGroup.padding.bottom;
            float spacing = verticalLayoutGroup.spacing;
            
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    RectTransform childRect = child.GetComponent<RectTransform>();
                    totalHeight += childRect.sizeDelta.y;
                    activeChildAmount++;
                }
            }

            totalHeight += (activeChildAmount - 1) * spacing + topPadding + bottomPadding;

            transform.sizeDelta = new Vector2(transform.sizeDelta.x, totalHeight);
        }
    }

    public static class UtilsExtension
    {
        public static Dictionary<string, IUnRegister> UnRegisters = new Dictionary<string, IUnRegister>();

        public static void AddUnRegister(this IUnRegister self, string key)
        {
            UnRegisters.Add(key, self);
        }

        public static void RemoveUnRegister(string key)
        {
            if (UnRegisters.ContainsKey(key))
            {
                UnRegisters[key].UnRegister();
                UnRegisters.Remove(key);
            }
        }
    }
}