using System;
using StatsSystem;
using UnityEngine;

namespace ObjectSystem
{
    public class PlayerPicker: MonoBehaviour
    {
        [field: SerializeField] public SanityController SanityController { get; private set; }
    }
}