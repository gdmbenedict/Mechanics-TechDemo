using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Material mt;
    [SerializeField] private float lerpTime =1;
    [ReadOnly(true)] private Color32[] colors;

    // Start is called before the first frame update
    void Start()
    {
        mt = gameObject.GetComponent<MeshRenderer>().material;
        colors = new Color32[6]
        {
            new Color32(255,0,0,255), //Red
            new Color32(255,0,255,255), //Magenta
            new Color32(0,0,255,255), //Blue
            new Color32(0,255,255,255), //Cyan
            new Color32(0,255,0,255), //Green
            new Color32(255,255,0,255), //Yellow
        };
        StartCoroutine(ColorCycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ColorCycle()
    {
        int i = 0; //tracks which lerp the cycle is on
        while (true) //continues forever
        {
            for (float interpolant = 0f;interpolant < 1f; interpolant += (1f/lerpTime * Time.deltaTime))
            {
                mt.color = Color.Lerp(colors[i % colors.Length], colors[(i+1) % colors.Length], interpolant);
                yield return null;
            }
            i++;
        }
    }
}
