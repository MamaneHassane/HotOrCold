using Microsoft.IdentityModel.Tokens;

namespace HotOrCold.Security.StaticDatas;

public class StaticSecurityDatas
{
    // La clé secrète
    public const string SecretKey = "CetteCleDoitNormalementEtreSurAzureKeyvaultOuAWSSecretManager";
    
    // L'algorithme de cryptage utilisé
    public const string SecurityAlgorithm = SecurityAlgorithms.HmacSha256;
    
    // Le temps de vie d'un JSON Web Token pour l'ensemble de l'application : 10 minutes ici
    public static readonly TimeSpan TokenLifeTime = TimeSpan.FromMinutes(10);
    
    // L'adresse du Issuer
    public const string Issuer = "https://thegoat.MamaneHassane.com";
    
    // L'adresse de l'audience
    public const string Audience = "https://www.HotOrCold.com";
}