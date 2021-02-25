using AutoMapper;
using Hahn.ApplicationProcess.February2021.Domain.Assets;

namespace Hahn.ApplicationProcess.February2021.Application.Assets
{
    public class AssetModelProfile: Profile
    {
        public AssetModelProfile()
        {
            CreateMap<Asset, AssetModel>();
        }
    }
}
