using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingLotIcons : MonoBehaviour
{
    // Start is called before the first frame update
    bool isGrowing = true;
    float maxSize = 80;
    float minSize = 30;
    float actualSize;
    Renderer m_Renderer;    
    void Start()
    {
        this.actualSize = 30;
        m_Renderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        ReScale();
        if(!m_Renderer.isVisible){
            
        }
    }

    private void ReScale()
    {
        if (isGrowing)
        {
            actualSize = actualSize + 0.1f;
        }
        else
        {
            actualSize = actualSize - 0.1f;
        }

        if (actualSize > maxSize)
            isGrowing = false;
        if (actualSize < minSize)
            isGrowing = true;
        var newScale = new Vector3()
        {
            x = actualSize,
            y = actualSize,
            z = 1
        };
        this.transform.localScale = newScale;
    }
}
