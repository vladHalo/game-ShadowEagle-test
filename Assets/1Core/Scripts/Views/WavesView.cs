using UnityEngine;
using UnityEngine.UI;

public class WavesViews : MonoBehaviour
{
    [SerializeField] private Text _textWaves;

    public void RefreshWaves(int current, int max)
    {
        _textWaves.text = $"{current + 1}/{max} waves";
    }
}