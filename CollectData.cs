using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace Ziyu_DataCollectionScript
{

    public class CollectData : EditorWindow
    {
        //���ԡ�������������������������������������������������������������������������������������������������������������������--
        //��������������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������

        //��������
        GameObject node = null;
        


        //��������

        //�����б�
        List<GameObject> childList = new List<GameObject>();
        List<GameObject> AllInstances=new List<GameObject>();
        List<GameObject> AllPrefab=new List<GameObject>();
        List<string> AllPrefabPath=new List<string>();
        List<GameObject> childListPrefab=new List<GameObject>();
        Renderer[] rendArray;
        List<Material> materials = new List<Material>();
        List<string> materialsName = new List<string>();



        //���ּ���
        int instanceCount = 0;
        int prefabCount = 0;
        int childCount = 0;
        int materialCount=0;

        int vertexInstance = 0;
        int surfaceInstance = 0;
        
       

        //����
        private Vector2 _scrollPosition;

        //UI���
        string filterInput;
        bool isOpen = false;



        //��������������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������

        //  UI���
        [MenuItem("Ziyu's Tool/Data Collection &q")]
        private static void ShowWindow()
        {
            CollectData window = GetWindow<CollectData>();
            window.titleContent = new GUIContent("�����ռ�");
            window.Show();
            window.position = new Rect(new Vector2(600, 25), new Vector2(600, 600));
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Label("��Ҫͳ����Ϣ������ڵ�", GUILayout.Width(200));

            EditorGUI.BeginChangeCheck();
            node = EditorGUILayout.ObjectField(node, typeof(GameObject), true, GUILayout.Width(350)) as GameObject;
         
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            if (EditorGUI.EndChangeCheck())
            {
                Init();
            }
            
            GUILayout.Button("��������", GUILayout.Width(595));
      


            GUILayout.BeginHorizontal();
            GUILayout.Label("�ýڵ���Instance��ĿΪ:",GUILayout.Width(500));
            GUILayout.Label(instanceCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("�ýڵ���Prefab��ĿΪ:", GUILayout.Width(500));
            GUILayout.Label(prefabCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("�ýڵ���GameObject��ĿΪ:", GUILayout.Width(500));
            GUILayout.Label(childCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("�ýڵ���Material��ĿΪ:",GUILayout.Width(500));
            GUILayout.Label(materialCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("�ýڵ���Vertex��ĿΪ:", GUILayout.Width(500));
            GUILayout.Label(vertexInstance.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("�ýڵ���Triangles��ĿΪ:",GUILayout.Width(500) );
            GUILayout.Label(surfaceInstance.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();





            GUILayout.Space(20);
           
            if(node!=null)
            {
                GUILayout.Label("�ýڵ�Prefab�б�", GUILayout.Width(600));
                isOpen = EditorGUILayout.ToggleLeft("�Ƿ�ͨ���ؼ��ֲ���Prefab", isOpen, GUILayout.Width(400));
                if (isOpen)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("������ؼ���", GUILayout.Width(280));
                    filterInput = GUILayout.TextField(filterInput, GUILayout.Width(300));
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);
                }
            }
                       
            
          
           
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            foreach (GameObject ob in AllPrefab)
            {
                if(ob.name.IndexOf(filterInput)==-1)
                {
                    continue;
                }
                GUILayout.BeginHorizontal();
                
                if(GUILayout.Button(ob.name,GUILayout.Width(200)))
                {
                    Selection.objects = SelectInstance(ob);
                    
                }
                GUILayout.Box("������Ϊ    " + GetVertexTriPrefab(ob).vertex, GUILayout.Width(190));
                GUILayout.Box("��������Ϊ    " + GetVertexTriPrefab(ob).triangle, GUILayout.Width(190));
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
         
        }



        //�̶�����������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        private void onEnable()
        {
            Init();
        }
        private void Update()
        {

        }




        //��ʼ��/ˢ�� ��������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        private void Init()
        {
            childList.Clear();
            AllPrefab.Clear();
            AllInstances.Clear();
            AllPrefabPath.Clear();
            childListPrefab.Clear();
            materials.Clear();
            materialsName.Clear();
            prefabCount = 0;
            instanceCount = 0;
            childCount = 0;
            vertexInstance = 0;
            surfaceInstance = 0;
            materialCount=0;
            filterInput = "";


            Count();
        }


        //���ռ���
        private void Count()  //ͳ�Ƹýڵ���������������Ŀ
        {
            //�����б�����
            GetAllChild(node);
            GetVertexTri();
            GetInstanceList(node);
            GetPrefabList();
            GetAllMaterials();



            //��ȡ����
            childCount = childList.Count;
            instanceCount = AllInstances.Count;
            prefabCount = AllPrefab.Count;
            materialCount = materials.Count;



        }


        //������  ������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        private void GetAllChild(GameObject node)
        {

            if (node != null)
            {
                for (int i = 0; i < node.transform.childCount; i++)
                {                    

                    if (node.transform.GetChild(i).childCount > 0)
                    {
                        GetAllChild(node.transform.GetChild(i).gameObject);
                    }                   

                    childList.Add(node.transform.GetChild(i).gameObject);             //��ȡ��ǰ����ڵ������������壬����������������������б�

                }
            }
        }
        private void GetVertexTri()         //���㶥������������
        {
            foreach (GameObject obj in childList)
            {
                Component[] filters;
                filters = obj.GetComponentsInChildren<MeshFilter>();
                foreach (MeshFilter f in filters)
                {
                    vertexInstance += f.sharedMesh.vertexCount;
                    surfaceInstance += f.sharedMesh.triangles.Length / 3;
                }
            }
        }

        //ʵ��������Instance  ������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        private void GetInstanceList(GameObject node)         //���������ڵ�������instant��prefab������������и����������instance�б���
        {
            
            for (int i = 0; i < node.transform.childCount; i++)
            {

                if (PrefabBool(node.transform.GetChild(i).gameObject))
                {
                    
                    AllInstances.Add(node.transform.GetChild(i).gameObject);
                }

                else if (node.transform.GetChild(i).childCount > 0)
                {
                    GetInstanceList(node.transform.GetChild(i).gameObject);
                }

            }
        }


        //Ԥ����Prefab    ����������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������

        private void GetPrefabList()             //���ڵ�������������prefab��⣬�����Ϊ��ͬprefab��������µ�list����ΪprefabList�����ظ�������
        {
            
            foreach (GameObject obj in AllInstances)
            {
                string prefabPath=GetPrefabPath(obj);
                if(!AllPrefabPath.Contains(prefabPath))
                {
                    AllPrefabPath.Add(prefabPath);
                    AllPrefab.Add(obj);
                }
                 

            }
        }
        private VerTri GetVertexTriPrefab(GameObject obj)         //���㶥������������
        {
            VerTri tri = new VerTri();
            foreach (GameObject ob in GetAllChildPrefab(obj))
            {
                Component[] filters;
                filters = ob.GetComponentsInChildren<MeshFilter>();
                foreach (MeshFilter f in filters)
                {
                    tri.vertex += f.sharedMesh.vertexCount;
                    tri.triangle += f.sharedMesh.triangles.Length / 3;
                }
            }
            return tri;
        }
        private List<GameObject> GetAllChildPrefab(GameObject obj)         
        {

            if (obj != null)
            {
                List <GameObject> list= new List<GameObject>();
                for (int i = 0; i < obj.transform.childCount; i++)
                {

                    if (obj.transform.GetChild(i).childCount > 0)
                    {
                        GetAllChildPrefab(obj.transform.GetChild(i).gameObject);
                    }

                    list.Add(obj.transform.GetChild(i).gameObject);             //��ȡ��ǰ����ڵ������������壬����������������������б�

                }
                return list;
            }
            return null;
        }

        //����Material    ����������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������


        

        private void GetAllMaterials()
        {
            materials.Clear();
            foreach(GameObject obj in childList)
            {
                rendArray = obj.transform.GetComponentsInChildren<Renderer>(true);
                for (int i = 0; i < rendArray.Length; i++)
                {
                    Material[] mats = rendArray[i].materials;
                    for (int j = 0; j < mats.Length; j++)
                    {
                        if(!materialsName.Contains(mats[j].name))
                        {
                            materials.Add(mats[j]);
                            materialsName.Add(mats[j].name);
                        }
                       
                    }
                }
            }
            
          
        }








        //���ֹ��ߺ�����������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        //��������������������������������������������������������������������������������������������������������������������������������
        private static bool PrefabBool(GameObject obj)           //�ж������Ƿ�ΪInstance Prefab
        {
            if (obj != null)
            {
                var type = PrefabUtility.GetPrefabAssetType(obj);
                var status = PrefabUtility.GetPrefabInstanceStatus(obj);
                if (type == PrefabAssetType.NotAPrefab || status == PrefabInstanceStatus.NotAPrefab)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return false;
        }

        private string GetPrefabPath(GameObject gobj)                //��ʵ���������ȡԴ����·��
        {
            if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(gobj))
            {
                return UnityEditor.AssetDatabase.GetAssetPath(gobj);
            }
            if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(gobj))
            {
                var assetPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(gobj);
                return UnityEditor.AssetDatabase.GetAssetPath(assetPrefab);
            }
            return null;

        }

        private GameObject[] SelectInstance(GameObject obj)
        {
            List<GameObject> sel = new List<GameObject>();
            string assetPath= GetPrefabPath(obj);
            foreach(GameObject instance in AllInstances)
            {
                string instancePath =GetPrefabPath(instance);
                if(assetPath== instancePath)
                {
                    sel.Add(instance);
                }
            }
            return sel.ToArray();
        }

  

       





    }

    class VerTri
    {
        public int vertex;
        public int triangle;
        public VerTri()
        {
            vertex = 0;
            triangle = 0;
        }

    }
}

