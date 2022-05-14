using UnityEngine;

public class ScrollViewController : MonoBehaviour
{
    [SerializeField] private int _listSize;
    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _listItemPrefab;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void On_GenerateClick()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }

        for (int i = 0; i < _listSize; i++)
        {
            Instantiate(_listItemPrefab, _content);
        }
    }
}
