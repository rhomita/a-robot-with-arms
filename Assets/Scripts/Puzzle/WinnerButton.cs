using Puzzle;
using UnityEngine;

namespace Puzzle
{
    public class WinnerButton : MonoBehaviour
    {
        void Awake()
        {
            Button button = transform.GetComponent<Button>();
            button.OnActivateTrigger += () =>
            {
                GameManager.Instance.LevelManager.GoToNextLevel();
            };
        }
    }
}