using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class AppInstaller : MonoInstaller
{
    [SerializeField] private GameObject _buttonView;
    [SerializeField] private ButtonsSettings buttonsSettings; 
    [SerializeField] private Popup popup; 

    void InitExecutionOrder()
    {
        Container.BindExecutionOrder<ButtonsSettings>(-10);
        Container.BindExecutionOrder<ObjectSpawner>(-20);
    }

    public override void InstallBindings()
    {        
        Container.Bind<ButtonsSettings>().FromInstance(buttonsSettings).AsSingle().NonLazy();
        Container.Bind<Popup>().FromInstance(popup).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ObjectSpawner>().AsSingle().NonLazy();
        ButtonInstaller.Install(Container);
        Container.BindFactory<ButtonView, ButtonView.Factory>().FromComponentInNewPrefab(_buttonView).UnderTransformGroup("Canvas").NonLazy();
        Container.BindFactory<ButtonView, Popup, ButtonController, ButtonController.Factory>();
        
        
    }

}