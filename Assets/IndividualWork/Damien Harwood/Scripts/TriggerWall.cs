using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamienHarwood
{
    public class TriggerWall : MonoBehaviour
    {
        private GameObject _gameObject;
        private Vector3 endPos;
        private Vector3 startPos;
        private Vector3 objPos;
        public float moveDuration;
        private float elapsedTime;
        private float percentComplete;


        public GameObject endPosMarker;

        // Start is called before the first frame update
        void Start()
        {
            GetPositions();
        }

        private void GetPositions()
        {
            // Sets the start position for the current move cycle to the current position and gets the position of the end marker
            startPos = transform.position;
            endPos = endPosMarker.transform.position;
        }


        // Update is called once per frame
        void Update()
        {
            elapsedTime += Time.deltaTime;
            float percentComplete = elapsedTime / moveDuration;
            transform.position = Vector3.Lerp(startPos, endPos, percentComplete);
        }

        void TryMove()
        {
            
        }
    }
}