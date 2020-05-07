using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;

public class ChunkActivator : MonoBehaviour
{
    [SerializeField]
    private int distFromPlayer;
   
    private GameObject player;

    public GameObject terrainChunk;

    public Dictionary<GameObject, NavMeshDataInstance> navInstances = new Dictionary<GameObject, NavMeshDataInstance>();

    public List<ActivatorChunk> activatorChunks, loadedChunks = new List<ActivatorChunk>(), generatedChunks = new List<ActivatorChunk>();


    // Start is called before the first frame update
    void Start()
    {
        
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        DirectoryInfo[] info = dir.GetDirectories();
        foreach(DirectoryInfo F in info)
        {
            if(!F.FullName.Contains("WorldChunks"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/WorldChunks");
            }
        }

        player = GameObject.Find("Player");
        player.GetComponent<Rigidbody>().isKinematic = true;
        activatorChunks = new List<ActivatorChunk>();

        Debug.Log(Application.persistentDataPath);
        //SaveToFile();
        StartCoroutine (LoadFromFile());
        StartCoroutine("CheckActivation");
    }

    void SaveToFile()
    {
        foreach(ActivatorChunk chunk in generatedChunks)
        {
            string destination = Application.persistentDataPath + "/WorldChunks/" + chunk.chunkPos.ToString();

            StreamWriter file = new StreamWriter(destination, false);

            file.WriteLine(chunk.color.ToString());
            //file.WriteLine(;

            file.Close();
        }

        generatedChunks.Clear();
    }

    IEnumerator LoadFromFile(string position = "(0, -10000, 0)")
    {
        yield return null;


        if (position == "(0, -10000, 0)")
        {
            Dictionary<string, string> chunkPosS = new Dictionary<string, string>();

            foreach (FileInfo F in GetFiles())
            {
                F.Refresh();
                WWW www = new WWW(Application.persistentDataPath + "/WorldChunks/" + F.Name);
                if (!www.isDone)
                {
                    yield return new WaitUntil(delegate () { return www.isDone; });
                }
                string data = www.text;
                Debug.Log(data);
                chunkPosS.Add(F.Name, data);
                
            }

            foreach (KeyValuePair<string, string> s in chunkPosS)
            {
                loadedChunks.Add(new ActivatorChunk(StringToVector3(s.Key)));
                Debug.Log(s.Value);
                loadedChunks[loadedChunks.Count - 1].color = StringToColour(s.Value);
            }
        }
        else
        {
            WWW www = new WWW(Application.persistentDataPath + "/WorldChunks/" + position);
            if (!www.isDone)
            {
                yield return new WaitUntil(delegate () { return www.isDone; });
            }
            string data = www.text;

            loadedChunks.Add(new ActivatorChunk(StringToVector3(position)));
            loadedChunks[loadedChunks.Count - 1].color = StringToColour(data);
        }
        player.GetComponent<Rigidbody>().isKinematic = false;
    }

    FileInfo[] GetFiles()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/WorldChunks/");
        FileInfo[] info = dir.GetFiles(".");

        return info;
    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

    public static Color StringToColour(string sColour)
    {
        // Remove the parentheses
       
            sColour = sColour.Substring(5, sColour.Length - 8);
        

        // split the items
        string[] sArray = sColour.Split(',');

        Debug.Log(sArray[0].Trim(' '));
        Debug.Log(sArray[1].Trim(' '));
        Debug.Log(sArray[2].Trim(' '));
        Debug.Log(sArray[3].Trim(' '));

        // store as a Vector3
        Color result = new Color(
            float.Parse(sArray[0].Trim(' ')),
            float.Parse(sArray[1].Trim(' ')),
            float.Parse(sArray[2].Trim(' ')),
            float.Parse(sArray[3].Trim(' ')));

        return result;
    }

    Vector3[] potentialChunks = { new Vector3(10, 0, 0), new Vector3(10, 0, 10), new Vector3(0, 0, 10), new Vector3(-10, 0, 0), new Vector3(-10, 0, -10), new Vector3(0, 0, -10) };
    IEnumerator CheckActivation()
    {


        if(loadedChunks.Count > 0)
        {

            Vector3 temp = Vector3.down * 10000;
            bool bake = false;
            foreach (ActivatorChunk item in loadedChunks)
            {

                if (Vector3.Distance(player.transform.position, item.chunkPos) < Vector3.Distance(player.transform.position, temp))
                {
                    temp = item.chunkPos;
                }
                if (Vector3.Distance(player.transform.position, item.chunkPos) <= distFromPlayer)
                {
                    if (item.chunk == null)
                    {
                        item.chunk = Instantiate(terrainChunk, item.chunkPos, Quaternion.identity);
                        //navInstances.Add(item.chunk, NavMesh.AddNavMeshData(item.chunk.GetComponent<NavMeshSurface>().navMeshData, item.chunk.transform.position, item.chunk.transform.rotation));
                            
                            
                        item.chunk.GetComponent<Renderer>().material.color = item.color;
                        bake = true;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    if(item.chunk != null)
                    {
                        //NavMesh.RemoveNavMeshData(navInstances[item.chunk]);
                        //navInstances.Remove(item.chunk);
                        
                        Destroy(item.chunk);
                        
                    }
                    bake = true;
                }

                
            }

            foreach (Vector3 PC in potentialChunks)
            {
                if(Vector3.Distance(temp + PC, player.transform.position) < distFromPlayer * 2)
                {
                    if(!ChunkExists(temp + PC))
                    {
                        GenerateChunk(temp + PC);
                    }
                }
            }
            if(bake)
            GetComponent<NavMeshSurface>().BuildNavMesh();
        }
        else
        {
            if (!ChunkExists(Vector3.down))
            {
                GenerateChunk(Vector3.down);
            }
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine("CheckActivation");
    }

    void GenerateChunk(Vector3 position)
    {
        generatedChunks.Add(new ActivatorChunk(position));

        generatedChunks[generatedChunks.Count - 1].color = Random.ColorHSV();

        SaveToFile();
        StartCoroutine (LoadFromFile(position.ToString()));
    }

    bool ChunkExists(Vector3 position)
    {
        if (position.x % 10 == 0 && position.z % 10 == 0 && position.y == -1)
        {
            List<string> fileNames = new List<string>();

            foreach (FileInfo F in GetFiles())
            {
                F.Refresh();
                fileNames.Add(F.Name);
            }
            return fileNames.Contains(position.ToString());
        }
        else
        {
            Debug.LogWarning("Position passed to Function is off the grid :" + position);
            return true;
        }
    }
}

[System.Serializable]
public class ActivatorChunk
{
    public GameObject chunk;
    public Vector3 chunkPos;
    public ActivatorChunk(Vector3 pos) 
    {
        chunkPos = pos;
    }
    public Color color;
}

