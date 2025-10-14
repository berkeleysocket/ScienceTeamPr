using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KSY.Manager
{
    public class SceneManager
    {
        //SceneType은 Build Settings의 씬 순서와 맞춰야 함
        public enum SceneType : sbyte
        {
            None = 0,
            Ksy_MainMenu,
            Ksy_InGame,
        }

        //씬 로딩 시작 이벤트
        private Dictionary<SceneType, Action> SceneLoading;

        //씬 로드 완료 이벤트
        private Dictionary<SceneType, Action> SceneLoaded;

        //씬 나가기 이벤트
        private Dictionary<SceneType, Action> SceneExit;

        //현재 씬
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
            // None이거나 Null이면 등록하지 않음
            if (t == SceneType.None || onFunction == null) return;

            //딕셔너리 초기화가 안되어있다면 초기화
            if (SceneLoading == null)
                SceneLoading = new Dictionary<SceneType, Action>();

            //키가 이미 있으면 등록, 없으면 만들고 등록
            if (SceneLoading.ContainsKey(t))
                SceneLoading[t] += onFunction;
            else
                SceneLoading.Add(t, onFunction);
        }
        public void AddEvent_Loaded(SceneType t, Action onFunction)
        {
            // None이거나 Null이면 등록하지 않음
            if (t == SceneType.None || onFunction == null) return;

            //딕셔너리 초기화가 안되어있다면 초기화
            if (SceneLoaded == null)
                SceneLoaded = new Dictionary<SceneType, Action>();

            //키가 이미 있으면 등록, 없으면 만들고 등록
            if (SceneLoaded.ContainsKey(t))
                SceneLoaded[t] += onFunction;
            else
            {
                SceneLoaded.Add(t, onFunction);

                UnityEngine.Debug.Log($"Scene name : {t}, {onFunction}");
            }
        }
        public void AddEvent_Exit(SceneType t, Action onFunction)
        {
            // None이거나 Null이면 등록하지 않음
            if (t == SceneType.None || onFunction == null) return;

            //딕셔너리 초기화가 안되어있다면 초기화
            if (SceneExit == null)
                SceneExit = new Dictionary<SceneType, Action>();

            //키가 이미 있으면 등록, 없으면 만들고 등록
            if (SceneExit.ContainsKey(t))
                SceneExit[t] += onFunction;
            else
                SceneExit.Add(t, onFunction);

            UnityEngine.Debug.Log("asdasdasdsdd");
        }

        public void LoadScene(SceneType t)
        {
            // None이면 로드하지 않음
            if (t == SceneType.None) return;

            //현재 씬에서 나가는 이벤트 실행
            if (SceneExit != null && SceneExit.ContainsKey(Scene_Current))
                SceneExit[Scene_Current]?.Invoke();

            //씬 로딩 이벤트 실행
            if (SceneLoading != null && SceneLoading.ContainsKey(t))
                SceneLoading[t]?.Invoke();

            //씬 로드 완료 이벤트 실행
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += Handle_SceneLoaded;

            //씬 로드
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)t);
        }
        #endregion

        #region private Methods
        private void Handle_SceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            //씬 로드 완료 이벤트 실행
            if (SceneLoaded != null && SceneLoaded.ContainsKey(Scene_Current))
            {
                UnityEngine.Debug.Log($"{Scene_Current}");
                SceneLoaded[Scene_Current]?.Invoke();

                //이벤트 해제
                UnityEngine.SceneManagement.SceneManager.sceneLoaded -= Handle_SceneLoaded;
            }
        }
        #endregion
    }
}