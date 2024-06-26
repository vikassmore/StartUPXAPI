using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IMasterService
    {
        /// <summary>
        /// Get FAQ By Id
        /// </summary>
        /// <param name="faqMasterId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        FAQModel GetFAQById(long faqMasterId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All FAQ
        /// </summary>
        /// <returns></returns>
        List<FAQModel> GetAllFAQ();
        /// <summary>
        /// Add/Update FAQ
        /// </summary>
        /// <param name="faqModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddFAQ(FAQModel faqModel,ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Delete FAQ
        /// </summary>
        /// <param name="faqMasterId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteFAQ(int faqMasterId, int userId,ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get Policy By Id
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        PolicyModel GetPolicyById(long policyId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All Policies
        /// </summary>
        /// <returns></returns>
        List<PolicyModel> GetAllPolicy();
        /// <summary>
        /// Add/Update Policy
        /// </summary>
        /// <param name="policyModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddPolicy(PolicyModel policyModel, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Delete Policy
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeletePolicy(int policyId,int userId, ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All Employee Count
        /// </summary>
        /// <returns></returns>
        List<EmployeeCountModel> GetAllEmployeeCount(ref ErrorResponseModel errorResponseModel);
    }
}
