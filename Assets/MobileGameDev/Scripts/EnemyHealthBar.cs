using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private TextMesh tm;

    public Camera playerCam;
    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = playerCam.transform.forward;
    }
    
    // Return the current Health by counting the '-'
    public int Current() {
        return tm.text.Length;
    }

// Decrease the current Health by removing one '-'
    public void Decrease() {
        if (Current() > 1)
            tm.text = tm.text.Remove(tm.text.Length - 1);
        else
            Destroy(transform.parent.gameObject);
    }
}
