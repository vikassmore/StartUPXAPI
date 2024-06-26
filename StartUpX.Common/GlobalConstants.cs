using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Common
{
    public class GlobalConstants
    {
        public const string AuthKey = "THIS IS KEY 12345";
        public const string NotFoundMessage = "{0} Not Found. ";
        public const string UserNotFoundMessage = "User not  found. Please enter valid credentials.";
        public const string InvalidRequest = "Invalid request, please verify details.";
        public const string Status500Message = "Something went wrong!";
        public const string Status503Message = "Service  not available for this user.";
        public const string ExistingUserMessage = "User already exists.";
        public const string RecordSaveMessage = "Record Saved Successfully.";
        public const string RecordNotSaveMessage = "Record not Saved Successfully. ";
        public const string RecordUpdateMessage = "Record Updated Successfully. ";
        public const string RecordDeleteMessage = "Record Deleted Successfully. ";
        public const string EmailSendMesage = "Email Send Successfully.";
        public const string ForgotPassword = "Wide  Wings  Forgot Password Link";
        public const string PasswordChangeMessage = "Changed password Successfully.";
        public const string EmailNotFound = "Email invalid. Plese check.";
        public const string ForgotPasswordMessage = "You have sent OTP on your email. Please check.";
        public const string AddIncidentMessage = "Incident saved Successfully.";
        public const string ExistingIncidentMessage = "Incident already exists.";
        public const string ActivationLinkMessage = "Activation link is sent to your email address.Please check your inbox to activate account.";
        public const string OTPverification = "StartUpX Account Verification";
        public const string RegistrationMessage= "Registration Successfully";
        public const string ExistingRecordMessage = "Record already exists.";
        public const string UserNotActivatedMessage = "Please Activate user from email.";
        public const string UserLoginMessage = "User logged in Successfully.";
        public const string SentForVerification = "Details are sent for the verification.";
        public const string FounderApproved = "Company approved Successfully.";
        public const string FounderNotApproved = "Company not approved and details sent to Company.";
        public const string InvestorApproved = "Investor approved Successfully.";
        public const string InvestorNotApproved = "Investor not approved and details sent to comment.";
        public const string FounderMakeAsLive = "Company Successfully set as Live for the Investment.";
        public const string FounderMakeAsPreview = "Company Successfully set as preview with the Gauiging amount.";
        public const string InvestorInvested = "Your Investment done Successfully.";
        public const string InvestorIndicated = "Your Interest sent Successfully.";
        public const string InvestorSetFounderOnWatch = "Company added to your Watch List.";
        public const string ExistingSeriesMessage = "Series Name already exists.";
        public const string InvestmentOpportunityMessage = "Investment Opportunity Detail Added Successfully.";
        public const string FounderAddedMessage = "You have Added as a Founder.";
        public const string RequestRaiseFundingMessage = "You have Successfully requested for the raise funding.";
        public const string RequestOfferingMessage = "You have Successfully requested for the offering.";
        public const string ServiceProviderCredential = "Login Credential sent to Your Email .Please check and Loggedin.";
        public const string FounderInterestServiceMessage = "You have shown Interest in Service.";
        public const string FounderInterestServiceExistMessage = "You have already added service to Interested list.";
        public const string FounderInterestAcceptMessage = "You have accepted founder Interest in Service.";
        public const string FounderInterestDenyMessage = "You have deny founder Interest in Service.";
        public const string DocumentUploadedMessage = "You have Successfully uploaded document.";
        public const string SameDocumentUploadedMessage = "You have already uploaded document with same name.";
        public const string DocumentDeleteMessage = "Document Deleted Successfully.";
        public static int loggedUser(int id)
        {
            int d1 = id;
            return d1;
        }
    }
}
