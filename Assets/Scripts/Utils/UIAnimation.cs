using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace GlideGame.Utils
{
    public static class UIAnimation
    {
        public static void AnimatePanel(this RectTransform panelTransform, Vector3 setScale, Vector3 targetScale, float animationDuration, Ease animationEase)
        {
            panelTransform.localScale = setScale;

            panelTransform.DOScale(targetScale, animationDuration)
                .SetEase(animationEase);
        }
    }
}
