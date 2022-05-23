using UnityEngine;
using UnityEngine.UI;
public class IntensityInput : MonoBehaviour{
    [SerializeField] InputField input;
    Button button;
    Text text;
    public static float intensity;
    void Awake(){
        //slider = GetComponent<Slider>();
        //slider.value = User.max_speed;
        text = GetComponentInChildren<Text>();
        button = GetComponentInChildren<Button>();
        Pause();
        input.text = "0,01";
        //max.text = "1";
    }

    void FixedUpdate(){
        float a;
        bool fl = true;
        if(float.TryParse(input.text, out a))
            intensity = a;
        else fl = false;
        text.color = fl ? Color.black : Color.red;
    }

    public static bool pause;
    public void Pause(){
        //input.enabled = pause;
        pause = !pause;
        button.image.color = pause ? Color.red : Color.green;
    }

}