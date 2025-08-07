using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RuleCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] protected Text ruleDescription;
    [SerializeField] protected Image ruleSprite;

    public ScriptableRule ruleInfo { get; private set;  }

    private Transform originalPos;
    private CanvasGroup canvasGroup;

    protected bool isDragable;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Setup(ScriptableRule ruleInfo)
    {
        this.ruleInfo = ruleInfo;
        ruleDescription.text = ruleInfo.ruleDescription;
        ruleSprite.sprite = ruleInfo.ruleSprite;
        isDragable = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDragable) return;
        originalPos = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragable) return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragable) return;
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(originalPos);

    }

    
}
