    using UnityEngine;

    public class ButtonTrigger : MonoBehaviour
    {
        public Collider platformCollider;
        private Renderer platformRenderer; 
    
        void Start()
        {
            platformRenderer = platformCollider.GetComponent<Renderer>();

            // Hide platform at start
            platformCollider.enabled = false;
            platformRenderer.enabled = false;
    }

        void OnTriggerEnter(Collider other)
        {
            platformCollider.enabled = true;
            platformRenderer.enabled = true;
            Debug.Log("true"); // ← 这里要用 Debug.Log() 而不是 console()
        }

        void OnTriggerExit(Collider other)
        {
            platformCollider.enabled = false;
            platformRenderer.enabled = false; 
            Debug.Log("false"); // ← 同上
        }
    }
