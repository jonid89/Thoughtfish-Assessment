using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class AppInstaller : MonoInstaller
{
    [SerializeField] private GameObject _buttonNumberView;
    [SerializeField] private ButtonsConfig buttonsConfig; 

    public override void InstallBindings()
    {        
        //Container.Bind<ButtonGrid>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ObjectSpawner>().AsSingle().NonLazy();
        ButtonInstaller.Install(Container);
        Container.BindFactory<ButtonNumberView, ButtonNumberView.Factory>().FromComponentInNewPrefab(_buttonNumberView).UnderTransformGroup("Canvas").NonLazy();
        
        Container.BindInterfacesTo<ButtonsConfig>();
        Container.Bind<ButtonsConfig>().FromInstance(buttonsConfig).AsSingle().NonLazy();
    }

}