using UnityEngine;
public class Core : MonoBehaviour{
    public static float MPU = 1000f/36f;
    public static int userCount = 0;
    public static TraceUI traceUI;
    public static Field field;
    public static Stats stats;
    public static float accuracy;
    public static float timeBoost;
    
    public static bool runing {get; private set;} = true;
    void Update(){
        if(Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    public void Pause(){
        runing = false;
    }

    public void Predict(){
        
    }

    public void ResetPrediction(){
        runing = true;
    }


}
