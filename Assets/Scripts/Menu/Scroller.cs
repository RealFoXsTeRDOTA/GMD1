using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] 
    private RawImage img; 
    [SerializeField] 
    private float scrollSpeedX = 0.2f; 
    [SerializeField] 
    private float scrollSpeedY = 0.2f; 

    private void Update()
    {
        Vector2 offset = new Vector2(scrollSpeedX, scrollSpeedY) * Time.deltaTime;
        img.uvRect = new Rect(img.uvRect.position + offset, img.uvRect.size);
    }
}
