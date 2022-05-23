using UnityEngine;
using UnityEngine.UI;

public class Splin : MonoBehaviour{
    [SerializeField] Texture2D textureSample;
    public static User user;
    Texture2D texture;
    Image image;
    void Awake(){
        image = GetComponent<Image>();
        texture = Instantiate<Texture2D>(textureSample);
    }

    
    void Start(){
        Rect rect = new Rect(0, 0, 1152, 64);
        Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 16);
        image.sprite = sprite;
    }
    
    void DrawSplin(){
        if(user != null){
            for(int i = 0; i < texture.width; i++)
                for(int j = 0; j < texture.height; j++)
                    if(user.SNR_history.Count > i)
                        image.sprite.texture.SetPixel(i, j, new Color((1-user.SNR_history[i])*0.75f + 0.25f, user.SNR_history[i]*0.75f + 0.25f, 0f, 1));
                    else
                        image.sprite.texture.SetPixel(i, j, new Color(0.25f, 0.25f, 0.25f, 1));
            image.sprite.texture.Apply();
        }
    }

    void Update(){
        if(user != null)
            DrawSplin();
    }

    public void Display(){
        
    }

}
