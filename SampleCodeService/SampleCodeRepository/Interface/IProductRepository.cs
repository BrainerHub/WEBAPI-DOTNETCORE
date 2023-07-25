using SampleCodeModel.CustomModels;
using SampleCodeModel.DbEntities;
using SampleCodeModel.RequestModel;
using SampleCodeModel.ResponseModel;

namespace SampleCodeService.SampleCodeRepository.Interface
{
    public interface IProductRepository
    {
        Task<ProductResponseModel> CreateProduct(ProductRequestModel model, List<string> fileName);

        Task<ProductResponseModel> UpdateProduct(ProductUpdateRequestModel model,int productId, List<string> fileName);

        Task<List<Product>> List(SearchRequestModel model);

        Task Delete(int productId);

    }
}
