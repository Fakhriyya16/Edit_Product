namespace Fiorello_MVC.Services.Interfaces
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetAllAsync();
        Task<string> GetPhoneAsync();
    }
}
