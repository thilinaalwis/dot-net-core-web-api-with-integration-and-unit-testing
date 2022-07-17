using BusinessModels.AWS;
using System.Threading.Tasks;

namespace BusinessLogicLayer.AWS
{
    public interface IStorageService
    {
        Task<S3ResponseDto> UploadFileAsync(S3Object s3obj, AwsCredentials awsCredentials);
    }
}
