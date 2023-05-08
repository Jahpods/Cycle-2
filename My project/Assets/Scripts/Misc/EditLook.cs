    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Rendering;
    using UnityEngine.Rendering.Universal;
    using UnityEngine.Events;
     
    public class EditLook : MonoBehaviour
    {
        [SerializeField]
        private Volume volume;
        private ColorAdjustments colorAdjustment;
        private ShadowsMidtonesHighlights smh;
        private float saturation = -40;
         
        public void Start()
        {
            VolumeProfile proflile = volume.sharedProfile;
     
     
            volume.profile.TryGet(out colorAdjustment);

            colorAdjustment.saturation.value = saturation;
        }

        public void UpdateLook(){
            StartCoroutine(Gradual());
        }

        private IEnumerator Gradual(){
            for(int i = 0; i < 8; i++){
                saturation += 1;

                colorAdjustment.saturation.value = saturation;

                yield return new WaitForSeconds(0.1f);
            }

        }
    }
