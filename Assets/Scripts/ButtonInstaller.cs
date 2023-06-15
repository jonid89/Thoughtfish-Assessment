using UnityEngine;
using Zenject;

public class ButtonInstaller : Installer<ButtonInstaller>
{
    public override void InstallBindings()
    {
        //Container.BindFactory<ButtonView, Popup, ButtonController, ButtonController.Factory>();
        //Container.BindInterfacesTo<ButtonNumberController>().AsSingle();
    }
}