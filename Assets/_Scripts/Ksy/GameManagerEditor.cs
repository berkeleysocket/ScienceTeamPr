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

        //���� ����Ǳ��� ���� ����
        old_testNum = reference.TestNum;
    }
    public override void OnInspectorGUI()
    {
        //bool ���� ��ȯ�ϴµ�, �⺻ GUI ��Ʈ�ѷ� �Է� �������� ���� ������ ��� true, �׷��� ���� ��� false�� ��ȯ�Ѵ�.
        //Ŀ���� ������ �������̽����� �⺻������ �׷����� �ν����͸� �׸���.
        //�̸� �̿��ؼ� ���� �ٲ�� �ƴ϶�� �ν����͸� �ٽ� �׸��� �ʰ���.

        if (!DrawDefaultInspector()) return;

        hasUnsavedChanges = true;

        //�׳� DrawDefaultInspector()�� ȣ���Ѵ�.
        base.OnInspectorGUI();


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

        EditorGUILayout.LabelField("Map Editor");
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

        //��Ҹ� �����ߴٸ� ���� ����Ǳ����� ���� ������.
        reference.TestNum = old_testNum;
    }
}