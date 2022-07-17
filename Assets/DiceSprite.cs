using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceSprite : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite[] sprites;
    public Image button;
    public TowerBuilder towerBuilder;
    public bool rollOnStart = true;
    public bool draggable = true;
    public float rollTime = 2f;
    public AnyDice anyDice;
    public bool continueDrag = false;
    private RectTransform canvasRect;
    public float leftEdge = -3f;
    public int face = 6;
    private bool rolling = false;
    private float rollRemaining = 2f;
    private int rollMod = 1;

    private void Start()
    {
        button = GetComponent<Image>();
        canvasRect = GetComponentInParent<RectTransform>();
        if (rollOnStart)
            Roll();
    }

    public void ShowFace(int i)
    {
        if(i > 0 && i-1 < sprites.Length)
        {
            button.sprite = sprites[i - 1];
            face = i;
        }
    }

    public void Roll()
    {
        rolling = true;
        rollRemaining = 0;
        rollMod = 1;
        button.color = Color.gray;
    }

    void Update()
    {
        if(rolling)
        {
            rollRemaining += Time.deltaTime;

            if (rollRemaining < rollTime)
            {
                if ((int)(rollRemaining * 1000f) % rollMod == 0)
                {
                    ShowFace(Random.Range(1, 7));
                    rollMod++;
                }
            }
            else
            {
                rolling = false;
                rollRemaining = rollTime;
                face = Random.Range(1, 7);
                ShowFace(face);
                button.color = Color.white;
                anyDice.ShowFace(face);
            }

        }

        if (continueDrag && draggable)
        {
            if (Input.GetMouseButtonUp(0))
            {
                continueDrag = false;
                OnEndDrag();
                return;
            }

            OnDrag();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        AkSoundEngine.PostEvent("Play_Pickup", gameObject);
        //throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!rolling && draggable)
        {
            Vector3 worldpoint;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, eventData.position, Camera.main, out worldpoint);
            this.transform.position = worldpoint;
            towerBuilder.gameObject.transform.localPosition = Vector3.zero;
            if(this.transform.localPosition.x < leftEdge)
            {
                anyDice.gameObject.SetActive(true);
                anyDice.ShowFace(face);
                button.enabled = false;
            }
            else
            {
                anyDice.gameObject.SetActive(false);
                button.enabled = true;
            }
        }
    }

    public void OnDrag()
    {
        if (!draggable)
            return;

        Vector3 worldpoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, Input.mousePosition, Camera.main, out worldpoint);
        this.transform.position = worldpoint;
        towerBuilder.gameObject.transform.localPosition = Vector3.zero;
        if (this.transform.localPosition.x < leftEdge)
        {
            anyDice.gameObject.SetActive(true);
            anyDice.ShowFace(face);
            button.enabled = false;
        }
        else
        {
            anyDice.gameObject.SetActive(false);
            button.enabled = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDrag();
    }

    public void OnEndDrag()
    {
        if(anyDice.gameObject.activeSelf)
        {
            AkSoundEngine.PostEvent("Play_Drop", gameObject);

            towerBuilder.Dropped();
        }
    }

    //fill tooltips
    public void OnPointerEnter(PointerEventData eventData)
    {
        TowerUI.instance.ShowText(face, face, face);
    }

    //remove tooltips
    public void OnPointerExit(PointerEventData eventData)
    {
        TowerUI.instance.ShowText();
    }
}
