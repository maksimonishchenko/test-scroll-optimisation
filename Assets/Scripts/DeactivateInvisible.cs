using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DeactivateInvisible : MonoBehaviour
{
	Camera main;
	GameObject[] childCache;
	RectTransform myRectTransform;
	Image mainImage;
    // Start is called before the first frame update
    void Start()
    {
		main = Camera.main;
		mainImage = transform.GetComponent<Image>();
		myRectTransform = transform.GetComponent<RectTransform>();
		childCache = transform.GetComponentsInChildren<Transform>().Select(t => t.gameObject).Where(g => g != gameObject).ToArray();
    }
	bool IsVisible()
	{
		return myRectTransform.IsVisibleFrom(main);
	}
    // Update is called once per frame
    void Update()
    {
		var fullMainRectPartiallyVisisble = IsVisible();
        for(int i =0; i< childCache.Length;i++)
		{
			childCache[i].SetActive(fullMainRectPartiallyVisisble);
		}
		mainImage.enabled = fullMainRectPartiallyVisisble;
    }
}
