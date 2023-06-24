using System;
using System.Collections.Generic;
using UnityEngine;

namespace DebugHelpers
{
    public class DebugActions : MonoBehaviour
    {
        private List<QuickActions> _quickActions = new List<QuickActions>();
        
        [SerializeField] private int actionButtonWidth = 50;
        [SerializeField] private int actionButtonHeight = 50;
        [SerializeField] private int actionButtonMargin = 20;
        
        public void AddQuickAction(string title, Action action)
        {
            _quickActions.Add(new QuickActions
            {
                Title = title,
                Action = action
            });
        }

        private void OnGUI()
        {
            var y = Screen.height - actionButtonMargin - actionButtonHeight;
            foreach (var quickAction in _quickActions)
            {
                if (GUI.Button(new Rect(actionButtonMargin, y, actionButtonWidth, actionButtonHeight), quickAction.Title))
                    quickAction.Action();
                y -= actionButtonHeight - actionButtonMargin;
            }
        }

        private struct QuickActions
        {
            public string Title;
            public Action Action;
        }
    }
}