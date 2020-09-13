using UnityEngine;
using UnityEngine.SceneManagement;

namespace WarRoad.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad = -1;

        void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Player")
            {
                print("Triggered portal");

                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}

