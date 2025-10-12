using UnityEditor;
using KSY.Manager;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameManager),false)]
public class GameManagerEditor : Editor
{
    private GameManager reference;
    private int old_testNum;
    private void OnEnable()
    {
        reference = (GameManager)target;

        //값이 변경되기전 값을 저장
        old_testNum = reference.TestNum;
    }
    public override void OnInspectorGUI()
    {
        //bool 값을 반환하는데, 기본 GUI 컨트롤로 입력 데이터의 값을 변경한 경우 true, 그렇지 않을 경우 false를 반환한다.
        //커스텀 에디터 인터페이스말고 기본적으로 그려지는 인스펙터를 그린다.
        //이를 이용해서 값이 바뀐게 아니라면 인스펙터를 다시 그리지 않게함.

        if (!DrawDefaultInspector()) return;

        hasUnsavedChanges = true;

        //그냥 DrawDefaultInspector()를 호출한다.
        base.OnInspectorGUI();


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

        EditorGUILayout.LabelField("Map Editor");
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

        //취소를 선택했다면 값이 변경되기전의 값을 복원함.
        reference.TestNum = old_testNum;
    }
}