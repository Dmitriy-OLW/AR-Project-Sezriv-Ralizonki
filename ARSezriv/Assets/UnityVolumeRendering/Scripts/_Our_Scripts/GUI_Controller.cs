using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using TMPro;

public class GUI_Controller : MonoBehaviour
{
    public GameObject Edit_dataset_UI; //окно настройки датасета
    public Slider[] Vis;
    public Slider[] Rot;
    //public GameObject Srez_menu_UI;
    //public GameObject Ramka;
    //public GameObject cube;
    //public Slider[] Scale_sliders;
    public TextMeshProUGUI _Load_hotBar;
    public GameObject Load_UI;
    public GameObject Load_dataset_Buton;
    public GameObject Load_file_Buton;
    
    
    public GameObject[] Menu_Mas;

    //public T
    private void Awake()
    {
        Edit_dataset_UI.SetActive(false);
        //Srez_menu_UI.SetActive(false);
        Load_UI.SetActive(false);
        Load_dataset_Buton.SetActive(false);
        Load_file_Buton.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (Edit_dataset_UI.gameObject.activeInHierarchy == true)
        {
            Visibility_Render(Vis[0].value, Vis[1].value);
           //rotation_Render(Rot[0].value, Rot[1].value, Rot[2].value);
            
        }
        //Update_Srez();
    }
    
    //Создание плоскости для разреза фантома из датасета
    public void Create_Slicer()
    {
        GameObject.FindObjectOfType<CrossSectionCreator>().CreateSlicer(); 
    }
    //Создание куба для разреза фантома из датасета, использовать для узи или для послойного разрезания, нужно добавить изменени я scale до одной картинки
    public void Ultrasound_Cropping()
    {
        GameObject.FindObjectOfType<CrossSection_box>().CreateSlicer_for_YSI();
    }
    //Метод для импорта датасета
    public void Import_DICOM_dataset_for_UI()
    {
        Load_dataset_Buton.SetActive(false);
        Load_file_Buton.SetActive(false);
        GameObject.FindObjectOfType<UnityVolumeRendering.RuntimeGUI>().Import_DICOM_dataset();
        Load_UI.SetActive(true);
        Load_dataset_Buton.SetActive(true);
        ActivateOnly(Menu_Mas[1]);


    }
    //Вызом меню для изменения датасета
    public void Edit_imported_dataset_for_UI()
    {
        Load_dataset_Buton.SetActive(false);
        Load_file_Buton.SetActive(false);
        GameObject.FindObjectOfType<UnityVolumeRendering.RuntimeGUI>().Edit_imported_dataset();
        //SetActive менюшки в AR
        Edit_dataset_UI.SetActive(true);
        ActivateOnly(Menu_Mas[3]);
    }
    
    
    //Edith Меню
    
    
    
    
    
    //Изменения  рендер моды, неоьходимо пере
    //0 = "Direct volume rendering", - станадарт 
    //1= "Maximum intensity projection", - прозрачность
    //2= "Isosurface rendering" - контраст
    public void Render_Mode(int index)
    {
        GameObject.FindObjectOfType<UnityVolumeRendering.EditVolumeGUI>().Render_Mode_Update(index);
    }
    //Настройка видимости от 0 до 1
    public void Visibility_Render(float x, float y)
    {
        GameObject.FindObjectOfType<UnityVolumeRendering.EditVolumeGUI>().Visibility_Update(x, y);
    }
    //rotation 
    public void rotation_Render(float x, float y, float z)
    {
        GameObject.FindObjectOfType<UnityVolumeRendering.EditVolumeGUI>().Rotation_Update(x *360, y* 360, z*360);
    }
    //открыть окно загрузки
    public void Load_def()
    {
        GameObject.FindObjectOfType<UnityVolumeRendering.EditVolumeGUI>().Load_transfer_function();
        Load_UI.SetActive(true);
        Load_file_Buton.SetActive(true);
        ActivateOnly(Menu_Mas[1]);
    }
    //Закрытие Эдит меню
    public void Close_Edit_Menu()
    {
        GameObject.FindObjectOfType<UnityVolumeRendering.EditVolumeGUI>().Close_Edit_UI();
        Edit_dataset_UI.SetActive(false);
    }
    
    //Load file Menu
    
    public void load_our_dataset_for_UI()
    {
        string text = _Load_hotBar.text;
        text = text.Substring(0, text.Length - 1);
        GameObject.FindObjectOfType<UnityVolumeRendering.RuntimeFileBrowser.RuntimeFileBrowserComponent>().load_our_dataset(text);
        _Load_hotBar.text = "";
    }
    public void load_our_seeting_for_UI()
    {
        string text = _Load_hotBar.text;
        text = text.Substring(0, text.Length - 1);
        GameObject.FindObjectOfType<UnityVolumeRendering.RuntimeFileBrowser.RuntimeFileBrowserComponent>().load_our_seeting(text);
        _Load_hotBar.text = "";
    }

    public void close_load_menu()
    {
       // GameObject.FindObjectOfType<UnityVolumeRendering.RuntimeFileBrowser.RuntimeFileBrowserComponent>().Close_load();
        Load_UI.SetActive(false);
        Load_dataset_Buton.SetActive(false);
        Load_file_Buton.SetActive(false);
    }
    
    
    
    //Srez Menu
    
    
    
    //Отурыть меню рамок для среза и обрезки
    /*public void Open_Srez_Menu()
    {
        Srez_menu_UI.SetActive(true);
    }
    //Закрыти меню рамок для среза и обрезки
    public void Close_Srez_Menu()
    {
        Srez_menu_UI.SetActive(false);
    }
//Scale для среза, сделать ограничение на минимальный срез
    public void Update_Srez()
    {
        
        cube.transform.localScale = new Vector3(Scale_sliders[0].value, Scale_sliders[1].value, Scale_sliders[2].value);
    }*/
    
    
    
    
    
    public void ActivateOnly(GameObject objectToActivate)
    {
        // Проверяем, существует ли массив
        if (Menu_Mas == null || Menu_Mas.Length == 0)
        {
            Debug.LogWarning("GameObjects array is empty or not assigned!");
            return;
        }

        // Отключаем все объекты в массиве
        foreach (GameObject go in Menu_Mas)
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }

        // Включаем указанный объект, если он не null
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Object to activate is null!");
        }
    }

    public void ActivateMainMenuAfterAnchoring()
    {
        ActivateOnly(Menu_Mas[0]);
    }

}
