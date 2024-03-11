using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBG : MonoBehaviour
{
    [SerializeField] float _widthBG;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Texture raw = spriteRenderer.sprite.texture;

        _widthBG = raw.width / spriteRenderer.sprite.pixelsPerUnit;
       // Debug.Log(raw.width/ spriteRenderer.sprite.pixelsPerUnit);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Mathf.Abs(this.transform.position.x - Camera.main.transform.position.x) > _widthBG)
        {
            float offsetX = Mathf.Abs(this.transform.position.x - Camera.main.transform.position.x) - _widthBG;
            Vector3 pos = this.transform.position;
            pos.x = Camera.main.transform.position.x + offsetX;

            this.transform.position = pos;
        }
    }
}
