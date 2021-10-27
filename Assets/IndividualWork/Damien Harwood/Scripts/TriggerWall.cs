using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamienHarwood
{
    public class TriggerWall : MonoBehaviour
    {
        
        private Vector3 currentPos;
        private bool wallMoving;
        
        [Range(0, 10)]
        public int breatheInSeconds;
        [Range(0, 10)]
        public int pauseAfterBreathIn;
        [Range(0, 10)]
        public int breatheOutSeconds;
        [Range(0, 10)]
        public int pauseAfterBreathOut;
        public int moveSpeed;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(BreathePattern());
        }
        
        IEnumerator BreathePattern()
        {
            while (true)
            {   
                // Start Breathing in
                wallMoving = false;
                yield return new WaitForSeconds(breatheInSeconds);
                // Breathe in finished
                
                // Start holding Breath

                yield return new WaitForSeconds(pauseAfterBreathIn);
                // Holding breath finished
                
                // Start breathing in
                wallMoving = true;
                yield return new WaitForSeconds(breatheOutSeconds);
                // Breathe out finished
                
                // Start holding breath out
                yield return new WaitForSeconds(pauseAfterBreathOut);
                // Holding breath out finished
                
            }
        }


        // Update is called once per frame
        void Update()
        {
            if (wallMoving)
            {
                transform.position += transform.forward * (moveSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ItemShaderTransition>())
            {
                other.GetComponent<ItemShaderTransition>().ChangeColor();
            }
        }
    }
}