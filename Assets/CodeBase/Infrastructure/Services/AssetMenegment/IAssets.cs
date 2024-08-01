using Assets.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.CodeBase.Services.AssetMenegment
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}