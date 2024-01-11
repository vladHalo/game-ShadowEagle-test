using UnityEngine;
using Zenject;

public class SceneManagerInstaller : MonoInstaller
{
    [SerializeField] SceneManager _sceneManager;

    public override void InstallBindings()
    {
        Container.Bind<SceneManager>().FromInstance(_sceneManager).AsSingle().NonLazy();
        Container.QueueForInject(_sceneManager);
    }
}