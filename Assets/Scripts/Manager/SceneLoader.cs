using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class SceneLoader : MonoBehaviour
    {
        private Animator _animator;

        void Awake()
        {
            _animator = transform.GetComponent<Animator>();
        }
        
        public void GoToScene(string scene)
        {
            Time.timeScale = 1;
            StartCoroutine(GoScene(scene));
        }

        IEnumerator GoScene(string scene)
        {
            _animator.SetTrigger("ChangeScene");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
            _animator.ResetTrigger("ChangeScene");
        }

        public void AddScene(string scene)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }

        public void Reload()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}