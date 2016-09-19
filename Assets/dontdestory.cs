using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class dontdestory : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.I))
        {
            DontDestroyOnLoad(transform.gameObject);
            SceneManager.LoadScene("level");
        }
        
     }
}
