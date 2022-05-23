using UnityEngine;
using UnityEngine.UI;

public class RuntimeDrawButton : MonoBehaviour{
    public static bool run_time_drawing = false;
    void Awake(){
        GetComponent<Image>().color = run_time_drawing ? Color.green : Color.red;
    }
    public void Click(){
        run_time_drawing = !run_time_drawing;
        GetComponent<Image>().color = run_time_drawing ? Color.green : Color.red;
    }
}