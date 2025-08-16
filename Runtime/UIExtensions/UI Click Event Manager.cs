    using System;
    using dev.nicklaj.clibs.deblog;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UIElements;
    using VInspector;

    namespace dev.nicklaj.clibs.UIExtensions
    {
        [RequireComponent(typeof(UIDocument))]
        public class UIClickEventManager : MonoBehaviour
        {
            private static readonly string LOG_CATEGORY = "UI Extensions";
            private static readonly Color LOG_COLOR = Color.cyan;
            
            public ButtonClickEvent[] Buttons;
            public ToggleClickEvent[] Toggles;
            
            private Action[] _cachedActions;
            private EventCallback<ChangeEvent<bool>>[] _cachedCallbacks;
                
            private VisualElement _root;
        
            #region Mono Functions
            private void Awake()
            {
                _root = GetComponent<UIDocument>().rootVisualElement;
            }

            private void OnEnable()
            {
                RegisterListeners();
            }

            private void OnDisable()
            {
                UnregisterListeners();
            }
            #endregion

            #region UI Functions
            private void RegisterListeners()
            {
                _cachedActions = new Action[Buttons.Length];
                _cachedCallbacks = new EventCallback<ChangeEvent<bool>>[Toggles.Length];

                var i = 0;
                foreach (var btn in Buttons)
                {
                    Button button = _root.Query<Button>(btn.Query);
                    if (button == null)
                    {
                        Deblog.LogWarning($"Couldn't find any button queried as {btn.Query}. Skipping injection.", LOG_CATEGORY, LOG_COLOR);
                        continue;
                    }

                    _cachedActions[i] = () => btn.InvokeEvent();
                    button.clicked += _cachedActions[i];
                    
                    i++;
                }

                i = 0;
                foreach (var tgl in Toggles)
                {
                    Toggle toggle = _root.Query<Toggle>(tgl.Query);
                    if (toggle == null) 
                    {
                        Deblog.LogWarning($"Couldn't find any toggle queried as {tgl.Query}. Skipping injection.", LOG_CATEGORY, LOG_COLOR);
                        continue;
                    }
                    
                    EventCallback<ChangeEvent<bool>> callback = evt => tgl.InvokeEvent(evt.newValue);
                    _cachedCallbacks[i] = callback;
                    toggle.RegisterValueChangedCallback(_cachedCallbacks[i]);

                    i++;
                }
            }

            private void UnregisterListeners()
            {
                var i = 0;
                foreach (var btn in Buttons)
                {
                    Button button = _root.Query<Button>(btn.Query);
                    if (button == null) continue;
                    
                    button.clicked -= _cachedActions[i];
                    
                    i++;
                }

                i = 0;
                foreach (var tgl in Toggles)
                {
                    Toggle toggle = _root.Query<Toggle>(tgl.Query);
                    if (toggle == null) continue;
                    
                    toggle.UnregisterValueChangedCallback(_cachedCallbacks[i]);

                    i++;
                }
            }
            #endregion
        }
        
        #region Event Structs
        [Serializable]
        public struct ButtonClickEvent
        {
            [Tooltip("Query used to find the element.")]
            public string Query;
            [Tooltip("Event that will be triggered when the button is clicked.")]
            public UnityEvent Event;

            public void InvokeEvent() => Event.Invoke();
        }
        
        [Serializable]
        public struct ToggleClickEvent
        {
            [Tooltip("Query used to find the element.")]
            public string Query;
            [Tooltip("Event that will be triggered when the toggle is clicked.")]
            public UnityEvent<bool> Event;

            public void InvokeEvent(bool v) => Event.Invoke(v);
        }
        #endregion
    }