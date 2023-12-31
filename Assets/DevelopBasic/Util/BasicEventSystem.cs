using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A basic C# Event System
public static class EventHandler
{
    public static event Action E_OnDrawNewLine;
    public static void Call_OnDrawNewLine(){E_OnDrawNewLine?.Invoke();}
    public static event Action E_OnDrawSeveralLines;
    public static void Call_OnDrawSeveralLines(){E_OnDrawSeveralLines?.Invoke();}
    public static event Action E_OnPickUpMonitor;
    public static void Call_OnPickUpMonitor(){E_OnPickUpMonitor?.Invoke();}
    public static event Action<string> E_OnFeelWords;
    public static void Call_OnFeelWords(string fellContent){E_OnFeelWords?.Invoke(fellContent);}
    public static event Action E_OnCDPlaying;
    public static void Call_OnCDPlaying(){E_OnCDPlaying?.Invoke();}
    public static event Action E_OnFinishDrawLine;
    public static void Call_OnFinishDrawLine(){E_OnFinishDrawLine?.Invoke();}
    public static event Action E_OnQuestionAsked;
    public static void Call_OnQuestionAsked(){E_OnQuestionAsked?.Invoke();}
    public static event Action E_OnHandShake;
    public static void Call_OnHandShake(){E_OnHandShake?.Invoke();}
    public static event Action<AnswerSelect> E_OnSelectAnswer;
    public static void Call_OnSelectAnswer(AnswerSelect answer){E_OnSelectAnswer?.Invoke(answer);}
    public static event Action E_OnPressButton;
    public static void Call_OnPressButton(){E_OnPressButton?.Invoke();}
}

//A More Strict Event System
namespace SimpleEventSystem{
    public abstract class Event{
        public delegate void Handler(Event e);
    }
    public class E_OnTestEvent:Event{
        public float value;
        public E_OnTestEvent(float data){value = data;}
    }

    public class EventManager{
        private static  EventManager instance;
        public static EventManager Instance{
            get{
                if(instance == null) instance = new EventManager();
                return instance;
            }
        }

        private Dictionary<Type, Event.Handler> RegisteredHandlers = new Dictionary<Type, Event.Handler>();
        public void Register<T>(Event.Handler handler) where T: Event{
            Type type = typeof(T);

            if(RegisteredHandlers.ContainsKey(type)){
                RegisteredHandlers[type] += handler;
            }
            else{
                RegisteredHandlers[type] = handler;
            }
        }
        public void UnRegister<T>(Event.Handler handler) where T: Event{
            Type type = typeof(T);
            Event.Handler handlers;

            if(RegisteredHandlers.TryGetValue(type, out handlers)){
                handlers -= handler;
                if(handlers == null){
                    RegisteredHandlers.Remove(type);
                }
                else{
                    RegisteredHandlers[type] = handlers;
                }
            }
        }
        public void FireEvent<T>(T e) where T:Event {
            Type type = e.GetType();
            Event.Handler handlers;

            if(RegisteredHandlers.TryGetValue(type, out handlers)){
                handlers?.Invoke(e);
            }
        }
        public void ClearList(){RegisteredHandlers.Clear();}
    }
}
