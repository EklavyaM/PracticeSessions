using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Global;

namespace Matrix
{
    public class VoodooCharacter : MonoBehaviour, IPoolable
    {
        [SerializeField] private Text _text;

        [SerializeField] private float _delayForStart;
        [SerializeField] private float _delayForCharacterChange;
        [SerializeField] private float _delayForDeath;

        [SerializeField] private bool _isAlive;
        [SerializeField] private bool _hasStarted;

        private static Color _textStartColor;
        private static Color _textMidColor;
        private static Color _textEndColor;

        private float _startTimeCounter = 0;
        private float _deathTimeCounter = 0;
        private float _characterChangeTimeCounter = 0;

        static VoodooCharacter()
        {
            _textStartColor = Color.white;
            _textMidColor = Color.green;
            _textEndColor = new Color(_textMidColor.r, _textMidColor.g, _textMidColor.b, 0);
        }

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void Start()
        {
            _text.text = VoodooCharacterSheet.GetRandomCharacter();
        }

        private void Update()
        {
            if (!_isAlive)
                return;

            if(!_hasStarted)
            {
                _startTimeCounter += Time.deltaTime;
                UpdateStartColor();
                if (TimerExpired(_startTimeCounter, _delayForStart))
                    _hasStarted = true;

                return;
            }

            _deathTimeCounter += Time.deltaTime;
            _characterChangeTimeCounter += Time.deltaTime;

            UpdateOpacity();

            if (TimerExpired(_deathTimeCounter, _delayForDeath))
            {
                _deathTimeCounter = 0;
                _isAlive = false;
                return;
            }

            if (TimerExpired(_characterChangeTimeCounter, _delayForCharacterChange))
            {
                _characterChangeTimeCounter = 0;
                _text.text = VoodooCharacterSheet.GetRandomCharacter();
            }
        }

        private void UpdateStartColor()
        {
            _text.color = Color.Lerp(_textStartColor, _textMidColor, _startTimeCounter / _delayForStart);
        }

        private void UpdateOpacity()
        {
            _text.color = Color.Lerp(_textMidColor, _textEndColor, _deathTimeCounter / _delayForDeath);
        }

        private bool TimerExpired(float timer, float delay)
        {
            return timer >= delay;
        }

        public bool IsAlive()
        {
            return _isAlive;
        }


        public void SetAlive(bool value)
        {
            _isAlive = value;
            _text.enabled = value;

            if (_isAlive)
                OnSpawn();
        }

        public void OnSpawn()
        {
            _hasStarted = false;

            _startTimeCounter = 0;
            _deathTimeCounter = 0;
            _characterChangeTimeCounter = 0;

            _delayForStart = Random.Range(0.3f, 1);
            _delayForCharacterChange = Random.Range(0.5f, 1.5f);
            _delayForDeath = Random.Range(1, 4f);
        }

        public void SetOrientation(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            transform.localPosition = position;
            transform.localEulerAngles = rotation;
            transform.localScale = scale;
        }
        /*
       public static void ResetMidColor()
       {
           _textMidColor = new Color(Random.Range(0.0f, 1), Random.Range(0.0f, 1), Random.Range(0.0f, 1));
           _textEndColor = new Color(_textMidColor.r, _textMidColor.g, _textMidColor.b, 0);
       }*/

    }
}


