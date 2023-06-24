using DebugHelpers;
using UI;
using UnityEngine;

namespace Core
{
    public class GameMaster : MonoBehaviour
    {
        public static GameMaster Instance { get; private set; }

        [Header("Player systems")]
        [Space(10)]
        [SerializeField] private SanityController sanityController;
        
        [Header("UI systems")]
        [Space(10)]
        [SerializeField] private SanityUIComponent sanityUI;
        
        [Header("Debug systems")]
        [Space(10)]
        [SerializeField] private DebugActions debug;

        
        
        public DebugActions Debug => debug;

        private void Awake()
        {
            var input = InputManager.Instance;
            
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        
        private void Start()
        {
            sanityUI.Init(sanityController);
        }
    }
}