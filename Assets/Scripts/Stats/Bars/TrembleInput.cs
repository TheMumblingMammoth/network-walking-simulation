using UnityEngine;
using UnityEngine.UI;
public class TrembleInput : MonoBehaviour{
    [SerializeField] InputField min, max;
    Text text;
    void Awake(){
        //slider = GetComponent<Slider>();
        //slider.value = User.max_speed;
        text = GetComponentInChildren<Text>();
        min.text = "0";
        max.text = "1";
    }

    void FixedUpdate(){
        float maxx, minn;
        bool fl = true;
        if(float.TryParse(max.text, out maxx))
            User.max_tremble = maxx;
        else fl = false;
        if(float.TryParse(min.text, out minn))
            User.min_tremble = minn;  
        else fl = false;

        text.color = fl || (maxx < minn)? Color.black : Color.red;
    }

}