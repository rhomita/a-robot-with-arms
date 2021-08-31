using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameObject _help;

        void Start()
        {
            HideHelp();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (_help.activeSelf)
                {
                    HideHelp();
                }
                else
                {
                    ShowHelp();
                }
            }
        }

        void HideHelp()
        {
            _help.SetActive(false);
        }

        void ShowHelp()
        {
            _help.SetActive(true);
        }
    }
}