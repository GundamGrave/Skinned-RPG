using UnityEngine;
using UnityEditor;
public class NavigationHelper : EditorWindow
{
    private const int COLLIDER_LAYER = -1; //edit this valve to set which layer a box collider needs to be on to be read

    private const string TEMP_NAV_MESH_OBJECT_TAG = "TempNavMeshItemDestroyable"; //you can change to what ever tag you would like as long as it isn't used by anything else
    private bool isSetup = true;

    [MenuItem("Window/Navigation Helper")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(NavigationHelper));
    }

    //return true if exists
    private bool CheckIfTagExists()
    {
        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
        for (int n = 0; n < tags.Length; n++)
        {
            if (tags[n] == TEMP_NAV_MESH_OBJECT_TAG)
            {
                return true;
            }
        }
        return false;
    }

    void OnGUI()
    {
        GUILayout.Label("Build Nav Mesh Including Custom Box Colliders", EditorStyles.boldLabel);

        if (!isSetup)
        {
            isSetup = CheckIfTagExists();
            if (!isSetup)
            {
                GUILayout.Label("Error - You first need to create a Tag called:");
                EditorGUILayout.TextArea(TEMP_NAV_MESH_OBJECT_TAG);
            }
        }

        if (isSetup)
        {
            if (GUILayout.Button("Build Nav Mesh!"))
            {
                if (CheckIfTagExists())
                {
                    BakeBoxColliders();
                }
                else
                {
                    Debug.LogError("Custom tag was not created");
                    isSetup = false;
                }
            }
        }
    }


    private void BakeBoxColliders()
    {
        CleanUpOldNavMeshItems();
        BoxCollider[] allBoxColliders = GameObject.FindObjectsOfType<BoxCollider>();
        GameObject navMeshCubePrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        DestroyImmediate(navMeshCubePrefab.GetComponent<Collider>());
        navMeshCubePrefab.name = TEMP_NAV_MESH_OBJECT_TAG;
        GameObjectUtility.SetStaticEditorFlags(navMeshCubePrefab, StaticEditorFlags.NavigationStatic);
        navMeshCubePrefab.tag = TEMP_NAV_MESH_OBJECT_TAG;

        GameObject tempNavMeshCube;
        foreach (BoxCollider c in allBoxColliders)
        {
            if (COLLIDER_LAYER < 0 || c.gameObject.layer == COLLIDER_LAYER)
            {
                tempNavMeshCube = Instantiate(navMeshCubePrefab) as GameObject;
                tempNavMeshCube.name = navMeshCubePrefab.name;
                tempNavMeshCube.transform.parent = c.transform;
                tempNavMeshCube.transform.localPosition = c.center;
                tempNavMeshCube.transform.localRotation = Quaternion.identity;
                tempNavMeshCube.transform.localScale = c.size;
                tempNavMeshCube.hideFlags = HideFlags.DontSave;
            }
        }
        DestroyImmediate(navMeshCubePrefab);
        UnityEditor.AI.NavMeshBuilder.BuildNavMeshAsync();
        CleanUpOldNavMeshItems();

    }

    private void CleanUpOldNavMeshItems()
    {
        GameObject[] oldNavMeshItems = GameObject.FindGameObjectsWithTag(TEMP_NAV_MESH_OBJECT_TAG);
        foreach (GameObject go in oldNavMeshItems)
        {
            DestroyImmediate(go);
        }
    }
}