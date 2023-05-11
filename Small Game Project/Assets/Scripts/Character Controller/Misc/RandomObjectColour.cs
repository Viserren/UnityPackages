using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectColour : MonoBehaviour
{
    MeshRenderer _mesh;
    #region Listen to event
    private void OnEnable()
    {
        AudioAnalyser.FrequencyHit += ChangeColour;
    }

    private void OnDisable()
    {
        AudioAnalyser.FrequencyHit -= ChangeColour;
    }
    #endregion
    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _mesh.material.color = Random.ColorHSV(0, 1, .75f, 1, .5f, 1);
    }

    void ChangeColour(int band)
    {
        switch (band)
        {
            case 0:
                _mesh.material.color = Random.ColorHSV(0, .1f, .75f, 1, .5f, 1);
                break;
            case 1:
                _mesh.material.color = Random.ColorHSV(.1f, .2f, .75f, 1, .5f, 1);
                break;
            case 2:
                _mesh.material.color = Random.ColorHSV(.2f, .3f, .75f, 1, .5f, 1);
                break;
            case 3:
                _mesh.material.color = Random.ColorHSV(.3f, .4f, .75f, 1, .5f, 1);
                break;
            case 4:
                _mesh.material.color = Random.ColorHSV(.4f, .5f, .75f, 1, .5f, 1);
                break;
            case 5:
                _mesh.material.color = Random.ColorHSV(.5f, .6f, .75f, 1, .5f, 1);
                break;
            case 6:
                _mesh.material.color = Random.ColorHSV(.6f, .7f, .75f, 1, .5f, 1);
                break;
            case 7:
                _mesh.material.color = Random.ColorHSV(.7f, .8f, .75f, 1, .5f, 1);
                break;
            default:
                break;
        }
    }
}
