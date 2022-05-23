using UnityEngine;
using UnityEngine.UI;
public class OverlapBar : MonoBehaviour{
    Slider slider;
    Text text;
    void Awake(){
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
    }

    void FixedUpdate(){
        Core.field.GetComponent<SpriteRenderer>().color = new Color(1,1,1, slider.value);
        text.text = "SINR Overlap: " + slider.value.ToString("0.");
    }

}