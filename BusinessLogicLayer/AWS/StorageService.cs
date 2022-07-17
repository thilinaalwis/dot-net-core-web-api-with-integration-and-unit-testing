using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using BusinessModels.AWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.AWS
{
    public class StorageService : IStorageService
    {
        /// <summary>
        /// Uploading the product image to the AWS s3 bucket
        /// </summary>
        /// <param name="s3obj"></param>
        /// <param name="awsCredentials"></param>
        /// <returns></returns>
        public async Task<S3ResponseDto> UploadFileAsync(S3Object s3obj, AwsCredentials awsCredentials)
        {
            // Adding AWS credentials
            var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecretKey);

            // Specify the region
            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USWest2
            };

            var response = new S3ResponseDto();

            try
            {
                // Create the upload request
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = s3obj.InputStream,
                    Key = s3obj.Name,
                    BucketName = s3obj.BucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                // Created an S3 client
                using var client = new AmazonS3Client(credentials, config);

                // upload utility to s3
                var transferUtiltiy = new TransferUtility(client);

                // We are actually uploading the file to S3
                await transferUtiltiy.UploadAsync(uploadRequest);

                response.StatusCode = 200;
                response.Message = $"{s3obj.Name} has been uploaded successfully";
            }
            catch (AmazonS3Exception ex)
            {
                response.StatusCode = (int)ex.StatusCode;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
