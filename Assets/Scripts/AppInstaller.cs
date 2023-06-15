using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class AppInstaller : MonoInstaller
{
    [SerializeField] private GameObject _buttonView;
    [SerializeField] private ButtonsSettings buttonsSettings; 

    void InitExecutionOrder()
    {
        Container.BindExecutionOrder<ButtonsSettings>(-10);
        Container.BindExecutionOrder<ObjectSpawner>(-20);
    }

    public override void InstallBindings()
    {        
        //Container.Bind<ButtonGrid>().AsSingle().NonLazy();
        Container.Bind<ButtonsSettings>().FromInstance(buttonsSettings).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ObjectSpawner>().AsSingle().NonLazy();
        ButtonInstaller.Install(Container);
        Container.BindFactory<ButtonView, ButtonView.Factory>().FromComponentInNewPrefab(_buttonView).UnderTransformGroup("Canvas").NonLazy();
        
        
    }

}