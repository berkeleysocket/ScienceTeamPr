using KSY.Tile;
using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Row<T>
{
    public Row(int colums)
    {
        this.colums = new T[colums];
    }
    public T[] colums;
}

[Serializable]
public class TileMatrix
{
    public TileMatrix(int row = 0, int colums = 0)
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
public class DestinationMatrix
{
    public DestinationMatrix(int row = 0, int colums = 0)
    {
        this.rows = new Row<TileObjectType>[row];

        for (int g = 0; g < row; g++)
        {
            this.rows[g] = new Row<TileObjectType>(colums);
        }
    }

    public Row<TileObjectType>[] rows;
}
#if UNITY_EDITOR
[CustomEditor(typeof(MapDataSO), false)]
public class MapDataSOEditor : Editor
{
    private MapDataSO _reference;

    private int _sizeX => _reference.SizeX;
    private int _sizeY => _reference.SizeY;

    private TileMatrix _tiles;
    private DestinationMatrix _destinations;

    private void OnEnable()
    {
        _reference = (MapDataSO)target;

        this._tiles = _reference.tiles;
        this._destinations = _reference.destinations;
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
        GUILayout.Label($"TileMatrix");

        if (GUILayout.Button("Create TileMatrix") && _tiles == null)
        {
            _tiles = new TileMatrix(_sizeX, _sizeY);
            _destinations = new DestinationMatrix(_sizeX, _sizeY);
        }
        else if (_tiles != null || _destinations != null)
        {
            Debug.Log("tiles is not null");
        }

            for (int h = 0; h < _sizeY; h++)
            {
                if (h >= _tiles.rows.Length)
                {
                    Debug.LogError($"[TileMatrix] h({h}) >= rows.Length({_tiles.rows.Length})");
                    break;
                }

                GUILayout.BeginHorizontal();
                for (int w = 0; w < _sizeX; w++)
                {
                    if (w >= _tiles.rows[h].colums.Length)
                    {
                        Debug.LogError($"[TileMatrix] w({w}) >= colums.Length({_tiles.rows[h].colums.Length})");
                        break;
                    }

                    GameObject field = _tiles.rows[h].colums[w];
                    GameObject tile = (GameObject)EditorGUILayout.ObjectField(field, typeof(GameObject), true);
                    _tiles.rows[h].colums[w] = tile;
                }
                GUILayout.EndHorizontal();
            }


        GUILayout.Label("");
        GUILayout.Label($"Targets");
        for (int h = 0; h < _sizeY; h++)
        {
            GUILayout.BeginHorizontal();
            for (int w = 0; w < _sizeX; w++)
            {
                TileObjectType field = _destinations.rows[h].colums[w];
                field = (TileObjectType)EditorGUILayout.EnumPopup(field);
                _destinations.rows[h].colums[w] = field;
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
#endif
