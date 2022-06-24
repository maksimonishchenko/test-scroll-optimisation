using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    [SerializeField] private int _listSize;
    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _listItemPrefab;

    private ScrollRect _scrollRect;
    private GameObject[] _itemsG;
    private int lastIdGroup;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _scrollRect.onValueChanged.AddListener(ScrollRectUpdate);
    }

    public void On_GenerateClick()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
        //caching list elements and subelements
        _itemsG = new  GameObject[_listSize];
       
        for (int i = 0; i < _listSize; i++)
        {
            var item = Instantiate(_listItemPrefab, _content);
            _itemsG[i] =  item.gameObject.transform.GetChild(0).gameObject;
        }

        for (int i = 0; i < _listSize; i++)
        {
            _itemsG[i].SetActive(false);
        }
    }

    ////while moving list elements is switching
    void ScrollRectUpdate(Vector2 pos) //146170
    {
        if(_itemsG == null)
            return;

        var idVisible = Mathf.RoundToInt((1f - pos.y) * _listSize);

        SetGroup(lastIdGroup, false);
        SetGroup(idVisible, true);

        lastIdGroup = idVisible;
        
    }

    void SetGroup(int idVisible, bool state)
    {
        //theese bound -6 and 9 must be calculated based on screen width height and GrdLayoutGroup
        // and prefab recttransforms to be universal by display size and ui settings layout

        int boundMin =  -3;
        int boundMax =  9;
        
        for (int i = idVisible + boundMin;  i < idVisible + boundMax && i < _listSize; i++)
        {
            if (i < 0)
                continue;

            _itemsG[i].SetActive(state);
        }
    }
}
