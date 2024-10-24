using fakePOSApi.DTOs;

namespace fakePOSApi.Services
{
    public interface IVentaService : IService<VentaDto, VentaInsertDto, VentaUpdateDto>
    {
        Task<bool> AnularVenta(int id);
        Task<bool> ValidateStateContable(int id);
    }
}
