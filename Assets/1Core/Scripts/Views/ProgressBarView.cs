using UnityEngine;
using UnityEngine.UI;

namespace _1Core.Scripts.Views
{
    public class ProgressBarView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _text;

        public void SetValue(float value)
        {
            _image.fillAmount = value;
            if (_image.fillAmount >= 1) _text.SetActive(true);
        }

        public void RefreshValue()
        {
            _image.fillAmount = 0;
            _text.SetActive(false);
        }
    }
}