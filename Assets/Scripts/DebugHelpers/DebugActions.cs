#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace DebugHelpers
{
    public class DebugActions : MonoBehaviour
    {
        [SerializeField] private int actionButtonWidth = 50;
        [SerializeField] private int actionButtonHeight = 50;
        [SerializeField] private int actionButtonMargin = 20;
        private List<QuickActions> _quickActions = new List<QuickActions>();

        private void OnGUI()
        {
            //Doesnt seem to work ((
            //Todo try to find solution for debug actions
            var y = Screen.height - actionButtonMargin - actionButtonHeight;
            foreach (var quickAction in _quickActions)
            {
                if (GUI.Button(new Rect(actionButtonMargin, y, actionButtonWidth, actionButtonHeight), quickAction.Title))
                    quickAction.Action();
                y -= actionButtonHeight - actionButtonMargin;
            }
        }


        public void AddQuickAction(string title, Action action)
        {
            _quickActions.Add(new QuickActions
            {
                Title = title,
                Action = action
            });
        }

        private struct QuickActions
        {
            public string Title;
            public Action Action;
        }
    }
}