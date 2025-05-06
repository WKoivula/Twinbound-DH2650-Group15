    using UnityEngine;

    public class ButtonTrigger : MonoBehaviour
    {
        public Collider platformCollider;
        private Renderer platformRenderer;
        AudioSource buttonSound;
    
        void Start()
        {
            platformRenderer = platformCollider.GetComponent<Renderer>();

            // Hide platform at start
            platformCollider.enabled = false;
            platformRenderer.enabled = false;
            buttonSound = GetComponent<AudioSource>();
    }

        void OnTriggerEnter(Collider other)
        {
            buttonSound.Play();
            platformCollider.enabled = true;
            platformRenderer.enabled = true;
        }

        void OnTriggerExit(Collider other)
        {
            platformCollider.enabled = false;
            platformRenderer.enabled = false; 
        }
    }
