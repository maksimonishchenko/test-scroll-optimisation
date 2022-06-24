using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    [SerializeField] private int _listSize;
    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _listItemPrefab;

    private ScrollRect _scrollRect;
    private RectTransform[] _itemsR;
    private GameObject[][] _itemsG;
    private Image[] _itemsI;
	private Camera _main;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        _main = Camera.main;
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
        _itemsR = new  RectTransform[_listSize];
        _itemsG = new  GameObject[_listSize][];
        _itemsI =  new Image[_listSize];

        for (int i = 0; i < _listSize; i++)
        {
            var item = Instantiate(_listItemPrefab, _content);

            _itemsR[i] = item.GetComponent<RectTransform>();
            _itemsI[i] = item.GetComponent<Image>();
            _itemsG[i] =  item.gameObject.transform.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
        }
    }

    ////while moving list elements is switching
    void ScrollRectUpdate(Vector2 pos)
    {
        if(_itemsR == null || _itemsG == null)
            return;

        for (int i = 0; i < _listSize; i++)
        {
            var vis = _itemsR[i].IsVisibleFrom(_main);
            _itemsI[i].enabled = vis;
            for(int j = 1; j < _itemsG[i].Length; j++)
            {
                _itemsG[i][j].gameObject.SetActive(vis);
            }
        }
    }
}
