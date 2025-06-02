using System.Collections.Generic;

public static class SceneTracker
{
    public static Stack<string> sceneHistory = new Stack<string>();

    public static void RecordScene(string sceneName)
    {
        sceneHistory.Push(sceneName);
    }

    public static string GetPreviousNonGalleryScene()
    {
        while (sceneHistory.Count > 0)
        {
            string previous = sceneHistory.Pop();
            if (!previous.StartsWith("Gallerij"))
            {
                return previous;
            }
        }
        return null;
    }
}
