using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[System.Serializable]
public class Pool{
    [SerializeField] public GameObject thePrefab;
    [SerializeField] public Transform parent;
    [SerializeField] public int Size;

    
}  
public class PoolController : MonoBehaviour
{   public static PoolController Instance;

    private Dictionary<string, LinkedList<GameObject>> activePools = new Dictionary<string, LinkedList<GameObject>>();
    private Dictionary<string, Queue<GameObject>> inactivePools = new Dictionary<string, Queue<GameObject>>();
    
    [SerializeField] private List<Pool> poolToInitialize = new List<Pool>(); 
    
    
    
    private void Awake() {
        Instance = this;
        foreach (Pool pool in poolToInitialize)
        {
            InitializePool(pool.thePrefab, pool.parent, pool.Size, pool.thePrefab.name);
        }
    }
    public GameObject GetPooledObject(string key)
    {
        if (inactivePools.ContainsKey(key))
        {
            if(inactivePools[key].Count == 0){
                if(key == "XPDrop"){
                    XPDropPool.Instance.Recycle();
                }else if(activePools[key].Count > 0){
                    inactivePools[key].Enqueue(activePools[key].First.Value);
                    activePools[key].RemoveFirst();
                }
                
            }
            if(inactivePools[key].Count > 0){
                var obj = inactivePools[key].Dequeue();
                if(key != "XPDrop"){
                    activePools[key].AddLast(obj);
                }
                obj.SetActive(true);
                return obj;
            }else{
                Debug.LogWarning("The Count is not suposed to be les then 0");
            }
            
        }
        return null; 
    }

    
    public virtual void ReturnPooledObject(string key, GameObject obj)
    {
        if (!inactivePools.ContainsKey(key))
        {   
            inactivePools[key] = new Queue<GameObject>();
        }
        if(key != "XPDrop"){
            activePools[key].Remove(obj);
        }
        obj.SetActive(false);
        inactivePools[key].Enqueue(obj);
    }

    
    private void InitializePool(GameObject prefab, Transform parent, int size, string key)
    {   
        if (!inactivePools.ContainsKey(key) && size > 0)
        {
            inactivePools[key] = new Queue<GameObject>();
            activePools[key] = new LinkedList<GameObject>();
            for (int i = 0; i < size; i++)
            {   
                GameObject newObj = Instantiate(prefab, parent);
                newObj.name = newObj.name.Replace("(Clone)", "").Trim();
                newObj.SetActive(false);
                inactivePools[key].Enqueue(newObj);
            }
        }
    }
}
