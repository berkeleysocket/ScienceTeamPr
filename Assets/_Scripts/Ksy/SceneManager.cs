using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace KSY.Manager
{
    public class SceneManager
    {
        //SceneType�� Build Settings�� �� ������ ����� ��
        public enum SceneType : sbyte
        {
            Title = 0,
            SelectMapMenu,
            Game,
            Clear,


            None = -1,
        }

        //�� �ε� ���� �̺�Ʈ
        private Dictionary<SceneType, Action> SceneLoading;

        //�� �ε� �Ϸ� �̺�Ʈ
        private Dictionary<SceneType, Action> SceneLoaded;

        //�� ������ �̺�Ʈ
        private Dictionary<SceneType, Action> SceneExit;

        //���� ��
        public SceneType Scene_Current
        {
            get
            {
                UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
                return (SceneType)scene.buildIndex;
            }
        }

        #region public Methods
        public void AddEvent_Loading(SceneType t, Action onFunction)
        {
            // None�̰ų� Null�̸� ������� ����
            if (t == SceneType.None || onFunction == null) return;

            //��ųʸ� �ʱ�ȭ�� �ȵǾ��ִٸ� �ʱ�ȭ
            if (SceneLoading == null)
                SceneLoading = new Dictionary<SceneType, Action>();

            //Ű�� �̹� ������ ���, ������ ����� ���
            if (SceneLoading.ContainsKey(t))
                SceneLoading[t] += onFunction;
            else
                SceneLoading.Add(t, onFunction);
        }
        public void AddEvent_Loaded(SceneType t, Action onFunction)
        {
            // None�̰ų� Null�̸� ������� ����
            if (t == SceneType.None || onFunction == null) return;

            //��ųʸ� �ʱ�ȭ�� �ȵǾ��ִٸ� �ʱ�ȭ
            if (SceneLoaded == null)
                SceneLoaded = new Dictionary<SceneType, Action>();

            //Ű�� �̹� ������ ���, ������ ����� ���
            if (SceneLoaded.ContainsKey(t))
                SceneLoaded[t] += onFunction;
            else
                SceneLoaded.Add(t, onFunction);
        }
        public void AddEvent_Exit(SceneType t, Action onFunction)
        {
            // None�̰ų� Null�̸� ������� ����
            if (t == SceneType.None || onFunction == null) return;

            //��ųʸ� �ʱ�ȭ�� �ȵǾ��ִٸ� �ʱ�ȭ
            if (SceneExit == null)
                SceneExit = new Dictionary<SceneType, Action>();

            //Ű�� �̹� ������ ���, ������ ����� ���
            if (SceneExit.ContainsKey(t))
                SceneExit[t] += onFunction;
            else
                SceneExit.Add(t, onFunction);
        }

        public void LoadScene(SceneType t)
        {
            // None�̸� �ε����� ����
            if (t == SceneType.None) return;

            //���� ������ ������ �̺�Ʈ ����
            if (SceneExit != null && SceneExit.ContainsKey(Scene_Current))
                SceneExit[Scene_Current]?.Invoke();

            //�� �ε� �̺�Ʈ ����
            if (SceneLoading != null && SceneLoading.ContainsKey(t))
                SceneLoading[t]?.Invoke();

            //�� �ε� �Ϸ� �̺�Ʈ ����
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += Handle_SceneLoaded;

            //�� �ε�
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)t);
        }
        #endregion

        #region private Methods
        private void Handle_SceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            //�̺�Ʈ ����
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= Handle_SceneLoaded;

            //�� �ε� �Ϸ� �̺�Ʈ ����
            if (SceneLoaded != null && SceneLoaded.ContainsKey(Scene_Current))
                SceneLoaded[Scene_Current]?.Invoke();
        }
        #endregion
    }
}