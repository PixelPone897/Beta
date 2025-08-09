using Assets.Scripts.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class TestMenu : MonoBehaviour, IMenuVisual
    {

        [Header("UI References"), SerializeField]
        private GameObject test;

        private List<GameObject> children;

        private void Awake()
        {
            children = new List<GameObject>();
        }

        private void Start()
        {
            children = 
                gameObject.GetComponentsInChildren<Transform>().Select(transform => transform.gameObject)
                .Where(gameObject => gameObject != this.gameObject).ToList();
        }

        public void Initialize(UnityServiceProvider serviceProvider)
        {
            
        }

        void IMenuVisual.HideMenu()
        {

        }

        public void UpdateMenu(int newMenuIndex)
        {
            for(int i = 0; i < children.Count; i++)
            {
                MenuOption current = children[i].GetComponent<MenuOption>();
                if(i != newMenuIndex)
                {
                    current.SetSelected(false);
                }
                else
                {
                    current.SetSelected(true);
                }
            }
        }

        void IMenuVisual.ShowMenu()
        {
            gameObject.SetActive(true);
        }
    }
}
