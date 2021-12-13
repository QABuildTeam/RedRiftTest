using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class DragField : MonoBehaviour
    {
        private UniversalEventManager EventManager => GlobalManager.Instance.Get<UniversalEventManager>();

        private Transform selfTransform;
        private Transform SelfTransform
        {
            get
            {
                if (selfTransform == null)
                {
                    selfTransform = GetComponent<Transform>();
                }
                return selfTransform;
            }
        }

        private void Awake()
        {
            EventManager.Get<CardEvents>().BeginDrag += OnBeginDrag;
            EventManager.Get<CardEvents>().Drag += OnDrag;
            EventManager.Get<CardEvents>().EndDrag += OnEndDrag;
        }

        private Transform draggedObject = null;
        private void OnBeginDrag(Transform transform, Vector3 position)
        {
            draggedObject = transform;
            draggedObject.SetParent(SelfTransform, worldPositionStays: true);
        }

        private void OnDrag(Transform transform, Vector3 position)
        {
            if (draggedObject != null)
            {
                draggedObject.position = position;
            }
        }

        private void OnEndDrag(Transform transform, Vector3 position)
        {
            draggedObject = null;
        }
    }
}
