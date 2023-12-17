using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform cadre;
    [SerializeField] private float timeToMove = 0.125f;
    // faire une référence à un texte partagé pour mettre dessus les infos correctes selon le pouvoir
    public string description = "";
    private TextMeshProUGUI infoText;
    private Button bouton;

    // Start is called before the first frame update
    void Start()
    {
        cadre = GameObject.Find("Cadre").transform;
        infoText = GameObject.Find("InfoPowerText").GetComponent<TextMeshProUGUI>();
        bouton = GetComponent<Button>();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        return;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //cadre.DOMoveX(this.transform.position.x, timeToMove);
        //infoText.text = description;
        //bouton.Select();
        return;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        return;
    }

    public void OnSelect(BaseEventData eventData)
    {
        cadre.DOMoveX(this.transform.position.x, timeToMove);
        infoText.text = description;
    }

    public void AddDescription(string nouvelleDescription)
    {
        description = nouvelleDescription;
    }

}
