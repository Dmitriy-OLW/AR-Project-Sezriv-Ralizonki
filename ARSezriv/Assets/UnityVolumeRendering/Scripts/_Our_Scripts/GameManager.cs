using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Метод для перезапуска текущей сцены
    public void RestartScene()
    {
        // Получаем индекс текущей сцены и загружаем её заново
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        
        // Альтернативный вариант с именем сцены (если нужно)
        // string currentSceneName = SceneManager.GetActiveScene().name;
        // SceneManager.LoadScene(currentSceneName);
    }

    // Метод для выхода из игры
    public void QuitGame()
    {
        // В редакторе Unity останавливаем проигрывание
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // В собранной версии игры закрываем приложение
        Application.Quit();
        #endif
    }
}