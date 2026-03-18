using System;
using System.Collections.Generic;
using TD.UI;
using UnityEngine;

namespace TD.Manager
{
    public class MainUIManager : MonoBehaviour
    {
        public static MainUIManager Instance;
        public List<BaseView> views = new List<BaseView>();
        private Dictionary<Type, BaseView> mapViews = new Dictionary<Type, BaseView>();

        private void Awake()
        {
            Instance = this;
            MapViews();
        }

        public void Start()
        {
            ShowView<HomeViewView>();
        }

        private void MapViews()
        {
            for (int i = 0; i < views.Count; i++)
            {
                if (!mapViews.ContainsKey(views[i].GetType()))
                    mapViews.Add(views[i].GetType(), views[i]);
            }
        }

        public TBaseView GetView<TBaseView>() where TBaseView : BaseView
        {
            if (mapViews.ContainsKey(typeof(TBaseView)))
                return (TBaseView)mapViews[typeof(TBaseView)];
            return null;
        }

        public void ShowView<TBaseView>() where TBaseView : BaseView
        {
            if (mapViews.ContainsKey(typeof(TBaseView)))
                mapViews[typeof(TBaseView)].ShowView();
        }

        public void HideView<TBaseView>() where TBaseView : BaseView
        {
            if (mapViews.ContainsKey(typeof(TBaseView)) && mapViews[typeof(TBaseView)].IsActive)
                mapViews[typeof(TBaseView)].HideView();
        }

        public void LoadGamePlayView()
        {
            ShowView<TopBarView>();
            ShowView<AllTowerCardView>();
            ShowView<GamePlayView>();
        }
        
        public void UnloadGamePlayView()
        {
            HideView<TopBarView>();
            HideView<AllTowerCardView>();
            HideView<GamePlayView>();
        }
    }
}