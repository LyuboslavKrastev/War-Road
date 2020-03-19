using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarRoad.Interfaces;

namespace WarRoad.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction _currentAction;
        public void StartAction(IAction action)
        {
            if (_currentAction == action)
            {
                return;
            }
            if (_currentAction != null)
            {
                _currentAction.Cancel();
            }
            _currentAction = action;

        }
    }

}