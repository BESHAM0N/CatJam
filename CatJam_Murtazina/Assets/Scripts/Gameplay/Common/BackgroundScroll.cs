using UnityEngine;
using UnityEngine.UI;

namespace CatJam
{
    public sealed class BackgroundScroll : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.0001f;

        private float _position;
        private RawImage _image;

        private void Start()
        {
            _image = GetComponent<RawImage>();
        }

        private void Update()
        {
            _position += _speed;
            if (_position > 1.0F)
                _position -= 1.0F;
            
            _image.uvRect =  new Rect(0, _position, 1, 1);
        }
    }
}