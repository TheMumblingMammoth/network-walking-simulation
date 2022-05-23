using UnityEngine;
using UnityEngine.UI;
public class TimeBar : MonoBehaviour{
    Button button;
    InputField input;
    
//    Area text;
    void Awake(){
        timeBoost = 0;
        Core.timeBoost = 0;
        input = GetComponentInChildren<InputField>();
        button = GetComponentInChildren<Button>();
        input.text = "100";
        Pause();
    }
    float timeBoost;
    void FixedUpdate(){
        if(pause) return;
        int a;
        bool fl = true;
        if(int.TryParse(input.text, out a)) Core.timeBoost = a;
        else fl = false;
        input.textComponent.color = fl ? Color.black : Color.red;        
    }
    public static bool pause;
    public void Pause(){
        //input.enabled = pause;
        pause = !pause;
        button.image.color = pause ? Color.red : Color.green;
        if(pause){
            timeBoost = Core.timeBoost;
            Core.timeBoost = 0;
        }else{
            Core.timeBoost = timeBoost;
        }
    }
}
