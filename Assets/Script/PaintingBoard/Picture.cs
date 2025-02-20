using System.Collections;
using UnityEngine;

namespace PaintingBoard
{
    public class Picture : MonoBehaviour, IPickable
    {
        public GameObject GameObject => throw new System.NotImplementedException();

        public void Drop()
        {
            throw new System.NotImplementedException();
        }

        public void Pickup()
        {
            throw new System.NotImplementedException();
        }
    }
}