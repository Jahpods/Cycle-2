using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class trainingTimer : MonoBehaviour
{
    [HeaderAttribute("Time Limit")]
    [SerializeField]
    private Slider sld;
    [SerializeField]
    private float sTimeLimit;
    [SerializeField]
    private float cTimeLimit;

    [HideInInspector]
    public GameObject[] enemies;
    private int maxEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        sld.maxValue = sTimeLimit;
        sld.value = sTimeLimit;
        cTimeLimit = sTimeLimit;
        maxEnemyCount = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        cTimeLimit -= Time.deltaTime;
        sld.value = Mathf.FloorToInt(cTimeLimit);
    }

    public void EnemyDies(){
        cTimeLimit += 30f;
        cTimeLimit = Mathf.Clamp(cTimeLimit, 0, sTimeLimit);
    }
}
