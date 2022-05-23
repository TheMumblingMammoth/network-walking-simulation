using UnityEngine;
using UnityEngine.UI;
public class SpeedInput : MonoBehaviour{
    [SerializeField] InputField min, max;
    Text text;
    void Awake(){
        //slider = GetComponent<Slider>();
        //slider.value = User.max_speed;
        text = GetComponentInChildren<Text>();
        min.text = "1";
        max.text = "2";
    }

    void FixedUpdate(){
        float maxx, minn;
        bool fl = true;
        if(float.TryParse(max.text, out maxx))
            User.max_speed = maxx;
        else fl = false;
        if(float.TryParse(min.text, out minn))
            User.min_speed = minn;  
        else fl = false;

        text.color = fl || (maxx < minn)? Color.black : Color.red;
    }

}