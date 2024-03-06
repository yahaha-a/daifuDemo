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
}