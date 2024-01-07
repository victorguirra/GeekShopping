using AutoMapper;

namespace GeekShopping.CouponAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(config =>
            {
                
            });

            return mappingConfig;
        }
    }
}
