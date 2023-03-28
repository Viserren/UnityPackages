using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    //public RectTransform ClockFace;
    public TextMeshProUGUI Date, Time, Season, Week;

    //public Image weatherSprite;
    //public Sprite[] weatherSprites;

    private float sunStartingRotation;

    public Light sunLight;
    public float dayIntensity;
    public float nightIntensity;
    public AnimationCurve dayNightCurve;
    public AnimationCurve sunHeightCurve;

    private void Awake()
    {
        //startingRotation = ClockFace.eulerAngles.z;
        sunStartingRotation = sunLight.transform.eulerAngles.z;
    }

    private void OnEnable()
    {
        TimeManager.OnDateChanged += UpdateDateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnDateChanged -= UpdateDateTime;
    }

    private void UpdateDateTime(DateTime dateTime)
    {
        Date.text = dateTime.DateToString();
        Time.text = dateTime.TimeAMPMToString();
        Season.text = dateTime.Season.ToString();
        Week.text = $"Week: {dateTime.CurrentWeek.ToString()}";
        //weatherSprite.sprite = weatherSprites[(int)]

        float t = (float)dateTime.Hours / 24f;
        float pos = (float)dateTime.CurrentWeek / 16;
        float newRotation = Mathf.Lerp(0,360,t);
        Debug.Log(newRotation);
        //ClockFace.localEulerAngles = new Vector3(0, 0, newRotation + startingRotation);

        // Winter 20
        // Autumn 40
        // Spring 60
        // Summer 80

        Quaternion lowAngle = Quaternion.Euler(20, 0, 0) * Quaternion.Euler(0, newRotation + sunStartingRotation, 0);
        Quaternion highAngle = Quaternion.Euler(80, 0, 0) * Quaternion.Euler(0, newRotation + sunStartingRotation, 0);

        float sunPos = sunHeightCurve.Evaluate(pos);
        sunLight.transform.rotation = Quaternion.Lerp(lowAngle, highAngle,sunPos);
    }
}
