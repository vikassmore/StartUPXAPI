using Microsoft.AspNetCore.Hosting;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class SuitabilityQuestionService : ISuitabilityQuestionService
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public SuitabilityQuestionService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }
        /// <summary>
        /// Added Suitablity Question Data
        /// </summary>
        /// <param name="model"></param>
        /// <param name="suitabilityquestion"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddSuitabilityQuestion(SuitabilityQuestionModel suitabilityquestion, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existingRecord = _startupContext.SuitabilityQuestions.Where(x => x.SquestionId == suitabilityquestion.SquestionId && x.UserId == suitabilityquestion.LoggedUserId && x.IsActive == true).FirstOrDefault();
            if (existingRecord == null)
            {

                var suitabilityquestionEntity = new SuitabilityQuestion();
                suitabilityquestionEntity.MaximumInvestent = suitabilityquestion.MaximumInvestent;
                suitabilityquestionEntity.AcceptInvestment = suitabilityquestion.AcceptInvestment;
                suitabilityquestionEntity.ReleventInvestment = suitabilityquestion.ReleventInvestment;
                suitabilityquestionEntity.LiquidWorth = suitabilityquestion.LiquidWorth;
                suitabilityquestionEntity.RiskFector = suitabilityquestion.RiskFector;
                suitabilityquestionEntity.ConfiditialAgreement = suitabilityquestion.ConfiditialAgreement;
                suitabilityquestionEntity.CreatedDate = DateTime.Now;
                suitabilityquestionEntity.CreatedBy = suitabilityquestion.LoggedUserId;
                suitabilityquestionEntity.UserId = suitabilityquestion.UserId;
                suitabilityquestionEntity.IsActive = true;
                _startupContext.SuitabilityQuestions.Add(suitabilityquestionEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Suitability Question";
                userAuditLog.Description = "Suitability Question details Added";
                userAuditLog.UserId = (int)suitabilityquestion.LoggedUserId;
                userAuditLog.CreatedBy = suitabilityquestion.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                var suitabilityquestionEntity = _startupContext.SuitabilityQuestions.FirstOrDefault(x => x.SquestionId == suitabilityquestion.SquestionId && x.UserId == suitabilityquestion.LoggedUserId && x.IsActive);
                suitabilityquestionEntity.SquestionId = suitabilityquestion.SquestionId;
                suitabilityquestionEntity.MaximumInvestent = suitabilityquestion.MaximumInvestent;
                suitabilityquestionEntity.AcceptInvestment = suitabilityquestion.AcceptInvestment;
                suitabilityquestionEntity.ReleventInvestment = suitabilityquestion.ReleventInvestment;
                suitabilityquestionEntity.LiquidWorth = suitabilityquestion.LiquidWorth;
                suitabilityquestionEntity.UserId = suitabilityquestion.UserId;
                suitabilityquestionEntity.RiskFector = suitabilityquestion.RiskFector;
                suitabilityquestionEntity.ConfiditialAgreement = suitabilityquestion.ConfiditialAgreement;
                suitabilityquestionEntity.UpdatedDate = DateTime.Now;
                suitabilityquestionEntity.UpdatedBy = suitabilityquestion.LoggedUserId;
                suitabilityquestionEntity.IsActive = true;
                _startupContext.SuitabilityQuestions.Update(suitabilityquestionEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordUpdateMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = " Edit Suitability Question";
                userAuditLog.Description = "Suitability Question details Updated";
                userAuditLog.UserId = (int)suitabilityquestion.LoggedUserId;
                userAuditLog.CreatedBy = suitabilityquestion.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            return message;
        }

        public string DeleteSuitabilityQuestion(int squestionId, ErrorResponseModel errorResponseModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get All Suitablity Question Data
        /// </summary>
        /// <returns></returns>
        public List<SuitabilityQuestionModel> GetAllSuitabilityQuestion()
        {
            var SuitabilityQuestionEntity = _startupContext.SuitabilityQuestions.Where(x => x.IsActive == true).ToList();
            var SuitabilityQuestionList = SuitabilityQuestionEntity.Select(x => new SuitabilityQuestionModel
            {
                SquestionId = x.SquestionId,
                UserId = x.UserId,
                MaximumInvestent = x.MaximumInvestent,
                AcceptInvestment = x.AcceptInvestment,
                ReleventInvestment = x.ReleventInvestment,
                LiquidWorth = x.LiquidWorth,
                RiskFector = x.RiskFector,
                ConfiditialAgreement = x.ConfiditialAgreement,
            }).ToList();
            return SuitabilityQuestionList;
        }

        /// <summary>
        /// Get Suitablity Question Data By id
        /// </summary>
        /// <param name="squestionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public SuitabilityQuestionModel GetSuitabilityQuestionById(long squestionId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var SuitabilityQuestionList = new SuitabilityQuestionModel();
            var suitabilityquestionEntity = _startupContext.SuitabilityQuestions.FirstOrDefault(x => x.SquestionId == squestionId && x.IsActive == true);
            if (suitabilityquestionEntity != null)
            {
                SuitabilityQuestionList.SquestionId = suitabilityquestionEntity.SquestionId;
                SuitabilityQuestionList.UserId = suitabilityquestionEntity.UserId;
                SuitabilityQuestionList.MaximumInvestent = suitabilityquestionEntity.MaximumInvestent;
                SuitabilityQuestionList.AcceptInvestment = suitabilityquestionEntity.AcceptInvestment;
                SuitabilityQuestionList.ReleventInvestment = suitabilityquestionEntity.ReleventInvestment;
                SuitabilityQuestionList.LiquidWorth = suitabilityquestionEntity.LiquidWorth;
                SuitabilityQuestionList.RiskFector = suitabilityquestionEntity.RiskFector;
                SuitabilityQuestionList.ConfiditialAgreement = suitabilityquestionEntity.ConfiditialAgreement;

            }
            return SuitabilityQuestionList;
        }

        /// <summary>
        /// Get Suitablity Question Data By User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public SuitabilityQuestionModel GetSuitabilityQuestionByuserId(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var suitabilityquestionEntity = _startupContext.SuitabilityQuestions.Where(x => x.UserId == userId && x.IsActive == true).FirstOrDefault();
            var suitabilityQuestionModel = new SuitabilityQuestionModel();
            if (suitabilityquestionEntity != null)
            {
                suitabilityQuestionModel.SquestionId = suitabilityquestionEntity.SquestionId;
                suitabilityQuestionModel.UserId = suitabilityquestionEntity.UserId;
                suitabilityQuestionModel.MaximumInvestent = suitabilityquestionEntity.MaximumInvestent;
                suitabilityQuestionModel.AcceptInvestment = suitabilityquestionEntity.AcceptInvestment;
                suitabilityQuestionModel.ReleventInvestment = suitabilityquestionEntity.ReleventInvestment;
                suitabilityQuestionModel.LiquidWorth = suitabilityquestionEntity.LiquidWorth;
                suitabilityQuestionModel.RiskFector = suitabilityquestionEntity.RiskFector;
                suitabilityQuestionModel.ConfiditialAgreement = suitabilityquestionEntity.ConfiditialAgreement;
            }
            return suitabilityQuestionModel;

        }
    }
}


