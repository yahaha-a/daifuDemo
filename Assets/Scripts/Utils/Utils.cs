using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using QFramework;
using UnityEngine;
using UnityEngine.UI;
using Timer = System.Threading.Timer;

namespace daifuDemo
{
    public interface IUtils : IUtility
    {
        void AdjustContentHeight(RectTransform transform);
        
        Sprite AdjustSprite(Texture2D texture);
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

        public Sprite AdjustSprite(Texture2D texture)
        {
            float pixelsPerUnit = 128;
            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f), pixelsPerUnit);
            return newSprite;
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

        public static ITimerPool TimerPool = new TimerPool();
    }
}