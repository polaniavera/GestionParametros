namespace BusinessServices
{
    public interface IPlantillaCampoServices
    {
        bool InactivateCampo(int campoId);
        bool ActivateCampo(int campoId);
    }
}
