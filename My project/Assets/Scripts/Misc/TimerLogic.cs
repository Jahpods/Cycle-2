using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class TimerLogic : MonoBehaviour
{
    [HeaderAttribute("Enemy Line Visual")]
    [SerializeField]
    private Vector3 childPos;
    private LineRenderer lr;

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

    private AudioManager am;
    private float timeBetweenTick = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        sld.maxValue = sTimeLimit;
        sld.value = sTimeLimit;
        cTimeLimit = sTimeLimit;
        lr = GetComponent<LineRenderer>();
        CalculateLines();
        maxEnemyCount = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateLines();
        cTimeLimit -= Time.deltaTime;
        sld.value = Mathf.FloorToInt(cTimeLimit);
        if(enemies.Length == 0){
            SceneManager.LoadScene("Won");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }else if(cTimeLimit <= 0){
            SceneManager.LoadScene("Lost");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void FixedUpdate(){
        if(timeBetweenTick <= 0){
            PlayTick();
            timeBetweenTick = Mathf.Lerp(0.01f, 2f, Mathf.Pow((cTimeLimit + sTimeLimit/2) / sTimeLimit, 2f));
        }else{
            timeBetweenTick -= Time.deltaTime;
        }
    }

    void CalculateLines(){
        int index = 0;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        lr.positionCount = enemies.Length * 2 + 1;
        lr.SetPosition(index, childPos);
        foreach(GameObject go in enemies){
            index++;
            lr.SetPosition(index, go.transform.position);
            index++;
            lr.SetPosition(index, childPos);
        }
    }

    public void EnemyDies(){
        cTimeLimit += 30f;
        cTimeLimit = Mathf.Clamp(cTimeLimit, 0, sTimeLimit);
    }

    void PlayTick(){
        float vol = (Mathf.Clamp01((sTimeLimit / cTimeLimit / 2f) - 1.25f));
        am.Play("tick", vol);
    }

    void OnDrawGizmos(){
        Gizmos.DrawSphere(childPos, 0.1f);
    }
}
