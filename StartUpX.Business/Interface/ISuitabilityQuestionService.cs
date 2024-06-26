using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ISuitabilityQuestionService
    {
        SuitabilityQuestionModel GetSuitabilityQuestionById(long squestionId, ref ErrorResponseModel errorResponseModel);
        List<SuitabilityQuestionModel> GetAllSuitabilityQuestion();
        string AddSuitabilityQuestion(SuitabilityQuestionModel suitabilityquestion, ref ErrorResponseModel errorResponseModel);
        string DeleteSuitabilityQuestion(int squestionId, ErrorResponseModel errorResponseModel);

        SuitabilityQuestionModel GetSuitabilityQuestionByuserId(long userId, ref ErrorResponseModel errorResponseModel);
    }
}
