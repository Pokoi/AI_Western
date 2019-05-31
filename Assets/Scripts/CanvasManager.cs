using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager Instance { get { return instance; } }
    static CanvasManager instance;

    public Text task_name;

    void Awake() { if (Instance == null) instance = this; else Destroy(this); }
    

    public void SetCurrentTask(string _s) { task_name.text = _s; }
}
