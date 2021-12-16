using System.Collections;
using UnityEngine;

namespace DamienHarwood
{
    public class TriggerWall : MonoBehaviour
    {
        
        private Vector3 currentPos;
        private bool wallMoving;
        
        public int startDelay;
        [Range(0, 10)]
        public int breatheInSeconds;
        [Range(0, 10)]
        public int pauseAfterBreathIn;
        [Range(0, 10)]
        public int breatheOutSeconds;
        [Range(0, 10)]
        public int pauseAfterBreathOut;
        public float moveSpeed;

        
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(BreathePattern());
        }
        
        IEnumerator BreathePattern()
        {
            yield return new WaitForSeconds(startDelay);
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
        void FixedUpdate()
        {
            if (wallMoving)
            {
                var transform1 = transform;
                transform1.position += transform1.forward * (moveSpeed * Time.deltaTime);
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