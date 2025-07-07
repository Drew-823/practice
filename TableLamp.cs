using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableLamp : MonoBehaviour
{
    public Light tablelamp;
    public Color lightColor = Color.yellow;
    public float lightRange = 5.0f;
    public float lightvalue = 1.0f;
    public float lightdown = 0f;
    public float lastlight = 3.0f;
    //public static bool GetKeyDown(Space);

    // Start is called before the first frame update
    void Start()
    {
        tablelamp = GetComponent<Light>();
        tablelamp.type = LightType.Point;
        tablelamp.color = lightColor;
        tablelamp.intensity = lightdown;
        tablelamp.range = lightRange;
        tablelamp.shadows = LightShadows.Soft;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(tablelamp.intensity == lightdown)
            {
                tablelamp.intensity = lastlight;
            }
            else
            {
                lastlight = tablelamp.intensity;
                tablelamp.intensity = lightdown;
            }
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (tablelamp.intensity != lightdown)
            {
                if (tablelamp.intensity < 10.0f)
                {
                    tablelamp.intensity = tablelamp.intensity + lightvalue;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tablelamp.intensity = tablelamp.intensity - lightvalue;
        }
    }
}
