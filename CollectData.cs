using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace Ziyu_DataCollectionScript
{

    public class CollectData : EditorWindow
    {
        //ÊôÐÔ¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª--
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª

        //ÊäÈëÊý¾Ý
        GameObject node = null;
        


        //±£´æÊý¾Ý

        //¸÷ÖÖÁÐ±í
        List<GameObject> childList = new List<GameObject>();
        List<GameObject> AllInstances=new List<GameObject>();
        List<GameObject> AllPrefab=new List<GameObject>();
        List<string> AllPrefabPath=new List<string>();
        List<GameObject> childListPrefab=new List<GameObject>();
        Renderer[] rendArray;
        List<Material> materials = new List<Material>();
        List<string> materialsName = new List<string>();



        //¸÷ÖÖ¼ÆÊý
        int instanceCount = 0;
        int prefabCount = 0;
        int childCount = 0;
        int materialCount=0;

        int vertexInstance = 0;
        int surfaceInstance = 0;
        
       

        //³£Á¿
        private Vector2 _scrollPosition;

        //UIÃæ°å
        string filterInput;
        bool isOpen = false;



        //º¯Êý¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª

        //  UIÃæ°å
        [MenuItem("Ziyu's Tool/Data Collection &q")]
        private static void ShowWindow()
        {
            CollectData window = GetWindow<CollectData>();
            window.titleContent = new GUIContent("Êý¾ÝÊÕ¼¯");
            window.Show();
            window.position = new Rect(new Vector2(600, 25), new Vector2(600, 600));
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Label("ÐèÒªÍ³¼ÆÐÅÏ¢µÄÎïÌå½Úµã", GUILayout.Width(200));

            EditorGUI.BeginChangeCheck();
            node = EditorGUILayout.ObjectField(node, typeof(GameObject), true, GUILayout.Width(350)) as GameObject;
         
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            if (EditorGUI.EndChangeCheck())
            {
                Init();
            }
            
            GUILayout.Button("ÎïÌåÊý¾Ý", GUILayout.Width(595));
      


            GUILayout.BeginHorizontal();
            GUILayout.Label("¸Ã½ÚµãÏÂInstanceÊýÄ¿Îª:",GUILayout.Width(500));
            GUILayout.Label(instanceCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("¸Ã½ÚµãÏÂPrefabÊýÄ¿Îª:", GUILayout.Width(500));
            GUILayout.Label(prefabCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("¸Ã½ÚµãÏÂGameObjectÊýÄ¿Îª:", GUILayout.Width(500));
            GUILayout.Label(childCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("¸Ã½ÚµãÏÂMaterialÊýÄ¿Îª:",GUILayout.Width(500));
            GUILayout.Label(materialCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("¸Ã½ÚµãÏÂVertexÊýÄ¿Îª:", GUILayout.Width(500));
            GUILayout.Label(vertexInstance.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("¸Ã½ÚµãÏÂTrianglesÊýÄ¿Îª:",GUILayout.Width(500) );
            GUILayout.Label(surfaceInstance.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();





            GUILayout.Space(20);
           
            if(node!=null)
            {
                GUILayout.Label("¸Ã½ÚµãPrefabÁÐ±í£º", GUILayout.Width(600));
                isOpen = EditorGUILayout.ToggleLeft("ÊÇ·ñÍ¨¹ý¹Ø¼ü×Ö²éÕÒPrefab", isOpen, GUILayout.Width(400));
                if (isOpen)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("ÇëÊäÈë¹Ø¼ü×Ö", GUILayout.Width(280));
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
                GUILayout.Box("¶¥µãÊýÎª    " + GetVertexTriPrefab(ob).vertex, GUILayout.Width(190));
                GUILayout.Box("Èý½ÇÃæÊýÎª    " + GetVertexTriPrefab(ob).triangle, GUILayout.Width(190));
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
         
        }



        //¹Ì¶¨º¯Êý¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        private void onEnable()
        {
            Init();
        }
        private void Update()
        {

        }




        //³õÊ¼»¯/Ë¢ÐÂ ¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
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


        //×îÖÕ¼ÆÊý
        private void Count()  //Í³¼Æ¸Ã½ÚµãÏÂËùÓÐ×ÓÎïÌåÊýÄ¿
        {
            //ÉèÖÃÁÐ±íÊý¾Ý
            GetAllChild(node);
            GetVertexTri();
            GetInstanceList(node);
            GetPrefabList();
            GetAllMaterials();



            //»ñÈ¡¼ÆÊý
            childCount = childList.Count;
            instanceCount = AllInstances.Count;
            prefabCount = AllPrefab.Count;
            materialCount = materials.Count;



        }


        //×ÓÎïÌå  ¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
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

                    childList.Add(node.transform.GetChild(i).gameObject);             //»ñÈ¡µ±Ç°ÎïÌå½ÚµãÏÂËùÓÐ×ÓÎïÌå£¬½«ËùÓÐ×ÓÎïÌå¼ÓÈë×ÓÎïÌåÁÐ±í

                }
            }
        }
        private void GetVertexTri()         //¼ÆËã¶¥µãÊýÓëÈý½ÇÃæ
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

        //ÊµÀý»¯¶ÔÏóInstance  ¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        private void GetInstanceList(GameObject node)         //¼ÆËã¸ÃÎïÌå½ÚµãÏÂËùÓÐinstantµÄprefab×ÓÎï¼þ£¬½«ËùÓÐ¸ÃÀàÎïÌå¼ÓÈëinstanceÁÐ±íÖÐ
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


        //Ô¤ÖÆÌåPrefab    ¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª

        private void GetPrefabList()             //½«½ÚµãÎïÌåÏÂ×ÓÎïÌåprefab¼ì²â£¬Èô¼ì²âÎª²»Í¬prefabÔò½«Æä·ÃÈëÐÂµÄlistÖÐ×÷ÎªprefabList£¬ÈôÖØ¸´ÔòÌø¹ý
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
        private VerTri GetVertexTriPrefab(GameObject obj)         //¼ÆËã¶¥µãÊýÓëÈý½ÇÃæ
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

                    list.Add(obj.transform.GetChild(i).gameObject);             //»ñÈ¡µ±Ç°ÎïÌå½ÚµãÏÂËùÓÐ×ÓÎïÌå£¬½«ËùÓÐ×ÓÎïÌå¼ÓÈë×ÓÎïÌåÁÐ±í

                }
                return list;
            }
            return null;
        }

        //²ÄÖÊMaterial    ¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª


        

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








        //¸÷ÖÖ¹¤¾ßº¯Êý¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        //¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª¡ª
        private static bool PrefabBool(GameObject obj)           //ÅÐ¶ÏÎïÌåÊÇ·ñÎªInstance Prefab
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

        private string GetPrefabPath(GameObject gobj)                //´ÓÊµÀý»¯ÎïÌå»ñÈ¡Ô´×ÊÁÏÂ·¾¶
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

