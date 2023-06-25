using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace UI
{
    public class CursorUIComponent: MonoBehaviour
    {
        public void Lock()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public void Unlock()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}