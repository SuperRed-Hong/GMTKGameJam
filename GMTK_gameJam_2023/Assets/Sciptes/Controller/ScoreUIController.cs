using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIController : MonoBehaviour
{
    public TextMeshProUGUI doctor;
    public TextMeshProUGUI patient;
    public TextMeshProUGUI round;

    public void SetDoctor(string Score)
    {
        doctor.text = Score;
    }
    public void SetPatient(string Score)
    {
        patient.text = Score;
    }
    public void SetRound(string RoundNumber)
    {
        round.text = RoundNumber;
    }
}
