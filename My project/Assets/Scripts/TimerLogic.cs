using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private TMP_Text txt;
    [SerializeField]
    private float sTimeLimit;
    private float cTimeLimit;

    private GameObject[] enemies;
    private int maxEnemyCount;
    // Start is called before the first frame update
    void Start()
    {
        cTimeLimit = sTimeLimit;
        lr = GetComponent<LineRenderer>();
        CalculateLines();
        maxEnemyCount = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateLines();
        cTimeLimit -= Time.deltaTime * enemies.Length / maxEnemyCount;
        txt.text = Mathf.FloorToInt(cTimeLimit).ToString();
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
}
