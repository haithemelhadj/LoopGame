using UnityEngine;
using UnityEngine.SceneManagement;
public class PuzzleManager : MonoBehaviour
{

    public Material[] skyboxes;
    public bool gamePaused = false;

    private void Start()
    {
        gamePaused = false;
        PauseGame();
        // Set the skybox material
        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[Random.Range(0, skyboxes.Length)];
        }



    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    #region PauseGame

    public GameObject nextSceneUi;
    public GameObject pauseUi;
    public GameObject gameUi;
    public void PauseGame()
    {
        if (!gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0f; // Pause the game
            //unlock cursor and show it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            gamePaused = false;
            Time.timeScale = 1f; // Pause the game
            //lock cursor and hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        pauseUi.SetActive(gamePaused);
        //nextSceneUi.SetActive(!gamePaused);
        gameUi.SetActive(!gamePaused);
    }

    #endregion

    #region
    public void MainMenu()
    {
        // Load the main menu scene (assuming it's the first scene in the build settings)
        SceneManager.LoadScene(0);
    }
    #endregion

    #region scene management
    // next scene in build settings
    public void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Load the next scene in the build settings
        Debug.Log("SC: "+SceneManager.sceneCount);
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings-1)
            SceneManager.LoadScene(currentSceneIndex + 1);
        else
            SceneManager.LoadScene(0);

    }

    #endregion

    #region CreateSlicedCircle
    /*
    public int sliceCount = 8;
    //public int slicesCount;
    public float radius = 1f;
    public float height = 0.2f;
    public Material sliceMaterial;
    public void CreateCircle()
    {
        float angleStep = 360f / sliceCount;

        for (int i = 0; i < sliceCount; i++)
        {
            float angle = i * angleStep;
            Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            GameObject slice = GameObject.CreatePrimitive(PrimitiveType.Cube);
            slice.transform.position = position;
            slice.transform.localScale = new Vector3(1, 1, 0.1f); // Adjust scale as needed
            slice.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    void CreateSlicedCircle()
    {
        float angleStep = 360f / sliceCount;

        for (int i = 0; i < sliceCount; i++)
        {
            float angleStart = i * angleStep;
            float angleEnd = angleStart + angleStep;

            GameObject slice = new GameObject("Slice_" + i);
            slice.transform.parent = transform;

            MeshFilter mf = slice.AddComponent<MeshFilter>();
            MeshRenderer mr = slice.AddComponent<MeshRenderer>();
            mr.material = sliceMaterial;

            mf.mesh = CreateSectorMesh(angleStart, angleEnd, radius, height);
        }
    }

    Mesh CreateSectorMesh(float angleStart, float angleEnd, float radius, float height)
    {
        int resolution = 10;

        float radStart = Mathf.Deg2Rad * angleStart;
        float radEnd = Mathf.Deg2Rad * angleEnd;

        Vector3[] bottom = new Vector3[resolution + 2];
        Vector3[] top = new Vector3[resolution + 2];

        bottom[0] = Vector3.zero;
        top[0] = Vector3.up * height;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / resolution;
            float angle = Mathf.Lerp(radStart, radEnd, t);
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            bottom[i + 1] = new Vector3(x, 0, z);
            top[i + 1] = new Vector3(x, height, z);
        }

        Mesh mesh = new Mesh();
        var vertices = new System.Collections.Generic.List<Vector3>();
        var triangles = new System.Collections.Generic.List<int>();

        // Bottom face
        int offset = vertices.Count;
        vertices.AddRange(bottom);
        for (int i = 1; i < bottom.Length - 1; i++)
        {
            triangles.Add(offset);
            triangles.Add(offset + i + 1);
            triangles.Add(offset + i);
        }

        // Top face
        offset = vertices.Count;
        vertices.AddRange(top);
        for (int i = 1; i < top.Length - 1; i++)
        {
            triangles.Add(offset);
            triangles.Add(offset + i);
            triangles.Add(offset + i + 1);
        }

        // Side faces
        for (int i = 1; i < bottom.Length; i++)
        {
            Vector3 bl = bottom[i];
            Vector3 br = (i < bottom.Length - 1) ? bottom[i + 1] : bottom[i];
            Vector3 tl = top[i];
            Vector3 tr = (i < top.Length - 1) ? top[i + 1] : top[i];

            int baseIndex = vertices.Count;
            vertices.Add(bl); // 0
            vertices.Add(br); // 1
            vertices.Add(tl); // 2
            vertices.Add(tr); // 3

            triangles.Add(baseIndex + 0);
            triangles.Add(baseIndex + 2);
            triangles.Add(baseIndex + 3);

            triangles.Add(baseIndex + 0);
            triangles.Add(baseIndex + 3);
            triangles.Add(baseIndex + 1);
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();
        return mesh;
    }
    /**/
    #endregion
}
