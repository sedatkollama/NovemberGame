using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EngelPointsCollect : MonoBehaviour
{
    public int puan=0;
    snowboardController snowboardcs;
    [SerializeField] TMP_Text _text;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("point"))
        {
            puan +=100;
            _text.text = puan.ToString();          
        }
        if (other.gameObject.CompareTag("endgame"))
        {
            //snowboardcs.FinishGame();
        }
    }
}
