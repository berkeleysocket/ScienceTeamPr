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
        //bool 값을 반환하는데, 기본 GUI 컨트롤로 입력 데이터의 값을 변경한 경우 true, 그렇지 않을 경우 false를 반환한다.
        //커스텀 에디터 인터페이스말고 기본적으로 그려지는 인스펙터를 그린다.
        //DrawDefaultInspector();

        //hasUnsavedChanges = true;

        //그냥 DrawDefaultInspector()를 호출한다.
        base.OnInspectorGUI();

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

        if (GUILayout.Button("Create TileMatrix"))
        {
            Debug.Log($"l : {_tiles.rows.Length != 0}");
            if(_tiles != null && _tiles.rows.Length != 0)
            {
                Debug.Log("Only one map can exist per SO");
                return;
            }

            _tiles = new TileMatrix(_sizeX, _sizeY);
            _destinations = new DestinationMatrix(_sizeX, _sizeY);
        }

        GUILayout.Label("");
        GUILayout.Label($"Tiles");
        if(_tiles != null && _tiles.rows.Length != 0)
        {
            for (int h = 0; h < _sizeY; h++)
            {
                GUILayout.BeginHorizontal();
                for (int w = 0; w < _sizeX; w++)
                {
                    GameObject field = _tiles.rows[h].colums[w];
                    GameObject tile = (GameObject)EditorGUILayout.ObjectField(field, typeof(GameObject), true);
                    _tiles.rows[h].colums[w] = tile;
                }
                GUILayout.EndHorizontal();
            }
        }


        GUILayout.Label("");
        GUILayout.Label($"Destinations");
        if(_destinations != null && _destinations.rows.Length != 0)
        {
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
        }

        //serializedObject : 직렬화된 오브젝트
        //Debug.Log(serializedObject.targetObject);

        //직렬화된 인스펙터의 변경 사항을 저장하라는 메세지를 보낼지 결정하는 속성

        //인스펙터 변경 사항을 저장하라는 메세지를 보낼 때 메세지를 변경할 수 있는 속성
        //saveChangesMessage = "Change Map?";
        //Debug.Log(saveChangesMessage);

        //이 에디터 스크립트가 참조하고 있는 Object를 반환한다.
        //Debug.Log(target);
        //Object 배열을 반환한다. (Object[])
        //Debug.Log(targets);
    }
    //이 메서드를 재정의(오버라이딩)하여 저장하지 않으면 저장하라는 메세지를 보낸다. 또한 저장되지 않은 작업을 잃지 않게 한다.
    //DiscardChanges와 반대 개념으로 둘 다 오버라이딩하면 둘 다 호출되긴 하는데 결국 수정한 내용이 저장된다.
    public override void SaveChanges()
    {
        base.SaveChanges();
        Debug.Log("saved successfully");
    }
    //이 메서드를 재정의(오버라이딩)하여 저장하지 않으면 인스펙터에서 변경한 저장 사항을 저장되지 않게한다.
    //반드시 부모의 DiscardChanges()를 호출해야한다.
    //그렇지 않으면 hasUnsavedChanges의 속성이 false로 설정되지 않기 때문.
    public override void DiscardChanges()
    {
        base.DiscardChanges();
        Debug.LogError($"not Changed :");
    }
}
#endif
