using UnityEngine;
using System.IO;

public class CreateTFFile : MonoBehaviour
{
    [Tooltip("Файл из проекта (перетащите .txt, .json и т. д.)")]
    public TextAsset sourceFile;

    [Tooltip("Имя выходного файла (без .tf)")]
    public string outputFileName = "output";

    [Tooltip("Автоматически создавать файл при старте?")]
    public bool createOnStart = true;

    [Tooltip("Перезаписывать, если файл уже существует?")]
    public bool overwriteExisting = true;

    void Start()
    {
        if (createOnStart)
            CreateFile();
    }

    public void CreateFile()
    {
        if (sourceFile == null)
        {
            Debug.LogError("[CreateTFFile] Source file not assigned!");
            return;
        }

        // Путь к папке persistentDataPath (работает в билде)
        string outputDir = Application.persistentDataPath;
        string outputPath = Path.Combine(outputDir, outputFileName + ".tf");

        // Проверяем, существует ли файл
        if (File.Exists(outputPath))
        {
            if (!overwriteExisting)
            {
                Debug.LogWarning($"[CreateTFFile] File already exists (overwrite disabled): {outputPath}");
                return;
            }
            Debug.Log($"[CreateTFFile] Overwriting existing file: {outputPath}");
        }

        try
        {
            // Создаём папку, если её нет (на всякий случай)
            Directory.CreateDirectory(outputDir);

            // Записываем файл
            File.WriteAllBytes(outputPath, sourceFile.bytes);
            Debug.Log($"[CreateTFFile] File saved to: {outputPath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[CreateTFFile] Error: {e.Message}");
        }
    }

    // Метод для ручного вызова из других скриптов
    public void CreateFile(TextAsset customFile, string customName)
    {
        sourceFile = customFile;
        outputFileName = customName;
        CreateFile();
    }

    public void LoadFile()
    {
        GameObject.FindObjectOfType<UnityVolumeRendering.RuntimeGUI>().Edit_imported_dataset();
        GameObject.FindObjectOfType<UnityVolumeRendering.EditVolumeGUI>().Load_transfer_function();
        GameObject.FindObjectOfType<UnityVolumeRendering.RuntimeFileBrowser.RuntimeFileBrowserComponent>().load_our_dataset(GetFilePath());
        GameObject.FindObjectOfType<UnityVolumeRendering.EditVolumeGUI>().Close_Edit_UI();
    }
    // Получение полного пути к файлу
    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, outputFileName + ".tf");
    }
}