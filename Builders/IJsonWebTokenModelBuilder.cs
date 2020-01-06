using AmberTestApp.Models;

namespace AmberTestApp.Builders
{
    internal interface IJsonWebTokenModelBuilder
    {
        JsonWebTokenModel BuildModel(string email);
    }
}
