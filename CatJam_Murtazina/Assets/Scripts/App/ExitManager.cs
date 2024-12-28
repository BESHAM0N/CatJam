using UnityEngine;

public class ExitManager : MonoBehaviour
{
    void Update()
    {
        // Проверяем, нажата ли клавиша Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Если приложение запущено в редакторе Unity
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // Если приложение запущено как сборка, выходим из игры
            Application.Quit();
#endif
        }
    }
}