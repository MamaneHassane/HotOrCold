using Microsoft.IdentityModel.Tokens;

namespace HotOrCold.Security.StaticDatas;

public class StaticSecurityDatas
{
    // La clé secrète
    public const string SecretKey = "CetteCleDoitNormalementEtreSurAzureKeyvaultOuAWSSecretManager";
    
    // L'algorithme de cryptage utilisé pour les clients
    public const string SecurityAlgorithmForCustomers = SecurityAlgorithms.HmacSha256;
    
    // L'algorithme de cryptage utilisé pour les administrateurs
    public const string SecurityAlgorithmForAdministrators = SecurityAlgorithms.HmacSha384;
    
    // Le temps de vie d'un JSON Web Token pour l'ensemble de l'application : 30 minutes ici
    public static readonly TimeSpan TokenLifeTime = TimeSpan.FromMinutes(30);
    
    // L'adresse du Issuer
    public const string Issuer = "https://thegoat.MamaneHassane.com";
    
    // L'adresse de l'audience
    public const string Audience = "https://www.HotOrCold.com";
}