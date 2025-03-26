using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelManager : MonoBehaviour
{
    [Header("Models")]
    public GameObject phantomModel;
    public GameObject referenceModel;

    [Header("Canvases")]
    public GameObject phantomCanvas;
    public GameObject referenceCanvas;

    [Header("UI Buttons")]
    public Button showPhantomButton;
    public Button showReferenceButton;

    private GameObject currentActiveModel;
    private GameObject currentActiveCanvas;

    private void Start()
    {
        // Инициализация - все модели и канвасы выключены
        phantomModel.SetActive(false);
        referenceModel.SetActive(false);
        phantomCanvas.SetActive(false);
        referenceCanvas.SetActive(false);

        // Назначение обработчиков кнопок
        showPhantomButton.onClick.AddListener(() => ShowModel(phantomModel, phantomCanvas));
        showReferenceButton.onClick.AddListener(() => ShowModel(referenceModel, referenceCanvas));
    }

    private void ShowModel(GameObject modelToShow, GameObject canvasToShow)
    {
        // Скрыть текущие активные модель и канвас
        if (currentActiveModel != null) currentActiveModel.SetActive(false);
        if (currentActiveCanvas != null) currentActiveCanvas.SetActive(false);

        // Показать выбранные модель и канвас
        modelToShow.SetActive(true);
        canvasToShow.SetActive(true);

        // Обновить текущие активные модель и канвас
        currentActiveModel = modelToShow;
        currentActiveCanvas = canvasToShow;
    }
}