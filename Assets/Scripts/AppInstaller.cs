using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppInstaller : MonoInstaller
{
    [SerializeField] private GameObject _buttonView;
    [SerializeField] private ButtonsSettings buttonsSettings; 
    //[SerializeField] private PlaybackCursor playbackCursor;

    public override void InstallBindings()
    {        
        Container.Bind<ButtonsSettings>().FromInstance(buttonsSettings).AsSingle().NonLazy();

        Popup popup = GameObject.FindObjectOfType<Popup>();
        Container.Bind<Popup>().FromInstance(popup).AsSingle().NonLazy();


        //Container.BindInstance(playbackCursor).AsSingle().NonLazy();
        PlaybackCursor playbackCursor = GameObject.FindObjectOfType<PlaybackCursor>();
        Container.Bind<PlaybackCursor>().FromInstance(playbackCursor).AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<Recorder>().FromComponentInHierarchy().AsSingle().NonLazy();
        /*Recorder recorder = GameObject.FindObjectOfType<Recorder>();
        Container.Bind<Recorder>().FromInstance(recorder).AsSingle().NonLazy();*/

        Container.BindInterfacesAndSelfTo<StartRecording>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveRecording>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LoadRecording>().AsSingle().NonLazy();


        Container.BindInterfacesAndSelfTo<ObjectSpawner>().AsSingle().NonLazy();

        Container.BindFactory<ButtonView, ButtonView.Factory>().FromComponentInNewPrefab(_buttonView).UnderTransformGroup("Buttons").NonLazy();
        Container.BindFactory<ButtonView, ButtonController, ButtonController.Factory>();
    }
}
