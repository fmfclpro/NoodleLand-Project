using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/*
MIT License

Copyright (c) 2023 Filipe Lopes | FMFCLPRO

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace NoodleLand.Events
{
    public static class FakeEventBus
    {
        public static void Subscribe<T>(object listener) where T : IEvent
        {
            string eventName = typeof(T).Name;

            if (!_eventListenerWrappers.ContainsKey(eventName))
            {
                _eventListenerWrappers[eventName] = new List<EventListenerWrapper>();
            }

            string b0 = $"On{eventName}";
            string methodName = b0.Replace("Event", "");

            var method = listener.GetType().GetMethod(methodName);
            Debug.Log(method);
            if (method != null)
            {
                List<EventListenerWrapper> listenerWrappers = _eventListenerWrappers[eventName];
                listenerWrappers.Add(new EventListenerWrapper(listener, methodName, method));
            }
        }

        public static void Unregister(object listener)
        {
            //TODO ADD Unregister
        }

        public static void InvokeEvent<T>(T e) where T : IEvent
        {
            string eventName = typeof(T).Name;
            if (!_eventListenerWrappers.ContainsKey(eventName)) return;

            var a = _eventListenerWrappers[eventName];
            for (var i = 0; i < a.Count; i++)
            {
                a[i].Invoke(e);
                Debug.Log(i);
            }
        }


        private static Dictionary<string, List<EventListenerWrapper>> _eventListenerWrappers =
            new Dictionary<string, List<EventListenerWrapper>>();

        private static List<EventListenerWrapper> listeners = new List<EventListenerWrapper>();

        private sealed class EventListenerWrapper
        {
            public object Listener { get; private set; }
            public Type EventType { get; private set; }

            private MethodBase method;

            public EventListenerWrapper(object listener, string eventName, MethodBase method)
            {
                Listener = listener;
                this.method = method;
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length != 1)
                    EventType = parameters[0].ParameterType;
            }

            public void Invoke(object e)
            {
                method.Invoke(Listener, new[] { e });
            }
        }
    }
}