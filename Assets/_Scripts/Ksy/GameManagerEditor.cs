//using System;
//using System.Collections.Generic;
//using KSY.Manager;
//using KSY.Tile;
//using UnityEditor;
//using UnityEngine;



//[CustomEditor(typeof(GameManager),false)]
//public class GameManagerEditor : Editor
//{
//    private GameManager reference;
//    private sbyte _mapSizeX => reference.MapSizeX;
//    private sbyte _mapSizeY => reference.MapSizeY;
//    private List<MapData> _maps => reference.Maps;
//    private List<TargetsData> _targets => reference.Targets;

//    private void OnEnable()
//    {
//        reference = (GameManager)target;
//    }
//    public override void OnInspectorGUI()
//    {
//        //bool ���� ��ȯ�ϴµ�, �⺻ GUI ��Ʈ�ѷ� �Է� �������� ���� ������ ��� true, �׷��� ���� ��� false�� ��ȯ�Ѵ�.
//        //Ŀ���� ������ �������̽����� �⺻������ �׷����� �ν����͸� �׸���.
//        //DrawDefaultInspector();

//        //hasUnsavedChanges = true;

//        //�׳� DrawDefaultInspector()�� ȣ���Ѵ�.
//        base.OnInspectorGUI();
//        GUILayout.Label("");

//        if(GUILayout.Button("Create Map"))
//        {
//            MapData mData = new MapData(_mapSizeX,_mapSizeY);
//            TargetsData tData = new TargetsData(_mapSizeX, _mapSizeY);
//            _maps.Add(mData);
//            _targets.Add(tData);
//        }

//        if(GUILayout.Button("Delete Map"))
//        {
//            if(_maps.Count > 0)
//                _maps.RemoveAt(_maps.Count - 1);

//            if(_targets.Count > 0)
//                _targets.RemoveAt(_targets.Count - 1);
//        }

//        for(int g = 0; g < _maps.Count; g++)
//        {
//            GUILayout.Label("");
//            GUILayout.Label("");
//            GUILayout.Label("");
//            GUILayout.Label($"Map {g + 1}");
//            for (int h = 0; h < _mapSizeY; h++)
//            {
//                GUILayout.BeginHorizontal();
//                for (int w = 0; w < _mapSizeX; w++)
//                {
//                    GameObject field = _maps[g].rows[h].colums[w];
//                    GameObject tile = (GameObject)EditorGUILayout.ObjectField(field, typeof(GameObject), true);
//                    _maps[g].rows[h].colums[w] = tile;
//                }
//                GUILayout.EndHorizontal();
//            }

//            GUILayout.Label("");
//            GUILayout.Label($"Targets {g + 1}");
//            for (int h = 0; h < _mapSizeY; h++)
//            {
//                GUILayout.BeginHorizontal();
//                for (int w = 0; w < _mapSizeX; w++)
//                {
//                    TileType field = _targets[g].rows[h].colums[w];
//                    field = (TileType)EditorGUILayout.EnumPopup(field);
//                    _targets[g].rows[h].colums[w] = field;
//                }
//                GUILayout.EndHorizontal();
//            }
//        }

//        //serializedObject : ����ȭ�� ������Ʈ
//        //Debug.Log(serializedObject.targetObject);

//        //����ȭ�� �ν������� ���� ������ �����϶�� �޼����� ������ �����ϴ� �Ӽ�

//        //�ν����� ���� ������ �����϶�� �޼����� ���� �� �޼����� ������ �� �ִ� �Ӽ�
//        //saveChangesMessage = "Change Map?";
//        //Debug.Log(saveChangesMessage);

//        //�� ������ ��ũ��Ʈ�� �����ϰ� �ִ� Object�� ��ȯ�Ѵ�.
//        //Debug.Log(target);
//        //Object �迭�� ��ȯ�Ѵ�. (Object[])
//        //Debug.Log(targets);
//    }
//    //�� �޼��带 ������(�������̵�)�Ͽ� �������� ������ �����϶�� �޼����� ������. ���� ������� ���� �۾��� ���� �ʰ� �Ѵ�.
//    //DiscardChanges�� �ݴ� �������� �� �� �������̵��ϸ� �� �� ȣ��Ǳ� �ϴµ� �ᱹ ������ ������ ����ȴ�.
//    public override void SaveChanges()
//    {
//        base.SaveChanges();
//        Debug.Log("saved successfully");
//    }
//    //�� �޼��带 ������(�������̵�)�Ͽ� �������� ������ �ν����Ϳ��� ������ ���� ������ ������� �ʰ��Ѵ�.
//    //�ݵ�� �θ��� DiscardChanges()�� ȣ���ؾ��Ѵ�.
//    //�׷��� ������ hasUnsavedChanges�� �Ӽ��� false�� �������� �ʱ� ����.
//    public override void DiscardChanges()
//    {
//        base.DiscardChanges();
//        Debug.LogError($"not Changed :");
//    }
//}