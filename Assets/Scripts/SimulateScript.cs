using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;

public class SimulateScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bits;
    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject slabe;
    [SerializeField]
    private GameObject dd;
    [SerializeField]
    private ScrollRect Scroll;

    public TMPro.TMP_Dropdown drop;

    private Transform bit1;

    private Complex[] b1 = new Complex[2];
    
    //private float[] b1 = new float[2];

    //private int[][] hGate = [[0, Mathf.Sqrt(1 / 2)],[]];

    private void Awake()
    {

        b1[0] = new Complex(0.0, 0.0);
        b1[1] = new Complex(1.0, 0.0);


        popup.SetActive(false);
        //canvas = transform.parent;
        //bits = canvas.GetChild(1);
        //Debug.Log(bits.name);
        bit1 = bits.transform.GetChild(0);
        bits.transform.GetChild(1).gameObject.SetActive(false);
        bits.transform.GetChild(2).gameObject.SetActive(false);
        bits.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void DropDownMethod(int val)
    {
        if(val == 0) {

            bits.transform.GetChild(1).gameObject.SetActive(false);
            bits.transform.GetChild(2).gameObject.SetActive(false);
            bits.transform.GetChild(3).gameObject.SetActive(false);
        }else if (val == 1) {
            bits.transform.GetChild(1).gameObject.SetActive(true);
            bits.transform.GetChild(2).gameObject.SetActive(false);
            bits.transform.GetChild(3).gameObject.SetActive(false);

        }else if(val == 2) {
            bits.transform.GetChild(1).gameObject.SetActive(true);
            bits.transform.GetChild(2).gameObject.SetActive(true);
            bits.transform.GetChild(3).gameObject.SetActive(false);
        }else if ( val == 3) {
            bits.transform.GetChild(1).gameObject.SetActive(true);
            bits.transform.GetChild(2).gameObject.SetActive(true);
            bits.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void Simulate()
    {
        int i = 1;
        Debug.Log("bit1 = " + bit1.name);
        while (i < bit1.transform.childCount)
        {

            //Debug.Log("i = " + i);
            //Debug.Log("before b1 = " + b1[0]+", " +
            //    " "+b1[1]);

            if (bit1.transform.GetChild(i).GetComponent<ItemSlot>().gate.CompareTo('H') == 0)
            {
                b1 = hg(b1);
            }
            else if (bit1.transform.GetChild(i).GetComponent<ItemSlot>().gate == 'X')
            {
                b1 = xgate(b1);
            }
            else if (bit1.transform.GetChild(i).GetComponent<ItemSlot>().gate == 'Y')
            {
                b1 = ygate(b1);
            }
            else if (bit1.transform.GetChild(i).GetComponent<ItemSlot>().gate == 'Z')
            {
                b1 = zgate(b1);
            }
            i++;

            // Debug.Log("After b1 = " + b1[0] + ", " + b1[1]);

        }

        popup.SetActive(true);
        //Debug.Log("b1 = " + b1[0] + ", " + b1[1]);
        //Debug.Log("drop.value = " + drop.value);

        int slabeCount = (int)Mathf.Pow(2,drop.value+1);

        RectTransform rt = content.GetComponent<RectTransform>();


        i = 0;

        while(i < slabeCount)
        {
            GameObject newSlabe =  Instantiate(slabe);
            newSlabe.transform.SetParent(content.transform,false);

            if (i == 0) {
                // newSlabe.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().SetText("( " + b1[0].Real.ToString("##.##") + " + " + b1[0].Imaginary.ToString("#.##")  + "i ) * |0>");

                newSlabe.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().SetText("( " + decimal.Round((decimal)b1[0].Real, 2, System.MidpointRounding.AwayFromZero) + " + " + decimal.Round((decimal)b1[0].Imaginary, 2, System.MidpointRounding.AwayFromZero) + " i ) * |0>");

//                decimal.Round((decimal)b1[1].Real, 2, System.MidpointRounding.AwayFromZero);
            }

            if( i == 1)
            {
                // newSlabe.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().SetText("( " + b1[1].Real.ToString("##.##") + " + " + b1[1].Imaginary.ToString("#.##") + "i ) * |1>");

                newSlabe.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().SetText("( " + decimal.Round((decimal)b1[1].Real, 2, System.MidpointRounding.AwayFromZero) + " + " + decimal.Round((decimal)b1[1].Imaginary, 2, System.MidpointRounding.AwayFromZero) + " i ) * |1>");

            }
            i++;

        }

        Scroll.verticalNormalizedPosition = 1;
    }


    private Complex[] hg(Complex[] inp)
    {
        Complex temp = new Complex(inp[0].Real, inp[0].Imaginary);

        Debug.Log("temp = " + temp.Real + ", " + temp.Imaginary);
        Debug.Log("inp[0] = " + inp[0].Real + ", " + inp[0].Imaginary);
        Debug.Log("inp[1] = " + inp[1].Real + ", " + inp[1].Imaginary);

        Debug.Log("Mathf.sqrt = " + Mathf.Sqrt((float)0.5));
        Debug.Log(" / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /");
        inp[0] = new Complex(Mathf.Sqrt((float)0.5),0.0) * inp[0] + new Complex(Mathf.Sqrt((float)0.5), 0.0) * inp[1];
        inp[1] = new Complex(Mathf.Sqrt((float)0.5), 0.0) * temp - new Complex(Mathf.Sqrt((float)0.5), 0.0) * inp[1];
        Debug.Log("temp = " + temp.Real + ", "+ temp.Imaginary); 
        Debug.Log("inp[0] = " + inp[0].Real + ", " + inp[0].Imaginary);
        Debug.Log("inp[1] = " + inp[1].Real + ", " + inp[1].Imaginary);
        return inp;
    }

    private Complex[] xgate(Complex[] inp)
    {
        Complex temp = new Complex(inp[0].Real,inp[0].Imaginary);
        inp[0] = inp[1];
        inp[1] = temp;
        return inp;
    }
    private Complex[] ygate(Complex[] inp)
    {
        Complex temp = new Complex(inp[0].Real, inp[0].Imaginary);
        inp[0] = -1 * inp[1] * new Complex(0, -1);
        inp[1] = new Complex(0, -1) * temp;
        return inp;
    }

    private Complex[] zgate(Complex[] inp)
    {
        Debug.Log("inp[1] = " + inp[1]);
        inp[1] = new Complex(-1,0) * inp[1];
        return inp;
    }

}
