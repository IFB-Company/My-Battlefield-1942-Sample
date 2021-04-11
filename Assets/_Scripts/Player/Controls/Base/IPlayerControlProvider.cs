namespace _Scripts.Player.Controls.Base
{
    public interface IPlayerControlProvider
    {
        IControlProvider GetControlProviderByName(string controlProviderName);

        IButtonControlProvider GetButtonControlProvider();
    }
}
