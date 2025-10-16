using KSY.Tile;
using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Row<T>
{
    public Row(sbyte colums)
    {
        this.colums = new T[colums];
    }
    public T[] colums;
}

[Serializable]
public class MapData
{
    public MapData(sbyte row = 0, sbyte colums = 0)
    {
        this.rows = new Row<GameObject>[row];

        for (int g = 0; g < row; g++)
        {
            this.rows[g] = new Row<GameObject>(colums);
        }
    }

    public Row<GameObject>[] rows;
}

[Serializable]
public class TargetsData
{
    public TargetsData(sbyte row = 0, sbyte colums = 0)
    {
        this.rows = new Row<TileType>[row];

        for (int g = 0; g < row; g++)
        {
            this.rows[g] = new Row<TileType>(colums);
        }
    }

    public Row<TileType>[] rows;
}
[CustomEditor(typeof(MapDataSO), false)]
public class MapDataSOEditor : Editor
{
    private MapDataSO reference;
    private sbyte _mapSizeX => reference.MapSizeX;
    private sbyte _mapSizeY => reference.MapSizeY;
    private MapData _map => reference.Map;
    private TargetsData _targets => reference.Targets;

    private void OnEnable()
    {
        reference = (MapDataSO)target;
    }
    public override void OnInspectorGUI()
    {
        //bool ���� ��ȯ�ϴµ�, �⺻ GUI ��Ʈ�ѷ� �Է� �������� ���� ������ ��� true, �׷��� ���� ��� false�� ��ȯ�Ѵ�.
        //Ŀ���� ������ �������̽����� �⺻������ �׷����� �ν����͸� �׸���.
        //DrawDefaultInspector();

        //hasUnsavedChanges = true;

        //�׳� DrawDefaultInspector()�� ȣ���Ѵ�.
        base.OnInspectorGUI();
        GUILayout.Label("");

        //if (GUILayout.Button("Create Map"))
        //{
        //    MapData mData = new MapData(_mapSizeX, _mapSizeY);
        //    TargetsData tData = new TargetsData(_mapSizeX, _mapSizeY);
        //    _maps.Add(mData);
        //    _targets.Add(tData);
        //}

        //if (GUILayout.Button("Delete Map"))
        //{
        //    if (_maps.Count > 0)
        //        _maps.RemoveAt(_maps.Count - 1);

        //    if (_targets.Count > 0)
        //        _targets.RemoveAt(_targets.Count - 1);
        //}

        GUILayout.Label("");
        GUILayout.Label($"Map");
        for (int h = 0; h < _mapSizeY; h++)
        {
            if (h >= _map.rows.Length)
            {
                Debug.LogError($"[Map] h({h}) >= rows.Length({_map.rows.Length})");
                break;
            }

            GUILayout.BeginHorizontal();
            for (int w = 0; w < _mapSizeX; w++)
            {
                if (w >= _map.rows[h].colums.Length)
                {
                    Debug.LogError($"[Map] w({w}) >= colums.Length({_map.rows[h].colums.Length})");
                    break;
                }

                GameObject field = _map.rows[h].colums[w];
                GameObject tile = (GameObject)EditorGUILayout.ObjectField(field, typeof(GameObject), true);
                _map.rows[h].colums[w] = tile;
            }
            GUILayout.EndHorizontal();
        }


        GUILayout.Label("");
        GUILayout.Label($"Targets");
        for (int h = 0; h < _mapSizeY; h++)
        {
            GUILayout.BeginHorizontal();
            for (int w = 0; w < _mapSizeX; w++)
            {
                TileType field = _targets.rows[h].colums[w];
                field = (TileType)EditorGUILayout.EnumPopup(field);
                _targets.rows[h].colums[w] = field;
            }
            GUILayout.EndHorizontal();
        }




        //serializedObject : ����ȭ�� ������Ʈ
        //Debug.Log(serializedObject.targetObject);

        //����ȭ�� �ν������� ���� ������ �����϶�� �޼����� ������ �����ϴ� �Ӽ�

        //�ν����� ���� ������ �����϶�� �޼����� ���� �� �޼����� ������ �� �ִ� �Ӽ�
        //saveChangesMessage = "Change Map?";
        //Debug.Log(saveChangesMessage);

        //�� ������ ��ũ��Ʈ�� �����ϰ� �ִ� Object�� ��ȯ�Ѵ�.
        //Debug.Log(target);
        //Object �迭�� ��ȯ�Ѵ�. (Object[])
        //Debug.Log(targets);
    }
    //�� �޼��带 ������(�������̵�)�Ͽ� �������� ������ �����϶�� �޼����� ������. ���� ������� ���� �۾��� ���� �ʰ� �Ѵ�.
    //DiscardChanges�� �ݴ� �������� �� �� �������̵��ϸ� �� �� ȣ��Ǳ� �ϴµ� �ᱹ ������ ������ ����ȴ�.
    public override void SaveChanges()
    {
        base.SaveChanges();
        Debug.Log("saved successfully");
    }
    //�� �޼��带 ������(�������̵�)�Ͽ� �������� ������ �ν����Ϳ��� ������ ���� ������ ������� �ʰ��Ѵ�.
    //�ݵ�� �θ��� DiscardChanges()�� ȣ���ؾ��Ѵ�.
    //�׷��� ������ hasUnsavedChanges�� �Ӽ��� false�� �������� �ʱ� ����.
    public override void DiscardChanges()
    {
        base.DiscardChanges();
        Debug.LogError($"not Changed :");
    }
}
