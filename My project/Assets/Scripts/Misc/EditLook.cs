    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Rendering;
    using UnityEngine.Rendering.Universal;
    using UnityEngine.Events;
     
    public class EditLook : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent enemyDies;
        [SerializeField]
        private Volume volume;
        private ColorAdjustments colorAdjustment;
        private ShadowsMidtonesHighlights smh;
        private float saturation = -40;
        private Vector4 midtone = new Vector4(-0.5f,-0.5f,-0.5f,-0.5f);
        private Vector4 highlight = new Vector4(-0.7f,-0.7f,-0.7f,-0.7f);
     
     
        public void Start()
        {
            VolumeProfile proflile = volume.sharedProfile;
     
     
            volume.profile.TryGet(out colorAdjustment);
            volume.profile.TryGet(out smh);

            colorAdjustment.saturation.value = saturation;
            enemyDies.AddListener(UpdateLook);
        }

        private void UpdateLook(){
            StartCoroutine(Gradual());
        }

        private IEnumerator Gradual(){
            for(int i = 0; i < 8; i++){
                saturation += 1;
                midtone += new Vector4(0.02f,0.02f,0.02f,0.02f);
                highlight += new Vector4(0.04f,0.04f,0.04f,0.04f);

                colorAdjustment.saturation.value = saturation;

                yield return new WaitForSeconds(0.1f);
            }

        }
    }
