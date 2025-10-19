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
    //private int __sizeX
    //{
    //    get
    //    {
    //        if (_sizeX == 0)
    //        {
    //            _sizeX = 1;
    //        }
    //        return _sizeX;
    //    }
    //    set 
    //    {
    //        if (value == 0) return;

    //        TileMatrix oldTiles = _reference.tiles.rows.;
    //        DestinationMatrix oldDestinations = _reference.destinations;

    //        _reference.tiles = new TileMatrix(value, __sizeY);
    //        _reference.destinations = new DestinationMatrix(value, __sizeY);

    //        if(value >= __sizeX)
    //        {
    //            for (int h = 0; h < __sizeY; h++)
    //            {
    //                for (int w = 0; w < __sizeX; w++)
    //                {
    //                    Debug.Log($"_reference.tiles.rows[0].colums.Length : {_reference.tiles.rows[0].colums.Length}");
    //                    Debug.Log($"y : {__sizeY}");
    //                    Debug.Log($"x : {__sizeX}");

    //                    _reference.tiles.rows[h].colums[w] = oldTiles.rows[h].colums[w];
    //                    _reference.destinations.rows[h].colums[w] = oldDestinations.rows[h].colums[w];
    //                }
    //            }
    //        }
    //        else if(__sizeX >= value)
    //        { 
    //            for (int h = 0; h < __sizeY; h++)
    //            {
    //                for (int w = 0; w < value; w++)
    //                {
    //                    _reference.tiles.rows[h].colums[w] = oldTiles.rows[h].colums[w];
    //                    _reference.destinations.rows[h].colums[w] = oldDestinations.rows[h].colums[w];
    //                }
    //            }
    //        }

    //        _sizeX = value;
    //        _reference.SizeX = value;
    //    }
    //}
    //private int __sizeY
    //{
    //    get
    //    {
    //        if (_sizeY == 0)
    //        {
    //            _sizeY = 1;
    //        }
    //        return _sizeY;
    //    }
    //    set
    //    {
    //        if (value == 0) return;

    //        TileMatrix oldTiles = _reference.tiles;
    //        DestinationMatrix oldDestinations = _reference.destinations;

    //        _reference.tiles = new TileMatrix(__sizeX, value);
    //        _reference.destinations = new DestinationMatrix(__sizeX, value);

    //        if(value >= __sizeY)
    //        {
    //            for (int h = 0; h < __sizeY; h++)
    //            {
    //                for (int w = 0; w < __sizeX; w++)
    //                {
    //                    _reference.tiles.rows[h].colums[w] = oldTiles.rows[h].colums[w];
    //                    _reference.destinations.rows[h].colums[w] = oldDestinations.rows[h].colums[w];
    //                }
    //            }
    //        }
    //        else if(__sizeY >= value)
    //        {
    //            for (int h = 0; h < value; h++)
    //            {
    //                for (int w = 0; w < __sizeX; w++)
    //                {
    //                    _reference.tiles.rows[h].colums[w] = oldTiles.rows[h].colums[w];
    //                    _reference.destinations.rows[h].colums[w] = oldDestinations.rows[h].colums[w];
    //                }
    //            }
    //        }

    //        _sizeY = value;
    //        _reference.SizeY = value;
    //    }
    //}

    private void OnEnable()
    {
        _reference = (MapDataSO)target;

        //__sizeX = _reference.SizeX;
        //__sizeY = _reference.SizeY;
    }
    public override void OnInspectorGUI()
    {
        //bool ���� ��ȯ�ϴµ�, �⺻ GUI ��Ʈ�ѷ� �Է� �������� ���� ������ ��� true, �׷��� ���� ��� false�� ��ȯ�Ѵ�.
        //Ŀ���� ������ �������̽����� �⺻������ �׷����� �ν����͸� �׸���.
        //DrawDefaultInspector();

        //hasUnsavedChanges = true;

        //�׳� DrawDefaultInspector()�� ȣ���Ѵ�.
        base.OnInspectorGUI();

        //int before_sizeX = __sizeX;
        //int after_sizeX = EditorGUILayout.IntField(before_sizeX);

        //if (before_sizeX != after_sizeX)
        //{
        //    __sizeX = after_sizeX;
        //}

        //int before_sizeY = __sizeY;
        //int after_sizeY = EditorGUILayout.IntField(before_sizeY);

        //if(before_sizeY != after_sizeY)
        //{
        //    __sizeY = after_sizeY;
        //}

        if (GUILayout.Button("Create TileMatrix"))
        {
            if(_reference.tiles != null)
            {
                if(_reference.tiles.rows.Length != 0)
                {
                    Debug.Log("Only one map can exist per SO");
                    return;
                }
            }
            _reference.tiles = new TileMatrix(_sizeX, _sizeX);
            _reference.destinations = new DestinationMatrix(_sizeX, _sizeX);
        }

        if(_reference.tiles != null)
        {
            GUILayout.Label("");
            GUILayout.Label($"Tiles");
            for (int h = 0; h < _reference.tiles.rows.Length; h++)
            {
                GUILayout.BeginHorizontal();
                for (int w = 0; w < _reference.tiles.rows.Length; w++)
                {
                    GameObject field = _reference.tiles.rows[h].colums[w];
                    GameObject tile = (GameObject)EditorGUILayout.ObjectField(field, typeof(GameObject), true);
                    _reference.tiles.rows[h].colums[w] = tile;
                }
                GUILayout.EndHorizontal();
            }
        }
        if(_reference.destinations != null)
        {
            GUILayout.Label("");
            GUILayout.Label($"Destinations");
            for (int h = 0; h < _reference.tiles.rows.Length; h++)
            {
                GUILayout.BeginHorizontal();
                for (int w = 0; w < _reference.tiles.rows.Length; w++)
                {
                    TileObjectType field = _reference.destinations.rows[h].colums[w];
                    field = (TileObjectType)EditorGUILayout.EnumPopup(field);
                    _reference.destinations.rows[h].colums[w] = field;
                }
                GUILayout.EndHorizontal();
            }
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
