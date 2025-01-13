using System;
using UnityEngine;

namespace Gameplay.Events
{
    public interface IInteractable
    {
        public int ID { get; }
        public Vector2 WorldCoords { get; }

        public Action Action { get; }
        public bool NeedConfirmation { get; }
    }
}