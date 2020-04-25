using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelDrawing1 : MonoBehaviour
{
    public RawImage img;
    Texture2D drawimg;
    List<string> weave;
    string itemOne;
    string itemTwo;
    Color[] weavePattern;
    int size = 85;
    Color aColor;
    Color bColor;
    Color cColor;
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        drawimg = new Texture2D(size, size, TextureFormat.ARGB32,false); //make 2D texture, 100x100 size, with alpha rgb format in 32 bit (the algorithm used to compress the image), false=?
        drawimg.filterMode = FilterMode.Point; //changing the filter mode of the texture to point from the default bilinear to keep the pixels sharp. Can see the options by double-clicking the texture in play mode in our script component.

        Color defaultColor = Color.blue;
        defaultColor.a = 0;
        Color[] colorArray = new Color[size * size]; //an array with enough slots to color all pixels of this drawimg texture

        for (int i = 0; i < colorArray.Length; i++)
        {
            colorArray[i] = defaultColor;
            //colorArray[i] = new Color(Random.value, Random.value, Random.value);
        }

        //(a+b+c)^3 is Dietz-ed to give the below string list
        weave = new List<string>() { "a", "a", "a", "a", "a", "b", "a", "a", "b", "a", "a", "b", "a", "b", "b", "a", "b", "b", "a", "b", "b", "a", "b", "c", "a", "b", "c", "a", "b", "c", "a", "b", "c", "a", "b", "c", "a", "b", "c", "a", "a", "c", "a", "a", "c", "a", "a", "c", "a", "c", "c", "a", "c", "c", "a", "c", "c", "b", "b", "b", "b", "b", "c", "b", "b", "c", "b", "b", "c", "b", "c", "c", "b", "c", "c", "b", "c", "c", "c", "c", "c" };
        
        weavePattern = new Color[size * size];

        Debug.Log(1 % 8); //remainder of 1/8 is 1

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (weave[y % weave.Count] == weave[x % weave.Count]) //simply finding the place of the right letter from the 'weave' list using modulo operation
                {
                    weavePattern[(x * size) + y] = Color.white; //simply finding the place of the pixel and giving it color
                }
                else {
                    weavePattern[(x * size) + y] = Color.black; //simply finding the place of the pixel and giving it color
                };
            }

        }

        drawimg.SetPixels(weavePattern); //sets the pixels with the array of colors given but does not apply
        drawimg.Apply(); //the apply function saves the pixel editing - which is a memory intensive step

        img.texture = drawimg; //applies the texture to the image. this line can be anywhere inside Start() as it works by reference.
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.anyKeyDown)
        //{
            itemOne = weave[50];
            weave.RemoveAt(50);
            weave.Add(itemOne);
            itemTwo = weave[24];
            weave.RemoveAt(24);
            weave.Add(itemTwo);
        //}

        aColor = new Color(counter / 256.0f, 0.0f, 1.0f, 1.0f);
        bColor = new Color(1.0f, counter / 256.0f, 0.2f, 1.0f);
        cColor = new Color(1.0f, 1.0f, 30 / 256.0f, 1.0f);

        if (counter < 106) {
            counter = counter + 1;
        } else
        {
            counter = 0;
        }

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                //Debug.Log(weave[y % weave.Length]);
                if (weave[y % weave.Count] == weave[x % weave.Count]) //simply finding which letter from weave[] using modulo operation
                {
                    weavePattern[(x * size) + y] = aColor; //simply finding the place of the pixel
                }
                else if(weave[y % weave.Count] == "c")
                {
                    weavePattern[(x * size) + y] = cColor; //simply finding the place of the pixel
                }
                else
                {
                    weavePattern[(x * size) + y] = bColor; //simply finding the place of the pixel
                };
            }

        }

        drawimg.SetPixels(weavePattern); //sets the pixels with the array of colors given but does not apply
        drawimg.Apply(); //the apply function saves the pixel editing - which is a memory intensive step

        img.texture = drawimg; //applies the texture to the image. this line can be anywhere inside Start() as it works by reference.
    }
}
