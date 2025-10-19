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
        //bool 값을 반환하는데, 기본 GUI 컨트롤로 입력 데이터의 값을 변경한 경우 true, 그렇지 않을 경우 false를 반환한다.
        //커스텀 에디터 인터페이스말고 기본적으로 그려지는 인스펙터를 그린다.
        //DrawDefaultInspector();

        //hasUnsavedChanges = true;

        //그냥 DrawDefaultInspector()를 호출한다.
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
