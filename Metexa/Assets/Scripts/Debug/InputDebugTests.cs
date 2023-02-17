using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MetaHexa.Debug
{
    public class InputDebugTests : MonoBehaviour
    {
        [Serializable]
        private struct InputTest
        {
            public KeyCode key;
            public UnityEvent onPush;
            public UnityEvent whilePushed;
            public UnityEvent onStopPushing;        
        }

        [SerializeField] private List<InputTest> _inputTests;
        
        void Update()
        {
            foreach (var inputTest in _inputTests)
            {
                if(UnityEngine.Input.GetKeyDown(inputTest.key)) inputTest.onPush.Invoke();
                if(UnityEngine.Input.GetKey(inputTest.key)) inputTest.whilePushed.Invoke();
                if(UnityEngine.Input.GetKeyUp(inputTest.key)) inputTest.onStopPushing.Invoke();
            }
        }
    }
}
