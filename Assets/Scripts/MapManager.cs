﻿using System.Linq;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public MapConfig config;
        public MapView view;

        public Map CurrentMap { get; private set; }

        [SerializeField] Scouter scouter;
        [SerializeField] Canvas canvas;
        [SerializeField] PlayerMap player;
        private bool dialogWindowOpen = false;
        private bool scoutingMode = false;
        private GameObject selectedNode;

        public bool IsDialogWindowOpen()
        {
            return dialogWindowOpen;
        }

        public void SetDialogWindowStatus(bool status)
        {
            dialogWindowOpen = status;
        }

        public void SetScoutingModeStatus(bool status)
        {
            scoutingMode = status;
            scouter.Focus(status);
            player.Focus(!status);
        }

        public bool GetScoutingModeStatus()
        {
            return scoutingMode;
        }

        public GameObject GetSelectedNode()
        {
            return selectedNode;
        }

        public void SetSelectedNode(GameObject node)
        {
            selectedNode = node;
            if (scoutingMode)
            {
                scouter.SetDestination(node);
            }
            else
            {
                player.SetDestination(node);
            }
        }

        private void Start()
        {
            // if (false)
            // // if (PlayerPrefs.HasKey("Map"))
            // {
            //     var mapJson = PlayerPrefs.GetString("Map");
            //     var map = JsonUtility.FromJson<Map>(mapJson);
            //     // using this instead of .Contains()
            //     if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
            //     {
            //         // payer has already reached the boss, generate a new map
            //         GenerateNewMap();
            //     }
            //     else
            //     {
            //         CurrentMap = map;
            //         // player has not reached the boss yet, load the current map
            //         view.ShowMap(map);
            //     }
            // }

            // else
            // {
                GenerateNewMap();
            // }
        }

        public void GenerateNewMap()
        {
            var map = MapGenerator.GetMap(config);
            CurrentMap = map;
            Debug.Log(map.ToJson());
            view.ShowMap(map);
        }

        public void SaveMap()
        {
            if (CurrentMap == null) return;

            var json = JsonUtility.ToJson(CurrentMap);
            PlayerPrefs.SetString("Map", json);
            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            SaveMap();
        }
    }
}
