using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public abstract class ModalPopUpElement : Manager<ModalPopUpElement>
{
    #region Variables
    [Header("Holders")]
    [SerializeField] Transform headerTransform;
    [SerializeField] Transform contentTransform;
    [SerializeField] Transform footerTransform;

    [Header("Text fields")]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI contentText;

    [Header("Buttons")]
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] Button closeButton;

    [Header("Button text fields")]
    [SerializeField] TextMeshProUGUI yesButtonText;
    [SerializeField] TextMeshProUGUI noButtonText;
    [SerializeField] TextMeshProUGUI closeButtonText;

    [Header("Actions for Button Callback")]
    Action OnYesButtonAction;
    Action OnNoButtonAction;
    #endregion

    #region PopUp System
    public void CreatePopUp(string title, string message, Action yes=null, string yesText=null, Action no=null, string noText = null)
    {
        bool hasTitle = string.IsNullOrEmpty(title);
        headerTransform.gameObject.SetActive(hasTitle);
        titleText.text = title;

        contentText.text = message;

        bool hasYesAction = (yes != null);
        yesButton.gameObject.SetActive(hasYesAction);
        if (yesText != null)yesButtonText.text = yesText;
        OnYesButtonAction = yes;

        bool hasNoAction = (no != null);
        noButton.gameObject.SetActive(hasNoAction);
        if (noText != null) noButtonText.text = noText;
        OnNoButtonAction = no;

        transform.SetAsFirstSibling();

        TooglePopUpWindow(Vector3.one);
    }

    void TooglePopUpWindow(Vector3 toggle) => LeanTween.scale(this.gameObject, toggle, 0.1f); // => transform.DOScale(toggle, 0.1f);
    #endregion

    #region Button Callbacks
    public void OnYesClicked()
    {
        OnYesButtonAction?.Invoke();
        TooglePopUpWindow(Vector3.zero);
    }
    public void OnNoClicked()
    {
        OnNoButtonAction?.Invoke();
        TooglePopUpWindow(Vector3.zero);
    }
    public void OnCloseClicked() => TooglePopUpWindow(Vector3.zero);
    
    #endregion
}
