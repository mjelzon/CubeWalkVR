using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

    public int index;

    public Sprite empty;
    public Sprite full;

    private Image image;

	// Use this for initialization
	void Start ()
    {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FillHeart()
    {
        image.sprite = full;
    }

    public void EmptyHeart()
    {
        image.sprite = empty;
    }
}
