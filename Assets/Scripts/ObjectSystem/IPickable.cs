using Unity.VisualScripting;
using UnityEngine;

namespace ObjectSystem
{
    public interface IPickable
    {
        void OnPicked();
        void Spawn(Transform position);

    }
}